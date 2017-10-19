// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Algorithms.Solution.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public sealed class DemoProvider
    {
        #region Private Constructors

        private DemoProvider()
        {
            this._methods = (from t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                             let hw = t.GetCustomAttribute<HomeworkAttribute>()
                             where hw is HomeworkAttribute
                             select new { t, hw } into list
                             from m in list.t.GetMethods()
                             where m.GetCustomAttribute<EntryPointAttribute>() is EntryPointAttribute && m.IsStatic
                             select new
                             {
                                 list.hw.Id,
                                 m
                             })
                             .ToDictionary(x => x.Id, x => x.m);
        }

        #endregion Private Constructors

        #region Private Fields

        private static volatile DemoProvider _instance;

        private static object _locker = new object();

        private Dictionary<int, MethodInfo> _methods;

        #endregion Private Fields

        #region Public Properties

        public static DemoProvider Selector
        {
            get
            {
                if (_instance == null)
                    lock (_locker)
                        if (_instance == null)
                            _instance = new DemoProvider();
                return _instance;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 使用 <see cref="EntryPointAttribute.DefaultArgs"/> 做為預設參數運行範例
        /// </summary>
        /// <param name="id">要運行的範例編號</param>
        /// <returns></returns>
        public dynamic RunDemo(int id)
        {
            if (this._methods.TryGetValue(id, out var m))
            {
                return m.Invoke(null, m.GetCustomAttribute<EntryPointAttribute>().DefaultArgs);
            }
            else
                throw new IndexOutOfRangeException(nameof(id));
        }
        /// <summary>
        /// 使用自訂義的參數做為預設參數運行範例
        /// </summary>
        /// <param name="id">要運行的範例編號</param>
        /// <param name="args">參數，若範例無參數請使用 null 做為參數不然會引發例外</param>
        /// <returns></returns>
        public dynamic RunDemo(int id, params object[] args)
        {
            if (this._methods.TryGetValue(id, out var m))
            {
                return m.Invoke(null, args);
            }
            else
                throw new IndexOutOfRangeException(nameof(id));
        }

        #endregion Public Methods
    }
}