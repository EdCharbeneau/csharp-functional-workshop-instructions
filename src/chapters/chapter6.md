## Functional Refactor

In the previous chapter you solved more hand ranks using LINQ and refactored with the yield keyword to create a custom LINQ-like extension method.

In this chapter you'll refactor to use a more functional approach to scoring a hand.

Feel free to run tests as needed. **For example, after every code change.**

### Data and Behavior 

Up until now we have combined our Hand object with the behavior of scoring. Ideally these concepts should not be mixed. Because there is no state to be concerned about, the scoring functions can be moved with relative ease.

<h4 class="exercise-start">
    <b>Exercise</b>: Refactor with Pure Functions
</h4>

Create a new public static class named FiveCardPokerScorer

Open Hand.cs and move the scoring functionality to FiveCardPokerScorer.cs. Because the functionality will be external to the data, the funcitons will need to be modified to accept the data as a parameter. In addtion, this will bring the application closer in-line with functional programming because the new functions are considered "[pure functions](https://en.wikipedia.org/wiki/Pure_function)".

Example:
```
public static class FiveCardPokerScorer
{
    public static Card HighCard(IEnumerable<Card> cards) => cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);

    private static bool HasFlush(IEnumerable<Card> cards) => cards.All(c => cards.First().Suit == c.Suit);
}
```

Create a new test class in /Tests named FiveCardPokerScorerTests

Move the corresponding tests from HandTests to FiveCardPokerScorerTests.

```
[Fact]
public void CanGetHighCard()
{
    var hand = new Hand();
    hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
    hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
    hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
    hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
    hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
    FiveCardPokerScorer.HighCard(hand.Cards).Value.Should().Be(CardValue.King);
}
```

### Functions as Data - VS2017 C# 7.1+

<div class="exercise-end"></div>

If you're using VS2015 C# 6.0 skip this exercise.

<h4 class="exercise-start">
    <b>Exercise</b>: Refactor GetHandRank with Tuples
</h4>

Create a new public class named Ranker

Open Hand.cs and create a new private method named Rankings that generates a collection of Tuples. Use the following code as a starting point, fill in the remaining items

    private List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)> Rankings() =>
            new List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)>
            {
                // more ranks here
            };

Find the GetHandRank method and remove the expression after the =>

The result should be:

    public static HandRank GetHandRank() =>

After the => write a new expression that uses the Rankings that evaluates the hand rank.

Hint: think LINQ

<div class="exercise-end"></div>

### Functions as Data VS2015 C# 6

If you're using VS2017 C# 7.x skip this exercise.

<h4 class="exercise-start">
    <b>Exercise</b>: Refactor GetHandRank VS2015 C# 6.0 (aka No Tuples)
</h4>

Create a new public class named Ranker

The Ranker an immutable class that will hold a delegate responsible for evaluating cards and a corresponding hand rank.

    public class Ranker
    {
        public Ranker(Func<IEnumerable<Card>, bool> eval, HandRank rank)
        {
            Eval = eval;
            Rank = rank;
        }

        public Func<IEnumerable<Card>, bool> Eval { get; }

        public HandRank Rank { get; }

    }

Open Hand.cs and create a new private method named Rankings that generates a collection of Ranker.

Use the following code as a starting point, fill in the remaining Ranker items

    private List<Ranker> Rankings() => new List<Ranker>
    {
        new Ranker(cards => HasRoyalFlush(), HandRank.RoyalFlush),
        // more ranks here
    };

Find the GetHandRank method and remove the expression after the =>

The result should be:

    public static HandRank GetHandRank() =>

After the => write a new expression that uses the List<Ranker> from Rankings() that evaluates the hand rank.

Hint: think LINQ

<div class="exercise-end"></div>

### Flexibility

The newest version of GetHandRank can be more flexible than the previous versions. Because it is a collection, it can easily be added to and operated on using LINQ.

<h4 class="exercise-start">
    <b>Exercise</b>: Adding new hand ranks
</h4>

Adding new hand ranks is easy. Simply add a new Ranker to the Rankings collection and the GetHandRank method will do the rest.

Make the GetHandRank method even more robust by allowing Ranks to be added in any order.

Find the GetHandRank method and order the Rankings by descending order by their Strength

    // With Tuples
    .OrderByDescending(rule => rule.rank)

    // Without Tuples
    .OrderByDescending(rule => rule.Rank)

Next, solve the following unit test

        [Fact]
        public void CanScoreTwoPair()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ace, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));

            hand.GetHandRank().Should().Be(HandRank.TwoPair);

        }

<div class="exercise-end"></div>
