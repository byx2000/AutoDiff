using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff
{
    /// <summary>
    /// 二元运算节点抽象基类
    /// </summary>
    public abstract class BinaryOp : Term
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lhs">左表达式</param>
        /// <param name="rhs">右表达式</param>
        public BinaryOp(Term lhs, Term rhs) : base(lhs, rhs) { }

        /// <summary>
        /// 返回左右操作数的运算结果（子类实现）
        /// </summary>
        /// <param name="lhs">左操作数的值</param>
        /// <param name="rhs">右操作数的值</param>
        /// <returns></returns>
        protected abstract double Eval(double lhs, double rhs);

        /// <summary>
        /// 计算左右操作数的偏导数（子类实现）
        /// </summary>
        /// <param name="lhs">左操作数的值</param>
        /// <param name="rhs">右操作数的值</param>
        /// <param name="dLhs">左操作数的偏导数</param>
        /// <param name="dRhs">右操作数的偏导数</param>
        protected abstract void Diff(double lhs, double rhs, out double dLhs, out double dRhs);

        protected override double Eval(List<double> input)
        {
            return Eval(input[0], input[1]);
        }

        protected override List<double> Diff(List<double> input)
        {
            Diff(input[0], input[1], out double dLhs, out double dRhs);
            return new List<double> { dLhs, dRhs };
        }
    }

    /// <summary>
    /// 加法节点
    /// </summary>
    public class Add : BinaryOp
    {
        public Add(Term lhs, Term rhs) : base(lhs, rhs) { }

        protected override double Eval(double lhs, double rhs)
        {
            return lhs + rhs;
        }

        protected override void Diff(double lhs, double rhs, out double dLhs, out double dRhs)
        {
            dLhs = 1;
            dRhs = 1;
        }
    }

    /// <summary>
    /// 减法节点
    /// </summary>
    public class Sub : BinaryOp
    {
        public Sub(Term lhs, Term rhs) : base(lhs, rhs) { }

        protected override double Eval(double lhs, double rhs)
        {
            return lhs - rhs;
        }

        protected override void Diff(double lhs, double rhs, out double dLhs, out double dRhs)
        {
            dLhs = 1;
            dRhs = -1;
        }
    }

    /// <summary>
    /// 乘法节点
    /// </summary>
    public class Mul : BinaryOp
    {
        public Mul(Term lhs, Term rhs) : base(lhs, rhs) { }

        protected override double Eval(double lhs, double rhs)
        {
            return lhs * rhs;
        }

        protected override void Diff(double lhs, double rhs, out double dLhs, out double dRhs)
        {
            dLhs = rhs;
            dRhs = lhs;
        }
    }

    /// <summary>
    /// 除法节点
    /// </summary>
    public class Div : BinaryOp
    {
        public Div(Term lhs, Term rhs) : base(lhs, rhs) { }

        protected override double Eval(double lhs, double rhs)
        {
            return lhs / rhs;
        }

        protected override void Diff(double lhs, double rhs, out double dLhs, out double dRhs)
        {
            dLhs = 1 / rhs;
            dRhs = -lhs / (rhs * rhs);
        }
    }

    /// <summary>
    /// 幂运算节点
    /// </summary>
    public class Pow : BinaryOp
    {
        public Pow(Term lhs, Term rhs) : base(lhs, rhs) { }

        protected override double Eval(double lhs, double rhs)
        {
            return Math.Pow(lhs, rhs);
        }

        protected override void Diff(double lhs, double rhs, out double dLhs, out double dRhs)
        {
            dLhs = rhs * Math.Pow(lhs, rhs - 1);
            dRhs = Math.Pow(lhs, rhs) * Math.Log(lhs);
        }
    }
}
