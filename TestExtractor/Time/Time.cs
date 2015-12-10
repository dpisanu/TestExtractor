using System;
using System.Diagnostics;

namespace TestExtractor.Time
{
    public class Time : ITime
    {
        private readonly Stopwatch _stopwatch;

        public Time ()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start ()
        {
            if (_stopwatch.IsRunning)
            {
                return;
            }
            _stopwatch.Start();
        }

        public void Reset ()
        {
            _stopwatch.Reset();
        }

        public void Stop ()
        {
            if (!_stopwatch.IsRunning)
            {
                return;
            }
            _stopwatch.Stop();
        }

        public TimeSpan Elapsed
        {
            get
            {
                return _stopwatch.Elapsed;
            }
        }
    }
}