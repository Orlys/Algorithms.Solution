// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Algorithms.Solution.Homework.Midterm_Exam
{
    using System;
    using System.Threading.Tasks;
    using Utils;

    [Homework(3)]
    public class DynamicProgramming
    {
        #region Private Enums

        private enum Shuttle
        {
            Left,
            Right
        }

        #endregion Private Enums

        #region Public Methods

        [EntryPoint]
        public static void Run()
        {

            var left = new TOQ(new[] { TO.Rice, TO.Wolf, TO.Dog, TO.Chicken });
            var right = new TOQ();
            var ship = new Ship();

            var switcher = default(Shuttle);
            Report("開始");
            Label_1:
            disp();
            switch (switcher)
            {
                case Shuttle.Left:
                    Report($"左岸佇列 為空且 船佇列 為空? :{left.IsEmpty && ship.IsEmpty}");
                    if (left.IsEmpty && ship.IsEmpty)
                    {
                        break;
                    }
                    Report("從 左岸佇列 取出第一個物件並存到 船佇列尾端");
                    ship.Enqueue(left.Dequeue(1));

                    Label_2:
                    Report($"左岸會產生問題嗎? :{left.HasProblem}");
                    if (left.HasProblem)
                    {
                        Report($"船是否已滿? :{ship.IsFill}");
                        if (ship.IsFill)
                        {
                            Report("將 船佇列 的第一個物件取出並存到 左岸佇列 的尾端");
                            Report("將 左岸佇列 的第一個物件取出並存到 船佇列 的尾端");
                            ship.Cross(left);
                        }
                        else
                        {
                            Report("從 左岸佇列 取出第一個物件，存到 船佇列 的尾端");
                            ship.Enqueue(left.Dequeue(1));
                        }
                        goto Label_2;
                    }
                    else
                    {
                        Report("船前往右岸");
                        switcher = Shuttle.Right;
                        goto Label_1;
                    }

                case Shuttle.Right:
                    Report($"左岸佇列 為空且 船佇列 不為空嗎? :{left.IsEmpty && !ship.IsEmpty}");
                    if (left.IsEmpty && !ship.IsEmpty)
                    {
                        Report($"把 船佇列 所有的物件加到 右岸佇列 的尾端");
                        right.Enqueue(ship.DequeueAll());
                        break;
                    }

                    Report($"把 船佇列 所有的物件加到 右岸佇列 的尾端(若船佇列已空則省略)");
                    right.Enqueue(ship.DequeueAll());

                    Label_3:
                    Report($"右岸會產生問題嗎? :{right.HasProblem}");
                    if (right.HasProblem)
                    {
                        Report($"船是否已滿? :{ship.IsFill}");
                        if (ship.IsFill)
                        {
                            Report("將 船佇列 的第一個物件取出並存到 右岸佇列 的尾端");
                            Report("將 右岸佇列 的第一個物件取出並存到 船佇列 的尾端");
                            ship.Cross(right);
                        }
                        else
                        {
                            Report("從 右岸佇列 取出第一個物件，存到 船佇列 的尾端");
                            ship.Enqueue(right.Dequeue(1));
                        }
                        goto Label_3;
                    }
                    else
                    {
                        Report("船前往左岸");
                        switcher = Shuttle.Left;
                        goto Label_1;
                    }
            }
            disp();
            Report("結束");
            Console.ReadKey();

            void disp()
            {
                Console.WriteLine();
                Console.WriteLine("# -------- 狀態顯示 --------");
                Console.WriteLine("# 船的位置: " + switcher);
                Console.WriteLine("# 左岸狀態: "+left);
                Console.WriteLine("# 船的狀態: "+ship);
                Console.WriteLine("# 右岸狀態: "+right);
                Console.WriteLine("# -------------------------");
            }

            void Report(string message)
            {
                Task.Delay(500).Wait();
                Console.WriteLine(message);
            }
        }

        #endregion Public Methods
    }
}