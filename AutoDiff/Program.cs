using System;

namespace AutoDiff
{
    class Program
    {
        static void Main()
        {
            Node x = 1;
            Node y = x + x;
            Node z = y + y;
            Node r = y + z;

            r.Propagate();
            Console.WriteLine(r.Value);

            r.BackPropagate();
            Console.WriteLine(x.Derivative);

            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }
    }
}
