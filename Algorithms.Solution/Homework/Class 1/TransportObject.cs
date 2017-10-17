
namespace Algorithms.Solution.Homework.Class_1
{
    using System;

    /// <summary>
    /// 欲轉傳的物件
    /// </summary>
    public class TransportObject : IEquatable<TransportObject>
    {
        public static bool operator ==(TransportObject o1, TransportObject o2)
            => o1?.Equals(o2) ?? false;
        public static bool operator !=(TransportObject o1, TransportObject o2)
            => !(o1?.Equals(o2) ?? false);

        /// <summary>
        /// 初始化 <see cref="TransportObject"/> 類別的新執行個體
        /// </summary>
        /// <param name="name">名稱</param>
        /// <param name="avoid">要避開的 <see cref="TransportObject"/> 物件</param>
        public TransportObject(string name, TransportObject avoid = default)
        {
            this.Name = name;
            this.Avoid = avoid;
        }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; }


        /// <summary>
        /// 要避開的 <see cref="TransportObject"/> 物件
        /// </summary>
        public TransportObject Avoid { get; }

        public override int GetHashCode()
            => this.Name.GetHashCode();

        public bool Equals(TransportObject to)
            => to.GetHashCode() == this.GetHashCode();

        public override bool Equals(object obj)
            => (obj is TransportObject to) ? this.Equals(to) : false;

        public override string ToString()
            => this.Name;

    }
}
