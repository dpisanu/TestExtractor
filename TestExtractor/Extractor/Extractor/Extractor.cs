using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using TestExtractor.Structure;

namespace TestExtractor.Extractor.Extractor
{
    /// <summary>
    ///     Abstract implementation of an Extractor Class
    ///     Implements Interface : <see cref="IExtractor" />
    /// </summary>
    [Serializable]
    public abstract class Extractor : IExtractor
    {
        protected const string AppDataDomainExtractionDomainName = "ExtractionDomain";
        protected const string AppDataDomainExtractionAssemblyName = "extractionAssemblies";
        protected const string AppDataDomainExtractionStubName = "extractionStubs";
        protected const string AppDataDomainExtractionSuiteName = "extractionSuits";
        private readonly object _lock = new object();

        /// <summary>
        ///     Implements <see cref="IExtractor.TestFramework" />
        /// </summary>
        public Enum TestFramework { get; protected set; }

        /// <summary>
        ///     Implements <see cref="IExtractor.Extract{T}" />
        /// </summary>
        public IList<T> Extract<T>(IList<string> extractionAssemblies) where T : INode
        {
            if (extractionAssemblies == null || !extractionAssemblies.Any())
            {
                throw new Exception("No assemblies to extract data from");
            }

            List<T> extractions;
            lock (_lock)
            {
                extractions = new List<T>();

                var tests = new List<IStubNode>();
                var testSuites = new List<ISuiteNode>();

                AppDomain appDomain = null;
                try
                {
                    appDomain = AppDomain.CreateDomain(
                        AppDataDomainExtractionDomainName,
                        new Evidence(AppDomain.CurrentDomain.Evidence),
                        AppDomain.CurrentDomain.BaseDirectory,
                        AppDomain.CurrentDomain.BaseDirectory,
                        true);

                    appDomain.SetData(AppDataDomainExtractionAssemblyName, extractionAssemblies);

                    appDomain.DoCallBack(Extract);

                    var domainTests = appDomain.GetData(AppDataDomainExtractionStubName) as IList<IStubNode>;
                    if (domainTests != null)
                    {
                        tests.AddRange(domainTests);
                    }
                    var domainSuits = appDomain.GetData(AppDataDomainExtractionSuiteName) as IList<ISuiteNode>;
                    if (domainSuits != null)
                    {
                        testSuites.AddRange(domainSuits);
                    }
                }
                finally
                {
                    if (appDomain != null)
                    {
                        AppDomain.Unload(appDomain);
                    }
                }

                if (typeof (ISuiteNode).IsAssignableFrom(typeof (T)))
                {
                    extractions.AddRange(testSuites.Cast<T>());
                }
                if (typeof (IStubNode).IsAssignableFrom(typeof (T)))
                {
                    extractions.AddRange(tests.Cast<T>());
                }
            }
            return extractions;
        }

        /// <summary>
        ///     Implements <see cref="IExtractor.ExtractTimed{T}" />
        /// </summary>
        public Tuple<IList<T>, TimeSpan> ExtractTimed<T>(IList<string> extractionAssemblies) where T : INode
        {
            var time = new Time.Time();
            time.Start();
            var extractResult = Extract<T>(extractionAssemblies);
            time.Stop();
            return new Tuple<IList<T>, TimeSpan>(extractResult, time.Elapsed);
        }

        protected abstract void Extract();
    }
}