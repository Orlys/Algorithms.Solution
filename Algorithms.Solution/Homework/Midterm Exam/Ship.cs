// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Algorithms.Solution.Homework.Midterm_Exam
{
    using System.Collections.Generic;
    using System.Linq;

    public class Ship
    {
        #region Public Constructors

        public Ship()
        {
            this._q = new Queue<TO>(MAXIMUM_STORAGE_COUNT);
        }

        #endregion Public Constructors

        #region Public Fields

        public const int MAXIMUM_STORAGE_COUNT = 2;

        #endregion Public Fields

        #region Private Fields

        private readonly Queue<TO> _q;

        #endregion Private Fields

        #region Public Properties

        public bool IsEmpty => this._q.Count == 0;

        public bool IsFill => this._q.Count == MAXIMUM_STORAGE_COUNT;

        #endregion Public Properties

        #region Public Methods

        public void Cross(TOQ q)
        {
            var de = q.Dequeue(1);
            var t = this.DequeueOnce();
            q.Enqueue(t);

            this.Enqueue(de);
        }

        public IEnumerable<TO> DequeueAll()
        {
            while (!this.IsEmpty)
            {
                yield return this.DequeueOnce().First();
            }
        }

        public IEnumerable<TO> DequeueOnce()
        {
            if (this.IsEmpty)
                yield break;

            yield return this._q.Dequeue();
        }

        public void Enqueue(IEnumerable<TO> os)
        {
            foreach (var item in os)
            {
                if (!this.Enqueue(item))
                    return;
            }
        }

        public bool Enqueue(TO o)
        {
            if (!this.IsFill && !this._q.Contains(o))
            {
                this._q.Enqueue(o);
                return true;
            }
            return false;
        }

        public override string ToString() => $"[{string.Join(", ", this._q)}]";

        #endregion Public Methods
    }
}