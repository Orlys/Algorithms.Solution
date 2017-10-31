// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Algorithms.Solution.Homework.Midterm_Exam
{
    using System.Collections.Generic;

    /// <summary>
    /// Transport Object Queue (TOQ)
    /// </summary>
    public class TOQ
    {
        #region Public Constructors

        public TOQ()
        {
            this._q = new List<TO>();
        }

        public TOQ(IEnumerable<TO> source)
        {
            this._q = new List<TO>(source);
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly List<TO> _q;

        #endregion Private Fields

        #region Public Properties

        public int Count => this._q.Count;

        public bool HasProblem
        {
            get
            {
                foreach (var target in this._q)
                    foreach (var compare in this._q)
                    {

                        if (target == compare)
                            continue;
                        if (target.Avoid.Contains(compare))
                        {
                            return true;
                        }
                    }
                return false;
            }
        }

        public bool IsEmpty => this._q.Count == 0;

        #endregion Public Properties

        #region Public Methods

        public IEnumerable<TO> Dequeue(uint count)
        {
            for (int i = 0; i < count; i++)
            {
                if (this.IsEmpty)
                    yield break;
                else
                {
                    yield return this._q[0];
                    this._q.RemoveAt(0);
                }
            }
        }

        public void Enqueue(IEnumerable<TO> os)
        {
            foreach (var item in os)
            {
                this.Enqueue(item);
            }
        }

        public void Enqueue(TO o)
        {
            if (!this._q.Contains(o))
            {
                this._q.Add(o);
            }
        }

        public override string ToString() => $"[{string.Join(", ", this._q)}]";

        #endregion Public Methods
    }
}