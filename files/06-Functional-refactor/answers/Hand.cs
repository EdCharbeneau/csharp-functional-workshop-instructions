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

        // Return the first rule to evaluate to true, this will be the strongest hand rank
        public HandRank GetHandRank() => Rankings().First(rule => rule.Eval(Cards)).Strength;

        // A list of ranks gives added flexibility to how hand ranks can be scored.
        // Each ranker has an Eval delegate that returns a bool

        private List<Ranker> Rankings() =>
            new List<Ranker>
            {
                        new Ranker(cards => HasRoyalFlush(), HandRank.RoyalFlush),
                        new Ranker(cards => HasStraightFlush(), HandRank.StraightFlush),
                        new Ranker(cards => HasFourOfAKind(), HandRank.FourOfAKind),
                        new Ranker(cards => HasFullHouse(), HandRank.FullHouse),
                        new Ranker(cards => HasFlush(), HandRank.Flush),
                        new Ranker(cards => HasStraight(), HandRank.Straight),
                        new Ranker(cards => HasThreeOfAKind(), HandRank.ThreeOfAKind),
                        new Ranker(cards => HasPair(), HandRank.Pair),
                        new Ranker(cards => true, HandRank.HighCard),
            };

        private bool HasFlush() => cards.All(c => cards.First().Suit == c.Suit);

        private bool HasRoyalFlush() => HasFlush() && cards.All(c => c.Value > CardValue.Nine);

        private bool HasOfAKind(int num) => cards.ToPairs().Any(c => c.Value == num);

        private bool HasPair() => HasOfAKind(2);
        private bool HasThreeOfAKind() => HasOfAKind(3);
        private bool HasFourOfAKind() => HasOfAKind(4);

        private bool HasFullHouse() => HasThreeOfAKind() && HasPair();

        private bool HasStraight() => cards.OrderBy(card => card.Value).SelectConsecutive((n, next) => n.Value + 1 == next.Value).All(value => value);

        private bool HasStraightFlush() => HasStraight() && HasFlush();
    }

}