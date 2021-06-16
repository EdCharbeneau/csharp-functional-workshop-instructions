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

        public IEnumerable<Card> Cards => cards;

        public void Draw(Card card) => cards.Add(card);

        public Card HighCard() => cards.Aggregate((result, nextCard) => result.Value > nextCard.Value ? result : nextCard);

        public HandRank GetHandRank() =>
            HasRoyalFlush() ? HandRank.RoyalFlush :
            HasStraightFlush() ? HandRank.StraightFlush :
            HasStraight() ? HandRank.Straight : 
            HasFlush() ? HandRank.Flush :
            HasFullHouse() ? HandRank.FullHouse :
            HasFourOfAKind() ? HandRank.FourOfAKind :
            HasThreeOfAKind() ? HandRank.ThreeOfAKind :
            HasTwoPair() ? HandRank.TwoPair :
            HasPair() ? HandRank.Pair :
            HandRank.HighCard;

        private bool HasFlush() => cards.All(c => cards.First().Suit == c.Suit);

        private bool HasRoyalFlush() => HasFlush() && cards.All(c => c.Value > CardValue.Nine);

        private bool HasOfAKind(int num) => cards.ToKindAndQuantities().Any(c => c.Value == num);
        private int CountOfAKind(int num) => cards.ToKindAndQuantities().Count(c => c.Value == num);

        private bool HasPair() => HasOfAKind(2);
        private bool HasTwoPair() => CountOfAKind(2) == 2;
        private bool HasThreeOfAKind() => HasOfAKind(3);
        private bool HasFourOfAKind() => HasOfAKind(4);

        private bool HasFullHouse() => HasThreeOfAKind() && HasPair();

        // The Zip and Skip LINQ methods are replaced by a custom extension method, SelectConsecutive
        // Select consecutive works like LINQ select, except it can evaluate two consecutive items in an collection
        // This is done using a yield keyword, the source code is in EvalExtensions.cs
        private bool HasStraight() => 
            cards.OrderBy(card => card.Value).SelectConsecutive((n, next) => n.Value + 1 == next.Value).All(value => value);

        private bool HasStraightFlush() => HasStraight() && HasFlush();
    }

}