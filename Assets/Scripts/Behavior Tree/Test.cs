using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    TreeNode myTR = new TreeNode();

	// Use this for initialization
	void Start () {
        Sequencer s = new Sequencer();
        myTR.AddChild(s);
        myTR.GetChild(0).AddChild(new Conditional());
        myTR.GetChild(0).AddChild(new Conditional());
        myTR.GetChild(0).AddChild(new Conditional());
        myTR.AddChild(new Selector());
        myTR.GetChild(1).AddChild(new Action());
        myTR.ShowTree();
    }
}
