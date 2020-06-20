using System;

namespace catGirlBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var neko = new Bot();
            Console.WriteLine("Initialized");
            neko.RunAsync().GetAwaiter().GetResult();
        }
    }
}
