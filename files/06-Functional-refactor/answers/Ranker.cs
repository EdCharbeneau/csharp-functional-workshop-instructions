using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpPoker
{
    public class Ranker
    {
        public Ranker(Func<IEnumerable<Card>, bool> eval, HandRank strength)
        {
            Eval = eval;
            Strength = strength;
        }

        // Holds a delegate responsible for evaluating a hand rank

        public Func<IEnumerable<Card>, bool> Eval { get; } 

        // The hand rank for the given eval delegate
        public HandRank Strength { get; }

    }
}