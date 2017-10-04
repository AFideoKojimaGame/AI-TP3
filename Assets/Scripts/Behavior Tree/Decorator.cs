using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : TreeNode {
    public override bool CanHaveChilden() {
        if (children.Count > 1)
            return false;
        else
            return true;
    }
}
