using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDiff.Sample
{
    class Entry
    {
        static void Main()
        {
            Sample1.Run();
            Sample2.Run();

            Console.WriteLine("请按任意键继续...");
            Console.ReadKey();
        }
    }
}
