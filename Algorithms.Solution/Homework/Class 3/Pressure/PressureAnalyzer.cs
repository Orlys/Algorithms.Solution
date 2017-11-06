// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution.Homework.Class_3
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Algorithms.Solution.Homework.Class_3.Pressure.Models;
    using Algorithms.Solution.Utils;
    using Reader;
    using Sorting;

    [Homework(4)]
    public sealed class PressureAnalyzer
    {
        #region Public Constructors

        public PressureAnalyzer(ISorting<PressureNode> sorting)
        {
            this.Sorting = sorting;
        }

        #endregion Public Constructors

        #region Public Properties

        public ISorting<PressureNode> Sorting { get; }

        #endregion Public Properties

        #region Public Methods

        [EntryPoint(5000,true)]
        public static async void Run(int delay,bool dispalyP2P)
        {
            var paths = new[]
            {
                @".\Homework\Class 3\Test Data\1-240-12.csv",
                @".\Homework\Class 3\Test Data\233-12.csv",
                @".\Homework\Class 3\Test Data\239-12.csv"
            };
            foreach (var path in paths)
            {
                Console.Clear();
                Console.WriteLine($"# File: {Path.GetFileName(path)}");
                

                var csv = CommaSeparatedValues<PressureNode>.Load(
                    csvFilePath: path,
                    converter: x => new PressureNode(TimeSpan.FromSeconds(double.Parse(x[0])), double.Parse(x[1])));

                var analyzer = new PressureAnalyzer(new QuickSort<PressureNode>(x => (decimal)x.TimeStamp.TotalMilliseconds));

                var result = analyzer.Analyze(csv);

                Console.WriteLine(result.PrettyPrint(dispalyP2P));
                await Task.Delay(delay);
            }
        }

        public PressureStatisticalResult Analyze(IEnumerable<PressureNode> nodes)
        {
            //sorting by time stamp
            var sorted = this.Sorting.Sort(nodes.ToList());

            var crest = default(PressureNode);
            var trough = default(PressureNode);

            var first = sorted[0];
            var last = sorted[sorted.Count - 1];
            var crests = new List<Crest>();
            var troughs = new List<Trough>();

            for (int i = 1; i < sorted.Count - 1; i++)
            {
                //finding crest
                if (sorted[i - 1].Value < sorted[i].Value && sorted[i].Value <= sorted[i + 1].Value)
                {
                    if (trough != null)
                    {
                        troughs.Add(new Trough(trough.TimeStamp, trough.Value));
                        trough = null;
                    }
                    crest = sorted[i];
                }

                //find trough
                else if (sorted[i - 1].Value > sorted[i].Value && sorted[i].Value >= sorted[i + 1].Value)
                {
                    if (crest != null)
                    {
                        crests.Add(new Crest(crest.TimeStamp, crest.Value));
                        crest = null;
                    }
                    trough = sorted[i];
                }
            }
            return new PressureStatisticalResult(first, last, crests, troughs);
        }

        #endregion Public Methods
    }
}