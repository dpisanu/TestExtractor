using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NDesk.Options;
using TestExtractor.Extractor;
using TestExtractor.Split;
using TestExtractor.Structure;
using TestExtractor.Structure.Enums;

namespace TestExtractor.Client.ExtractorCli
{
    internal class Program
    {
        #region Fields

        private static OptionSet _optionSet;

        private static List<string> _assemblies;
        private static List<string> _excludesCategories;

        private static bool _extractSuits;
        private static bool _extractStubs;
        private static bool _includeIgnores;
        private static bool _showhelp;

        private static int _chunkSize;
        private static string _exportfile;
        private static string _testFramework;

        #endregion

        private static void Main(string[] args)
        {
            Init();
            _showhelp = !args.Any();

            CreateOptions();
            Parse(args);

            if (_showhelp || !Validate())
            {
                ShowHelp();
                return;
            }
            
            var nodes = Extract();
            var filteredNodes = Filter(nodes);
            var splitResult = Split(filteredNodes);
            Export(splitResult);
        }

        private static void Init()
        {
            _assemblies = new List<string>();
            _extractSuits = false;
            _extractStubs = false;
            _includeIgnores = false;
            _excludesCategories = new List<string>();
            _chunkSize = 0;
            _exportfile = string.Empty;
            _testFramework = string.Empty;
        }

        private static void CreateOptions()
        {
            _optionSet =  new OptionSet
            {
                {
                    "a|assemblies=", "the {ASSEMBLY} to load.",
                    v => { if (v != null) _assemblies.Add(v); }
                },
                {
                    "ec|excludecategories=", "the categories to exclude.",
                    v => { if (v != null) _excludesCategories.Add(v); }
                },
                {
                    "ts|suits", "extract test suits",
                    v => { if (v != null) _extractSuits = true; }
                },
                {
                    "tm|methods", "extract test methods",
                    v => { if (v != null) _extractStubs = true; }
                },
                {
                    "ii|includeignores", "include ignored nodes. Standard it is set to false",
                    v => { if (v != null) _includeIgnores = true; }
                },
                {
                    "cs|chunksize=", "the chunksize to split by. 0 means one big list.",
                    (int v) =>  _chunkSize = v
                },
                {
                    "f|file=", "export file",
                    v => { if (v != null) _exportfile = v; }
                },
                {
                    "tf|testframework=", "test framework. e.g.: NUnit",
                    v => { if (v != null) _testFramework = v; }
                },
                {
                    "h|help", "show this message and exit",
                    v => { if (v != null) _showhelp = true; }
                }
            };
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Extract Unit Tests from a list of Test Assemblies.");
            Console.WriteLine("The Extracted Tests can either be Test Methods (Stubs) or Test Suits.");
            Console.WriteLine("The Extracted Data can be exported to 1 or or be split evenly across 'n' files of chunksize 'x'.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            _optionSet.WriteOptionDescriptions(Console.Out);
        }

        private static List<string> Parse(IEnumerable<string> args)
        {
            var extra = new List<string>();
            try
            {
                extra.AddRange(_optionSet.Parse(args));
            }
            catch (OptionException e)
            {
                Console.Write ("Parsing the options caused an Error: ");
                Console.WriteLine (e.Message);
                Console.WriteLine ("Try `-h | --help' for more information.");
            }
            return extra;
        }

        private static bool Validate()
        {
            if (!_assemblies.Any())
            {
                Console.Write("At least one Assembly should be specified.");
                return false;
            }
            if (_extractSuits == _extractStubs)
            {
                Console.Write("Test Methods and Test Suits can not be extracted at the same time.");
                return false;
            }
            if (string.IsNullOrEmpty(_exportfile))
            {
                Console.Write("An export file needs to be specified.");
                return false;
            }
            if (string.IsNullOrEmpty(_testFramework))
            {
                Console.Write("A test Framework needs to be specified.");
                return false;
            }
            return true;
        }

        private static List<INode> Extract()
        {
            var framework = _testFramework.ToLower();
            if (!framework.Equals("nunit"))
            {
                Console.Write("Unsupported Test Framework.");
            }

            var nodes = new List<INode>();
            var extractor = ExtractFactory.Extractor<Extractors.NUnit.Extractor.NUnit>();
            if (_extractSuits)
            {
                nodes.AddRange(extractor.ExtractTimed<ISuiteNode>(_assemblies).Item1);
            }
            else
            {
                nodes.AddRange(extractor.ExtractTimed<IStubNode>(_assemblies).Item1);
            }
            return nodes;
        }

        private static List<INode> Filter(IList<INode> nodes)
        {
            var filteredNodes = new List<INode>();
            var filter = new Filter.Filter();
            var nodeTypeFilterValue = _extractSuits ? NodeTypes.TestFixture : NodeTypes.TestMethod;

            // First Filter the Ignore State
            var ignoredFiltered = new List<INode>();
            if (_includeIgnores)
            {
                ignoredFiltered.AddRange(nodes);
            }
            else
            {
                ignoredFiltered.AddRange(filter.FilterOutIgnores(nodes).OfFilters);
            }

            // Second Filter by Node Type
            var filteredByNodeType = filter.FilterNodeTypes(ignoredFiltered, new List<NodeTypes> { nodeTypeFilterValue });

            // Third Filter by Category
            var filteredByCategory = filter.FilterCategories(filteredByNodeType.OfFilters, _excludesCategories);

            filteredNodes.AddRange(filteredByCategory.NotOfFilters);
            return filteredNodes;
        }

        private static ISplitResult<INode> Split(ICollection<INode> nodes)
        {
            var chunksize = _chunkSize == 0 ? nodes.Count : _chunkSize;

            var blocks = TestExtractor.Split.Split.SplitByPackageSize(nodes, chunksize);

            return blocks;
        }

        private static void Export(ISplitResult<INode> splitResult)
        {
            var counter = 0;
            foreach (var block in splitResult)
            {
                counter++;
                var fileName = _exportfile + counter;

                // Example #1: Write an array of strings to a file.
                // Create a string array that consists of three lines.
                var lines = block.Select(node => node.NodeName.FullName).ToList();

                // WriteAllLines creates a file, writes a collection of strings to the file,
                // and then closes the file.  You do NOT need to call Flush() or Close().
                File.WriteAllLines(fileName, lines);
            }
        }
    }
}