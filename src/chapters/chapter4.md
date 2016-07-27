## Finding Pairs

In the previous chapter you solved a few hand ranks using LINQ and refactored tests with method chains with the help of FluentAssertions.

In this chapter you'll continue with imperative and OOP style programming. Throughout the examples you'll be asked to refactor using functional programming. Later in this chapter, you'll learn about working with concurrency in C#.

Feel free to run tests as needed. **For example, after every code change.**

### Scoring Pairs

Continue to update the poker hand used throughout the workshop.

<h4 class="exercise-start">
    <b>Exercise</b>: Score pairs (of a kind) hand ranks
</h4>

        [Fact]
        public void CanScorePair()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            hand.GetHandRank().Should().Be(HandRank.Pair);
        }

        [Fact]
        public void CanScoreThreeOfAKind()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Nine, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.GetHandRank().Should().Be(HandRank.ThreeOfAKind);
        }
        [Fact]
        public void CanScoreFourOfAKind()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.GetHandRank().Should().Be(HandRank.FourOfAKind);

        }
        [Fact]
        public void CanScoreFullHouse()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.GetHandRank().Should().Be(HandRank.FullHouse);

        }

When this tests pass, move on to the next exercise.

<div class="exercise-end"></div>

### Hand rankings, answers

Now that the Pairs (of a kind) hand ranks have been scored review the answers to see how the code can be written using functional programming.

<h4 class="exercise-start">
    <b>Exercise</b>: Answers for Pairs (of a kind), FullHouse
</h4>

Open open the folder /chapter4/answers/first-pass

Open Hand.cs and review the comments

<div class="exercise-end"></div>

### Hand rankings, answers

Now that the Pairs (of a kind) hand ranks have been scored review the answers to see how the code can be refactored using functional programming.

<h4 class="exercise-start">
    <b>Exercise</b>: Answers for Pairs (of a kind), FullHouse
</h4>

Open open the folder /chapter4/answers/refactored

Open Hand.cs and review the comments
Open EvalExtensions.cs and review the comments

<div class="exercise-end"></div>