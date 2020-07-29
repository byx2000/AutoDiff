using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff.Sample
{
    /// <summary>
    /// 自动调整学习率
    /// </summary>
    static class Sample3
    {
        private static readonly Random rand = new Random();

        // 求最小值
        private static void Minimum(List<Var> x, Expr y)
        {
            List<double> lastX = new List<double>();
            foreach (Var v in x)
            {
                double r = rand.NextDouble() * 2 - 1;
                v.Value = r;
                lastX.Add(r);
            }

            y.Forward();
            double lastY = y.Value;

            double rate = 0.1;
            int maxEpoch = 1000;
            int cnt = 0;

            for (int i = 0; i < maxEpoch; ++i)
            {
                if (cnt > 100)
                {
                    break;
                }

                Console.WriteLine("epoch " + i + ": " + lastY + "\trate: " + rate);

                y.Backward();
                foreach (Var v in x)
                {
                    v.Value -= rate * v.Derivative;
                }

                y.Forward();
                if (y.Value > lastY || Math.Abs(y.Value - lastY) <= 1e-6)
                {
                    for (int j = 0; j < x.Count; ++j)
                    {
                        x[j].Value = lastX[j];
                    }
                    rate /= 2;
                    cnt++;
                }
                else
                {
                    lastY = y.Value;
                    for (int j = 0; j < x.Count; ++j)
                    {
                        lastX[j] = x[j].Value;
                    }

                    if (cnt == 0)
                    {
                        rate *= 2;
                    }

                    cnt = 0;
                }
            }

            Console.WriteLine("(" + x[0].Value + ")x^2+(" + x[1].Value + ")x+(" + x[2].Value + ")");
        }

        public static void Run()
        {
            Console.WriteLine("Sample 3:");

            // 1
            //Var x = new Var();
            //Minimum(new List<Var> { x }, ((x * x - 8 * x + 20) ^ 0.5) + ((x * x + 1) ^ 0.5));

            // 2
            //List<double> x = new List<double>();
            //List<double> y = new List<double>();

            //for (double d = -10; d <= 10; d += 0.1)
            //{
            //    x.Add(d);
            //    y.Add(1.2 * d - 5.3);
            //}

            //Var k = new Var(), b = new Var();
            //Expr loss = 0;

            //for (int i = 0; i < x.Count; ++i)
            //{
            //    Expr delta = k * x[i] + b - y[i];
            //    loss += delta * delta;
            //}

            //Minimum(new List<Var> { k, b }, loss);


            // 3
            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (double d = -5; d <= 5; d += 0.1)
            {
                x.Add(d);
                y.Add(1.5 * d * d - 3.7 * d + 2);
            }

            Var a = new Var(), b = new Var(), c = new Var();
            Expr loss = 0;

            for (int i = 0; i < x.Count; ++i)
            {
                Expr delta = a * x[i] * x[i] + b * x[i] + c - y[i];
                loss += delta * delta;
            }

            Minimum(new List<Var> { a, b, c }, loss);
        }
    }
}
