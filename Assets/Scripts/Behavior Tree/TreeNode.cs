using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BTStates {
    None,
    Running,
    True,
    False
}

public class TreeNode  {

    protected TreeNode parent;
    protected List<TreeNode> children = new List<TreeNode>();
    public int childCount = 0;
    public int childNumber = -1;
    protected BTStates currentState = BTStates.None;
    public BlackboardMinero blackboard;

    // Update is called once per frame
    public void AddChild (TreeNode n) {
        if (CanHaveChilden()) {
            children.Add(n);
            n.SetParent(this);
            childCount++;
            n.childNumber = childCount;
            n.SetBlackboard(blackboard);
        }
    }

    public TreeNode GetChild(int index) {
        if (index >= children.Count || !CanHaveChilden())
            return null;

        return children[index];
    }

    protected void SetParent(TreeNode n) {
        parent = n;
    }

    public virtual bool CanHaveChilden() { return true; }

    public virtual string NodeName() { return "TreeNode"; }

    public void ShowTree() {
        string myName = NodeName();
        if (parent != null)
            myName += " Child Of: " + parent.NodeName();

        if (CanHaveChilden() && childCount > 0) {
            for (int i = 0; i < childCount; i++)
            {
                children[i].ShowTree();
            }
        }else
            myName += " - NO CHILDREN -";

        Debug.Log(myName);
    }

    public virtual BTStates ExecuteAction() {

        for(int i = 0; i < children.Count; i++) {
            children[i].ExecuteAction();
        }

        return BTStates.Running;
    }

    public virtual void Awake() {
        currentState = BTStates.Running;
    }

    public BTStates GetState() {
        return currentState;
    }

    public void SetBlackboard(BlackboardMinero b) {
        blackboard = b;
    }
}
