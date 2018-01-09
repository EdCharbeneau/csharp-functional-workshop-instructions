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

        public IEnumerable<Card> Cards { get { return cards; } }

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
            HasPair() ? HandRank.Pair :
            HandRank.HighCard;

        private bool HasFlush() => cards.All(c => cards.First().Suit == c.Suit);

        private bool HasRoyalFlush() => HasFlush() && cards.All(c => c.Value > CardValue.Nine);

        private bool HasOfAKind(int num) => cards.ToKindAndQuantities().Any(c => c.Value == num);

        private bool HasPair() => HasOfAKind(2);
        private bool HasThreeOfAKind() => HasOfAKind(3);
        private bool HasFourOfAKind() => HasOfAKind(4);

        private bool HasFullHouse() => HasThreeOfAKind() && HasPair();

        // The Zip LINQ method operates on two collections at once
        // The second instance is offset by one, n + 1 is comapred with the next value in the offset collection.
        // If all evaluate to True, the collection is a straight
        private bool HasStraight() => cards.OrderBy(card => card.Value)
            .Zip(cards.OrderBy(card => card.Value).Skip(1), (n, next) => n.Value + 1 == next.Value)
            .All(value => value /* true */ );

        private bool HasStraightFlush() => HasStraight() && HasFlush();
    }

}