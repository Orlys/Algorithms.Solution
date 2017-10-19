// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
// License: CC BY-SA 4.0 https://creativecommons.org/licenses/by-sa/4.0/

namespace Algorithms.Solution
{
    using Algorithms.Solution.Homework.Class_2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Collections;
    using System.Linq.Expressions;
    using System.Diagnostics;
    using System.Threading.Tasks;

    internal class Program
    {



        #region Private Methods

        private static async Task Main(string[] args)
        {
            var homeworks = (from t in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                             let hw = t.GetCustomAttribute<HomeworkAttribute>()
                             where hw is HomeworkAttribute
                             select new { t, hw } into list
                             from m in list.t.GetMethods()
                             where m.GetCustomAttribute<EntryPointAttribute>() is EntryPointAttribute
                             select new
                             {
                                 list.hw.Id,
                                 m
                             }).ToDictionary(x => x.Id, x => x.m);

            foreach (var item in homeworks)
            {
                Console.WriteLine(item);
            }


            Console.ReadKey();
            return;
            /*
            int times = 1;
            var tsp = new List<TimeSpan>(times);


            var work = new EinsteinQuestion();
            work.OnRunCompleted += (result) =>
            {
                tsp.Add(result.Consuming);
            };

            var cache = default(EinsteinResult);

            for (int i = 0; i < times; i++)
            {
                cache = await work.RunAsync();
            }
            
            if(cache.IsSuccess)
            {
                foreach (var p in cache.Persons)
                {
                    Console.WriteLine(p);
                }
            }
            Console.WriteLine("==============================================================================");
            Console.WriteLine(cache.IsSuccess ? "Success" : "Failure");


            Console.WriteLine($"{times} 次中平均耗時: { Math.Round( tsp.Average(x => x.TotalMilliseconds),5)} ms/次");
            Console.WriteLine($"{times} 次中最低耗時: { Math.Round(tsp.Min(x => x.TotalMilliseconds),5)} ms /次");
            Console.WriteLine($"{times} 次中最高耗時: { Math.Round(tsp.Max(x => x.TotalMilliseconds),5)} ms /次");

            Console.WriteLine("Q: 誰養魚?");
            var answer = cache.Ask(x => x.Pet == Pet.Fish).FirstOrDefault();
            Console.WriteLine($"A: { answer  }");

            Console.ReadKey(true);
            */
        }

        #endregion Private Methods
    }
}




