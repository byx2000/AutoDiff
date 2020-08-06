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
        [TestMethod]
        public void Case1()
        {
            Var x = 10;
            Var y = 20;
            Var z = 30;
            Term v = x + y;
            Term w = y + z;
            Term r = v * w;

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

        [TestMethod]
        public void Case2()
        {
            Var x = 5;
            Term y = x + x + x;

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

        [TestMethod]
        public void Case3()
        {
            Var x = 5;
            Term y = x * x * x;

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

        [TestMethod]
        public void Case4()
        {
            Var x = 1;
            Var y = 2;
            Term u = x + y;
            Term r = u * u;

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

        [TestMethod]
        public void Case5()
        {
            Var x = 12;
            Term y = (x + 1) * (2 * x + 5);

            Assert.IsTrue(x.Children.Count == 0);
            Assert.IsTrue(x.Parents.Count == 2);
            Assert.IsTrue(x.Parents[0].Parents[0] == y);
            Assert.IsTrue(x.Parents[1].Parents[0].Parents[0] == y);

            Assert.IsTrue(y.Children.Count == 2);
            Assert.IsTrue(y.Parents.Count == 0);
        }

        [TestMethod]
        public void Case6()
        {
            Var x = 1;
            Var y = 2;
            Term u = x + y;
            Term v = x * y;
            Term r = u * v + v * u;

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

        [TestMethod]
        public void Case7()
        {
            Var x = 1;
            Term y = x + x;
            Term z = y + y;
            Term r = y + z;

            Assert.IsTrue(y.Children.Count == 2);
            Assert.IsTrue(y.Parents.Count == 3);

            Assert.IsTrue(z.Children.Count == 2);
            Assert.IsTrue(z.Parents.Count == 1);

            Assert.IsTrue(r.Children.Count == 2);
            Assert.IsTrue(r.Parents.Count == 0);
        }

        [TestMethod]
        public void Case8()
        {
            Var x = 12;
            Term y = x - 1;
            Term z = 7 - x;

            Assert.IsTrue(y.Children.Count == 2);
            Assert.IsTrue(y.Children[0] == x);

            Assert.IsTrue(z.Children.Count == 2);
            Assert.IsTrue(z.Children[1] == x);
        }
    }
}
