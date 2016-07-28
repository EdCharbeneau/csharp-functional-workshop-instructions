using CsharpPoker;
using System.Collections.Concurrent;
using System.Collections.Generic;

public static class EvalExtensions {

    // Extension methods are an easy way to create a pipeline.
    // The ToPairs extension method is a specialized mapping method to convert from IEnumerable<Card> to IEnumerable<KeyValuePair<CardValue, int>> (cards, to counted pairs)
    // LINQ is a great example of how effective extension methods can be.
    public static IEnumerable<KeyValuePair<CardValue, int>> ToPairs(this IEnumerable<Card> cards)
    {
        var dict = new ConcurrentDictionary<CardValue, int>();
        foreach (var card in cards)
        {
            dict.AddOrUpdate(card.Value, 1, (cardValue, quantity) => ++quantity);
        }
        return dict;
    }
}