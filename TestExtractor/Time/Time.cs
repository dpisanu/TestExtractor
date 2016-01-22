using System;
using System.Diagnostics;

namespace TestExtractor.Time
{
    /// <summary>
    ///     Concrete Time Measurement Implementation Class
    ///     Implements Interface : <see cref="ITime" />
    /// </summary>
    [Serializable]
    public class Time : ITime
    {
        private readonly Stopwatch _stopwatch;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Time" /> class.
        /// </summary>
        public Time()
        {
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        ///     Implements <see cref="ITime.Start" />
        /// </summary>
        public void Start()
        {
            if (_stopwatch.IsRunning)
            {
                return;
            }
            _stopwatch.Start();
        }

        /// <summary>
        ///     Implements <see cref="ITime.Reset" />
        /// </summary>
        public void Reset()
        {
            _stopwatch.Reset();
        }

        /// <summary>
        ///     Implements <see cref="ITime.Stop" />
        /// </summary>
        public void Stop()
        {
            if (!_stopwatch.IsRunning)
            {
                return;
            }
            _stopwatch.Stop();
        }

        /// <summary>
        ///     Implements <see cref="ITime.Elapsed" />
        /// </summary>
        public TimeSpan Elapsed
        {
            get { return _stopwatch.Elapsed; }
        }
    }
}