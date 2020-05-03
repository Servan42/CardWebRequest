using CardWebRequest.Model;
using CardWebRequest.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardWebRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            Pack pack = new Pack();
            WebRequest cardWebRequest = new WebRequest();
            pack.Shuffle();

            int numberOfCardsToDraw;

            Console.WriteLine("Enter a number and press enter:\n\t0  : Distributes the whole pack of cards.\n\tX  : Distributes X cards per player.\n\t-1 : Reshuffle the pack of cards.\n");

            while (true)
            {
                try
                {
                    Console.Write("Number of cards to draw: ");
                    numberOfCardsToDraw = int.Parse(Console.ReadLine());
                    if (numberOfCardsToDraw > (pack.TotalCards / cardWebRequest.NumberOfPlayers)) throw new Exception("Can't draw that much cards.");

                    if(numberOfCardsToDraw < 0)
                    {
                        Console.WriteLine("Reset and reshuffle the pack");
                        pack.Init();
                        pack.Shuffle();
                    }
                    else if (numberOfCardsToDraw == 0)
                    {
                        Console.WriteLine("Distributing the whole pack.");
                        pack.Init();
                        pack.Shuffle();
                        for (int i = 0; i < cardWebRequest.NumberOfPlayers; i++)
                        {
                            cardWebRequest.SendToNextPlayer(pack.Draw(pack.TotalCards / cardWebRequest.NumberOfPlayers));
                        }
                    }
                    else
                    {
                        if (numberOfCardsToDraw * cardWebRequest.NumberOfPlayers > pack.PackList.Count)
                        {
                            Console.WriteLine("The pack of card is empty, reset and reshuffle");
                            pack.Init();
                            pack.Shuffle();
                        }

                        for (int i = 0; i < cardWebRequest.NumberOfPlayers; i++)
                        {
                            cardWebRequest.SendToNextPlayer(pack.Draw(numberOfCardsToDraw));
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(pack);
            Console.WriteLine("\n");
            cardWebRequest.SendToNextPlayer(pack.Draw(5));
            cardWebRequest.SendToNextPlayer(pack.Draw(5));
            Console.ReadKey();
            cardWebRequest.SendToNextPlayer(pack.Draw(5));
            cardWebRequest.SendToNextPlayer(pack.Draw(5));
            Console.ReadKey();
        }
    }
}
