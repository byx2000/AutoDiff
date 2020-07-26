using System;
using System.Collections.Generic;

namespace AutoDiff
{
    class Program
    {
        static Random rand = new Random();

        static void Main()
        {
            // 用梯度下降法求函数y = x * x的最小值 (0, 0)

            Node x = (rand.NextDouble() * 2 - 1) * 1000;
            Node y = x * x;

            double rate = 0.1;
            for (int i = 0; i < 100; ++i)
            {
                y.Forward();
                Console.WriteLine("x = " + x.Value + "\ty = " + y.Value);
                y.Backward();
                x.Value -= rate * x.Derivative;
            }

            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }
    }
}
