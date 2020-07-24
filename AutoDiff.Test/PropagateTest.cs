using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoDiff.Test
{
    /// <summary>
    /// 测试前向传播
    /// </summary>
    [TestClass]
    public class PropagateTest
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

            Assert.IsTrue(x.Value == 10);
            Assert.IsTrue(y.Value == 20);
            Assert.IsTrue(z.Value == 30);
            Assert.IsTrue(v.Value == 30);
            Assert.IsTrue(w.Value == 50);
            Assert.IsTrue(r.Value == 1500);
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

            Assert.IsTrue(x.Value == 5);
            Assert.IsTrue(y.Value == 15);
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

            Assert.IsTrue(x.Value == 5);
            Assert.IsTrue(y.Value == 125);
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

            Assert.IsTrue(x.Value == 1);
            Assert.IsTrue(y.Value == 2);
            Assert.IsTrue(u.Value == 3);
            Assert.IsTrue(r.Value == 9);
        }
    }
}
