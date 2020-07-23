using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoDiff.Test
{
    [TestClass]
    public class NodeTest
    {
        /// <summary>
        /// 测试计算图构建是否正确：r = (x + y) * (y + z)
        /// </summary>
        [TestMethod]
        public void GraphBuildTest1()
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
    }
}
