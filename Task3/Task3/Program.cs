using System;
using System.Text;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            GameRulesValidator gameRules = new GameRulesValidator(args);
            gameRules.ValidateArgs();

            while (true)
            {
                byte[] HMACKey = KeyGenerator.GenerateHMACKey();
                int computerMove = 3;
                byte[] HMAC = KeyGenerator.GenerateHMACSHA256(HMACKey, BitConverter.GetBytes(computerMove));
                
                Console.WriteLine("HMAC: " + KeyGenerator.CreateStrFromByteArr(HMAC));
                GameInterface.PrintMenu(args);

                int playerMove;
                do
                {
                    Console.Write("Enter your move: ");
                    playerMove = gameRules.ValidatePlayerMove(Console.ReadLine());
                } while (playerMove == -1);
                

                Console.WriteLine("Your move: " + args[playerMove - 1]);
                Console.WriteLine("Computer move: " + args[computerMove - 1]);
                Console.WriteLine(gameRules.DetermineWinner(playerMove, computerMove));
                Console.WriteLine("HMAC key: " + KeyGenerator.CreateStrFromByteArr(HMACKey));

                break;
            }
        }
    }
}
