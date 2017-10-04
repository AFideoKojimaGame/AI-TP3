using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditional : Leaf {

    Condition myCondition;

    public Conditional(Condition c) {
        myCondition = c;
    }

    public void SetCondition(Condition c) {
        myCondition = c;
    }

    public override string NodeName() {
        string name = "Conditional";
        if (parent != null)
            name += childNumber.ToString();

        return name;
    }

    public override BTStates ExecuteAction() {
        if (myCondition.Invoke())
            return BTStates.True;
        else
            return BTStates.False;
    }
}