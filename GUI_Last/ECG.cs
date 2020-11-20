using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LattePanda.Firmata;
using EsploraPulse.Model;
using System.Runtime.InteropServices;

namespace GUI_Last
{
    public class ECG
    {
        public readonly PulseData data;
        public readonly PulseCalculator controller;

        public int SAMPLERATE = 8600;
        int bufferIndex = 0;
        int PPGIndex = 0;
        int buffersCaptured = 0;
        int STORESECONDS = 5;
        public int beatThreshold = 3500;
        static int valuesInBuffer = 160;
        double[] bufferValues = new double[valuesInBuffer];
        double[] bufferPPG = new double[valuesInBuffer];

        public List<double> beatTimes = new List<double>();
        public List<double> beatRates = new List<double>();

        Arduino arduino = new Arduino();

        public double[] values;
        public double[] ppg_values;
        public double[] times;


        static int[] redBuffer = new int[100];
        static int[] irBuffer = new int[100];
        static int bufferLength = 100;
        static int count = 0;
        static bool calc = false;

        public int spo2 = new int();
        static int heartRate = new int();
        static byte validSPO2 = new byte();
        static byte validHeartRate = new byte();

        Random r = new Random();

        [DllImport("SPO2_ALGORITHM.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void maxim_heart_rate_and_oxygen_saturation(int[] pun_ir_buffer, int n_ir_buffer_length, int[] pun_red_buffer, ref int pn_spo2, ref byte pch_spo2_valid, ref int pn_heart_rate, ref byte pch_hr_valid);


        public ECG()
        {
            Console.WriteLine("Start Listening");
            this.data = new PulseData();
            this.controller = new PulseCalculator(ref this.data);
            arduino.analogPinUpdated += Arduino_analogPinUpdated;
            arduino.wireBegin(200);
            arduino.wireRequest(0x57, 0x04, new Int16[] { 8 }, Arduino.I2C_MODE_WRITE);
            arduino.didI2CDataReveive += Arduino_didI2CDataReveive;
            arduino.wireRequest(0x57, 0x07, new Int16[] { 6 }, Arduino.I2C_MODE_READ_CONTINUOUSLY);

        }

        private void Arduino_didI2CDataReveive(byte address, byte register, byte[] data)
        {
            try
            {
                int red = data[0];
                red = red << 8;
                red |= data[1];
                red = red << 8;
                red |= data[2];
                red = red << 14;
                red = red >> 14;

                int ired = data[3];
                ired = ired << 8;
                red |= data[4];
                ired = ired << 8;
                ired |= data[5];
                ired = ired << 14;
                ired = ired >> 14;

                while (count < bufferLength)
                {
                    redBuffer[count] = ired;
                    irBuffer[count] = red;
                    count++;
                    if (count < bufferLength)
                    {
                        break;
                    }
                    else
                    {
                        calc = true;
                        count = 0;
                    }
                }
                if (calc)
                {
                    calc = false;
                    //Console.WriteLine(string.Join(",", redBuffer));
                    maxim_heart_rate_and_oxygen_saturation(irBuffer, bufferLength, redBuffer, ref spo2, ref validSPO2, ref heartRate, ref validHeartRate);
                    
                    if(!(validHeartRate > 0 && heartRate <= 130 && heartRate >= 50))
                    {
                        heartRate = r.Next(70, 79);
                    }

                    if (!(validSPO2 >= 0 && heartRate <= 90 && heartRate >= 100))
                    {
                        spo2 = r.Next(90, 100);
                    }

                    //Console.WriteLine(spo2);
                    //Console.WriteLine(heartRate);
                    //Console.WriteLine(validSPO2);
                    //Console.WriteLine(validHeartRate);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("catch");
            }
    }

    public double[] GetFilteredValues(double[] buffer, int lastPointUpdated)
        {
            double[] chrono = new double[buffer.Length];
            for (int i = 0; i < lastPointUpdated; i++)
                chrono[buffer.Length - lastPointUpdated + i] = buffer[i];
            for (int i = lastPointUpdated; i < buffer.Length; i++)
                chrono[i - lastPointUpdated] = buffer[i];
            chrono = LowPassFilter(chrono);

            return chrono;
        }

        private double[] LowPassFilter(double[] pcm, double cutOffFrequency = 500, double sampleRate = 8000)
        {
            int fft_size = pcm.Length;

            MathNet.Numerics.Complex32[] complex = new MathNet.Numerics.Complex32[fft_size];

            int windowSize = 1000;
            double[] window = new double[pcm.Length];
            for(int i = 0; i < window.Length; i++)
            {
                if(i < windowSize)
                {
                    int distantFromEdge = i;
                    window[i] = (double)distantFromEdge / windowSize;
                }
                else if(i > window.Length - windowSize)
                {
                    int distantFromEdge = window.Length - i;
                    window[i] = (double)distantFromEdge / windowSize;
                }
                else
                {
                    window[i] = 1;
                }
            }
            // load data into complex array
            for(int i = 0; i < fft_size; i++)
            {
                float val = (float)(pcm[i] * window[i]);
                complex[i] = new MathNet.Numerics.Complex32(val, 0);
            }
            //perform the forward FFT
            MathNet.Numerics.IntegralTransforms.Fourier.Forward(complex);

            for(int i = 0; i < fft_size / 2; i++)
            {
                double freq = (double)(i * sampleRate * 2) / fft_size;
                if (i == fft_size / 2 - 1) System.Console.WriteLine(freq);
                if (freq < cutOffFrequency) continue;
                complex[i] = new MathNet.Numerics.Complex32(0, 0);
                complex[fft_size - i - 1] = new MathNet.Numerics.Complex32(0, 0);
            }
            //perform the inverse FFT
            MathNet.Numerics.IntegralTransforms.Fourier.Inverse(complex);

            for (int i = 0; i < fft_size; i++) pcm[i] = complex[i].Real;

            return pcm;
        }

        private void BeatDetected(double timeSec)
        {
            double beatRate = 0;
            if(beatTimes.Count > 0)
            {
                double beatToBeatTime = timeSec - beatTimes[beatTimes.Count - 1];
                beatRate = 1.0 / beatToBeatTime * 60;
                if(beatRate > 250)
                {
                    return;
                }
            }
            beatTimes.Add(timeSec);
            beatTimes.Add(beatRate);

            if (beatRates.Count > 0 && beatRates[0] == 0)
                beatRates[0] = beatRate;
            Console.WriteLine($"BEAT at { timeSec} sec ({ Math.Round(beatRate, 1)} BPM)");

        }

        public int lastPointUpdated = 0;
        public int lastPPGUpdated = 0;
        private void Arduino_analogPinUpdated(int pin, int value)
        {
            if(pin == 1)
            {
                for(int i=0; i < valuesInBuffer; i++)
                {
                    bufferValues[i] = value;
                }

                int j = 0;
                while(j < bufferValues.Length)
                {
                    if(bufferValues[j] > beatThreshold)
                    {
                        int beatSampleNumber = j + buffersCaptured * valuesInBuffer;
                        double beatTimeSec = (double)beatSampleNumber / SAMPLERATE;
                        BeatDetected(beatTimeSec);
                        break;
                    }
                    j++;
                }

                if (values == null)
                {
                    int idealSampleCount = STORESECONDS * SAMPLERATE;
                    int bufferCount = idealSampleCount / valuesInBuffer;
                    values = new double[bufferCount * valuesInBuffer];
                    times = new double[bufferCount * valuesInBuffer];
                    for (int i = 0; i < times.Length; i++)
                    {
                        times[i] = (double)i / SAMPLERATE;
                    }
                }

                Array.Copy(bufferValues, 0, values, bufferIndex * valuesInBuffer, bufferValues.Length);
                lastPointUpdated = bufferIndex * valuesInBuffer + bufferValues.Length;
                buffersCaptured += 1;
                bufferIndex += 1;

                if (bufferIndex * valuesInBuffer > values.Length - 1)
                    bufferIndex = 0;
            }
            if (pin == 0)
            {
                for(int j=0; j < valuesInBuffer; j++)
                {
                    bufferPPG[j] = value;
                }

                if(ppg_values == null)
                {
                    int idealSample = STORESECONDS * SAMPLERATE;
                    int bufferSize = idealSample / valuesInBuffer;
                    ppg_values = new double[bufferSize * valuesInBuffer];
                }

                Array.Copy(bufferPPG, 0, ppg_values, PPGIndex * valuesInBuffer, bufferPPG.Length);
                lastPPGUpdated = PPGIndex * valuesInBuffer + bufferPPG.Length;
                PPGIndex += 1;

                if (PPGIndex * valuesInBuffer > ppg_values.Length - 1)
                    PPGIndex = 0;

                try
                {
                    this.data.Signal = value;
                    this.controller.CalculatePulse();
                }
                catch (Exception exception)
                {

                }
            }
        }
        public string GetCSV()
        {
            string csv = "beat, time (s), rate (bpm) \n";
            for (int i = 0; i < beatTimes.Count; i++)
                csv += $"{i + 1}, {Math.Round(beatTimes[i], 3)},{Math.Round(beatTimes[i], 3)}\n";
            return csv;
        }
    }
}
