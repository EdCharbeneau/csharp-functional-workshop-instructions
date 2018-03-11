## Creating a unit test

In this chapter you'll learn how to create simple unit tests with xUnit.

### Create a Card test

Throughout this workshop you'll be using poker cards to learn new functional language features. In this exercise you'll create a new folder to hold unit tests and write your first unit test for a poker card. To keep things simple, in this workshop you'll use one project for all of your code. In a real-world app you would probably use a separate test project.

<h4 class="exercise-start">
    <b>Exercise</b>: Create a Card test
</h4>

Add a folder named Tests to your project.

In /Tests Add an empty class file named CardTests.cs

Make sure the CardTests class is defined as **public** so the test runner can see the test class.

    public class CardTests

In CardTests.cs add a new test method named CanCreateCard. Test methods in xUnit use the `[Fact]` attribute. You may need to import xUnit with a `using ` statement, highlight Fact and press ctrl+. (control period), the press enter.

	[Fact]
	public void CanCreateCard() { }

In the CanCreateCard method write a test that confirms that a new Card object can be created.

    var card = new Card();
    Assert.NotNull(card);

There is no class defined yet, so you can consider this the first failing test. In the next exercise you'll create a Card object to satisfy this test.

<div class="exercise-end"></div>

### Create a Card object

In this exercise you'll create a card object.

<h4 class="exercise-start">
    <b>Exercise</b>: Create a Card object
</h4>

In the root folder of your project, create a Card class named Card.cs.

Make the card class public.

    public class Card {}

This should be enough to satisfy the first unit test. Build the project (ctrl+shift+b), open Test  Explorer and click **Run All**. You should receive a green check-mark next to the CanCreateCard test.

![](images/chapter2/test-explorer-pass.jpg)

If the Test Explorer window is not visible, type Test Explorer in the quick launch box.

![](images/chapter2/test-explorer.jpg)

<div class="exercise-end"></div>

### Complete the Card object

A card must be able to represent a Suit and Value to be useful. In the next exercise you'll add some properties to the card and tests to insure a card has a Suit and Value when it is created.

<h4 class="exercise-start">
    <b>Exercise</b>: description
</h4>

Give the card properties to hold a CardValue and CardSuit

	public class Card
    {
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }
    }

Create enums for CardValue and CardSuit

        // CardValue.cs
		public enum CardValue
	    {
	        Two = 2,
	        Three,
	        Four,
	        Five,
	        Six,
	        Seven,
	        Eight,
	        Nine,
	        Ten,
	        Jack,
	        Queen,
	        King,
	        Ace
	    }

        // CardSuit.cs
    	public enum CardSuit
	    {
	        Spades,
	        Diamonds,
	        Clubs,
	        Hearts
	    }

Change the CanCreateCard test so that a Card has a value when it is created, rename the test to CanCreateCardWithValue. Test to make sure the properties are NotNull.

        [Fact]
        public void CanCreateCardWithValue() {

            var card = new Card();

            Assert.NotNull(card.Suit);
            Assert.NotNull(card.Value);
        }

Note that the test will pass even when no value was assigned. This is because enums have a default value.

To insure that a value is intentionally set, add a constructor to the Card that requires a Suit and Value.

    public class Card
    {
        public Card(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }

    }

Change the assertion so that it checks for a predetermined Suit and Value.

	var card = new Card(CardValue.Ace, CardSuit.Clubs);
 
	Assert.Equal(CardSuit.Clubs, card.Suit);
    Assert.Equal(CardValue.Ace, card.Value);

Now when a Card is created it must have its properties set to a value. In the next exercise you'll refactor the Card so that it is immutable, meaning that its properties cannot be changed once the object is created.

Re-run the test to verify that the test passes.

If you're using **Visual Studio 2017 Enterprise edition**. You can save time by enabling Live Unit Testing.

![](images/chapter2/enable-live-ut.jpg)

<div class="exercise-end"></div>

### Describing a Card

One positive aspect of C# programming is that both functional and OOP styles of programming are not mutually exclusive. In this exercise you'll override the inherited ToString method of the Card object and use it to describe the Card's values.

<h4 class="exercise-start">
    <b>Exercise</b>: Override the inherited ToString method
</h4>

Create a new test CanDescribeCard, this test should test a Card with the values of `CardValue.Ace` and `CardSuit.Spades`. The ToString method should return `"Ace of Spades"`.

	[Fact]
    public void CanDescribeCard()
    {
  		var card = new Card(CardValue.Ace, CardSuit.Spades);
		  
		  Assert.Equal("Ace of Spades", card.ToString());

    }

Run the test to verify that it fails.

Next, update the Card object to make the test pass. Override the ToString method on the Card class and utilize the string interpolation syntax to write out the Card's description.

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

Re-run the test to verify that the test passes.

<div class="exercise-end"></div>

### Describing a Card, refactor

The ToString method is a simple, single line of code that returns a value. In this exercise you'll use an expression-bodied member to reduce the amount of code further into a concise expression.

<h4 class="exercise-start">
    <b>Exercise</b>: Convert ToString to an expression-bodied member
</h4>

Find the ToString method of the Card class. Remove the braces {} from the method, and replace the `return` statement with a lambda arrow =>. The arrow implies that the method will return a value, and the result is a much simpler syntax. 

This will reduce the method to a simple one line statement. 

        public override string ToString() => $"{Value} of {Suit}";

Re-run the test to verify that the test passes.

<div class="exercise-end"></div>


### Immutable Card object

In functional programming immutable objects are used to reduce complexity and avoid unintended changes in state. An immutable object's state cannot be modified after it is created, lowering the risk of side-effects.

<h4 class="exercise-start">
    <b>Exercise</b>: Modify the Card to make it immutable
</h4>

In C# 6 or higher, backing fields and explicit readonly property declarations are not needed. By simply removing the `set` operator from a property will make it a read-only property.

In the Card class, remove the `set` declarations from each property.

    public class Card
    {
        public Card(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public CardValue Value { get; }
        public CardSuit Suit { get; }

        public override string ToString() => $"{Value} of {Suit}";
    }

Now the properties can only be set during the object's initialization.

Re-run the test to verify that the test passes.

<div class="exercise-end"></div>

In this chapter you created a simple object and used some functional aspects of C#. By using expression bodied-members, the ToString method was reduced to a single expression. The Card class was made immutable by using the objects constructor and removing the `set` declarations from the object. In the Card object, both OOP and functional programming were used in a single class.

This chapter also outlined the basics of writing and running unit tests. Now that you're comfortable with running unit tests, you will not be instructed to run tests after this point, instead feel free to run them as needed throughout the remainder of the next chapters.