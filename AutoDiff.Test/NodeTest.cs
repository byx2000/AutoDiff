using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoDiff.Test
{
    [TestClass]
    public class NodeTest
    {
        /// <summary>
        /// 测试计算图构建
        /// <para>
        /// r = (x + y) * (y + z)
        /// </para>
        /// </summary>
        [TestMethod]
        public void GraphBuild1()
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
        /// 测试计算图构建
        /// <para>
        /// y = x + x + x
        /// </para>
        /// </summary>
        [TestMethod]
        public void GraphBuild2()
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
        /// 测试前向传播
        /// <para>
        /// r = (x + y) * (y + z), (x, y, z) = (10, 20, 30)
        /// </para>
        /// </summary>
        [TestMethod]
        public void Propagate1()
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
        /// 测试前向传播
        /// <para>
        /// y = x + x + x, x = 5
        /// </para>
        /// </summary>
        [TestMethod]
        public void Propagate2()
        {
            Node x = new Var(5);
            Node y = x + x + x;

            y.Propagate();

            Assert.IsTrue(x.Value == 5);
            Assert.IsTrue(y.Value == 15);
        }

        /// <summary>
        /// 测试反向传播
        /// <para>
        /// r = (x + y) * (y + z), (x, y, z) = (10, 20, 30)
        /// </para>
        /// </summary>
        [TestMethod]
        public void BackPropagate1()
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
        /// 测试反向传播
        /// <para>
        /// r = (x + y) * (y + z), (x, y, z) = (10, 20, 30)
        /// </para>
        /// </summary>
        [TestMethod]
        public void BackPropagate2()
        {
            Node x = new Var(5);
            Node y = x + x + x;

            y.Propagate();
            y.BackPropagate();

            Assert.IsTrue(y.Derivative == 1);
            Assert.IsTrue(x.Derivative == 3);
        }
    }
}
