// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/
namespace Algorithms.Solution.Homework.Class_3.Sorting
{
    using System;
    using System.Collections.Generic;

    public abstract class Sorting<T> : ISorting<T>
    {
        private Sorting()
        {
        }

        protected Sorting(SortEvaluator<T> evaluator)
        {
            this.Evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
            this.Sort(new T[0]);
        }

        public SortEvaluator<T> Evaluator { get; }

        public abstract SortedResult<T> Sort(IList<T> unsorted);
    }
}