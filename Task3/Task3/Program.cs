using System;
using System.Text;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            SecureRandom secureRandom = new SecureRandom();
            GameRulesValidator.ValidateArgs(args); // Validation of the input args

            while (true)
            {
                byte[] HMACKey = KeyGenerator.GenerateHMACKey(); // Generation of random key
                int computerMove = secureRandom.Next(1, args.Length); // Random computer motion generation  BitConverter.GetBytes(computerMove)
                Console.WriteLine(computerMove);
                byte[] HMAC = KeyGenerator.GenerateHMACSHA256(HMACKey, Encoding.UTF8.GetBytes(args[computerMove - 1])); // Calculating HMAC from action with the generation key
                
                Console.WriteLine("HMAC: " + KeyGenerator.CreateStrFromByteArr(HMAC));
                GameInterface.PrintMenu(args);

                int playerMove;
                do
                {
                    Console.Write("Enter your move: ");
                    playerMove = GameRulesValidator.ValidatePlayerMove(Console.ReadLine(), args);
                } while (playerMove == -1);

                Console.WriteLine("Your move: " + args[playerMove - 1]);
                Console.WriteLine("Computer move: " + args[computerMove - 1]);
                int winner = GameRulesValidator.DetermineWinner(playerMove, computerMove, args);
                Console.WriteLine(winner == 1 ? "You win :)": winner == -1 ? "Computer win :(" : "Draw :/");
                Console.WriteLine("HMAC key: " + KeyGenerator.CreateStrFromByteArr(HMACKey));
                Console.WriteLine();
            }
        }
    }
}
