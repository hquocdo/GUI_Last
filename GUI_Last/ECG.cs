using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LattePanda.Firmata;
using EsploraPulse.Model;

namespace GUI_Last
{
    public class ECG
    {
        public readonly PulseData data;
        public readonly PulseCalculator controller;

        public int SAMPLERATE = 8000;
        int bufferIndex = 0;
        int buffersCaptured = 0;
        int STORESECONDS = 5;
        public int beatThreshold = 3500;
        static int valuesInBuffer = 160;
        double[] bufferValues = new double[valuesInBuffer];

        public List<double> beatTimes = new List<double>();
        public List<double> beatRates = new List<double>();

        Arduino arduino = new Arduino();

        public double[] values;
        public double[] times;

        public ECG()
        {
            Console.WriteLine("Start Listening");
            this.data = new PulseData();
            this.controller = new PulseCalculator(ref this.data);
            arduino.analogPinUpdated += Arduino_analogPinUpdated;
        }
        public double[] GetFilteredValues()
        {
            double[] chrono = new double[values.Length];
            for (int i = 0; i < lastPointUpdated; i++)
                chrono[values.Length - lastPointUpdated + i] = values[i];
            for (int i = lastPointUpdated; i < values.Length; i++)
                chrono[i - lastPointUpdated] = values[i];
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
