using System;

namespace AutoDiff
{
    class Program
    {
        static void Main()
        {
            //Node x = new Var(10);
            //Node y = new Var(20);
            //Node z = new Var(30);
            //Node r = (x + y) * (y + z);

            //r.Propagate();
            //Console.WriteLine(r.Value);

            //r.BackPropagate();
            //Console.WriteLine(x.Derivative + " " + y.Derivative + " " + z.Derivative);

            Node x = new Var(100);
            Node y = 1 + x;

            y.Propagate();
            Console.WriteLine(y.Value);

            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }
    }
}
