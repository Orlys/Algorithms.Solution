// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

/*
 * 
 *  1. There are 5 houses in five different colors. They are lined up in a row side by side.
 *  2. In each house lives a person with a different nationality.
 *  3. These 5 owners drink a certain drink, smoke a certain brand of tobacco and keep a certain pet.
 *  4. No owners have the same pet, smoke the same tobacco, or drink the same drink.
 *  5. As you look at the 5 houses from across the street, the green house is adjacent to the left of the white house.
 *  
 *  Q: Who owns the Fish?
 */

namespace Algorithms.Solution.Homework.Class_2
{
    using Algorithms.Solution.Utils;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [Homework(2)]
    public class EinsteinPuzzle
    {
        #region Public Methods

        [EntryPoint(arguments: 10000)]
        public static async void Operate(int times)
        {
            var cache = default(EinsteinResult);
            var cts = new CancellationTokenSource();
            var tsp = new List<TimeSpan>(times);
            var eq = new EinsteinPuzzle();
            eq.OnRunCompleted += (result) => tsp.Add(result.Consuming);

            
            RunningDisp();
            for (int i = 0; i < times; i++)
                cache = await eq.RunAsync();
            cts.Cancel(false);

            Console.Clear();


            if (cache.IsSuccess)
            {
                foreach (var p in cache.Persons)
                    Console.WriteLine(p);

                Console.WriteLine("================================================================================");

                Console.WriteLine($"{times} 次中最高耗時: { Math.Round(tsp.Max(x => x.TotalMilliseconds), 5)} ms/次");
                Console.WriteLine($"{times} 次中平均耗時: { Math.Round(tsp.Average(x => x.TotalMilliseconds), 5)} ms/次");
                Console.WriteLine($"{times} 次中最低耗時: { Math.Round(tsp.Min(x => x.TotalMilliseconds), 5)} ms/次");
                Console.WriteLine();

                Console.WriteLine("Q: 誰養魚?");
                var answer = cache.Ask(x => x.Pet == Pet.Fish).FirstOrDefault().Nationality;
                Console.WriteLine($"A: { answer  }");
            }
            else
                Console.WriteLine("Failure");
            


            void RunningDisp()
            {
                Task.Run(() =>
                {
                    int i = 1;
                    Loop:
                    if (!cts.IsCancellationRequested)
                    {
                        if (i % 4 != 0)
                        {
                            Console.Write(".");
                            Task.Delay(500).Wait();
                            i++;
                        }
                        else
                        {
                            Console.Clear();
                            Task.Delay(500).Wait();
                            i = 1;
                        }
                        goto Loop;
                    }

                },cts.Token);
            }
        }

        #endregion Public Methods

        #region Public Constructors

