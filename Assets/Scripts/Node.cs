using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType{
    Normal,
    Mina,
    Cueva
}

public class Node : MonoBehaviour {

    Node parent = null;
    List<Node> adjNodes = new List<Node>();

    public bool isWall = false;
    public bool alreadyOpened = false;

    public Node left;
    public Node up;
    public Node right;
    public Node down;

    public float weight = 0;
    public float hx = 0;
    public float moveval = 0;

    public Material wallMat;
    public Material normalMat;

    MeshRenderer myMR;

    TargetType tt;

    float baseweight;

	// Use this for initialization
	void Start () {

        baseweight = weight;

        myMR = gameObject.GetComponent<MeshRenderer>();
        normalMat = myMR.material;

        if (isWall)
            myMR.material = wallMat;

        if (left)
            AddAdjacent(left);

        if (right)
            AddAdjacent(right);

        if (up)
            AddAdjacent(up);

        if (down)
            AddAdjacent(down);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    public List<Node> GetAdjacent() {
        return adjNodes;
    }

    public void AddAdjacent(Node n) {
        adjNodes.Add(n);
    }

    public void SetParent(Node n) {
        parent = n;
        weight = baseweight + parent.weight;
    }

    public void SetDestination(Node n) {
        hx = Vector3.Distance(n.transform.position, transform.position);
    }

    public Node GetParent() {
        return parent;
    }

    public void DeleteParent() {
        parent = null;
        alreadyOpened = false;
        if(tt == TargetType.Normal)
            myMR.material = normalMat;
        weight = baseweight;
    }

    public TargetType GetTargetType() {
        return tt;
    }

    public void SetTargetType(TargetType tar, Material m) {
        tt = tar;
        if (myMR)
            myMR.material = m;
        else
            GetComponent<MeshRenderer>().material = m;
    }
}
