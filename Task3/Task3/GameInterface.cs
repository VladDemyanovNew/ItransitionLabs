using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    static class GameInterface
    {
        /// <summary>
        /// It prints a menu to the console
        /// </summary>
        /// <param name="args"></param>
        public static void PrintMenu(string[] args)
        {
            Console.WriteLine("Available moves:");
            for (int i = 1; i <= args.Length; i++)
                Console.WriteLine($"{i} - {args[i - 1]}");
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }

        /// <summary>
        /// It prints a score table to the console
        /// </summary>
        /// <param name="args"></param>
        public static void PrintTable(string[] args)
        {
            string[][] table = CreateScoreTable(args);


            var consoleTable = new ConsoleTable(table[0]);
            foreach (var row in table.Skip(1))
            {
                consoleTable.AddRow(row);
            }

            consoleTable.Write();
            Console.WriteLine();
        }

        /// <summary>
        /// It creates score table
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string[][] CreateScoreTable(string[] args)
        {
            string[][] table = new string[args.Length + 1][];

            for (int i = 0; i < args.Length + 1; i++)
            {
                string[] row = new string[args.Length + 1];
                for (int j = 0; j < args.Length + 1; j++)
                {
                    if (j == 0 && i == 0)
                        row[j] = "PC \\ USER";
                    else if (i == 0)
                        row[j] = args[j - 1];
                    else if (j == 0)
                        row[j] = args[i - 1];
                    else
                    {
                        int winner = GameRulesValidator.DetermineWinner(j, i, args);
                        row[j] = winner == 1 ? "WIN" : winner == -1 ? "LOSE" : "DRAW";
                    }
                }
                table[i] = row;
            }

            return table;
        }
    }
}
