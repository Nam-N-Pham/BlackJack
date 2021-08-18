using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("deck_id")]
        public string DeckId { get; set; }

        [JsonPropertyName("shuffled")]
        public bool Shuffled { get; set; }

        [JsonPropertyName("remaining")]
        public int Remaining { get; set; }

        class DealCardsResponse
        {
            [JsonPropertyName("success")]
            public bool Success { get; set; }

            [JsonPropertyName("deck_id")]
            public string DeckId { get; set; }

            [JsonPropertyName("remaining")]
            public int Remaining { get; set; }

            [JsonPropertyName("cards")]
            public List<Card> Cards { get; set; }
        }

        public async Task<List<Card>> DealCardsAsync(int count)
        {
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync($"https://deckofcardsapi.com/api/deck/{DeckId}/draw/?count={count}");

            var response = await JsonSerializer.DeserializeAsync<DealCardsResponse>(responseAsStream);

            return response.Cards;
        }

        // public List<Card> cardsInDeck = new List<Card>();

        // public Deck CreateDeck()
        // {
        //     List<string> suits = new List<string>() { "Spades", "Clubs", "Diamonds", "Hearts" };
        //     List<string> ranks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
        //     Deck newDeck = new Deck();

        //     foreach (string suit in suits)
        //     {
        //         foreach (string rank in ranks)
        //         {
        //             Card cardToAdd = new Card();
        //             cardToAdd.Suit = suit;
        //             cardToAdd.Rank = rank;
        //             newDeck.cardsInDeck.Add(cardToAdd);
        //         }
        //     }

        //     return newDeck;
        // }

        // public void Shuffle()
        // {
        //     for (int shuffleIndex = this.cardsInDeck.Count - 1; shuffleIndex >= 1; shuffleIndex--)
        //     {
        //         System.Random RandomNumberGenerator = new Random();
        //         int randomIndex = RandomNumberGenerator.Next(shuffleIndex);

        //         Card shuffleCard = this.cardsInDeck[shuffleIndex];
        //         Card randomCard = this.cardsInDeck[randomIndex];
        //         this.cardsInDeck[shuffleIndex] = randomCard;
        //         this.cardsInDeck[randomIndex] = shuffleCard;
        //     }
        // }

        // public Card Deal()
        // {
        //     Card cardToDeal = this.cardsInDeck[0];
        //     this.cardsInDeck.RemoveAt(0);
        //     return cardToDeal;
        // }
    }
}
