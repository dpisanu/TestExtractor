using System;
using System.Diagnostics;

namespace TestExtractor.Time
{
    public interface ITime
    {
        void Start ();

        void Reset ();

        void Stop ();

        TimeSpan Elapsed { get; }
    }
}