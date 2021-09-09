using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    public class GameRulesValidator
    {
        #region Fields
        /// <summary>
        /// Min count of args
        /// </summary>
        public const int MinArgsCount = 3;
        #endregion

        #region Validation of input args
        /// <summary>
        /// It validates input args
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void ValidateArgs(string[] args)
        {
            if (args.Length < MinArgsCount)
            {
                Console.WriteLine($"Number of parameters must be >= {MinArgsCount}");
                PrintInputArgsExamples();
                Environment.Exit(0);
            }
            if (args.Length % 2 != 1)
            {
                Console.WriteLine("Number of parameters must be odd");
                PrintInputArgsExamples();
                Environment.Exit(0);
            }
            if (args.Distinct().Count() != args.Length)
            {
                Console.WriteLine("Parameters must not be repeated");
                PrintInputArgsExamples();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// It outputs examples of valid and non-valid input args 
        /// </summary>
        public static void PrintInputArgsExamples()
        {
            Console.WriteLine("\nExamples of correct input:\n" +
                              "\trock paper scissors\n" +
                              "\trock scissors paper lizard Spock\n");
            Console.WriteLine("\nExamples of incorrect input:\n" +
                              "\trock scissors\n" +
                              "\trock trock scissors\n" +
                              "\ttrock scissors paper lizard\n");
        }
        #endregion

        #region Determination of the winner
        /// <summary>
        /// It is determine who has won
        /// (0 = Draw;
        /// -1 = Computer win;
        /// 1 = You win)
        /// </summary>
        /// <param name="playerMove"></param>
        /// <param name="computerMove"></param>
        /// <returns></returns>
        public static int DetermineWinner(int playerMove, int computerMove, string[] args)
        {
            if (playerMove.Equals(computerMove))
                return 0;
            else if ((playerMove + args.Length - computerMove) % args.Length > args.Length / 2)
                return -1;
            else
                return 1;
        }
        #endregion

        #region Validation of player move
        public static int ValidatePlayerMove(string move, string[] args)
        {
            if (move == "?")
            {
                GameInterface.PrintTable(args);
                GameInterface.PrintMenu(args);
                return -1;
            }

            int result;
            bool check = int.TryParse(move, out result);

            if (check)
            {
                if (result == 0)
                    Environment.Exit(0);
                return result;
            }
            else
            {
                Console.WriteLine($"Error! You must enter numbers between 0 and {args.Length} or '?'");
                GameInterface.PrintMenu(args);
                return -1;
            }

        }
        #endregion
    }
}
