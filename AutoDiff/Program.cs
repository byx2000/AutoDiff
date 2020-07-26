using System;
using System.Collections.Generic;

namespace AutoDiff
{
    class Program
    {
        static Random rand = new Random();

        static void Main()
        {
            Sample.RunSample1();
            Sample.RunSample2();

            //Var x = 12;
            //x.Value = -1;

            //Var y = x + 1;
            //y.Value = 12;

            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }
    }
}
