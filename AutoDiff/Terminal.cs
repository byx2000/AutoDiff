using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff
{
    /// <summary>
    /// 常量节点
    /// </summary>
    public class Const : Expr
    {
        public new double Value { get; }

        public Const(double value)
        {
            Value = value;
        }

        protected override double Eval(List<double> input)
        {
            return Value;
        }

        protected override List<double> Diff(List<double> input)
        {
            return new List<double>();
        }

        /// <summary>
        /// 将浮点常数转换为常数节点
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Const(double value)
        {
            return new Const(value);
        }
    }

    /// <summary>
    /// 变量节点
    /// </summary>
    public class Var : Expr
    {
        public new double Value { get; set; }

        public Var(double value = 0)
        {
            Value = value;
        }

        protected override double Eval(List<double> input)
        {
            return Value;
        }

        protected override List<double> Diff(List<double> input)
        {
            return new List<double>();
        }

        /// <summary>
        /// 将浮点常数转换为变量节点
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Var(double value)
        {
            return new Var(value);
        }

        /// <summary>
        /// 将常数节点转换为变量节点
        /// </summary>
        /// <param name="c"></param>
        public static implicit operator Var(Const c)
        {
            return new Var(c.Value);
        }
    }
}
