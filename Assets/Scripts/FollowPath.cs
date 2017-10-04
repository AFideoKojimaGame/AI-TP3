using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    List<Node> myPath;

    float moveSpeed = 3;

    bool reachedTarget = false;
    int i = 0;

    public Node current = null;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(myPath != null && !reachedTarget){
            Follow();
        }
    }

    void Follow() {

        transform.position = Vector3.MoveTowards(transform.position, myPath[i].transform.position, Time.deltaTime * moveSpeed);
        if(transform.position.x > myPath[i].transform.position.x - 0.01f && transform.position.x < myPath[i].transform.position.x + 0.01f &&
            transform.position.y > myPath[i].transform.position.y - 0.01f && transform.position.y < myPath[i].transform.position.y + 0.01f) {
            transform.position = myPath[i].transform.position;
            current = myPath[i];
            i--;
            if(i < 0) {
                reachedTarget = true;
                myPath = null;
            }
        }
    }

    public void SetPath(List<Node> mp) {
        myPath = mp;
        i = myPath.Count - 1;
        reachedTarget = false;
    }

    public bool ReachedTarget() {
        return reachedTarget;
    }
}
