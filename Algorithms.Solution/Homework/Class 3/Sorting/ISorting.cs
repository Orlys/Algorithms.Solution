// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution.Homework.Class_3.Sorting
{
    using System.Collections.Generic;

    public interface ISorting<T>
    {
        #region Public Properties

        SortEvaluator<T> Evaluator { get; }

        #endregion Public Properties

        #region Public Methods

        SortedResult<T> Sort(IList<T> unsorted);

        #endregion Public Methods
    }

    public delegate decimal SortEvaluator<in V>(V obj);
}