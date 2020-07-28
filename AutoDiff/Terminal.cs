using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff
{
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
    }
}
