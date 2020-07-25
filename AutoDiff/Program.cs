using System;

namespace AutoDiff
{
    class Program
    {
        static void Main()
        {
            Node x = 12;
            Node y = x - x;

            y.Propagate();
            y.BackPropagate();

            Console.WriteLine(x.Derivative);

            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }
    }
}
