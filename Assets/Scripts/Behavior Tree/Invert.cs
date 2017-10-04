using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invert : Decorator {

    public override string NodeName() {
        string name = "Invert";
        if (parent != null)
            name = "Sequencer" + childNumber + " Child of: " + parent.NodeName();
        return name;
    }

    public override void Awake() {
        currentState = BTStates.Running;
    }

    public override BTStates ExecuteAction() {

        if(children.Count > 0) {
            switch (children[0].ExecuteAction()) {
                case BTStates.None:
                    children[0].Awake();
                    currentState = BTStates.None;
                    break;

                case BTStates.Running:
                    currentState = BTStates.Running;
                    break;

                case BTStates.False:
                    currentState = BTStates.True;
                    break;

                case BTStates.True:
                    currentState = BTStates.True;
                    break;
            }
        }else {
            currentState = BTStates.None;
        }

        return currentState;
    }
}
