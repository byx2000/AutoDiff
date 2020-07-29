using AutoDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    /// <summary>
    /// 用梯度下降法求函数y=x^2的最小值
    /// </summary>
    static class Sample1
    {
        private static readonly Random rand = new Random();

        public static void Run()
        {
            Console.WriteLine("Sample 1:");

            Var x = (rand.NextDouble() * 2 - 1) * 1000;
            Expr y = x * x;

            double rate = 0.1;
            int epoch = 100;
            for (int i = 0; i < epoch; ++i)
            {
                y.Forward();
                Console.WriteLine("x = " + x.Value + "\ty = " + y.Value);
                y.Backward();
                x.Value -= rate * x.Derivative;
            }
        }
    }
}
