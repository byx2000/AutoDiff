using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoDiff.Test
{
    /// <summary>
    /// 测试计算图构建
    /// </summary>
    [TestClass]
    public class GraphTest
    {
        /// <summary>
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

            Assert.IsTrue(x.Children.Count == 0);
            Assert.IsTrue(x.Parents.Count == 1);
            Assert.IsTrue(x.Parents[0] == v);

            Assert.IsTrue(y.Children.Count == 0);
            Assert.IsTrue(y.Parents.Count == 2);
            Assert.IsTrue(y.Parents[0] == v);
            Assert.IsTrue(y.Parents[1] == w);

            Assert.IsTrue(z.Children.Count == 0);
            Assert.IsTrue(z.Parents.Count == 1);
            Assert.IsTrue(z.Parents[0] == w);

            Assert.IsTrue(v.Children.Count == 2);
            Assert.IsTrue(v.Children[0] == x);
            Assert.IsTrue(v.Children[1] == y);
            Assert.IsTrue(v.Parents.Count == 1);
            Assert.IsTrue(v.Parents[0] == r);

            Assert.IsTrue(w.Children.Count == 2);
            Assert.IsTrue(w.Children[0] == y);
            Assert.IsTrue(w.Children[1] == z);
            Assert.IsTrue(w.Parents.Count == 1);
            Assert.IsTrue(w.Parents[0] == r);

            Assert.IsTrue(r.Children.Count == 2);
            Assert.IsTrue(r.Children[0] == v);
            Assert.IsTrue(r.Children[1] == w);
            Assert.IsTrue(r.Parents.Count == 0);
        }

        /// <summary>
        /// y = x + x + x
        /// </summary>
        [TestMethod]
        public void Case2()
        {
            Node x = new Var(5);
            Node y = x + x + x;

            Assert.IsTrue(x.Children.Count == 0);
            Assert.IsTrue(x.Parents.Count == 3);
            Assert.IsTrue(x.Parents[0] == x.Parents[1]);
            Assert.IsTrue(x.Parents[0] != y);
            Assert.IsTrue(x.Parents[2] == y);

            Assert.IsTrue(y.Children.Count == 2);
            Assert.IsTrue(y.Children[0] == x.Parents[0]);
            Assert.IsTrue(y.Children[1] == x);
            Assert.IsTrue(y.Parents.Count == 0);
        }

        /// <summary>
        /// y = x * x * x
        /// </summary>
        [TestMethod]
        public void Case3()
        {
            Node x = new Var(5);
            Node y = x * x * x;

            Assert.IsTrue(x.Children.Count == 0);
            Assert.IsTrue(x.Parents.Count == 3);
            Assert.IsTrue(x.Parents[0] == x.Parents[1]);
            Assert.IsTrue(x.Parents[0] != y);
            Assert.IsTrue(x.Parents[2] == y);

            Assert.IsTrue(y.Children.Count == 2);
            Assert.IsTrue(y.Children[0] == x.Parents[0]);
            Assert.IsTrue(y.Children[1] == x);
            Assert.IsTrue(y.Parents.Count == 0);
        }

        /// <summary>
        /// r = (x + y) * (x + y)
        /// </summary>
        [TestMethod]
        public void Case4()
        {
            Node x = new Var(1);
            Node y = new Var(2);
            Node u = x + y;
            Node r = u * u;

            Assert.IsTrue(x.Children.Count == 0);
            Assert.IsTrue(x.Parents.Count == 1);
            Assert.IsTrue(x.Parents[0] == u);

            Assert.IsTrue(y.Children.Count == 0);
            Assert.IsTrue(y.Parents.Count == 1);
            Assert.IsTrue(y.Parents[0] == u);

            Assert.IsTrue(u.Children.Count == 2);
            Assert.IsTrue(u.Children[0] == x);
            Assert.IsTrue(u.Children[1] == y);
            Assert.IsTrue(u.Parents.Count == 2);
            Assert.IsTrue(u.Parents[0] == r);
            Assert.IsTrue(u.Parents[1] == r);

            Assert.IsTrue(r.Children.Count == 2);
            Assert.IsTrue(r.Children[0] == u);
            Assert.IsTrue(r.Children[1] == u);
            Assert.IsTrue(r.Parents.Count == 0);
        }

        /// <summary>
        /// y = (x + 1) * (2 * x + 5)
        /// </summary>
        [TestMethod]
        public void Case5()
        {
            Node x = new Var(12);
            Node y = (x + 1) * (2 * x + 5);

            Assert.IsTrue(x.Children.Count == 0);
            Assert.IsTrue(x.Parents.Count == 2);
            Assert.IsTrue(x.Parents[0].Parents[0] == y);
            Assert.IsTrue(x.Parents[1].Parents[0].Parents[0] == y);

            Assert.IsTrue(y.Children.Count == 2);
            Assert.IsTrue(y.Parents.Count == 0);
        }

        /// <summary>
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

            Assert.IsTrue(x.Children.Count == 0);
            Assert.IsTrue(x.Parents.Count == 2);
            Assert.IsTrue(x.Parents[0] == u);
            Assert.IsTrue(x.Parents[1] == v);

            Assert.IsTrue(y.Children.Count == 0);
            Assert.IsTrue(y.Parents.Count == 2);
            Assert.IsTrue(y.Parents[0] == u);
            Assert.IsTrue(y.Parents[1] == v);

            Assert.IsTrue(u.Children.Count == 2);
            Assert.IsTrue(u.Children[0] == x);
            Assert.IsTrue(u.Children[1] == y);
            Assert.IsTrue(u.Parents.Count == 2);
            Assert.IsTrue(u.Parents[0] == v.Parents[0]);
            Assert.IsTrue(u.Parents[1] == v.Parents[1]);
            Assert.IsTrue(u.Parents[0].Parents[0] == r);
            Assert.IsTrue(u.Parents[1].Parents[0] == r);

            Assert.IsTrue(v.Children.Count == 2);
            Assert.IsTrue(v.Children[0] == x);
            Assert.IsTrue(v.Children[1] == y);
            Assert.IsTrue(u.Parents.Count == 2);
            Assert.IsTrue(v.Parents[0] == u.Parents[0]);
            Assert.IsTrue(v.Parents[1] == u.Parents[1]);
            Assert.IsTrue(v.Parents[0].Parents[0] == r);
            Assert.IsTrue(v.Parents[1].Parents[0] == r);

            Assert.IsTrue(r.Children.Count == 2);
        }

        /// <summary>
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

            Assert.IsTrue(y.Children.Count == 2);
            Assert.IsTrue(y.Parents.Count == 3);

            Assert.IsTrue(z.Children.Count == 2);
            Assert.IsTrue(z.Parents.Count == 1);

            Assert.IsTrue(r.Children.Count == 2);
            Assert.IsTrue(r.Parents.Count == 0);
        }
    }
}
