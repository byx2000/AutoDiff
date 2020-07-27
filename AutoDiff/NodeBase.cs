using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff
{
    /// <summary>
    /// 计算节点抽象基类
    /// </summary>
    public abstract class Node
    {
        private readonly List<Node> children = new List<Node>();
        /// <summary>
        /// 子节点列表
        /// </summary>
        public IReadOnlyList<Node> Children
        {
            get { return children; }
        }

        private readonly List<Node> parents = new List<Node>();
        /// <summary>
        /// 父节点列表
        /// </summary>
        public IReadOnlyList<Node> Parents
        {
            get { return parents; }
        }

        /// <summary>
        /// 计算值
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// 导数值
        /// </summary>
        public double Derivative { get; private set; }

        /// <summary>
        /// 求值（子类实现）
        /// </summary>
        /// <param name="input">参数数组</param>
        /// <returns>计算值</returns>
        protected abstract double Eval(List<double> input);

        /// <summary>
        /// 求导数（子类实现）
        /// </summary>
        /// <param name="input">参数数组</param>
        /// <returns>对各个输入参数的偏导数</returns>
        protected abstract List<double> Diff(List<double> input);

        /// <summary>
        /// 正向传播
        /// </summary>
        public void Forward()
        {
            Forward(new Hashtable());
        }

        /// <summary>
        /// 带记忆化搜索的正向传播
        /// </summary>
        /// <param name="book">记录已处理的节点</param>
        private void Forward(Hashtable book)
        {
            if (book.Contains(this))
            {
                return;
            }
            book[this] = 1;

            List<double> p = new List<double>();
            foreach (Node c in Children)
            {
                c.Forward(book);
                p.Add(c.Value);
            }
            Value = Eval(p);
        }

        /// <summary>
        /// 反向传播
        /// </summary>
        public void Backward()
        {
            // 记录所有节点的出度、梯度清零
            Hashtable outDegree = new Hashtable();
            BackPropagatePreparation(outDegree);

            Derivative = 1;

            // 按拓扑排序处理节点
            Queue<Node> ready = new Queue<Node>();
            ready.Enqueue(this);
            while (ready.Count > 0)
            {
                // 获取当前节点
                Node cur = ready.Dequeue();

                // 更新节点出度，添加出度为0的节点到就绪队列
                foreach (Node c in cur.Children)
                {
                    int val = (int)outDegree[c];
                    outDegree[c] = val - 1;
                    if (val - 1 == 0 && c.Children.Count > 0)
                    {
                        ready.Enqueue(c);
                    }
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
            }
        }

        /// <summary>
        /// 反向传播前的准备工作
        /// </summary>
        /// <param name="inDegree">保存所有节点的出度</param>
        private void BackPropagatePreparation(Hashtable inDegree)
        {
            // 跳过已处理的节点
            if (inDegree.Contains(this))
            {
                return;
            }

            // 将梯度清零
            Derivative = 0;

            // 记录节点出度
            inDegree[this] = parents.Count;

            // 递归处理子节点
            foreach (Node c in Children)
            {
                c.BackPropagatePreparation(inDegree);
            }
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="n"></param>
        protected void AddChild(Node n)
        {
            children.Add(n);
            n.parents.Add(this);
        }

        /// <summary>
        /// 将浮点常数转换为计算节点
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Node(double value)
        {
            return new Const(value);
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
        public static Node operator -(Node lhs, Node rhs)
        {
            return new Sub(lhs, rhs);
        }

        /// <summary>
        /// 重载*运算符
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Node operator *(Node lhs, Node rhs)
        {
            return new Mul(lhs, rhs);
        }

        /// <summary>
        /// 重载/运算符
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Node operator /(Node lhs, Node rhs)
        {
            return new Div(lhs, rhs);
        }

        /// <summary>
        /// 重载^运算符
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Node operator ^(Node lhs, Node rhs)
        {
            return new Pow(lhs, rhs);
        }

        /// <summary>
        /// 重载负号运算符
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Node operator -(Node expr)
        {
            return new Neg(expr);
        }

        /// <summary>
        /// 求幂
        /// </summary>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public Node Pow(Node rhs)
        {
            return new Pow(this, rhs);
        }

        /// <summary>
        /// 自然对数的幂
        /// </summary>
        /// <returns></returns>
        public Node Exp()
        {
            return new Exp(this);
        }
    }
}
