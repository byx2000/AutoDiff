using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff
{
    /// <summary>
    /// 一元运算节点抽象基类
    /// </summary>
    public abstract class UnaryNode : Node
    {
        public UnaryNode(Node node)
        {
            AddChild(node);
        }

        /// <summary>
        /// 计算值
        /// </summary>
        /// <param name="x">操作数值</param>
        /// <returns>计算值</returns>
        public abstract double Eval(double x);

        /// <summary>
        /// 计算操作数的导数值
        /// </summary>
        /// <param name="x">操作数值</param>
        /// <returns>导数值</returns>
        public abstract double Diff(double x);

        public override double Eval(List<double> input)
        {
            return Eval(input[0]);
        }

        public override List<double> Diff(List<double> input)
        {
            return new List<double> { Diff(input[0]) };
        }
    }

    /// <summary>
    /// 负号节点
    /// </summary>
    class Neg : UnaryNode
    {
        public Neg(Node node) : base(node) { }

        public override double Eval(double x)
        {
            return -x;
        }

        public override double Diff(double x)
        {
            return -1;
        }
    }
}
