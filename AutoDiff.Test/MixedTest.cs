using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoDiff.Test
{
    /// <summary>
    /// 混合测试
    /// </summary>
    [TestClass]
    public class MixedTest
    {
        private const double epsilon = 1e-6;

        [TestMethod]
        public void Case1()
        {
            Var x = 5;
            Var y = 4;
            Var z = 7;
            Expr r = (x * y + z) + (x + y) * z;

            r.Forward();
            Assert.IsTrue(r.Value == (5 * 4 + 7) + (5 + 4) * 7);

            r.Backward();
            Assert.IsTrue(x.Derivative == 11);
            Assert.IsTrue(y.Derivative == 12);
            Assert.IsTrue(z.Derivative == 10);
        }

        [TestMethod]
        public void Case2()
        {
            Var x = 23;
            Var y = 77;
            Expr u = x * y;
            Expr v = u * u;
            Expr r = v + v;

            r.Forward();
            Assert.IsTrue(r.Value == 6272882);

            r.Backward();
            Assert.IsTrue(x.Derivative == 545468);
            Assert.IsTrue(y.Derivative == 162932);
        }

        [TestMethod]
        public void Case3()
        {
            Var x = 2;
            Var y = -3;
            Var z = 11;
            Var w = 7;
            Expr r = (x * y + 3 * z + 6 * w) * (12 + y * z + 6 * w) + 10 * x * (y + z) * (w + 3);

            r.Forward();
            Assert.IsTrue(r.Value == 3049);

            r.Backward();
            Assert.IsTrue(x.Derivative == 737);
            Assert.IsTrue(y.Derivative == 1001);
            Assert.IsTrue(z.Derivative == 56);
            Assert.IsTrue(w.Derivative == 700);
        }

        [TestMethod]
        public void Case4()
        {
            Var x = 12;
            Var y = 13;
            Expr u = x + y;
            Expr v = x * y;
            Expr r = 2 * u * v * v + 3 * u * u * v + 4;

            r.Forward();
            Assert.IsTrue(u.Value == 25);
            Assert.IsTrue(v.Value == 156);
            Assert.IsTrue(r.Value == 1509304);

            r.Backward();
            Assert.IsTrue(u.Derivative == 72072);
            Assert.IsTrue(v.Derivative == 17475);
            Assert.IsTrue(x.Derivative == 299247);
            Assert.IsTrue(y.Derivative == 281772);
        }

        [TestMethod]
        public void Case5()
        {
            Var x = -3;
            Var y = 2;
            Expr u = x + y;
            Expr v = x * y;
            Expr a = 3 * u + 2 * v;
            Expr b = 7 * u * v;
            Expr r = x + y + u + v + a + b + x * y * u * v * a * b;

            r.Forward();
            Assert.IsTrue(r.Value == 22699);

            r.Backward();
            Assert.IsTrue(x.Derivative == -78669);
            Assert.IsTrue(y.Derivative == -6829);
        }

        [TestMethod]
        public void Case6()
        {
            Var x = 3;
            Expr y = (-3) * x;

            y.Forward();
            Assert.IsTrue(y.Value == -9);

            y.Backward();
            Assert.IsTrue(x.Derivative == -3);
        }

        [TestMethod]
        public void Case7()
        {
            Var x = 3;
            Expr y = 155;

            y.Forward();
            Assert.IsTrue(y.Value == 155);

            y.Backward();
            Assert.IsTrue(y.Derivative == 1);
            Assert.IsTrue(x.Derivative == 0);
        }

        [TestMethod]
        public void Case8()
        {
            Var x = 2;
            Var y = 7;
            Var z = -11;
            Expr u = x + y + z;
            Expr v = x * y + y * z + x * z;
            Expr w = x * y * z + (-7);
            Expr r = 2 * x + 7 * (u * v + w) + (-16) * (v + w) + w * u + (-1) * u * v * w + 3 * y + 4 * z;

            r.Forward();
            Assert.IsTrue(r.Value == 31672);

            r.Backward();
            Assert.IsTrue(x.Derivative == 906);
            Assert.IsTrue(y.Derivative == -7288);
            Assert.IsTrue(z.Derivative == -20139);
        }

        [TestMethod]
        public void Case9()
        {
            Var x = 17;
            Var y = -5;
            Expr r = x * x + y * y - 2 * x * y;

            r.Forward();
            Assert.IsTrue(r.Value == 484);

            r.Backward();
            Assert.IsTrue(x.Derivative == 44);
            Assert.IsTrue(y.Derivative == -44);
        }

        [TestMethod]
        public void Case10()
        {
            Var x = 5;
            Var y = 2;
            Expr r = x - (3 * x - 2 * y) * (x + y - 55) + 7 - 66;

            r.Forward();
            Assert.IsTrue(r.Value == 474);

            r.Backward();
            Assert.IsTrue(x.Derivative == 134);
            Assert.IsTrue(y.Derivative == -107);
        }

        [TestMethod]
        public void Case11()
        {
            Var x = -3;
            Var y = 7;
            Var z = 11;
            Expr r = x - y - z;

            r.Forward();
            Assert.IsTrue(r.Value == -21);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1);
            Assert.IsTrue(y.Derivative == -1);
            Assert.IsTrue(z.Derivative == -1);
        }

        [TestMethod]
        public void Case12()
        {
            Var x = -3;
            Var y = 7;
            Var z = 11;
            Expr r = x - y + z;

            r.Forward();
            Assert.IsTrue(r.Value == 1);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1);
            Assert.IsTrue(y.Derivative == -1);
            Assert.IsTrue(z.Derivative == 1);
        }

        [TestMethod]
        public void Case13()
        {
            Var x, y;
            Expr u, v, a, b, r;
            x = -3;
            y = 2;
            u = x - y;
            v = x * y;
            a = 3 * u - 2 * v;
            b = 7 * (u - v);
            r = x + y - u + v + a + b - x * y * u * v * a * b;

            r.Forward();
            Assert.IsTrue(r.Value == -3778);

            r.Backward();
            Assert.IsTrue(x.Derivative == 5790);
            Assert.IsTrue(y.Derivative == -8300);
        }

        [TestMethod]
        public void Case14()
        {
            Var x, y;
            Expr u, r;
            x = 7;
            y = 13;
            u = x - y;
            r = x - u;

            r.Forward();
            Assert.IsTrue(r.Value == 13);

            r.Backward();
            Assert.IsTrue(x.Derivative == 0);
            Assert.IsTrue(y.Derivative == 1);
        }

        [TestMethod]
        public void Case15()
        {
            Var x = 12;
            Expr y = x - 1;
            Expr z = 7 - x;

            y.Forward();
            Assert.IsTrue(y.Value == 11);

            y.Backward();
            Assert.IsTrue(x.Derivative == 1);

            z.Forward();
            Assert.IsTrue(z.Value == -5);

            z.Backward();
            Assert.IsTrue(x.Derivative == -1);
        }

        [TestMethod]
        public void Case16()
        {
            Var x = 12;
            Expr y = 5;
            Expr r = y;

            r.Forward();
            Assert.IsTrue(r.Value == 5);

            r.Backward();
            Assert.IsTrue(x.Derivative == 0);
            Assert.IsTrue(y.Derivative == 1);
        }

        [TestMethod]
        public void Case17()
        {
            Var x = 10;
            Expr y = 7 + x - x;

            y.Forward();
            Assert.IsTrue(y.Value == 7);

            y.Backward();
            Assert.IsTrue(x.Derivative == 0);
        }

        [TestMethod]
        public void Case18()
        {
            Var x = 1;
            Expr y = x + x;
            Expr z = y + y;
            Expr r = y + z;

            r.Forward();
            Assert.IsTrue(r.Value == 6);

            r.Backward();
            Assert.IsTrue(x.Derivative == 6);
        }

        [TestMethod]
        public void Case19()
        {
            Var x = 5;
            Expr y = x + x + x;

            y.Forward();
            Assert.IsTrue(y.Value == 15);

            y.Backward();
            Assert.IsTrue(x.Derivative == 3);
        }

        [TestMethod]
        public void Case20()
        {
            Var x = 5;
            Expr y = 2 * x + 3 * x + 5 * x;

            y.Forward();
            Assert.IsTrue(y.Value == 50);

            y.Backward();
            Assert.IsTrue(x.Derivative == 10);
        }

        [TestMethod]
        public void Case21()
        {
            Var x1, y1, x2, y2, k, b;
            Expr d1, d2, loss;
            x1 = 5;
            y1 = 7;
            x2 = -2;
            y2 = 3;
            k = -1;
            b = 4;
            d1 = k * x1 + b - y1;
            d2 = k * x2 + b - y2;
            loss = d1 * d1 + d2 * d2;

            loss.Forward();
            Assert.IsTrue(loss.Value == 73);

            loss.Backward();
            Assert.IsTrue(k.Derivative == -92);
            Assert.IsTrue(b.Derivative == -10);
        }

        [TestMethod]
        public void Case22()
        {
            Var x1, y1, x2, y2, x3, y3, a, b, c;
            Expr d1, d2, d3, loss;
            x1 = 2;
            y1 = 7;
            x2 = 1;
            y2 = 5;
            x3 = -3;
            y3 = 5;
            a = 6;
            b = -3;
            c = -5;
            d1 = a * x1 * x1 + b * x1 + c - y1;
            d2 = a * x2 * x2 + b * x2 + c - y2;
            d3 = a * x3 * x3 + b * x3 + c - y3;
            loss = d1 * d1 + d2 * d2 + d3 * d3;

            loss.Forward();
            Assert.IsTrue(loss.Value == 2894);

            loss.Backward();
            Assert.IsTrue(a.Derivative == 988);
            Assert.IsTrue(b.Derivative == -308);
            Assert.IsTrue(c.Derivative == 104);
        }

        [TestMethod]
        public void Case23()
        {
            Var x = 2;
            Expr y = x * x;
            Expr z = y + y;
            Expr r = x - y - z;

            r.Forward();
            Assert.IsTrue(r.Value == -10);

            r.Backward();
            Assert.IsTrue(x.Derivative == -11);
        }

        [TestMethod]
        public void Case24()
        {
            Var x = 1, y = 2, z = 3;
            Expr u = y + z;
            Expr v = u + x;
            Expr w = u + v;
            Expr r = v + w;

            r.Forward();
            Assert.IsTrue(r.Value == 17);

            r.Backward();
            Assert.IsTrue(x.Derivative == 2);
            Assert.IsTrue(y.Derivative == 3);
            Assert.IsTrue(z.Derivative == 3);
        }

        [TestMethod]
        public void Case25()
        {
            Var x = 1, y = 2, z = 3;
            Expr r = x - (y - z);

            r.Forward();
            Assert.IsTrue(r.Value == 2);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1);
            Assert.IsTrue(y.Derivative == -1);
            Assert.IsTrue(z.Derivative == 1);
        }

        [TestMethod]
        public void Case26()
        {
            Var x = 3;
            Expr y = 1 / x;

            y.Forward();
            Assert.IsTrue(y.Value == 1.0 / 3);

            y.Backward();
            Assert.IsTrue(x.Derivative == -1.0 / 9);
        }

        [TestMethod]
        public void Case27()
        {
            Var x = 3;
            Expr y = x / 2;

            y.Forward();
            Assert.IsTrue(y.Value == 3 / 2.0);

            y.Backward();
            Assert.IsTrue(x.Derivative == 1 / 2.0);
        }

        [TestMethod]
        public void Case28()
        {
            Var x = 37, y = 12;
            Expr r = x / y;

            r.Forward();
            Assert.IsTrue(r.Value == 37.0 / 12);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1.0 / 12);
            Assert.IsTrue(y.Derivative == -37.0 / (12 * 12));
        }

        [TestMethod]
        public void Case29()
        {
            Var x = 3, y = -4;
            Expr r = (x + y) / (x - y);

            r.Forward();
            Assert.IsTrue(r.Value == -1 / 7.0);

            r.Backward();
            Assert.IsTrue(x.Derivative == 8 / 49.0);
            Assert.IsTrue(y.Derivative == 6 / 49.0);
        }

        [TestMethod]
        public void Case30()
        {
            Var x = 3, y = -4;
            Expr r = 1 / (1 + x * y) - (x * x + y * y) / (2 * x * y);

            r.Forward();
            Assert.IsTrue(r.Value == 251 / 264.0);

            r.Backward();
            Assert.AreEqual(x.Derivative, -559 / 8712.0, epsilon);
            Assert.AreEqual(y.Derivative, -1135 / 11616.0, epsilon);
        }

        [TestMethod]
        public void Case31()
        {
            Var x = 3, y = -4, z = 2;
            Expr r = x / y / z;

            r.Forward();
            Assert.IsTrue(r.Value == -3 / 8.0);

            r.Backward();
            Assert.IsTrue(x.Derivative == -1 / 8.0);
            Assert.IsTrue(y.Derivative == -3 / 32.0);
            Assert.IsTrue(z.Derivative == 3 / 16.0);
        }

        [TestMethod]
        public void Case32()
        {
            Var x = 3, y = -4, z = 2;
            Expr r = x / (y / z);

            r.Forward();
            Assert.IsTrue(r.Value == -3 / 2.0);

            r.Backward();
            Assert.IsTrue(x.Derivative == -1 / 2.0);
            Assert.IsTrue(y.Derivative == -3 / 8.0);
            Assert.IsTrue(z.Derivative == -3 / 4.0);
        }

        [TestMethod]
        public void Case33()
        {
            Var x = 3, y = 2;
            Expr r = x / y * y;

            r.Forward();
            Assert.IsTrue(r.Value == 3);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1);
            Assert.IsTrue(y.Derivative == 0);
        }

        [TestMethod]
        public void Case34()
        {
            Var x = 3, y = 2;
            Expr r = x * y / y;

            r.Forward();
            Assert.IsTrue(r.Value == 3);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1);
            Assert.IsTrue(y.Derivative == 0);
        }

        [TestMethod]
        public void Case35()
        {
            Var x, y;
            Expr u, v, r;
            x = 7;
            y = 3;
            u = x / y;
            v = y / x;
            r = 1 / x - (2 * u + 3 * v) / (5 * u - 7 * v + 2) + 1 / y;

            r.Forward();
            Assert.AreEqual(r.Value, -55 / 672.0, epsilon);

            r.Backward();
            Assert.AreEqual(x.Derivative, 551 / 12544.0, epsilon);
            Assert.AreEqual(y.Derivative, -4213 / 16128.0, epsilon);
        }

        [TestMethod]
        public void Case36()
        {
            Var x = 3;
            Expr y = x ^ 4;

            y.Forward();
            Assert.IsTrue(y.Value == 81);

            y.Backward();
            Assert.IsTrue(x.Derivative == 108);
        }

        [TestMethod]
        public void Case37()
        {
            Var x = 3;
            Expr y = 4 ^ x;

            y.Forward();
            Assert.IsTrue(y.Value == 64);

            y.Backward();
            Assert.IsTrue(x.Derivative == 64 * Math.Log(4));
        }

        [TestMethod]
        public void Case38()
        {
            Var x = 5, y = 3;
            Expr r = x ^ y;

            r.Forward();
            Assert.IsTrue(r.Value == 125);

            r.Backward();
            Assert.IsTrue(x.Derivative == 75);
            Assert.IsTrue(y.Derivative == 125 * Math.Log(5));
        }

        [TestMethod]
        public void Case39()
        {
            Var x = 2, y = 3;
            Expr r = (x + y) ^ (2 * x - 3 * y);

            r.Forward();
            Assert.IsTrue(r.Value == 1 / 3125.0);

            r.Backward();
            Assert.AreEqual(x.Derivative, (Math.Log(25) - 1) / 3125, epsilon);
            Assert.AreEqual(y.Derivative, (-3 * Math.Log(5) - 1) / 3125, epsilon);
        }

        [TestMethod]
        public void Case40()
        {
            Var x = 3;
            Expr y = (x ^ 3) - (4 ^ x) + (x + 7) * (x + 1) / (2 * x - 3);

            y.Forward();
            Assert.AreEqual(y.Value, -71 / 3.0, epsilon);

            y.Backward();
            Assert.AreEqual(x.Derivative, 205 / 9.0 - 64 * Math.Log(4), epsilon);
        }

        [TestMethod]
        public void Case41()
        {
            Var x = 4, y = 3, z = 2;
            Expr r = x ^ y ^ z;

            r.Forward();
            Assert.IsTrue(r.Value == 4096);

            r.Backward();
            Assert.AreEqual(x.Derivative, 6144, epsilon);
            Assert.AreEqual(y.Derivative, 8192 * Math.Log(4), epsilon);
            Assert.AreEqual(z.Derivative, 4096 * Math.Log(64), epsilon);
        }

        [TestMethod]
        public void Case42()
        {
            Var x = 4, y = 3, z = 2;
            Expr r = x ^ (y ^ z);

            r.Forward();
            Assert.IsTrue(r.Value == 262144);

            r.Backward();
            Assert.AreEqual(x.Derivative, 589824, epsilon);
            Assert.AreEqual(y.Derivative, 1572864 * Math.Log(4), epsilon);
            Assert.AreEqual(z.Derivative, 2359296 * Math.Log(3) * Math.Log(4), epsilon);
        }

        [TestMethod]
        public void Case43()
        {
            Var r = 3;
            Const pi = 3.14;
            Expr s = pi * r * r;

            s.Forward();
            Assert.IsTrue(s.Value == 3.14 * 3 * 3);

            s.Backward();
            Assert.IsTrue(r.Derivative == 2 * 3.14 * 3);

            r.Value = 4;
            s.Forward();
            Assert.IsTrue(s.Value == 3.14 * 4 * 4);

            s.Backward();
            Assert.IsTrue(r.Derivative == 2 * 3.14 * 4);
        }

        [TestMethod]
        public void Case44()
        {
            Var x = 13;
            Expr y = -x + 3;

            y.Forward();
            Assert.IsTrue(y.Value == -10);

            y.Backward();
            Assert.IsTrue(x.Derivative == -1);
        }

        [TestMethod]
        public void Case45()
        {
            Var x = 13;
            Expr y = 2 * x * (-x);

            y.Forward();
            Assert.IsTrue(y.Value == -338);

            y.Backward();
            Assert.IsTrue(x.Derivative == -52);
        }

        [TestMethod]
        public void Case46()
        {
            Var x = 5;
            Expr u = x;
            Expr v = -x;
            Expr r = u + v;

            r.Forward();
            Assert.IsTrue(r.Value == 0);

            r.Backward();
            Assert.IsTrue(x.Derivative == 0);
        }

        [TestMethod]
        public void Case47()
        {
            Var x = -2;
            Expr y = -((x ^ 2) - 6 * x + 5);

            y.Forward();
            Assert.IsTrue(y.Value == -21);

            y.Backward();
            Assert.IsTrue(x.Derivative == 10);
        }

        [TestMethod]
        public void Case48()
        {
            Const pi = 3.14;
            Assert.IsTrue(pi.Value == 3.14);
            Var x = pi;
            Assert.IsTrue(x.Value == 3.14);
            x.Value = 12;
            Assert.IsTrue(x.Value == 12);
            Assert.IsTrue(pi.Value == 3.14);
        }

        [TestMethod]
        public void Case49()
        {
            Var x = 3;
            Expr y = x.Exp();

            y.Forward();
            Assert.IsTrue(y.Value == Math.Exp(3));

            y.Backward();
            Assert.IsTrue(x.Derivative == Math.Exp(3));
        }

        [TestMethod]
        public void Case50()
        {
            Var x = 3, y = -2;
            Expr r = (-(x.Pow(2) + y.Pow(2))).Exp();

            r.Forward();
            Assert.IsTrue(r.Value == 1 / Math.Exp(13));

            r.Backward();
            Assert.AreEqual(x.Derivative, -6 / Math.Exp(13), epsilon);
            Assert.AreEqual(y.Derivative, 4 / Math.Exp(13), epsilon);
        }

        [TestMethod]
        public void Case51()
        {
            Var x = 13;
#pragma warning disable CS1717 // 对同一变量进行了赋值
            x = x;
#pragma warning restore CS1717 // 对同一变量进行了赋值
            Expr y = x;

            y.Forward();
            Assert.IsTrue(y.Value == 13);

            y.Backward();
            Assert.IsTrue(x.Derivative == 1);
        }

        [TestMethod]
        public void Case52()
        {
            Var x = 3;
            Expr y = 2 * x + 1;
#pragma warning disable CS1717 // 对同一变量进行了赋值
            y = y;
#pragma warning restore CS1717 // 对同一变量进行了赋值

            y.Forward();
            Assert.IsTrue(y.Value == 7);

            y.Backward();
            Assert.IsTrue(x.Derivative == 2);
        }

        [TestMethod]
        public void Case53()
        {
            Var x = 3;
            Expr y = 2 * x + 1;
            y = y * 6 - x;

            y.Forward();
            Assert.IsTrue(y.Value == 39);

            y.Backward();
            Assert.IsTrue(x.Derivative == 11);
        }
    }
}
