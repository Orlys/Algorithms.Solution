// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

// #define COMPLEXITY

namespace Algorithms.Solution.Homework.Class_2
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public sealed class EinsteinResult
    {
        #region Public Constructors

        static EinsteinResult()
        {
            Failure = new EinsteinResult();
        }

        #endregion Public Constructors

        #region Private Constructors

        private EinsteinResult(IEnumerable<Person> persons, TimeSpan consuming)
        {
            this.IsSuccess = true;
            this.Persons = persons;
            this.Consuming = consuming;
        }

        private EinsteinResult()
        {
            this.IsSuccess = false;
            this.Persons = Array.Empty<Person>();
            this.Consuming = TimeSpan.MaxValue;
        }

        #endregion Private Constructors

        #region Public Properties

        public static EinsteinResult Failure { get; }

        public TimeSpan Consuming { get; }

        public bool IsSuccess { get; }

        public IEnumerable<Person> Persons { get; }

        #endregion Public Properties

        #region Public Methods

        public static EinsteinResult Success(IEnumerable<Person> result, TimeSpan consuming)
                                    => new EinsteinResult(result, consuming);

        public IEnumerable<Person> Ask(Expression<Predicate<Person>> question)
        {
            if (this.IsSuccess)
            {
                foreach (var r in this.Persons)
                    if (question.Compile()(r))
                        yield return r;
            }
            else
                yield break;
        }

        #endregion Public Methods
    }
}