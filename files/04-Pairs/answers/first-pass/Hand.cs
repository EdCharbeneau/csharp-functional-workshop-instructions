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
            HasFlush() ? HandRank.Flush :
            HasFullHouse() ? HandRank.FullHouse :
            HasFourOfAKind() ? HandRank.FourOfAKind :
            HasThreeOfAKind() ? HandRank.ThreeOfAKind :
            HasPair() ? HandRank.Pair :
            HandRank.HighCard;

        private bool HasFlush() => cards.All(c => cards.First().Suit == c.Suit);

        private bool HasRoyalFlush() => HasFlush() && cards.All(c => c.Value > CardValue.Nine);

        // The Any LINQ method validates that there are dictionary items with a specified pair count value.
        private bool HasOfAKind(int num) => GetPairs(cards).Any(c => c.Value == num);

        private bool HasPair() => HasOfAKind(2);
        private bool HasThreeOfAKind() => HasOfAKind(3);
        private bool HasFourOfAKind() => HasOfAKind(4);

        private bool HasFullHouse() => HasThreeOfAKind() && HasPair();

        /* Since a collection of pairs needs to be built, a mutable collection must be temporarily created.
         Because the scope is limited, risk for side-effect is minimal. In addition, the ConcurrentDictionary's AddOrUpdate method is Thread Safe.
         While the ConcurrentDictionary mutates state via AddOrUpdate, it does use a functional principal called a higher order function.
         Higer order functions are simply a function that accepts or returns another function. 
         The AddOrUpdate method accepts a function which delegates how the item is updated.
        */ 
        private IEnumerable<KeyValuePair<CardValue, int>> GetPairs(IEnumerable<Card> cards)
        {
            var dict = new ConcurrentDictionary<CardValue, int>();
            foreach (var card in cards)
            {
                // Add the value to the dictionary, or increase the count
                dict.AddOrUpdate(card.Value, 1, (cardValue, quantity) => ++quantity);
            }
            return dict;
        }
    }

}