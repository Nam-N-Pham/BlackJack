using System;
using System.Collections.Generic;

namespace BlackJack
{
    class Hand
    {
        // List<Card> cardsInHand = new List<Card>();
        public List<Card> cardsInHand { get; set; }



        public int HandValue()
        {
            int sum = 0;
            foreach (Card card in cardsInHand)
            {
                sum += card.ValueOfCard();
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
                Console.Write(card.Value + " of " + card.Suit + ", ");
            }
            Console.WriteLine();
        }
    }
}
