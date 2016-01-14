using System;

namespace TestExtractor.Time
{
    public interface ITime
    {
        /// <summary>
        ///     <see cref="TimeSpan" /> of the Measured Time
        /// </summary>
        TimeSpan Elapsed { get; }

        /// <summary>
        ///     Start a Time Measurement
        /// </summary>
        void Start();

        /// <summary>
        ///     Reset the Time Measurement
        /// </summary>
        void Reset();

        /// <summary>
        ///     Stop the Time Measurement
        /// </summary>
        void Stop();
    }
}