using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff
{
    public static class Sample
    {
        private static Random rand = new Random();

        /// <summary>
        /// 用梯度下降法求函数y=x^2的最小值
        /// </summary>
        public static void RunSample1()
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

        /// <summary>
        /// 拟合直线y=2.5x-3.7
        /// </summary>
        public static void RunSample2()
        {
            Console.WriteLine("Sample 2:");

            // 构造训练数据：y=2.5x-3.7
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            for (double i = -5; i <= 5; i += 0.1)
            {
                x.Add(i);
                y.Add(2.5 * i - 3.7);
            }

            Expr loss = 0;
            Var k = rand.NextDouble();
            Var b = rand.NextDouble();
            for (int i = 0; i < x.Count; ++i)
            {
                Expr delta = k * x[i] + b - y[i];
                loss += delta * delta;
            }

            double rate = 0.001;
            int epoch = 100;
            for (int i = 0; i < epoch; ++i)
            {
                loss.Forward();
                Console.WriteLine("y=(" + k.Value + ")x+(" + b.Value + ")\tloss=" + loss.Value);
                loss.Backward();
                k.Value -= rate * k.Derivative;
                b.Value -= rate * b.Derivative;
            }
        }
    }
}
