using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : TreeNode {

    private void Start() {
    }

    public override bool CanHaveChilden() {
        return false;
    }
}
