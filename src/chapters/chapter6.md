## Functional Refactor

In the previous chapter you solved more hand ranks using LINQ and refactored with the yeild keyword to create a custom LINQ-like extension method.

In this chapter you'll refactor to use a more functional approach to scoring a hand.

Feel free to run tests as needed. **For example, after every code change.**

<h4 class="exercise-start">
    <b>Exercise</b>: Refactor GetHandRank
</h4>

Create a new public class named Ranker

The Ranker an immutable class that will hold a delegate responsible for evaluating cards and a corresponding hand rank.

    public class Ranker
    {
        public Ranker(Func<IEnumerable<Card>, bool> eval, HandRank strength)
        {
            Eval = eval;
            Strength = strength;
        }

        public Func<IEnumerable<Card>, bool> Eval { get; }

        public HandRank Strength { get; }

    }

Open Hand.cs and create a new private method named Rankings that generates a collecton of Ranker.

Use the following code as a starting point, fill in the remaining Ranker items
    private List<Ranker> Rankings() => new List<Ranker>
    {
        new Ranker(cards => HasRoyalFlush(), HandRank.RoyalFlush),
        // more ranks here
    };

Find the GetHandRank method and remove the expression after the =>

The result should be:

    public HandRank GetHandRank() =>

After the => write a new expression that uses the List<Ranker> from Rankings() that evaluates the hand rank.

Hint: think LINQ

<div class="exercise-end"></div>

### Hand rankings, refactored

Review the answers to see how the code can be refactored using functional programming.

<h4 class="exercise-start">
    <b>Exercise</b>: GetHandRank Refactored
</h4>

Open open the folder files/06-Functional-refactor/answers

Open Hand.cs and review the comments

<div class="exercise-end"></div>

### Flexibility

The newest version of GetHandRank can be more flexible than the previous versions. Because it is a collection, it can easily be added to and operated on using LINQ.

<h4 class="exercise-start">
    <b>Exercise</b>: Adding new hand ranks
</h4>

Adding new hand ranks is easy. Simply add a new Ranker to the Rankings collection and the GetHandRank method will do the rest.

Make the GetHandRank method even more robust by allowing Ranks to be added in any order.

    Find the GetHandRank method and order the Rankings by descending order by their Strength

    .OrderByDescending(r => r.Strength)

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
