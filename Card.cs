using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlackJack
{
    class Card
    {
        public static List<string> ListOfSuits = new List<string>() { "Spades", "Clubs", "Diamonds", "Hearts" };
        public static List<string> ListOfRanks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("suit")]
        public string Suit { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        // public string Suit { get; set; }
        // public string Rank { get; set; }

        public int ValueOfCard()
        {
            Dictionary<string, int> valueOfEachCard = new Dictionary<string, int>()
            {
              {"ACE", 11},
              {"2", 2},
              {"3", 3},
              {"4", 4},
              {"5", 5},
              {"6", 6},
              {"7", 7},
              {"8", 8},
              {"9", 9},
              {"10", 10},
              {"JACK", 10},
              {"QUEEN", 10},
              {"KING", 10}
            };

            return valueOfEachCard[Value];
        }
    }
}
