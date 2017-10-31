// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Algorithms.Solution.Homework.Midterm_Exam
{
    using System.Collections.Generic;

    /// <summary>
    /// Transport Object (TO)
    /// </summary>
    public class TO
    {
        #region Public Constructors

        static TO()
        {
            Wolf = new TO("狼");
            Dog = new TO("狗", Wolf);
            Chicken = new TO("雞", Dog, Wolf);
            Rice = new TO("米", Chicken);
        }

        #endregion Public Constructors

        #region Private Constructors

        private TO(string name, params TO[] avoid)
        {
            this.Name = name;
            this.Avoid = avoid ?? new TO[0];
        }

        #endregion Private Constructors

        #region Public Properties

        public static TO Chicken { get; }

        public static TO Dog { get; }

        public static TO Rice { get; }

        public static TO Wolf { get; }

        public IList<TO> Avoid { get; }

        public string Name { get; }

        #endregion Public Properties

        #region Public Methods

        public static bool operator !=(TO obj1, TO obj2)
        {
            return !obj1.Equals(obj2);
        }

        public static bool operator ==(TO obj1, TO obj2)
        {
            return obj1.Equals(obj2);
        }

        public override bool Equals(object obj)
        {
            if (obj is TO o)
            {
                return this.GetHashCode() == o.GetHashCode();
            }
            return false;
        }

        public override int GetHashCode() => this.Name.GetHashCode();

        public override string ToString() => this.Name.ToString();

        #endregion Public Methods
    }
}