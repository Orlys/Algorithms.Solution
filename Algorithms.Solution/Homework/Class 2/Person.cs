// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Algorithms.Solution.Homework.Class_2
{
    using System.Linq;

    /// <summary>
    /// 飲料
    /// </summary>
    public enum Beverage : int
    {
        /// <summary>
        /// 咖啡
        /// </summary>
        Coffee = 1,
        /// <summary>
        /// 牛奶
        /// </summary>
        Milk,
        /// <summary>
        /// 水
        /// </summary>
        Water,
        /// <summary>
        /// 茶
        /// </summary>
        Tea,
        /// <summary>
        /// 啤酒
        /// </summary>
        Beer
    }

    /// <summary>
    /// 菸
    /// </summary>
    public enum Cigaret : int
    {
        Dunhill = 1,
        Blend,
        PallMall,
        BlueMaster,
        Prince
    }

    /// <summary>
    /// 房屋顏色
    /// </summary>
    public enum HouseColor : int
    {/// <summary>
    /// 紅
    /// </summary>
        Red = 1,
        /// <summary>
        /// 白
        /// </summary>
        White,
        /// <summary>
        /// 黃
        /// </summary>
        Yellow,
        /// <summary>
        /// 綠
        /// </summary>
        Green,
        /// <summary>
        /// 藍
        /// </summary>
        Blue
    }

    /// <summary>
    /// 國籍
    /// </summary>
    public enum Nationality : int
    {
        /// <summary>
        /// 英國
        /// </summary>
        UK = 1,
        /// <summary>
        /// 瑞典
        /// </summary>
        Sweden,
        /// <summary>
        /// 挪威
        /// </summary>
        Norway,
        /// <summary>
        /// 丹麥
        /// </summary>
        Denmark,
        /// <summary>
        /// 德國
        /// </summary>
        Germany
    }

    /// <summary>
    /// 寵物
    /// </summary>
    public enum Pet : int
    {
        /// <summary>
        /// 貓
        /// </summary>
        Cat = 1,
        /// <summary>
        /// 狗
        /// </summary>
        Dog,
        /// <summary>
        /// 鳥
        /// </summary>
        Bird,
        /// <summary>
        /// 馬
        /// </summary>
        Horse,
        /// <summary>
        /// 魚
        /// </summary>
        Fish
    }

    public class Person
    {
        #region Public Constructors

        static Person()
        {
            Empty = new Person() ;
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal Person(int houseColor, int nationality, int pet, int beverage, int cigaret)
            : this((HouseColor)houseColor, (Nationality)nationality, (Pet)pet, (Beverage)beverage, (Cigaret)cigaret)
        {
        }
        internal Person(HouseColor houseColor, Nationality nationality, Pet pet, Beverage beverage, Cigaret cigaret)
        {
            this.HouseColor = houseColor;
            this.Nationality = nationality;
            this.Pet = pet;
            this.Beverage = beverage;
            this.Cigaret = cigaret;
        }

        #endregion Internal Constructors

        #region Private Constructors

        private Person()
        {

        }
        #endregion Private Constructors

        #region Public Properties

        public static Person Empty { get; }

        public Beverage Beverage { get; }

        public Cigaret Cigaret { get; }

        public HouseColor HouseColor { get; }

        public Nationality Nationality { get; }

        public Pet Pet { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// 是否有部分相等
        /// </summary>
        /// <param name="other">其他的 <see cref="Person"/> 物件實體</param>
        /// <returns></returns>
        public bool PartitalEquals(Person other)
            => (HouseColor == other.HouseColor ||
                Nationality == other.Nationality ||
                Pet == other.Pet ||
                Beverage == other.Beverage ||
                Cigaret == other.Cigaret);

        public override string ToString()
        {
            var objs = new object[]
            {
                Nationality,
                HouseColor,
                Pet,
                Beverage,
                Cigaret
            }.Select(x => x.ToString().PadRight(7) + " \t");

            return string.Join("| ", objs);
        }

        #endregion Public Methods
    }
}