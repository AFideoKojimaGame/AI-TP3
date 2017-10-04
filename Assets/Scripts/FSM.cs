using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour {

    public int[,] fsm;
    public int stateNum = 4;
    public int eventNum = 4;
    public int currentState = 0;
	
	public void Init() {
        fsm = new int[stateNum, eventNum];

        for (int i = 0; i < stateNum; i++) {
            for (int j = 0; j < eventNum; j++) {
                fsm[i, j] = -1;
            }
        }
    }

    public void SetEvent(int e) {
        //if (e < fsm.GetLength(1)) {
            if (fsm[currentState, e] != -1)
                currentState = fsm[currentState, e];
        //}
    }

    public void SetRelation(int e, int s, int changeTo) {
        fsm[s, e] = changeTo;
    }

    public int GetState() {
        return currentState;
    }
}
