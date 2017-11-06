// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution.Homework.Class_3.Pressure.Models
{
    using System;

    public class Trough : PressureNode
    {
        #region Internal Constructors

        internal Trough(TimeSpan timeStamp, double value) : base(timeStamp, value)
        {
        }

        #endregion Internal Constructors
    }
}