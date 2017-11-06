// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution.Homework.Class_3.Reader
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public sealed class CommaSeparatedValues<T> : IReadOnlyList<T>
    {
        #region Public Enums

        public enum Seperator
        {
            Comma = 44,
            Tab = 9
        }

        #endregion Public Enums

        #region Public Constructors

        public CommaSeparatedValues(string raw, Func<string[], T> converter, Seperator seperator = Seperator.Comma, bool hasHeader = false)
        {
            this.Raw = raw ?? throw new ArgumentNullException(nameof(raw));
            this.Converter = converter ?? throw new ArgumentNullException(nameof(converter));
            var rows = raw.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var o = rows.Select(x => converter(x.Split((char)seperator)));

            this._list = new List<T>(o.Skip(hasHeader ? 1 : 0));
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly List<T> _list;

        #endregion Private Fields

        #region Public Properties

        public Func<string[], T> Converter { get; }

        public int Count
            => this._list.Count;

        public string Raw { get; }

        #endregion Public Properties

        #region Public Indexers

        public T this[int index]
            => this._list.ElementAt(index);

        #endregion Public Indexers

        #region Public Methods

        public static CommaSeparatedValues<T> Load(string csvFilePath, Func<string[], T> converter, Seperator seperator = Seperator.Comma, bool hasHeader = false)
        {
            if (csvFilePath is null)
                throw new ArgumentNullException(nameof(csvFilePath));
            if (converter is null)
                throw new ArgumentNullException(nameof(converter));

            using (var fstream = File.Open(csvFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fstream))
                {
                    var raw = sr.ReadToEnd();
                    Debug.WriteLine(raw);
                    return new CommaSeparatedValues<T>(raw, converter, seperator, hasHeader);
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
            => this._list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this._list.GetEnumerator();

        #endregion Public Methods
    }
}