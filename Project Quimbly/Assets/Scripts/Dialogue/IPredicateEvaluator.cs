using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectQuimbly.Dialogue
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(ConditionPredicate predicate, List<string> parameters);
    }
}
