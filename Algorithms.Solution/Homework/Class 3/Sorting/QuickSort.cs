// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution.Homework.Class_3.Sorting
{
    using System.Collections.Generic;

    public sealed class QuickSort<T> : Sorting<T>
    {
        #region Public Constructors

        public QuickSort(SortEvaluator<T> evaluator) : base(evaluator)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override SortedResult<T> Sort(IList<T> unsorted)
        {
            sort(unsorted, 0, unsorted.Count - 1);
            return SortedResult<T>.Create(unsorted);

            void sort(IList<T> sources, int left, int right)
            {
                if (left < right)
                {
                    T pivot = sources[(left + right) / 2];
                    int i = left - 1;
                    int j = right + 1;
                    Loop:
                    while (this.Evaluator(sources[++i]) < this.Evaluator(pivot)) ;
                    while (this.Evaluator(sources[--j]) > this.Evaluator(pivot)) ;

                    if (i < j)
                    {
                        swap(sources, i, j);
                        goto Loop;
                    }

                    sort(sources, left, i - 1);
                    sort(sources, j + 1, right);
                }
            }

            void swap(IList<T> list, int index1, int index2)
            {
                var tmp = list[index1];
                list[index1] = list[index2];
                list[index2] = tmp;
            }
        }

        #endregion Public Methods
    }
}