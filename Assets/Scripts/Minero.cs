using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MinerStates {
    Idle,
    GoToMine,
    Mine,
    GoToCave,
    Store
}

public enum MinerEvents {
    GetDestination,
    ReachedDestination,
    CantHoldMore,
    Empty,
    MineEmpty
}

public class Minero : MonoBehaviour {

    public Node mina;
    public Node cueva;

    FollowPath follow;
    Pathfinder finder;

    Node current = null;
    public bool inMotion = false;

    public Material caveMat, mineMat;

    int ore = 0;
    int oreInCave = 0;
    public int maxOres = 100;
    public int mineTotal = 1000;

    Text heldText, storedText, mineText, stateText;

    FSM myFSM;

    MinerStates myState = MinerStates.Idle;

    BlackboardMinero blackboard;

    void Start() {
        blackboard = GetComponent<BlackboardMinero>();
        follow = gameObject.GetComponent<FollowPath>();
        finder = gameObject.GetComponent<Pathfinder>();
        mina.SetTargetType(TargetType.Mina, mineMat);
        cueva.SetTargetType(TargetType.Cueva, caveMat);
        heldText = GameObject.Find("HeldText").GetComponent<Text>();
        storedText = GameObject.Find("StoredText").GetComponent<Text>();
        mineText = GameObject.Find("MineText").GetComponent<Text>();
        stateText = GameObject.Find("StateText").GetComponent<Text>();

        myFSM = gameObject.AddComponent<FSM>();
        myFSM.stateNum = 5;
        myFSM.eventNum = 5;
        myFSM.Init();
        myFSM.SetRelation((int)MinerEvents.GetDestination, (int)MinerStates.Idle, (int)MinerStates.GoToMine);
        myFSM.SetRelation((int)MinerEvents.ReachedDestination, (int)MinerStates.GoToMine, (int)MinerStates.Mine);
        myFSM.SetRelation((int)MinerEvents.CantHoldMore, (int)MinerStates.Mine, (int)MinerStates.GoToCave);
        myFSM.SetRelation((int)MinerEvents.ReachedDestination, (int)MinerStates.GoToCave, (int)MinerStates.Store);
        myFSM.SetRelation((int)MinerEvents.Empty, (int)MinerStates.Store, (int)MinerStates.GoToMine);
        myFSM.SetRelation((int)MinerEvents.MineEmpty, (int)MinerStates.Mine, (int)MinerStates.GoToCave);
        myFSM.SetRelation((int)MinerEvents.MineEmpty, (int)MinerStates.Store, (int)MinerStates.Idle);

        //for(int i = 0; i < 5; i++) {
        //    for(int j = 0; j < 4; j++) {
        //        Debug.Log(i + "," + j + ": " + myFSM.fsm[i,j]);
        //    }
        //}
    }

	// Update is called once per frame
	void FixedUpdate () {

        ore = blackboard.heldGold;
        oreInCave = blackboard.storedGold;
        mineTotal = blackboard.mineGold;

        heldText.text = ore.ToString();
        storedText.text = oreInCave.ToString();
        mineText.text = mineTotal.ToString();

        if (follow.current != null) {
            current = follow.current;
        }

        //MinerStateMachine();       

        //if(current == mina) {
        //    ore++;
        //}

        //if(current == cueva && ore != 0) {
        //    oreInCave += ore;
        //    ore = 0;
        //}

        //if (follow.ReachedTarget() || current == null) {
        //    if(Input.GetKeyUp(KeyCode.Z) && !inMotion)
        //        GoToMina();

        //    if (Input.GetKeyUp(KeyCode.X) && !inMotion)
        //        GoToCueva();
        //}
    }

    void MinerStateMachine() {
        switch (myFSM.currentState) {
            case (int)MinerStates.Idle:
                if (Input.GetKeyUp(KeyCode.Z) && !inMotion) {
                    myFSM.SetEvent((int)MinerEvents.GetDestination);
                }
            break;

            case (int)MinerStates.GoToMine:

                if(!inMotion)
                    GoTo(mina);

                if (current == mina) {
                    inMotion = false;
                    myFSM.SetEvent((int)MinerEvents.ReachedDestination);
                }
                break;

            case (int)MinerStates.Mine:

                if(mineTotal <= 0)
                    myFSM.SetEvent((int)MinerEvents.MineEmpty);

                if (ore < maxOres) {
                    ore++;
                    mineTotal--;
                }else{
                    myFSM.SetEvent((int)MinerEvents.CantHoldMore);
                }
                break;

            case (int)MinerStates.GoToCave:

                if (!inMotion)
                    GoTo(cueva);

                if (current == cueva) {
                    inMotion = false;
                    myFSM.SetEvent((int)MinerEvents.ReachedDestination);
                }
                break;

            case (int)MinerStates.Store:
                if (ore > 0) {
                    ore--;
                    oreInCave++;
                }else {
                    ore = 0;
                    if(mineTotal > 0)
                        myFSM.SetEvent((int)MinerEvents.Empty);
                    else
                        myFSM.SetEvent((int)MinerEvents.MineEmpty);
                }
                break;
        }
    }

    public void GoToMina() {
        GoTo(mina);
    }

    public void GoToCueva() {
        GoTo(cueva);
    }

    void GoTo(Node n) {
        inMotion = true;
        follow.SetPath(finder.GetPath(current, n));
    }
}
