using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpPoker;

namespace CsharpPoker
{
    public class Hand
    {
        private readonly List<Card> cards = new List<Card>();

        // simplified to an expression-bodied member
        public IEnumerable<Card> Cards => cards;
        
        // simplified to an expression-bodied member
        public void Draw(Card card) => cards.Add(card);

        // simplified to an expression-bodied member
        public Card HighCard() => cards.Aggregate((result, nextCard) => result.Value > nextCard.Value ? result : nextCard);

        // The return early pattern can be replaced with tenary operators
        // then shortended to an expression-bodied member
        public HandRank GetHandRank() =>
            HasRoyalFlush() ? HandRank.RoyalFlush :
            HasFlush() ? HandRank.Flush :
            HandRank.HighCard;

        // simplified to an expression-bodied member
        private bool HasFlush() => cards.All(c => cards.First().Suit == c.Suit);

        // simplified to an expression-bodied member
        public bool HasRoyalFlush() => HasFlush() && cards.All(c => c.Value > CardValue.Nine);
    }

}