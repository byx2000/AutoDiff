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
            Node x = new Var(5);
            Node y = new Var(4);
            Node z = new Var(7);
            Node r = (x * y + z) + (x + y) * z;

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
            Node x = new Var(23);
            Node y = new Var(77);
            Node u = x * y;
            Node v = u * u;
            Node r = v + v;

            r.Forward();
            Assert.IsTrue(r.Value == 6272882);

            r.Backward();
            Assert.IsTrue(x.Derivative == 545468);
            Assert.IsTrue(y.Derivative == 162932);
        }

        [TestMethod]
        public void Case3()
        {
            Node x = new Var(2);
            Node y = new Var(-3);
            Node z = new Var(11);
            Node w = new Var(7);
            Node r = (x * y + 3 * z + 6 * w) * (12 + y * z + 6 * w) + 10 * x * (y + z) * (w + 3);

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
            Node x = new Var(12);
            Node y = new Var(13);
            Node u = x + y;
            Node v = x * y;
            Node r = 2 * u * v * v + 3 * u * u * v + 4;

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
            Node x = -3;
            Node y = 2;
            Node u = x + y;
            Node v = x * y;
            Node a = 3 * u + 2 * v;
            Node b = 7 * u * v;
            Node r = x + y + u + v + a + b + x * y * u * v * a * b;

            r.Forward();
            Assert.IsTrue(r.Value == 22699);

            r.Backward();
            Assert.IsTrue(x.Derivative == -78669);
            Assert.IsTrue(y.Derivative == -6829);
        }

        [TestMethod]
        public void Case6()
        {
            Node x = 3;
            Node y = (-3) * x;

            y.Forward();
            Assert.IsTrue(y.Value == -9);

            y.Backward();
            Assert.IsTrue(x.Derivative == -3);
        }

        [TestMethod]
        public void Case7()
        {
            Node x = 3;
            Node y = 155;

            y.Forward();
            Assert.IsTrue(y.Value == 155);

            y.Backward();
            Assert.IsTrue(y.Derivative == 1);
            Assert.IsTrue(x.Derivative == 0);
        }

        [TestMethod]
        public void Case8()
        {
            Node x = 2;
            Node y = 7;
            Node z = -11;
            Node u = x + y + z;
            Node v = x * y + y * z + x * z;
            Node w = x * y * z + (-7);
            Node r = 2 * x + 7 * (u * v + w) + (-16) * (v + w) + w * u + (-1) * u * v * w + 3 * y + 4 * z;

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
            Node x = 17;
            Node y = -5;
            Node r = x * x + y * y - 2 * x * y;

            r.Forward();
            Assert.IsTrue(r.Value == 484);

            r.Backward();
            Assert.IsTrue(x.Derivative == 44);
            Assert.IsTrue(y.Derivative == -44);
        }

        [TestMethod]
        public void Case10()
        {
            Node x = 5;
            Node y = 2;
            Node r = x - (3 * x - 2 * y) * (x + y - 55) + 7 - 66;

            r.Forward();
            Assert.IsTrue(r.Value == 474);

            r.Backward();
            Assert.IsTrue(x.Derivative == 134);
            Assert.IsTrue(y.Derivative == -107);
        }

        [TestMethod]
        public void Case11()
        {
            Node x = -3;
            Node y = 7;
            Node z = 11;
            Node r = x - y - z;

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
            Node x = -3;
            Node y = 7;
            Node z = 11;
            Node r = x - y + z;

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
            Node x, y, u, v, a, b, r;
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
            Node x, y, u, r;
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
            Node x = 12;
            Node y = x - 1;
            Node z = 7 - x;

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
            Node x = 12;
            Node y = 5;
            Node r = y;

            r.Forward();
            Assert.IsTrue(r.Value == 5);

            r.Backward();
            Assert.IsTrue(x.Derivative == 0);
            Assert.IsTrue(y.Derivative == 1);
        }

        [TestMethod]
        public void Case17()
        {
            Node x = 10;
            Node y = 7 + x - x;

            y.Forward();
            Assert.IsTrue(y.Value == 7);

            y.Backward();
            Assert.IsTrue(x.Derivative == 0);
        }

        [TestMethod]
        public void Case18()
        {
            Node x = 1;
            Node y = x + x;
            Node z = y + y;
            Node r = y + z;

            r.Forward();
            Assert.IsTrue(r.Value == 6);

            r.Backward();
            Assert.IsTrue(x.Derivative == 6);
        }

        [TestMethod]
        public void Case19()
        {
            Node x = 5;
            Node y = x + x + x;

            y.Forward();
            Assert.IsTrue(y.Value == 15);

            y.Backward();
            Assert.IsTrue(x.Derivative == 3);
        }

        [TestMethod]
        public void Case20()
        {
            Node x = 5;
            Node y = 2 * x + 3 * x + 5 * x;

            y.Forward();
            Assert.IsTrue(y.Value == 50);

            y.Backward();
            Assert.IsTrue(x.Derivative == 10);
        }

        [TestMethod]
        public void Case21()
        {
            Node x1, y1, x2, y2, k, b, d1, d2, loss;
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
            Node x1, y1, x2, y2, x3, y3, a, b, c, d1, d2, d3, loss;
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
            Node x = 2;
            Node y = x * x;
            Node z = y + y;
            Node r = x - y - z;

            r.Forward();
            Assert.IsTrue(r.Value == -10);

            r.Backward();
            Assert.IsTrue(x.Derivative == -11);
        }

        [TestMethod]
        public void Case24()
        {
            Node x = 1, y = 2, z = 3;
            Node u = y + z;
            Node v = u + x;
            Node w = u + v;
            Node r = v + w;

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
            Node x = 1, y = 2, z = 3;
            Node r = x - (y - z);

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
            Node x = 3;
            Node y = 1 / x;

            y.Forward();
            Assert.IsTrue(y.Value == 1.0 / 3);

            y.Backward();
            Assert.IsTrue(x.Derivative == -1.0 / 9);
        }

        [TestMethod]
        public void Case27()
        {
            Node x = 3;
            Node y = x / 2;

            y.Forward();
            Assert.IsTrue(y.Value == 3 / 2.0);

            y.Backward();
            Assert.IsTrue(x.Derivative == 1 / 2.0);
        }

        [TestMethod]
        public void Case28()
        {
            Node x = 37, y = 12;
            Node r = x / y;

            r.Forward();
            Assert.IsTrue(r.Value == 37.0 / 12);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1.0 / 12);
            Assert.IsTrue(y.Derivative == -37.0 / (12 * 12));
        }

        [TestMethod]
        public void Case29()
        {
            Node x = 3, y = -4;
            Node r = (x + y) / (x - y);

            r.Forward();
            Assert.IsTrue(r.Value == -1 / 7.0);

            r.Backward();
            Assert.IsTrue(x.Derivative == 8 / 49.0);
            Assert.IsTrue(y.Derivative == 6 / 49.0);
        }

        [TestMethod]
        public void Case30()
        {
            Node x = 3, y = -4;
            Node r = 1 / (1 + x * y) - (x * x + y * y) / (2 * x * y);

            r.Forward();
            Assert.IsTrue(r.Value == 251 / 264.0);

            r.Backward();
            Assert.AreEqual(x.Derivative, -559 / 8712.0, epsilon);
            Assert.AreEqual(y.Derivative, -1135 / 11616.0, epsilon);
        }

        [TestMethod]
        public void Case31()
        {
            Node x = 3, y = -4, z = 2;
            Node r = x / y / z;

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
            Node x = 3, y = -4, z = 2;
            Node r = x / (y / z);

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
            Node x = 3, y = 2;
            Node r = x / y * y;

            r.Forward();
            Assert.IsTrue(r.Value == 3);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1);
            Assert.IsTrue(y.Derivative == 0);
        }

        [TestMethod]
        public void Case34()
        {
            Node x = 3, y = 2;
            Node r = x * y / y;

            r.Forward();
            Assert.IsTrue(r.Value == 3);

            r.Backward();
            Assert.IsTrue(x.Derivative == 1);
            Assert.IsTrue(y.Derivative == 0);
        }

        [TestMethod]
        public void Case35()
        {
            Node x, y, u, v, r;
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
            Node x = 3;
            Node y = x ^ 4;

            y.Forward();
            Assert.IsTrue(y.Value == 81);

            y.Backward();
            Assert.IsTrue(x.Derivative == 108);
        }

        [TestMethod]
        public void Case37()
        {
            Node x = 3;
            Node y = 4 ^ x;

            y.Forward();
            Assert.IsTrue(y.Value == 64);

            y.Backward();
            Assert.IsTrue(x.Derivative == 64 * Math.Log(4));
        }

        [TestMethod]
        public void Case38()
        {
            Node x = 5, y = 3;
            Node r = x ^ y;

            r.Forward();
            Assert.IsTrue(r.Value == 125);

            r.Backward();
            Assert.IsTrue(x.Derivative == 75);
            Assert.IsTrue(y.Derivative == 125 * Math.Log(5));
        }

        [TestMethod]
        public void Case39()
        {
            Node x = 2, y = 3;
            Node r = (x + y) ^ (2 * x - 3 * y);

            r.Forward();
            Assert.IsTrue(r.Value == 1 / 3125.0);

            r.Backward();
            Assert.AreEqual(x.Derivative, (Math.Log(25) - 1) / 3125, epsilon);
            Assert.AreEqual(y.Derivative, (-3 * Math.Log(5) - 1) / 3125, epsilon);
        }

        [TestMethod]
        public void Case40()
        {
            Node x = 3;
            Node y = (x ^ 3) - (4 ^ x) + (x + 7) * (x + 1) / (2 * x - 3);

            y.Forward();
            Assert.AreEqual(y.Value, -71 / 3.0, epsilon);

            y.Backward();
            Assert.AreEqual(x.Derivative, 205 / 9.0 - 64 * Math.Log(4), epsilon);
        }

        [TestMethod]
        public void Case41()
        {
            Node x = 4, y = 3, z = 2;
            Node r = x ^ y ^ z;

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
            Node x = 4, y = 3, z = 2;
            Node r = x ^ (y ^ z);

            r.Forward();
            Assert.IsTrue(r.Value == 262144);

            r.Backward();
            Assert.AreEqual(x.Derivative, 589824, epsilon);
            Assert.AreEqual(y.Derivative, 1572864 * Math.Log(4), epsilon);
            Assert.AreEqual(z.Derivative, 2359296 * Math.Log(3) * Math.Log(4), epsilon);
        }
    }
}
