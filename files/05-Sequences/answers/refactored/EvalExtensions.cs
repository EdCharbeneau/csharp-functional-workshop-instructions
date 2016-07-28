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

    /* 

    The SelectConsecutive method iterates over two consecutive items in a collection.
    This is done using the yeild keyword
    Each call to the iterator function proceeds to the next execution of the yield return statement
    This method is very similar to the source code found in LINQ methods

    */

    public static IEnumerable<TResult> SelectConsecutive<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> selector)
    {
        int index = -1;
        foreach (TSource element in source.Take(source.Count() - 1)) // skip the last, it will be evaluated by source.ElementAt(index + 1)
        {
            checked { index++; } // explicitly enable overflow checking
            yield return selector(element, source.ElementAt(index + 1)); // delegate element and element[+1] to Func<TSource, TSource, TResult>
        }
    }
}