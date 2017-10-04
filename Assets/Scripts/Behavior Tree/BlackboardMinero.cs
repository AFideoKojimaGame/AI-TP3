using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool Condition();
public delegate BTStates DoAction();

public class BlackboardMinero : MonoBehaviour {

    public Minero theMiner;
    public Pathfinder pf;
    public Node mina;
    public Node cueva;
    public FollowPath follower;
    public int mineGold = 1000;
    public int heldGold = 0;
    public int storedGold = 0;
}
