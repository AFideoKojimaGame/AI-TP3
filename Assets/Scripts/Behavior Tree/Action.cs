using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : Leaf {

    DoAction myAction;

    public Action(DoAction a) {
        myAction = a;
    }

    public void SetAction(DoAction a) {
        myAction = a;
    }

    public override string NodeName() {
        string name = "Action";
        if (parent != null)
            name += childNumber.ToString();

        return name;
    }

    public override BTStates ExecuteAction() {
        return myAction.Invoke();
    }
}
