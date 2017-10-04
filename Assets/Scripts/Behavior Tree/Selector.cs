using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : TreeNode {

    int index = 0;

    public override string NodeName() {
        string name = "Selector";
        if (parent != null)
            name += childNumber.ToString();

        return name;
    }

    public override void Awake() {
        index = 0;
        currentState = BTStates.Running;
    }

    public override BTStates ExecuteAction() {

        if (currentState == BTStates.None)
            Awake();

        currentState = BTStates.Running;

        for (int i = index; i < children.Count; i++) {
            switch (children[i].ExecuteAction()) {
                case BTStates.None:
                    children[i].Awake();
                    break;

                case BTStates.Running:
                    return BTStates.Running;
                    break;

                case BTStates.False:
                    index++;
                    break;

                case BTStates.True:
                    currentState = BTStates.None;
                    return BTStates.False;
                    break;
            }
        }

        currentState = BTStates.None;
        return BTStates.True;
    }
}
