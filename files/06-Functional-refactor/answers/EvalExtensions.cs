using CsharpPoker;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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

    public static IEnumerable<TResult> SelectConsecutive<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> selector)
    {
        int index = -1;
        foreach (TSource element in source.Take(source.Count() - 1))
        {
            checked { index++; }
            yield return selector(element, source.ElementAt(index + 1));
        }
    }
}