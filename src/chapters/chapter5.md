## Finding Sequences

In the previous chapter you solved more hand ranks using higher order functions and refactored with an extension method to create a method chain.

In this chapter you'll continue with imperative and OOP style programming. Throughout the examples you'll be asked to refactor using functional programming. Later in this chapter, you'll learn about working with the Yeild operator in C#.

Feel free to run tests as needed. **For example, after every code change.**

### Scoring a Straight

Continue to update the poker hand used throughout the workshop.

<h4 class="exercise-start">
    <b>Exercise</b>: Score a Straight
</h4>

        [Fact]
        public void CanScoreStraight()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Queen, CardSuit.Spades));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));

            hand.GetHandRank().Should().Be(HandRank.Straight);
        }

        [Fact]
        public void CanScoreStraightUnordered()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Queen, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));

            hand.GetHandRank().Should().Be(HandRank.Straight);
        }

When this tests pass, move on to the next exercise.

<div class="exercise-end"></div>

### Hand rankings, answers

Now that the Straight hand rank has been scored, review the answers to see how the code can be written using functional programming.

<h4 class="exercise-start">
    <b>Exercise</b>: Answer for Straight
</h4>

Open open the folder files/05-Sequences/answers/first-pass

Open Hand.cs and review the comments

<div class="exercise-end"></div>

### Hand rankings, refactored

Now that the Straight hand rank has been scored, review the answers to see how the code can be refactored using functional programming.

<h4 class="exercise-start">
    <b>Exercise</b>: Answers for Straight
</h4>

Open open the folder files/05-Sequences/answers/refactored

Open Hand.cs and review the comments
Open EvalExtensions.cs and review the comments

<div class="exercise-end"></div>