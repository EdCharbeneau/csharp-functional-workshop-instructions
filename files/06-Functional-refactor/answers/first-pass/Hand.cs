using System.Collections.Generic;
using System.Linq;

namespace CsharpPoker
{
    public class Hand
    {
        private readonly List<Card> cards = new List<Card>();
        public IEnumerable<Card> Cards => cards;
        public void Draw(Card card) => cards.Add(card);
        
        public Card HighCard() => cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);
        private bool HasFlush() => cards.All(c => cards.First().Suit == c.Suit);
        public bool HasRoyalFlush() => HasFlush() && cards.All(c => c.Value > CardValue.Nine);
        private bool HasOfAKind(int num) => cards.ToKindAndQuantities().Any(c => c.Value == num);
        private bool HasPair() => HasOfAKind(2);
        private bool HasThreeOfAKind() => HasOfAKind(3);
        private bool HasFourOfAKind() => HasOfAKind(4);
        private bool HasFullHouse() => HasThreeOfAKind() && HasPair();
        private bool HasStraight() => cards.OrderBy(card => card.Value).SelectConsecutive((n, next) => n.Value + 1 == next.Value).All(value => value);
        private bool HasStraightFlush() => HasStraight() && HasFlush();

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
    }
}