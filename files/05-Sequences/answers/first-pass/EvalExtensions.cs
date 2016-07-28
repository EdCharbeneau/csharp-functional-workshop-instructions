using CsharpPoker;
using System.Collections.Concurrent;
using System.Collections.Generic;

public static class EvalExtensions {

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