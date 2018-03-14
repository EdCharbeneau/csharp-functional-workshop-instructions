using System.Collections.Generic;
using System.Linq;

namespace CsharpPoker
{
    public class Hand
    {
        // private readonly setter for cards, initilaizes a new List<Card>
        private readonly List<Card> cards = new List<Card>();

        // To limit mutablility of the Cards property, an IEnumerable is returned instead of List
        // Cards is mutable but only by calling Draw
        public IEnumerable<Card> Cards { get { return cards; } }

        public void Draw(Card card)
        {
            cards.Add(card);
        }

        // A LINQ Aggregate is used to find the HighCard

        public Card HighCard()
        {
            // Card highCard = Cards.First();
            // foreach (var nextCard in Cards)
            // {
            //     if (nextCard.Value > highCard.Value)
            //     {
            //         highCard = nextCard;
            //     }
            // }
            // return highCard;
            return cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);
            // OrderBy is also valid, but could use more resources than Aggregate
            //return cards.OrderBy(c => c.Value).Last();
        }
        
        // A Return Early pattern is used to call each hand ranking function
        public HandRank GetHandRank()
        {
            if (HasRoyalFlush()) return HandRank.RoyalFlush;
            if (HasFlush()) return HandRank.Flush;
            return HandRank.HighCard;
        }

        // A LINQ All method combined with First can check if all suits are the same value
        private bool HasFlush()
        {
            return cards.All(c => cards.First().Suit == c.Suit);
        }

        // A LINQ All method can determine if all cards are greater than Nine or [Ten, Jack, Queen, King, Ace ]
        public bool HasRoyalFlush()
        {
            return HasFlush() && cards.All(c => c.Value > CardValue.Nine);
        }

    }
}