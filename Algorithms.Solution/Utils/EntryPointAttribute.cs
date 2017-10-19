﻿// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Algorithms.Solution.Utils
{
    using System;

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class EntryPointAttribute : Attribute
    {
        #region Public Constructors

        public EntryPointAttribute()
        {
        }

        public EntryPointAttribute(params object[] arguments)
        {
            this.DefaultArgs = arguments;
        }

        #endregion Public Constructors

        #region Public Properties

        public object[] DefaultArgs { get; }

        #endregion Public Properties
    }
}