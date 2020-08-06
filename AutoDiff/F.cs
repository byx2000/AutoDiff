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
        public static Term Pow(Term lhs, Term rhs)
        {
            return new Pow(lhs, rhs);
        }

        /// <summary>
        /// 以e为底的指数函数
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Term Exp(Term expr)
        {
            return new Exp(expr);
        }

        /// <summary>
        /// 自然对数
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Term Ln(Term expr)
        {
            return new Ln(expr);
        }
    }
}
