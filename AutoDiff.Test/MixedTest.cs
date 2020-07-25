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
        /// <summary>
        /// r = (x * y + z) + (x + y) * z, (x, y, z) = (5, 4, 7)
        /// </summary>
        [TestMethod]
        public void Case1()
        {
            Node x = new Var(5);
            Node y = new Var(4);
            Node z = new Var(7);
            Node r = (x * y + z) + (x + y) * z;

            r.Propagate();
            Assert.IsTrue(r.Value == (5 * 4 + 7) + (5 + 4) * 7);

            r.BackPropagate();
            Assert.IsTrue(x.Derivative == 11);
            Assert.IsTrue(y.Derivative == 12);
            Assert.IsTrue(z.Derivative == 10);
        }

        /// <summary>
        /// r = (x * y) * (x * y) + (x * y) * (x * y), (x, y) = (23, 77)
        /// </summary>
        [TestMethod]
        public void Case2()
        {
            Node x = new Var(23);
            Node y = new Var(77);
            Node u = x * y;
            Node v = u * u;
            Node r = v + v;

            r.Propagate();
            Assert.IsTrue(r.Value == 6272882);

            r.BackPropagate();
            Assert.IsTrue(x.Derivative == 545468);
            Assert.IsTrue(y.Derivative == 162932);
        }

        /// <summary>
        /// r = (x * y + 3 * z + 6 * w) * (12 + y * z + 6 * w) + 10 * x * (y + z) * (w + 3), (x, y, z, w) = (2, -3, 11, 7)
        /// </summary>
        [TestMethod]
        public void Case3()
        {
            Node x = new Var(2);
            Node y = new Var(-3);
            Node z = new Var(11);
            Node w = new Var(7);
            Node r = (x * y + 3 * z + 6 * w) * (12 + y * z + 6 * w) + 10 * x * (y + z) * (w + 3);

            r.Propagate();
            Assert.IsTrue(r.Value == 3049);

            r.BackPropagate();
            Assert.IsTrue(x.Derivative == 737);
            Assert.IsTrue(y.Derivative == 1001);
            Assert.IsTrue(z.Derivative == 56);
            Assert.IsTrue(w.Derivative == 700);
        }

        /// <summary>
        /// u = x + y, v = x * y, r = 2 * u * v * v + 3 * u * u * v + 4, (x, y) = (12, 13)
        /// </summary>
        [TestMethod]
        public void Case4()
        {
            Node x = new Var(12);
            Node y = new Var(13);
            Node u = x + y;
            Node v = x * y;
            Node r = 2 * u * v * v + 3 * u * u * v + 4;

            r.Propagate();
            Assert.IsTrue(u.Value == 25);
            Assert.IsTrue(v.Value == 156);
            Assert.IsTrue(r.Value == 1509304);

            r.BackPropagate();
            Assert.IsTrue(u.Derivative == 72072);
            Assert.IsTrue(v.Derivative == 17475);
            Assert.IsTrue(x.Derivative == 299247);
            Assert.IsTrue(y.Derivative == 281772);
        }

        /// <summary>
        /// u = x + y
        /// v = x * y
        /// a = 3 * u + 2 * v
        /// b = 7 * u * v
        /// r = x + y + u + v + a + b + x * y * u * v * a * b
        /// (x, y) = (-3, 2)
        /// </summary>
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

            r.Propagate();
            Assert.IsTrue(r.Value == 22699);

            r.BackPropagate();
            Assert.IsTrue(x.Derivative == -78669);
            Assert.IsTrue(y.Derivative == -6829);
        }

        /// <summary>
        /// x = 3
        /// y = (-3) * x
        /// </summary>
        [TestMethod]
        public void Case6()
        {
            Node x = 3;
            Node y = (-3) * x;

            y.Propagate();
            Assert.IsTrue(y.Value == -9);

            y.BackPropagate();
            Assert.IsTrue(x.Derivative == -3);
        }

        /// <summary>
        /// x = 3
        /// y = 155
        /// </summary>
        [TestMethod]
        public void Case7()
        {
            Node x = 3;
            Node y = 155;

            y.Propagate();
            Assert.IsTrue(y.Value == 155);

            y.BackPropagate();
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

            r.Propagate();
            Assert.IsTrue(r.Value == 31672);

            r.BackPropagate();
            Assert.IsTrue(x.Derivative == 906);
            Assert.IsTrue(y.Derivative == -7288);
            Assert.IsTrue(z.Derivative == -20139);
        }
    }
}
