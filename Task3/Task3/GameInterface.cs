using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    static class GameInterface
    {
        public static void PrintMenu(string[] args)
        {
            Console.WriteLine("Available moves:");
            for (int i = 1; i <= args.Length; i++)
                Console.WriteLine($"{i} - {args[i - 1]}");
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }

        public static void PrintTable(string[] args)
        {
            Console.WriteLine("Table");
        }
    }
}
