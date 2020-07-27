﻿using System;
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
        public UnaryNode(Node expr)
        {
            AddChild(expr);
        }

        /// <summary>
        /// 计算值
        /// </summary>
        /// <param name="x">操作数值</param>
        /// <returns>计算值</returns>
        protected abstract double Eval(double x);

        /// <summary>
        /// 计算操作数的导数值
        /// </summary>
        /// <param name="x">操作数值</param>
        /// <returns>导数值</returns>
        protected abstract double Diff(double x);

        protected override double Eval(List<double> input)
        {
            return Eval(input[0]);
        }

        protected override List<double> Diff(List<double> input)
        {
            return new List<double> { Diff(input[0]) };
        }
    }

    /// <summary>
    /// 负号节点
    /// </summary>
    public class Neg : UnaryNode
    {
        public Neg(Node expr) : base(expr) { }

        protected override double Eval(double x)
        {
            return -x;
        }

        protected override double Diff(double x)
        {
            return -1;
        }
    }

    /// <summary>
    /// 以e为底的指数函数
    /// </summary>
    public class Exp : UnaryNode
    {
        public Exp(Node expr) : base(expr) { }

        protected override double Eval(double x)
        {
            return Math.Exp(x);
        }

        protected override double Diff(double x)
        {
            return Math.Exp(x);
        }
    }
}
