// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution.Homework.Class_1
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public sealed class Farmer
    {
        #region Public Constructors

        public Farmer()
        {
            this._void = new TransportObject("　");

            var dog = new TransportObject("狗");
            var chicken = new TransportObject("雞", dog);
            var rice = new TransportObject("米", chicken);

            this.LeftShore = new Queue<TransportObject>(new[]
            {
                dog,rice,chicken
            });
            this.RightShore = new Queue<TransportObject>();
        }

        #endregion Public Constructors

        #region Public Classes

        public class StatusReportEventArgs : EventArgs
        {
            #region Internal Constructors

            internal StatusReportEventArgs(Queue<TransportObject> leftShore, TransportObject ship, Queue<TransportObject> rightShore)
            {
                this.LeftShore = leftShore;
                this.Ship = ship;
                this.RightShore = rightShore;
            }

            internal StatusReportEventArgs(Queue<TransportObject> leftShore, Queue<TransportObject> rightShore) : this(leftShore, null, rightShore)
            {
            }

            #endregion Internal Constructors

            #region Public Properties

            public Queue<TransportObject> LeftShore { get; }

            public Queue<TransportObject> RightShore { get; }

            public TransportObject Ship { get; }

            #endregion Public Properties
        }

        #endregion Public Classes

        #region Private Fields

        private TransportObject _void;

        #endregion Private Fields

        #region Public Events

        public event Action OnCompleted;

        public event Action<string> OnProgressUpdate;

        public event Action OnStart;

        public event Action<StatusReportEventArgs> OnStatusUpdate;

        public event Action<int> OnStepUpdate;

        #endregion Public Events

        #region Public Properties

        public Queue<TransportObject> LeftShore { get; }

        public Queue<TransportObject> RightShore { get; }

        #endregion Public Properties

        #region Public Methods

        public static async void Transport(int millisecond = 1000)
        {
            var f = new Farmer();

            f.OnProgressUpdate += msg => Console.WriteLine(msg);
            f.OnStart += () => Console.WriteLine("Start");
            f.OnCompleted += () => Console.WriteLine("Stop");
            f.OnStepUpdate += (step) => Console.WriteLine($"================= {step} =================");
            f.OnStatusUpdate += e =>
            {
                Console.WriteLine(Left(e.LeftShore) + Middle(e.Ship) + Right(e.RightShore));
                Console.WriteLine(LShore() + MShip(e.Ship) + RShore());
                Console.WriteLine();

                string Left(Queue<TransportObject> q)
                    => string.Join(null, q).PadRight(5, '　');
                string Right(Queue<TransportObject> q)
                    => string.Join(null, q).PadLeft(5, '　');
                string Middle(TransportObject s)
                    => s?.ToString() ?? "　";

                string LShore()
                    => "███".PadRight(5, '～');
                string RShore()
                    => "███".PadLeft(5, '～');
                string MShip(TransportObject s)
                    => s is TransportObject ? "▃" : "～";
            };

            await f.TransportAsync(millisecond);
        }

        public async Task TransportAsync(int delayMillisecond = 1000)
        {
            var step = 0;
            var carry = default(TransportObject);

            this.OnStart?.Invoke();
            Step();

            while (true)
            {
                await Report("檢查左岸是否為空");
                if (LeftShore.Count == 0)
                {
                    await Report("左岸為空，不進行搬運");
                    break;
                }

                carry = LeftShore.Dequeue();
                await Report($"將 '{carry}' 放上船");
                Status(new StatusReportEventArgs(LeftShore, carry, RightShore));

                //檢查左岸是否會發生需要避免的狀況
                await Report("檢查左岸是否會發生需要避免的狀況");

                if (CheckAvoidSituation(LeftShore))
                {
                    await Report($"會有危險，把 '{carry}' 放回左岸");

                    //會有危險，把TO物件放回左岸並換下一個
                    LeftShore.Enqueue(carry);
                    Status(new StatusReportEventArgs(LeftShore, RightShore));
                    continue;
                }
                else
                {
                    await Report("左岸沒危險，渡河，到達右岸");

                    //左岸沒危險，渡河，到達右岸
                    RightShore.Enqueue(carry);
                    Status(new StatusReportEventArgs(LeftShore, RightShore));
                    
                    //左岸全空
                    if (LeftShore.Count == 0)
                    {
                        //結束
                        await Report("左岸全空");
                        break;
                    }

                    //檢查右岸是否會發生需要避免的狀況
                    await Report("檢查右岸是否會發生需要避免的狀況");
                    if (CheckAvoidSituation(RightShore))
                    {
                        carry = RightShore.Dequeue();
                        await Report($"會有危險，把會造成危機的 '{carry}' 放上船");
                        Status(new StatusReportEventArgs(LeftShore, carry, RightShore));

                        //送往左岸
                        LeftShore.Enqueue(carry);
                        await Report("渡河，到達左岸");
                        Status(new StatusReportEventArgs(LeftShore, RightShore));
                    }

                    //右岸沒有危險，空手回左岸
                    else
                    {
                        await Report("右岸沒有危險，空手回左岸");
                        Status(new StatusReportEventArgs(LeftShore, this._void, RightShore));
                    }
                }

                Step();
                await Report("目前狀況");
                Status(new StatusReportEventArgs(LeftShore, RightShore));
            }

            this.OnCompleted?.Invoke();
            return;

            void Step()
                => this.OnStepUpdate?.Invoke(step++);

            void Status(StatusReportEventArgs e)
                => this.OnStatusUpdate?.Invoke(e);

            async Task Report(string message)
            {
                this.OnProgressUpdate?.Invoke(message);
                await Task.Delay(delayMillisecond);
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// 檢查是否會發生需要避免的狀況
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        private bool CheckAvoidSituation(Queue<TransportObject> queue)
        {
            foreach (var a in queue)
                foreach (var b in queue)
                    if (a == b)
                        continue;
                    else if (a.Avoid == b)
                        return true;
            return false;
        }

        #endregion Private Methods
    }
}