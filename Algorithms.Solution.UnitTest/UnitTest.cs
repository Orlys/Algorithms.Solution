// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Algorithms.Solution.UnitTest
{
    using Homework.Class_1;
    using Homework.Class_2;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture(Author = "Viyrex(aka Yuyu)", Description = "作業測試")]
    public class UnitTest
    {
        #region Public Methods

        [Test(Author = "Viyrex(aka Yuyu)", Description = "雞米狗問題")]
        public async Task TestClass1()
        {
            var f = new Farmer();
            Assert.IsInstanceOf<Farmer>(f);

            Assert.AreEqual(f.LeftShore.Count, 3);
            Assert.AreEqual(f.RightShore.Count, 0);

            var delay = 0;
            await f.TransportAsync(delay);

            Assert.AreEqual(f.LeftShore.Count, 0);
            Assert.AreEqual(f.RightShore.Count, 3);
        }

        [Test(Author = "Viyrex(aka Yuyu)", Description = "愛因斯坦問題")]
        public async Task TestClass2()
        {
            var puzzle = new EinsteinPuzzle();

            var result = await puzzle.RunAsync();

            var n = result
                .Ask((p) => p.Pet == Pet.Fish)
                .FirstOrDefault()
                .Nationality;

            Assert.AreEqual(n, Nationality.Germany);
        }

        #endregion Public Methods
    }
}