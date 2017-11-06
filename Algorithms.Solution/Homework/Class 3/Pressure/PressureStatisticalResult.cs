// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution.Homework.Class_3
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Pressure.Models;

    public sealed class PressureStatisticalResult
    {
        #region Internal Constructors

        internal PressureStatisticalResult(PressureNode first, PressureNode last, IEnumerable<Crest> crests, IEnumerable<Trough> troughs)
        {
            this.First = first;
            this.Last = last;
            this.Crests = crests.ToList();
            this.Troughs = troughs.ToList();
            this.PeakToPeak = this.Crests
                .Cast<PressureNode>()
                .Concat(this.Troughs)
                .OrderBy(x => x.TimeStamp)
                .ToList();
        }

        #endregion Internal Constructors

        #region Public Properties

        public IList<Crest> Crests { get; }

        public PressureNode First { get; }

        public PressureNode Last { get; }

        public IList<Trough> Troughs { get; }

        public IList<PressureNode> PeakToPeak { get; }

        #endregion Public Properties

        #region Public Methods

        public string PrettyPrint(bool displayP2P = false)
        {
            return string.Join("\n", p().ToList());

            IEnumerable<string> p()
            {
                yield return $"# First:   { this.First.ToString()}";
                yield return $"# Last:    {this.Last.ToString()}";
                yield return $"# Crests:";
                yield return $"[";
                for (int i = 0; i < this.Crests.Count; i++)
                {
                    yield return $"    [{i.ToString().PadLeft(3, '0')}] {this.Crests[i].ToString()}";
                }
                yield return $"]";
                yield return $"# Troughs:";
                yield return $"[";
                for (int i = 0; i < this.Troughs.Count; i++)
                {
                    yield return $"    [{i.ToString().PadLeft(3, '0')}] {this.Troughs[i].ToString()}";
                }
                yield return $"]";

                if (displayP2P)
                {
                    yield return $"# Peak-To-Peak:";
                    yield return $"[";
                    for (int i = 0; i < this.PeakToPeak.Count; i++)
                    {
                        var pp = this.PeakToPeak[i];
                        yield return $"    [{i.ToString().PadLeft(3, '0')}, {pp.GetType().Name.PadRight(7, ' ')}] {pp.ToString()}";
                    }
                    yield return $"]";
                }
            }
        }

        #endregion Public Methods
    }
}