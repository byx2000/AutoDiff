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
        /// x = 10
        /// y = 20
        /// z = 30
        /// r = (x + y) * (y + z)
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
        /// x = 5
        /// y = x + x + x
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
        /// x = 5
        /// y = x * x * x
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
        /// x = 1
        /// y = 2
        /// r = (x + y) * (x + y)
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

        /// <summary>
        /// x = 12
        /// y = (x + 1) * (2 * x + 5)
        /// </summary>
        [TestMethod]
        public void Case5()
        {
            Node x = new Var(12);
            Node y = (x + 1) * (2 * x + 5);

            y.Propagate();

            Assert.IsTrue(x.Value == 12);
            Assert.IsTrue(y.Value == 377);
        }

        /// <summary>
        /// x = 1
        /// y = 2
        /// u = x + y
        /// v = x * y
        /// r = u * v + v * u
        /// </summary>
        [TestMethod]
        public void Case6()
        {
            Node x = new Var(1);
            Node y = new Var(2);
            Node u = x + y;
            Node v = x * y;
            Node r = u * v + v * u;

            r.Propagate();

            Assert.IsTrue(x.Value == 1);
            Assert.IsTrue(y.Value == 2);
            Assert.IsTrue(u.Value == 3);
            Assert.IsTrue(v.Value == 2);
            Assert.IsTrue(r.Value == 12);
        }

        /// <summary>
        /// x = 1
        /// y = x + x
        /// z = y + y
        /// r = y + z
        /// </summary>
        [TestMethod]
        public void Case7()
        {
            Node x = 1;
            Node y = x + x;
            Node z = y + y;
            Node r = y + z;

            r.Propagate();

            Assert.IsTrue(x.Value == 1);
            Assert.IsTrue(y.Value == 2);
            Assert.IsTrue(z.Value == 4);
            Assert.IsTrue(r.Value == 6);
        }
    }
}
