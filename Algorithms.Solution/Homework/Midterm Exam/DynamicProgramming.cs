
namespace Algorithms.Solution.Homework.Midterm_Exam
{
    using System;
    using Utils;

    [Homework(3)]
    public class DynamicProgramming
    {
        [EntryPoint]
        public static void Run()
        {

            var left = new TOQ(new[] { TO.Rice, TO.Wolf, TO.Dog, TO.Chicken });
            var right = new TOQ();
            var ship = new Ship();

            var switcher = 'l';

            Label_1:
            disp();
            switch (switcher)
            {
                case 'l':

                    if (left.IsEmpty && ship.IsEmpty)
                    {
                        goto default;
                    }

                    ship.Enqueue(left.Dequeue(1));

                    Label_2:
                    if (left.HasProblem)
                    {
                        if (ship.IsFill)
                        {
                            ship.Cross(left);
                        }
                        else
                        {
                            ship.Enqueue(left.Dequeue(1));
                        }
                        goto Label_2;
                    }
                    else
                    {
                        switcher = 'r';
                        goto Label_1;
                    }

                case 'r':
                    if (left.IsEmpty && !ship.IsEmpty)
                    {
                        right.Enqueue(ship.DequeueAll());
                        goto default;
                    }

                    right.Enqueue(ship.DequeueAll());

                    Label_3:
                    if (right.HasProblem)
                    {
                        if (ship.IsFill)
                        {
                            ship.Cross(right);
                        }
                        else
                        {
                            ship.Enqueue(right.Dequeue(1));
                        }
                        goto Label_3;
                    }
                    else
                    {
                        switcher = 'l';
                        goto Label_1;
                    }

                default:
                    break;
            }
            disp();
            Console.ReadKey();

            void disp()
            {
                Console.ReadKey();
                Console.WriteLine("status: " + switcher);
                Console.WriteLine(left);
                Console.WriteLine(ship);
                Console.WriteLine(right);
                Console.WriteLine();

            }

        }

    }
}
