using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    public class GameRulesValidator
    {
        #region Properties
        public string[] Args { get; private set; }
        #endregion

        #region Fields
        /// <summary>
        /// Min count of args
        /// </summary>
        public const int MinArgsCount = 3;
        #endregion

        public GameRulesValidator(string[] args)
        {
            this.Args = args;
        }

        #region Validation of input args
        /// <summary>
        /// It validates input args
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public void ValidateArgs()
        {
            if (Args.Length < MinArgsCount)
            {
                Console.WriteLine($"Число параметров должно быть >= {MinArgsCount}");
                PrintInputArgsExamples();
                Environment.Exit(0);
            }
            if (Args.Length % 2 != 1)
            {
                Console.WriteLine("Число параметров должно быть нечётным");
                PrintInputArgsExamples();
                Environment.Exit(0);
            }
            if (Args.Distinct().Count() != Args.Length)
            {
                Console.WriteLine("Параметры не должны повторяться");
                PrintInputArgsExamples();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// It outputs examples of valid and non-valid input args 
        /// </summary>
        public void PrintInputArgsExamples()
        {
            Console.WriteLine("\nПримеры корректного ввода:\n" +
                              "\tкамень ножницы бумага\n" +
                              "\tкамень ножницы бумага колодец ящерица\n");
            Console.WriteLine("\nПримеры некорректного ввода:\n" +
                              "\tкамень ножницы\n" +
                              "\tкамень камень ножницы\n" +
                              "\tкамень ножницы бумага колодец\n");
        }
        #endregion

        #region Determination of the winner
        /// <summary>
        /// It is determine who has won
        /// </summary>
        /// <param name="playerMove"></param>
        /// <param name="computerMove"></param>
        /// <returns></returns>
        public string DetermineWinner(int playerMove, int computerMove)
        {
            if (playerMove.Equals(computerMove))
                return "Draw :/";
            else if ((playerMove + Args.Length - computerMove) % Args.Length > Args.Length / 2)
                return "Computer win :(";
            else
                return "You win :)";
        }
        #endregion

        #region Validation of player move
        public int ValidatePlayerMove(string move)
        {
            if (move == "?")
            {
                GameInterface.PrintTable(Args);
                GameInterface.PrintMenu(Args);
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
                Console.WriteLine($"Error! You must enter numbers between 0 and {Args.Length} or '?'");
                GameInterface.PrintMenu(Args);
                return -1;
            }

        }
        #endregion
    }
}
