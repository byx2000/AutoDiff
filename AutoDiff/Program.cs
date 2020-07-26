using System;
using System.Collections.Generic;

namespace AutoDiff
{
    class Program
    {
        static void Main()
        {
            Sample.RunSample1();
            Sample.RunSample2();

            Var r = 3;
            Const pi = 3.14;
            Node s = pi * r * r;

            s.Forward();
            Console.WriteLine(s.Value);

            s.Backward();
            Console.WriteLine(r.Derivative);
            Console.WriteLine(pi.Derivative);
            

            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }
    }
}