        public EinsteinPuzzle()
        {
            this._stopwatch = new Stopwatch();

            this._persons = new Person[7];
            this._persons[0] = Person.Empty;
            this._persons[6] = Person.Empty;

            this._1 = new List<Person>();
            this._2 = new List<Person>();
            this._3 = new List<Person>();
            this._45 = new List<Person>();
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly List<Person> _1;
        private readonly List<Person> _2;
        private readonly List<Person> _3;
        private readonly List<Person> _45;

        private readonly IList<Person> _persons;
        private readonly Stopwatch _stopwatch;

        #endregion Private Fields

        #region Public Events

        public event Action<EinsteinResult> OnRunCompleted;

        #endregion Public Events

        #region Public Methods

        public EinsteinResult Run()
        {
            this.Init();
            var r = EinsteinResult.Failure;

            this._stopwatch.Start();
            /*
                產生 Person 資料
             */
            for (var houseColor = 1; houseColor <= 5; houseColor++)
                for (var nationality = 1; nationality <= 5; nationality++)
                    for (var pet = 1; pet <= 5; pet++)
                        for (var beverage = 1; beverage <= 5; beverage++)
                            for (var cigaret = 1; cigaret <= 5; cigaret++)
                            {
                                var person = new Person(houseColor, nationality, pet, beverage, cigaret);

                                //使用逆否命題判斷關連
                                if (Contrapositive(person))
                                {
                                    //分類

                                    //如果國籍為挪威，存入 _1 序列中
                                    if (person.Nationality == Nationality.Norway)
                                        this._1.Add(person);

                                    //如果房屋為藍色，存入 _2 序列中
                                    else if (person.HouseColor == HouseColor.Blue)
                                        this._2.Add(person);

                                    //如果喜歡的飲料為牛奶，存入 _3 序列中
                                    else if (person.Beverage == Beverage.Milk)
                                        this._3.Add(person);

                                    //剩餘無法分類的存進 _45 序列中，後面要做交集用
                                    else
                                        this._45.Add(person);
                                }
                            }

            foreach (var p1 in _1)
            {
                //鎖定一號位置
                this._persons[1] = p1;
                foreach (var p2 in _2)
                {
                    //鎖定二號位置
                    this._persons[2] = p2;

                    //確定 1~2號 符合條件
                    if (PropertiesChecker(1, 2))
                        foreach (var p3 in _3)
                        {
                            //鎖定三號位置
                            this._persons[3] = p3;

                            //確定 1~3號 符合條件
                            if (PropertiesChecker(1, 3))
                                foreach (var p4 in _45)
                                {
                                    //鎖定四號位置
                                    this._persons[4] = p4;

                                    //確定 1~4號 符合條件
                                    if (PropertiesChecker(1, 4))
                                        foreach (var p5 in _45)
                                        {
                                            //鎖定五號位置
                                            _persons[5] = p5;

                                            //確定 1~5號 符合條件
                                            if (PropertiesChecker(1, 5) && RelativePosition())
                                            {
                                                //成功
                                                this._stopwatch.Stop();
                                                r = EinsteinResult.Success(this._persons.Skip(1).Take(5), this._stopwatch.Elapsed);
                                                this.OnRunCompleted?.Invoke(r);
                                                return r;
                                            }
                                        }
                                }
                        }
                }
            }

            //失敗
            this._stopwatch.Stop();
            this.OnRunCompleted?.Invoke(r);
            return r;



            bool PropertiesChecker(int min, int max)
            {
                for (int i = min; i < max; i++)
                    if (this._persons[max].PartitalEquals(this._persons[i]))
                        return false;
                return true;
            }
        }

        public async Task<EinsteinResult> RunAsync()
        {
            this.Init();
            return await Task.Run(() => this.Run());
        }

        private bool Contrapositive(Person p)
        {
            var contrapositive =

                //英國人住在紅色房子裡
                p.Nationality == Nationality.UK & p.HouseColor != HouseColor.Red ||
                p.Nationality != Nationality.UK & p.HouseColor == HouseColor.Red ||

                //Sweden人養狗
                p.Nationality == Nationality.Sweden & p.Pet != Pet.Dog ||
                p.Nationality != Nationality.Sweden & p.Pet == Pet.Dog ||

                //丹麥人喝茶
                p.Nationality == Nationality.Denmark & p.Beverage != Beverage.Tea ||
                p.Nationality != Nationality.Denmark & p.Beverage == Beverage.Tea ||

                //綠色房子的屋主喝咖啡
                p.HouseColor == HouseColor.Green & p.Beverage != Beverage.Coffee ||
                p.HouseColor != HouseColor.Green & p.Beverage == Beverage.Coffee ||

                //抽Palll Mall香煙的屋主養鳥
                p.Pet == Pet.Bird & p.Cigaret != Cigaret.PallMall ||
                p.Pet != Pet.Bird & p.Cigaret == Cigaret.PallMall ||

                //黄色屋主抽Dunhill；
                p.HouseColor == HouseColor.Yellow & p.Cigaret != Cigaret.Dunhill ||
                p.HouseColor != HouseColor.Yellow & p.Cigaret == Cigaret.Dunhill ||

                //抽Blue Master香煙的屋主喝啤酒
                p.Beverage == Beverage.Beer & p.Cigaret != Cigaret.BlueMaster ||
                p.Beverage != Beverage.Beer & p.Cigaret == Cigaret.BlueMaster ||

                //德國人抽Prince
                p.Nationality == Nationality.Germany & p.Cigaret != Cigaret.Prince ||
                p.Nationality != Nationality.Germany & p.Cigaret == Cigaret.Prince;

            return !contrapositive;
        }

        private void Init()
        {
            this._1.Clear();
            this._2.Clear();
            this._3.Clear();
            this._45.Clear();
            this._stopwatch.Reset();
        }

        private bool RelativePosition()
        {
            int progress = 5;
            for (int k = 1; k <= progress; k++)
            {
                //綠色的房屋在白色房屋的左邊
                if (_persons[k].HouseColor == HouseColor.White && _persons[k - 1].HouseColor == HouseColor.Green)
                    for (int i = 1; i <= progress; i++)
                    {
                        //抽Blend的人住在養貓人家的隔壁
                        if ((_persons[i].Pet == Pet.Cat && _persons[i - 1].Cigaret == Cigaret.Blend) ||
                            (_persons[i].Pet == Pet.Cat && _persons[(i + 1) % 6].Cigaret == Cigaret.Blend))
                            for (int j = 1; j <= progress; j++)
                            {
                                //養馬的屋主隔壁是抽Dunhill
                                if ((_persons[j].Cigaret == Cigaret.Dunhill && _persons[j - 1].Pet == Pet.Horse) ||
                                   (_persons[j].Cigaret == Cigaret.Dunhill && _persons[(j + 1) % 6].Pet == Pet.Horse))
                                    return true;
                            }
                    }
            }
            return false;
        }

        #endregion Public Methods
    }
}