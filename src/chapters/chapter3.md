## A poker hand

In the previous chapter you created a simple object and used some functional aspects of C#.

In this chapter you'll start with imperative and OOP style programming. Throughout the examples you'll be asked to refactor using functional programming. This will help identify where to find balance between the two styles. As the this guide progresses, instructions will become less detailed so that you can explore your on your own.

Now that you're comfortable with running unit tests, you will not be instructed to run tests after this point, instead feel free to run them as needed. **For example, after every code change.**

### Crate a Hand

The poker hand will be used throughout the workshop. The Hand will represent a player's hand of cards. For this workshop a five card hand will be scored using imperative and functional programming.

<h4 class="exercise-start">
    <b>Exercise</b>: Create the HandTests
</h4>

Create a test class named HandTests in the /Tests folder. Make sure HandTests is public.

Use the following tests to create a Hand class that has a Cards property and a Draw method.

        [Fact]
        public void CanCreateHand()
        {
            var hand = new Hand();
            Assert.Equal(hand.Cards.Any(), false);
        }

        [Fact]
        public void CanHandDrawCard()
        {
            var card = new Card(CardValue.Ace, CardSuit.Spades);
            var hand = new Hand();

            hand.Draw(card);

            Assert.Equal(hand.Cards.First(), card);
        }

Create a Hand class that satisfies the tests.

    public class Hand
    {
        public Hand()
        {
            Cards = new List<Card>();
        }
        public List<Card> Cards { get;}

        public void Draw(Card card)
        {
            Cards.Add(card);
        }

    }

The Hand class will hold a players hand of cards. In the next exercises you will be scoring the Hand of cards from the Cards property in the Hand class.

<div class="exercise-end"></div>

### Getting the high card

In a game where all players hands are a equal in rank, the winner is decided by comparing the highest card in their hands. Add a HighCard method to the Hand that returns the highest CardValue in the hand. 

<h4 class="exercise-start">
    <b>Exercise</b>: Score the high card
</h4>

Below is a test method you can use to validate your HighCard method.

        [Fact]
        public void CanGetHighCard()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
            Assert.Equal(CardValue.King, hand.HighCard().Value);
        }

When this test passes, move on to the next exercise.

<div class="exercise-end"></div>

### Hand rankings

In the previous exercise, you determined the high card. Now add a HandRank GetHandRank method that will return the Hand's HandRank.

<h4 class="exercise-start">
    <b>Exercise</b>: Score the high card
</h4>

Add the HandRank enum to your project.

    public enum HandRank
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

Use the following tests to create a GetHandRank method on the Hand object that will return the correct HandRank for the test. Only score the Ranks below, you'll be refactoring as the workshop progresses and scoring additional HandRanks as you learn about OOP and functional programming.

        [Fact]
        public void CanScoreHighCard()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
            hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
            hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
            Assert.Equal(HandRank.HighCard, hand.GetHandRank());
        }

        [Fact]
        public void CanScoreFlush()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Two, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Three, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Five, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Six, CardSuit.Spades));
            Assert.Equal(HandRank.Flush, hand.GetHandRank());
        }
        [Fact]
        public void CanScoreRoyalFlush()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.Ten, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Jack, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Queen, CardSuit.Spades));
            hand.Draw(new Card(CardValue.King, CardSuit.Spades));
            hand.Draw(new Card(CardValue.Ace, CardSuit.Spades));
            Assert.Equal(HandRank.RoyalFlush, hand.GetHandRank());
        }

When this test passes, move on to the next exercise.

<div class="exercise-end"></div>

### Hand rankings, answers

Now that the HighCard, Flush, and RoyalFlush hand ranks have been scored, review the answers to see how the code can be written using functional programming.

<h4 class="exercise-start">
    <b>Exercise</b>: Answers for HighCard, Flush, RoyalFlush
</h4>

Open open the folder /chapter3/answers/first-pass

Open Hand.cs and review the comments

<div class="exercise-end"></div>

### Hand rankings, refactored

<div class="exercise-end"></div>

Now that the HighCard, Flush, and RoyalFlush hand ranks have been scored, review the answers to see how the code can be refactored using functional programming.

<h4 class="exercise-start">
    <b>Exercise</b>: Answers for HighCard, Flush, RoyalFlush
</h4>

Open open the folder /chapter3/answers/refactored

Open Hand.cs and review the comments

Refactor your own code and make sure all of your tests pass.

<div class="exercise-end"></div>

### Hand Tests refactored

Now it's time to refactor the tests using method chains. Pipelines are often found in functional programming languages. Pipelines allow functions to be chained or composed to produce easily maintainable and readable code.

<h4 class="exercise-start">
    <b>Exercise</b>: Using Fluent Assertions
</h4>

Open /Tests/HandTests.cs

Add a reference to FluentAssertions.

    using FluentAssertions;

Modify the Assert statement to use the FluentAssertions chain instead. To do this, start with the value that will be tested and continue with the method Should().Be(expectedValue)

    Assert.Equal(CardValue.King, hand.HighCard().Value);

becomes

    hand.HighCard().Value.Should().Be(CardValue.King);

Below is a example of the completed CanGetHighCard test

    [Fact]
    public void CanGetHighCard()
    {
        var hand = new Hand();
        hand.Draw(new Card(CardValue.Seven, CardSuit.Spades));
        hand.Draw(new Card(CardValue.Ten, CardSuit.Clubs));
        hand.Draw(new Card(CardValue.Five, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.King, CardSuit.Hearts));
        hand.Draw(new Card(CardValue.Two, CardSuit.Hearts));
    
        hand.HighCard().Value.Should().Be(CardValue.King);
    }

Refactor all tests to use Fluent Assertsions.
       
<div class="exercise-end"></div>