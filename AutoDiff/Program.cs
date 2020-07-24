using System;

namespace AutoDiff
{
    class Program
    {
        static void Main()
        {
            Node x = new Var(1);
            Node y = new Var(2);
            Node u = x + y;
            Node v = x * y;
            Node r = u * v + v * u;

            r.Propagate();
            Console.WriteLine(r.Value);

            r.BackPropagate();
            Console.WriteLine(u.Derivative + " " + v.Derivative);
            Console.WriteLine(x.Derivative + " " + y.Derivative);

            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }
    }
}
