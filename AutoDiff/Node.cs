using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AutoDiff
{
    /// <summary>
    /// 计算节点基类
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// 子节点
        /// </summary>
        public List<Node> Children { get; set; } = new List<Node>();

        /// <summary>
        /// 父节点
        /// </summary>
        public List<Node> Parents { get; set; } = new List<Node>();

        /// <summary>
        /// 计算值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 导数值
        /// </summary>
        public double Derivative { get; set; }

        public abstract double Eval(List<double> input);
        public abstract List<double> Diff(List<double> input);

        /// <summary>
        /// 正向传播
        /// </summary>
        public void Propagate()
        {
            List<double> p = new List<double>();
            foreach (Node c in Children)
            {
                c.Propagate();
                p.Add(c.Value);
            }
            Value = Eval(p);
        }

        /// <summary>
        /// 反向传播
        /// </summary>
        public void BackPropagate()
        {
            BackPropagate(new Hashtable());
        }

        private void BackPropagate(Hashtable book)
        {
            if (book.Contains(this))
            {
                return;
            }

            book[this] = 1;

            if (Children.Count <= 0)
            {
                return;
            }

            if (Parents.Count <= 0)
            {
                Derivative = 1;
            }

            List<double> input = new List<double>();
            foreach (Node c in Children)
            {
                input.Add(c.Value);
            }
            List<double> localDerivatives = Diff(input);

            for (int i = 0; i < localDerivatives.Count; ++i)
            {
                Children[i].Derivative += localDerivatives[i] * Derivative;
            }

            foreach (Node c in Children)
            {
                c.BackPropagate(book);
            }
        }

        protected void AddChild(Node n)
        {
            Children.Add(n);
            n.Parents.Add(this);
        }

        public static Node operator +(Node lhs, Node rhs)
        {
            return new Add(lhs, rhs);
        }

        public static Node operator *(Node lhs, Node rhs)
        {
            return new Mul(lhs, rhs);
        }
    }

    /// <summary>
    /// 变量节点
    /// </summary>
    public class Var : Node
    {
        public Var(double value)
        {
            Value = value;
        }

        public override double Eval(List<double> input)
        {
            return Value;
        }

        public override List<double> Diff(List<double> input)
        {
            return new List<double> { 1 };
        }
    }

    /// <summary>
    /// 加法节点
    /// </summary>
    public class Add : Node
    {
        public Add(Node lhs, Node rhs)
        {
            AddChild(lhs);
            AddChild(rhs);
        }

        public override double Eval(List<double> input)
        {
            double res = 0;
            foreach (double i in input)
            {
                res += i;
            }
            return res;
        }

        public override List<double> Diff(List<double> input)
        {
            List<double> res = new List<double>();
            for (int i = 0; i < input.Count; ++i)
            {
                res.Add(1);
            }
            return res;
        }
    }

    /// <summary>
    /// 乘法节点
    /// </summary>
    public class Mul : Node
    {
        public Mul(Node lhs, Node rhs)
        {
            AddChild(lhs);
            AddChild(rhs);
        }

        public override double Eval(List<double> input)
        {
            double res = 1;
            foreach (double i in input)
            {
                res *= i;
            }

            return res;
        }

        public override List<double> Diff(List<double> input)
        {
            double m = 1;
            foreach (double i in input)
            {
                m *= i;
            }
            List<double> res = new List<double>();
            foreach (double i in input)
            {
                res.Add(m / i);
            }
            return res;
        }
    }
}
