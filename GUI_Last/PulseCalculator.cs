using System;

namespace EsploraPulse.Model
{
    /// <summary>
    /// This class is responsible for calculating the pulse.
    /// </summary>
    /// <author>Jonathan Walker</author>
    /// <version>11/27/2015</version>
    public class PulseCalculator
    {
        // How often data is read through the Serial Port, in milliseconds. 
        // Ensure this matches the READ_RATE in the Arduino code.
        private const int ReadRate = 50;

        // This holds the data for the calculations.
        private readonly PulseData data;

        /// <summary>
        /// Initializes a new instance of the <see cref="PulseCalculator"/> class.
        /// </summary>
        /// <param name="data">The data object.</param>
        /// <exception cref="ArgumentNullException">
        /// @the pulse sensor data object cannot be null
        /// </exception>
        public PulseCalculator(ref PulseData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), @"the pulse sensor data object cannot be null");
            }
            
            this.data = data;
        }

        /// <summary>
        /// Calculates the pulse.
        /// This class is based off of the code found here: https://github.com/WorldFamousElectronics/PulseSensor_Amped_Arduino/blob/master/PulseSensorAmped_Arduino_1dot4/Interrupt.ino.
        /// </summary>
        public void CalculatePulse()
        {
            // Increase the total time by the ReadRate.
            this.data.SampleCounter += ReadRate;

            // Record the time since a heart beat was detected.
            var timeElapsed = (int)(this.data.SampleCounter - this.data.LastBeatTime);

            // Calculate the Peak and Trough of the pulse waveform.
            this.calculateTrough(timeElapsed);
            this.calculatePeak();
            
            // If a heart beat is detected, update the necessary information.
            if (timeElapsed > 250)
            {
                this.handlePulse(timeElapsed);   
            }

            // If the values are decreasing, the heart beat is over.
            if (this.data.Signal < this.data.Threshold && this.data.HeartBeatExists)
            {
                this.data.HeartBeatExists = false;
                this.data.Amplitude = this.data.Peak - this.data.Trough;
                this.data.Threshold = this.data.Amplitude/2 + this.data.Trough;
                this.data.Peak = this.data.Threshold;
                this.data.Trough = this.data.Threshold;
            }

            // If 2.5 seconds elapse without a heart beat, reset the data to their original values.
            if (timeElapsed > 2500)
            {
                this.data.Threshold = 512;
                this.data.Peak = this.data.Threshold;
                this.data.Trough = this.data.Threshold;
                this.data.LastBeatTime = this.data.SampleCounter;
                this.data.FirstBeat = true;
                this.data.SecondBeat = false;
            }
        }

        private void handlePulse(int timeElapsed)
        {
            // A pulse has been detected.
            if ((this.data.Signal > this.data.Threshold) && (this.data.HeartBeatExists == false) &&
                    (timeElapsed > this.data.IBI / 5 * 3))
            {
                // Update the information from the last pulse.
                this.data.HeartBeatExists = true;
                this.data.IBI = (int)(this.data.SampleCounter - this.data.LastBeatTime);
                this.data.LastBeatTime = this.data.SampleCounter;

                // If this isn't the first heart beat detected.
                if (this.data.SecondBeat)
                {
                    this.data.SecondBeat = false;
                    for (var i = 0; i <= 9; i++)
                    {
                        this.data.SetRateByIndex(this.data.IBI, i);
                    }
                }

                // If this is the first we've found a heart beat.
                if (this.data.FirstBeat)
                {
                    this.data.FirstBeat = false;
                    this.data.SecondBeat = true;
                    return;
                }

                // Calculate the BPM using a history of the heart beats.
                uint runningTotal = 0;

                for (var i = 0; i <= 8; i++)
                {
                    this.data.SetRateByIndex(this.data.GetRateByIndex(i + 1), i);
                    runningTotal += (uint)this.data.GetRateByIndex(i);
                }

                this.data.SetRateByIndex(this.data.IBI, 9);
                runningTotal += (uint)this.data.GetRateByIndex(9);
                runningTotal /= 10;
                this.data.BPM = 60000 / (int)runningTotal;
                this.data.HeartBeatDetected = true;
            }
        }

        private void calculatePeak()
        {
            if (this.data.Signal > this.data.Threshold && this.data.Signal > this.data.Peak)
            {
                this.data.Peak = this.data.Signal;
            }
        }

        private void calculateTrough(int n)
        {
            if (this.data.Signal >= this.data.Threshold || n <= this.data.IBI/5*3)
            {
                return;
            }

            if (this.data.Signal < this.data.Trough)
            {
                this.data.Trough = this.data.Signal;
            }
        }
    }
}
