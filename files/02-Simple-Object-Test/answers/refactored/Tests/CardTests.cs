﻿using Xunit;

namespace CsharpPoker
{
    public class CardTests
    {

        [Fact]
        public void CanCreateCardWithValue()
        {

            var card = new Card(CardValue.Ace, CardSuit.Spades);

            Assert.Equal(CardSuit.Spades, card.Suit);
            Assert.Equal(CardValue.Ace, card.Value);
        }

        [Fact]
        public void CanCreateDescribeCard()
        {

            var card = new Card(CardValue.Ace, CardSuit.Spades);

            Assert.Equal("Ace of Spades", card.ToString());

        }
    }
}
