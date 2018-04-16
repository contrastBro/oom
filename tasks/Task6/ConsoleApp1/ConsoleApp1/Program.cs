using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive.Linq;
using System.Reactive.Subjects;

using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SomeAwesomeClass
    {
        private string input;
        private string output;

        public string Input { set { input = value; output = value + value; } }
        public string Output { get { return output; } }

        public SomeAwesomeClass(string input_value)
        {
            this.Input = input_value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var task2 = Task.Run(() => {
                Task.Delay(TimeSpan.FromSeconds(0.5)).Wait();
                Console.WriteLine("Task2 rennt besser - und tut alles");
            });

            var prod = new Subject<SomeAwesomeClass>();
            prod.Sample(TimeSpan.FromMilliseconds(100));
            prod.Subscribe(output => {
                Console.WriteLine($"received: transformed input= {output.Output}");
                if (task2.IsCompleted)
                {
                    task2.ContinueWith(l => { Console.WriteLine($"Task2 wurde genötigt weiter zu machen {l.Status}"); });
                }
            });
            
            var t = new Thread(() =>
            {
                var i = 0;
                while(true)
                {
                    Thread.Sleep(100);
                    prod.OnNext(new SomeAwesomeClass("[" + i + "] was sagtest du?"));
                    Console.WriteLine($"created: new SomeAwesomeClass({i})");
                    i++;
                }
            });

            t.Start();

            var observable1 = Observable.Interval(TimeSpan.FromSeconds(1)).Timestamp();
            observable1.Subscribe(x => Console.WriteLine("{0}: {1}", x.Value, x.Timestamp));

            var task = Task.Run(() => {
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                Console.WriteLine("Task1 rennt irgendwie - tut aber nichts");
            });

            var obsFilter1 = from ts in observable1
                             where ts.Value % 5 == 0
                             select ts;

            obsFilter1.Subscribe(filteredTs =>
            {
                Console.WriteLine($"FILTERED TS: {filteredTs}");
                if(task.IsCompleted)
                {
                    task.ContinueWith(l => { Console.WriteLine($"Task1 wurde genötigt weiter zu machen {l.Status}"); });
                }
            });

            /*
            var obsFilter2 = from ts in observable1
                             group ts by ts.Timestamp into tsGroup
                             select tsGroup;

            obsFilter2.Subscribe(filteredTs =>
            {
                Console.WriteLine($"GROUP-FILTERED TS: {filteredTs}");
            });
            */
        }
    }
}
