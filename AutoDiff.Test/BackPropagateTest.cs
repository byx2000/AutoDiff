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
            r.BackPropagate();

            Assert.IsTrue(r.Derivative == 1);
            Assert.IsTrue(v.Derivative == 50);
            Assert.IsTrue(w.Derivative == 30);
            Assert.IsTrue(x.Derivative == 50);
            Assert.IsTrue(y.Derivative == 80);
            Assert.IsTrue(z.Derivative == 30);
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
            y.BackPropagate();

            Assert.IsTrue(y.Derivative == 1);
            Assert.IsTrue(x.Derivative == 3);
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
            y.BackPropagate();

            Assert.IsTrue(y.Derivative == 1);
            Assert.IsTrue(x.Derivative == 75);
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
            r.BackPropagate();

            Assert.IsTrue(r.Derivative == 1);
            Assert.IsTrue(u.Derivative == 6);
            Assert.IsTrue(x.Derivative == 6);
            Assert.IsTrue(y.Derivative == 6);
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
            y.BackPropagate();

            Assert.IsTrue(y.Derivative == 1);
            Assert.IsTrue(x.Derivative == 55);
        }

        /// <summary>
        /// x = 1
        /// y = 2
        /// u = x + y, 
        /// v = x * y, 
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
            r.BackPropagate();

            Assert.IsTrue(r.Derivative == 1);
            Assert.IsTrue(u.Derivative == 4);
            Assert.IsTrue(v.Derivative == 6);
            Assert.IsTrue(x.Derivative == 16);
            Assert.IsTrue(y.Derivative == 10);
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
            r.BackPropagate();

            Assert.IsTrue(x.Derivative == 6);
        }
    }
}
