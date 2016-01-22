using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Core;
using NUnit.Util;
using TestExtractor.Extractors.NUnit.Structure;
using TestExtractor.Structure;

namespace TestExtractor.Extractors.NUnit.Extractor
{
    /// <summary>
    ///     Concrete implementation of an Extractor to NUnit Test Assemblies
    ///     Inherits Class : <see cref="TestExtractor.Extractor.Extractor.Extractor" />
    /// </summary>
    [Serializable]
    public sealed class NUnit : TestExtractor.Extractor.Extractor.Extractor
    {
        internal static IList<ISuiteNode> TestSuites;
        internal static IList<IStubNode> TestMethods;
        private static string _assembly;

        /// <summary>
        ///     Created a new instance of <see cref="NUnit" />
        /// </summary>
        public NUnit()
        {
            TestFramework = TestExtractor.Extractor.Enums.TestFramework.NUnit;
        }

        /// <summary>
        ///     Overrides <see cref="TestExtractor.Extractor.Extractor.Extractor.Extract()" />
        /// </summary>
        protected override void Extract()
        {
            var assemblies = AppDomain.CurrentDomain.GetData(AppDataDomainExtractionAssemblyName) as IList<string>;
            if (assemblies == null || !assemblies.Any())
            {
                return;
            }

            TestSuites = new List<ISuiteNode>();
            TestMethods = new List<IStubNode>();

            ServiceManager.Services.AddService(new SettingsService());
            ServiceManager.Services.AddService(new DomainManager());
            ServiceManager.Services.AddService(new ProjectService());
            ServiceManager.Services.AddService(new AddinRegistry());
            ServiceManager.Services.AddService(new AddinManager());
            ServiceManager.Services.AddService(new TestAgency());
            ServiceManager.Services.InitializeServices();

            foreach (var assembly in assemblies.Distinct().Where(File.Exists))
            {
                _assembly = assembly;

                var loader = new TestLoader();
                loader.Events.TestLoaded += NodeLoadEvent;
                loader.LoadProject(assembly);
                loader.LoadTest();

                loader.UnloadTest();
                loader.UnloadProject();
                loader.Events.TestLoaded -= NodeLoadEvent;

                _assembly = string.Empty;
            }

            var tests = TestMethods.ToList();
            var testSuites = TestSuites.ToList();

            AppDomain.CurrentDomain.SetData(AppDataDomainExtractionStubName, tests);
            AppDomain.CurrentDomain.SetData(AppDataDomainExtractionSuiteName, testSuites);

            ServiceManager.Services.StopAllServices();
            ServiceManager.Services.ClearServices();
        }

        /// <summary>
        /// Callback Method for the TestLoaded Event
        /// </summary>
        /// <param name="sender">Sender that triggered the event</param>
        /// <param name="args"><see cref="TestEventArgs"/> of the Event</param>
        private static void NodeLoadEvent(object sender, TestEventArgs args)
        {
            var test = args.Test as TestNode;
            if (test != null)
            {
                AddNode(test);
            }
        }

        /// <summary>
        /// Function that is called when a new Node is loaded.
        /// Checks the <see cref="TestNode.IsSuite"/> Property.
        /// Depending on the value it adds a <see cref="SuiteNode"/> or a <see cref="StubNode"/>
        /// </summary>
        /// <param name="node"><see cref="TestNode"/> to be added</param>
        private static void AddNode(TestNode node)
        {
            if (node.IsSuite)
            {
                var suite = new SuiteNode(node) {Assembly = _assembly};
                TestSuites.Add(suite);
                foreach (TestNode test in node.Tests)
                {
                    AddNode(test);
                }
            }
            else
            {
                var test = new StubNode(node) {Assembly = _assembly};
                TestMethods.Add(test);
            }
        }
    }
}