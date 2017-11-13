// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution.Homework.Class_3
{
    using System;

    public class PressureNode : IComparable<PressureNode>
    {
        #region Internal Constructors

        internal PressureNode(TimeSpan timeStamp, double value)
        {
            this.TimeStamp = timeStamp;
            this.Value = value;
        }

        #endregion Internal Constructors

        #region Public Properties

        public TimeSpan TimeStamp { get; }

        public double Value { get; }

        #endregion Public Properties

        #region Public Methods

        int IComparable<PressureNode>.CompareTo(PressureNode other)
        {
            if (other.Value > this.Value)
                return -1;
            else if (other.Value < this.Value)
                return 1;
            else
                return 0;
        }

        public override string ToString() => $"Stamp: {this.TimeStamp.ToString(@"mm\:ss\.ff").PadRight(8)} | Pressure: {this.Value.ToString("F2")}";

        #endregion Public Methods
    }
}