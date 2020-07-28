using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff
{
    /// <summary>
    /// 内置函数
    /// </summary>
    public static class F
    {
        /// <summary>
        /// 求幂
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Expr Pow(Expr lhs, Expr rhs)
        {
            return new Pow(lhs, rhs);
        }

        /// <summary>
        /// 以e为底的指数函数
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Expr Exp(Expr expr)
        {
            return new Exp(expr);
        }

        /// <summary>
        /// 自然对数
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Expr Ln(Expr expr)
        {
            return new Ln(expr);
        }
    }
}
