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
        /// 子节点列表
        /// </summary>
        public List<Node> Children { get; set; } = new List<Node>();

        /// <summary>
        /// 父节点列表
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

        /// <summary>
        /// 求值（子类实现）
        /// </summary>
        /// <param name="input">参数数组</param>
        /// <returns></returns>
        public abstract double Eval(List<double> input);

        /// <summary>
        /// 求导数（子类实现）
        /// </summary>
        /// <param name="input">参数数组</param>
        /// <returns></returns>
        public abstract List<double> Diff(List<double> input);

        /// <summary>
        /// 正向传播
        /// </summary>
        public void Propagate()
        {
            Propagate(new Hashtable());
        }

        private void Propagate(Hashtable book)
        {
            if (book.Contains(this))
            {
                return;
            }
            book[this] = 1;

            List<double> p = new List<double>();
            foreach (Node c in Children)
            {
                c.Propagate(book);
                p.Add(c.Value);
            }
            Value = Eval(p);
        }

        /// <summary>
        /// 反向传播
        /// </summary>
        public void BackPropagate()
        {
            Queue<Node> q = new Queue<Node>();
            Hashtable book = new Hashtable();
            q.Enqueue(this);
            Derivative = 1;
            while (q.Count > 0)
            {
                // 获取并标记当前节点
                Node cur = q.Dequeue();
                //book[cur] = 1;

                // 叶子节点
                if (cur.Children.Count <= 0)
                {
                    continue;
                }

                // 计算本地梯度
                List<double> input = new List<double>();
                foreach (Node c in cur.Children)
                {
                    input.Add(c.Value);
                }
                List<double> localDerivatives = cur.Diff(input);

                // 更新子节点梯度值
                for (int i = 0; i < localDerivatives.Count; ++i)
                {
                    cur.Children[i].Derivative += localDerivatives[i] * cur.Derivative;
                }

                // 将子节点加入队列
                foreach (Node c in cur.Children)
                {
                    if (!book.Contains(c))
                    {
                        q.Enqueue(c);
                        book[c] = 1;
                    }
                }
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="n"></param>
        protected void AddChild(Node n)
        {
            Children.Add(n);
            n.Parents.Add(this);
        }

        /// <summary>
        /// 将浮点常数转换为计算节点
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Node(double value)
        {
            return new Var(value);
        }

        /// <summary>
        /// 重载+运算符
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Node operator +(Node lhs, Node rhs)
        {
            return new Add(lhs, rhs);
        }

        /// <summary>
        /// 重载-运算符
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
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
            return input[0] + input[1];
        }

        public override List<double> Diff(List<double> input)
        {
            return new List<double> { 1, 1 };
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
            return input[0] * input[1];
        }

        public override List<double> Diff(List<double> input)
        {
            return new List<double> { input[1], input[0] };
        }
    }
}
