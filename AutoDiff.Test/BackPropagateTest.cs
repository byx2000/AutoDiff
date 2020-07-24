using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoDiff.Test
{
    /// <summary>
    /// 测试反向传播
    /// </summary>
    [TestClass]
    public class BackPropagateTest
    {
        /// <summary>
        /// r = (x + y) * (y + z), (x, y, z) = (10, 20, 30)
        /// </summary>
        [TestMethod]
        public void Case1()
        {
            Node x = new Var(10);
            Node y = new Var(20);
            Node z = new Var(30);
            Node v = x + y;
            Node w = y + z;
            Node r = v * w;

            r.Propagate();
            r.BackPropagate();

            Assert.IsTrue(r.Derivative == 1);
            Assert.IsTrue(v.Derivative == 50);
            Assert.IsTrue(w.Derivative == 30);
            Assert.IsTrue(x.Derivative == 50);
            Assert.IsTrue(y.Derivative == 80);
            Assert.IsTrue(z.Derivative == 30);
        }

        /// <summary>
        /// y = x + x + x, x = 5
        /// </summary>
        [TestMethod]
        public void Case2()
        {
            Node x = new Var(5);
            Node y = x + x + x;

            y.Propagate();
            y.BackPropagate();

            Assert.IsTrue(y.Derivative == 1);
            Assert.IsTrue(x.Derivative == 3);
        }

        /// <summary>
        /// y = x * x * x, x = 5
        /// </summary>
        [TestMethod]
        public void Case3()
        {
            Node x = new Var(5);
            Node y = x * x * x;

            y.Propagate();
            y.BackPropagate();

            Assert.IsTrue(y.Derivative == 1);
            Assert.IsTrue(x.Derivative == 75);
        }

        /// <summary>
        /// r = (x + y) * (x + y), (x, y) = (1, 2)
        /// </summary>
        [TestMethod]
        public void Case4()
        {
            Node x = new Var(1);
            Node y = new Var(2);
            Node u = x + y;
            Node r = u * u;

            r.Propagate();
            r.BackPropagate();

            Assert.IsTrue(r.Derivative == 1);
            Assert.IsTrue(u.Derivative == 6);
            Assert.IsTrue(x.Derivative == 6);
            Assert.IsTrue(y.Derivative == 6);
        }
    }
}
