using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : TreeNode {

    int index = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public override string NodeName() {
        string name = "Sequencer";
        if(parent != null)
            name = "Sequencer" + childNumber + " Child of: " + parent.NodeName();
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

        for(int i = index; i < children.Count; i++) {
            switch (children[i].ExecuteAction()) {
                case BTStates.None:
                    children[i].Awake();
                break;

                case BTStates.Running:
                    return BTStates.Running;
                break;

                case BTStates.False:
                    currentState = BTStates.None;
                    return BTStates.False;
                break;

                case BTStates.True:
                    index++;
                break;
            }
        }

        currentState = BTStates.None;
        return BTStates.True;
    }
}
