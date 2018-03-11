namespace CsharpPoker
{
    public class Card
    {
        public Card(CardValue value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }
}