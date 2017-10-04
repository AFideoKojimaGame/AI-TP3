using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBehaviorTree : MonoBehaviour {

    BlackboardMinero blackboard;
    Selector root = new Selector();

	// Use this for initialization
	void Start () {
        blackboard = GetComponent<BlackboardMinero>();
        root.SetBlackboard(blackboard);
        root.AddChild(new Sequencer());
        root.AddChild(new Sequencer());
        root.GetChild(0).AddChild(new Conditional(HaveSpace));
        root.GetChild(0).AddChild(new Action(GoToMine));
        root.GetChild(0).AddChild(new Conditional(GoldInMine));
        root.GetChild(0).AddChild(new Action(MineGold));
        root.GetChild(1).AddChild(new Conditional(HaveGold));
        root.GetChild(1).AddChild(new Action(GoToCave));
        root.GetChild(1).AddChild(new Action(StoreGold));
    }

    void Update() {
        root.ExecuteAction();
    }

    public bool GoldInMine() {
        if (blackboard.mineGold > 0)
            return true;

        return false;
    }

    public bool HaveSpace() {
        if (blackboard.heldGold < 100)
            return true;

        return false;
    }

    public bool HaveGold() {
        if (blackboard.heldGold > 0)
            return true;

        return false;
    }

    public BTStates StoreGold() {
        if (HaveGold()) {
            blackboard.heldGold-=5;
            blackboard.storedGold+=5;
            return BTStates.Running;
        }
        else {
            blackboard.heldGold = 0;
            return BTStates.True;
        }
    }

    public BTStates MineGold() {
        if (HaveSpace()) {
            blackboard.heldGold+=5;
            blackboard.mineGold-=5;
            return BTStates.Running;
        }
        else {
            return BTStates.True;
        }
    }

    public BTStates GoToMine() {
        if (blackboard.theMiner.inMotion) {
            if (blackboard.follower.ReachedTarget()) {
                blackboard.theMiner.inMotion = false;
                return BTStates.True;
            }

            return BTStates.Running;
        }else {
            blackboard.theMiner.GoToMina();
            return BTStates.Running;
        }
    }

    public BTStates GoToCave() {
        if (blackboard.theMiner.inMotion) {
            if (blackboard.follower.ReachedTarget()) {
                blackboard.theMiner.inMotion = false;
                return BTStates.True;
            }

            return BTStates.Running;
        }else {
            blackboard.theMiner.GoToCueva();
            return BTStates.Running;
        }
    }
}
