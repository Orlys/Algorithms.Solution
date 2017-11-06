// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/
namespace Algorithms.Solution.Homework.Class_3.Sorting
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class SortedResult<T> : IReadOnlyCollection<T>, IReadOnlyList<T>
    {
        #region Private Constructors

        private SortedResult(IList<T> collection)
        {
            this._list = collection;
        }

        #endregion Private Constructors

        #region Private Fields

        private readonly IList<T> _list;

        #endregion Private Fields

        #region Public Properties

        public int Count => this._list.Count;

        #endregion Public Properties

        #region Public Indexers

        public T this[int index] => this._list[index];

        #endregion Public Indexers

        #region Public Methods

        public IEnumerator<T> GetEnumerator() => this._list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        #endregion Public Methods

        #region Internal Methods

        internal static SortedResult<T> Create(IEnumerable<T> collection)
        {
            return new SortedResult<T>(collection?.ToList() ?? throw new ArgumentNullException(nameof(collection)));
        }

        #endregion Internal Methods
    }
}