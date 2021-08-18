using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlackJack
{

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to BlackjackCS");
            Console.WriteLine("Enter any key to play");
            string pressToPlay = Console.ReadLine();

            HttpClient client = new HttpClient();

            Boolean play = true;
            Boolean playAgain = false;
            while (play)
            {
                // Deck newDeck = new Deck();
                // newDeck = newDeck.CreateDeck();
                // newDeck.Shuffle();

                var responseAsStream = await client.GetStreamAsync("http://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");

                var newDeck = await JsonSerializer.DeserializeAsync<Deck>(responseAsStream);

                Hand playerHand = new Hand();
                Hand dealerHand = new Hand();
                // playerHand.AddCard(newDeck.Deal());
                // playerHand.AddCard(newDeck.Deal());
                // dealerHand.AddCard(newDeck.Deal());
                // dealerHand.AddCard(newDeck.Deal());

                playerHand.cardsInHand = await newDeck.DealCardsAsync(2);
                dealerHand.cardsInHand = await newDeck.DealCardsAsync(2);

                Console.Write("Your hand is: ");
                playerHand.PrintHand();
                if (playerHand.HandValue() > 21)
                {
                    Console.WriteLine("You busted, you lose!");
                    Console.WriteLine("Would you like to play again? Type Y to play again or N to quit.");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "y")
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }

                playAgain = false;
                Boolean playerTurn = true;
                while (playerTurn)
                {
                    Console.WriteLine("Type \"Hit\" to hit or \"Stand\" to stand");
                    string hitOrStand = Console.ReadLine().ToLower();

                    if (hitOrStand == "hit")
                    {
                        // playerHand.AddCard(newDeck.Deal());
                        List<Card> cardToAdd = await newDeck.DealCardsAsync(1);
                        playerHand.AddCard(cardToAdd[0]);
                        Console.Write("Your hand is: ");
                        playerHand.PrintHand();

                        if (playerHand.HandValue() > 21)
                        {
                            Console.WriteLine("You busted, you lose!");
                            Console.WriteLine("Would you like to play again? Type Y to play again or N to quit.");
                            string answer = Console.ReadLine().ToLower();
                            if (answer == "y")
                            {
                                playAgain = true;
                                break;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (playAgain)
                {
                    continue;
                }
                playAgain = false;

                Boolean dealerTurn = true;
                while (dealerTurn)
                {
                    Console.Write("Dealer's hand is: ");
                    dealerHand.PrintHand();
                    if (dealerHand.HandValue() > 21)
                    {
                        Console.WriteLine("Dealer busted, you win!");
                        Console.WriteLine("Would you like to play again? Type Y to play again or N to quit.");
                        string answer = Console.ReadLine().ToLower();
                        if (answer == "y")
                        {
                            playAgain = true;
                            break;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (dealerHand.HandValue() >= 17)
                    {
                        break;
                    }
                    else
                    {
                        // dealerHand.AddCard(newDeck.Deal());
                        List<Card> cardToAddDealer = await newDeck.DealCardsAsync(1);
                        dealerHand.AddCard(cardToAddDealer[0]);
                    }
                }

                if (playAgain)
                {
                    continue;
                }
                playAgain = false;

                if (playerHand.HandValue() > dealerHand.HandValue())
                {
                    Console.WriteLine("You hand is higher, you win!");
                    Console.WriteLine("Would you like to play again? Type Y to play again or N to quit.");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "y")
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (playerHand.HandValue() < dealerHand.HandValue())
                {
                    Console.WriteLine("You hand is lower, you lose!");
                    Console.WriteLine("Would you like to play again? Type Y to play again or N to quit.");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "y")
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Your hand is tied with the dealer's hand, you lose!");
                    Console.WriteLine("Would you like to play again? Type Y to play again or N to quit.");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "y")
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}
