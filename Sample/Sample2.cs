using AutoDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    /// <summary>
    /// 拟合直线y=2.5x-3.7
    /// </summary>
    static class Sample2
    {
        private static readonly Random rand = new Random();

        public static void Run()
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
            Var k = (rand.NextDouble() * 2 - 1) * rand.Next(100);
            Var b = (rand.NextDouble() * 2 - 1) * rand.Next(100);
            for (int i = 0; i < x.Count; ++i)
            {
                Expr delta = k * x[i] + b - y[i];
                loss += delta * delta;
            }

            double rate = 1;
            int epoch = 100;

            loss.Forward();
            double lastLoss = loss.Value;
            double lastK = k.Value;
            double lastB = b.Value;

            for (int i = 0; i < epoch; ++i)
            {

                Console.WriteLine("y=(" + k.Value + ")x+(" + b.Value + ")\tloss=" + loss.Value);
                loss.Backward();

                k.Value -= rate * k.Derivative;
                b.Value -= rate * b.Derivative;
                loss.Forward();

                while (loss.Value > lastLoss)
                {
                    k.Value = lastK;
                    b.Value = lastB;
                    rate /= 2;
                    k.Value -= rate * k.Derivative;
                    b.Value -= rate * b.Derivative;
                    loss.Forward();
                }

                lastLoss = loss.Value;
                lastK = k.Value;
                lastB = b.Value;
            }
        }
    }
}
