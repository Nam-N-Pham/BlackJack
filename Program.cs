using System;
using System.Collections.Generic;

namespace BlackJack
{
    class Card
    {
        public static List<string> ListOfSuits = new List<string>() { "Spades", "Clubs", "Diamonds", "Hearts" };
        public static List<string> ListOfRanks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        public string Suit { get; set; }
        public string Rank { get; set; }

        public int Value()
        {
            Dictionary<string, int> valueOfEachCard = new Dictionary<string, int>()
            {
              {"Ace", 11},
              {"2", 2},
              {"3", 3},
              {"4", 4},
              {"5", 5},
              {"6", 6},
              {"7", 7},
              {"8", 8},
              {"9", 9},
              {"10", 10},
              {"Jack", 10},
              {"Queen", 10},
              {"King", 10}
            };

            return valueOfEachCard[this.Rank];
        }
    }

    class Deck
    {
        public List<Card> cardsInDeck = new List<Card>();

        public Deck CreateDeck()
        {
            List<string> suits = new List<string>() { "Spades", "Clubs", "Diamonds", "Hearts" };
            List<string> ranks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            Deck newDeck = new Deck();

            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    Card cardToAdd = new Card();
                    cardToAdd.Suit = suit;
                    cardToAdd.Rank = rank;
                    newDeck.cardsInDeck.Add(cardToAdd);
                }
            }

            return newDeck;
        }

        public void Shuffle()
        {
            for (int shuffleIndex = this.cardsInDeck.Count - 1; shuffleIndex >= 1; shuffleIndex--)
            {
                System.Random RandomNumberGenerator = new Random();
                int randomIndex = RandomNumberGenerator.Next(shuffleIndex);

                Card shuffleCard = this.cardsInDeck[shuffleIndex];
                Card randomCard = this.cardsInDeck[randomIndex];
                this.cardsInDeck[shuffleIndex] = randomCard;
                this.cardsInDeck[randomIndex] = shuffleCard;
            }
        }

        public Card Deal()
        {
            Card cardToDeal = this.cardsInDeck[0];
            this.cardsInDeck.RemoveAt(0);
            return cardToDeal;
        }
    }

    class Hand
    {
        List<Card> cardsInHand = new List<Card>();

        public int HandValue()
        {
            int sum = 0;
            foreach (Card card in this.cardsInHand)
            {
                sum += card.Value();
            }
            return sum;
        }

        public void AddCard(Card card)
        {
            this.cardsInHand.Add(card);
        }

        public void PrintHand()
        {
            foreach (Card card in this.cardsInHand)
            {
                Console.Write(card.Rank + " of " + card.Suit + ", ");
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to BlackjackCS");
            Console.WriteLine("Enter any key to play");
            string pressToPlay = Console.ReadLine();

            Boolean play = true;
            Boolean playAgain = false;
            while (play)
            {
                Deck newDeck = new Deck();
                newDeck = newDeck.CreateDeck();
                newDeck.Shuffle();

                Hand playerHand = new Hand();
                Hand dealerHand = new Hand();
                playerHand.AddCard(newDeck.Deal());
                playerHand.AddCard(newDeck.Deal());
                dealerHand.AddCard(newDeck.Deal());
                dealerHand.AddCard(newDeck.Deal());

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
                        playerHand.AddCard(newDeck.Deal());
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
                        dealerHand.AddCard(newDeck.Deal());
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
