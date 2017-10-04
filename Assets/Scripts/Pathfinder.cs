using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathFind {
    BreadthFirst,
    DepthFirst,
    Dijkstra,
    AStar
}

public class Pathfinder : MonoBehaviour {

    public PathFind algorithm = 0;

    public Node initialNode;
    public Node destination;

    List<Node> openNodes = new List<Node>();
    List<Node> closedNodes = new List<Node>();
    List<Node> path = new List<Node>();

    Queue<Node> breadth = new Queue<Node>();
    Stack<Node> depth = new Stack<Node>();

    Node currentNode;

    public Material pathMat;
    public Material startMat;
    public Material targetMat;


    // Use this for initialization
    void Start () {}

    public List<Node> GetPath() { 
        while(openNodes.Count > 0) {
            currentNode = VisitNode();
            if(currentNode == destination) {
                CreatePath(currentNode);
                //Debug.Log(currentNode);
                return path;
            }else {
                CloseNode(currentNode);
                OpenAdjacent(currentNode);
            }
        }

        return null;
    }

    public List<Node> GetPath(Node start, Node end) {
        if (start != null)
            initialNode = start;
        else {
            initialNode = GameObject.Find("Cube1").GetComponent<Node>();
        }
        destination = end;

        for (int i = 0; i < openNodes.Count; i++) {
            openNodes[i].DeleteParent();
        }

        openNodes.Clear();

        for (int i = 0; i < closedNodes.Count; i++) {
            closedNodes[i].DeleteParent();
        }

        closedNodes.Clear();

        for (int i = 0; i < path.Count; i++) {
            path[i].DeleteParent();
        }

        path.Clear();

        openNodes = new List<Node>();
        closedNodes = new List<Node>();
        path = new List<Node>();

        OpenNode(initialNode);
        if(destination.GetTargetType() == TargetType.Normal)
            destination.GetComponent<MeshRenderer>().material = targetMat;
        return GetPath();
    }

    Node VisitNode() {

        Node n = null;

        switch (algorithm) {
            case PathFind.BreadthFirst:
                n = breadth.Dequeue();
                openNodes.RemoveAt(openNodes.Count - 1);
            break;

            case PathFind.DepthFirst:
                n = depth.Pop();
                openNodes.RemoveAt(0);
            break;

            case PathFind.Dijkstra:

                n = openNodes[0];

                for (int i = 0; i < openNodes.Count; i++) {
                    if (openNodes[i].weight < n.weight) {
                        n = openNodes[i];
                    }
                }

                openNodes.Remove(n);
                break;

            case PathFind.AStar:
                n = openNodes[0];
                n.SetDestination(destination);
                for (int i = 0; i < openNodes.Count; i++) {
                    openNodes[i].SetDestination(destination);
                    if ((openNodes[i].weight + openNodes[i].hx) < (n.weight + n.hx)) {
                        n = openNodes[i];
                    }
                    else if ((openNodes[i].weight + openNodes[i].hx) == (n.weight + n.hx)) {
                        if(openNodes[i].hx < n.hx) {
                            n = openNodes[i];
                        }
                    }
                }

                openNodes.Remove(n);
            break;
        }

        return n;
    }

    void OpenNode(Node n) {
        if (n != initialNode) {
            if (n.GetParent() == null) {
                n.SetParent(currentNode);
            }
        }
        n.alreadyOpened = true;
        //openNodes.Add(n);
        switch (algorithm) {
            case PathFind.BreadthFirst:
                breadth.Enqueue(n);
                openNodes.Add(n);
            break;

            case PathFind.DepthFirst:
                depth.Push(n);
                openNodes.Add(n);
            break;

            case PathFind.Dijkstra:
                openNodes.Add(n);
            break;

            case PathFind.AStar:
                openNodes.Add(n);
            break;
        }
    }

    void CloseNode(Node n) {
        closedNodes.Add(n);
    }

    void OpenAdjacent(Node n) {
        List<Node> adj = n.GetAdjacent();
        for (int i = 0; i < adj.Count; i++) {
            if(algorithm < PathFind.Dijkstra) {
                if(!adj[i].alreadyOpened && !adj[i].isWall) {
                    OpenNode(adj[i]);
                }
            }else
                if (!adj[i].alreadyOpened && !adj[i].isWall) {
                    OpenNode(adj[i]);
                    if (algorithm == PathFind.AStar)
                        n.SetDestination(destination);
                }
        }
    }

    void CreatePath(Node n) {
        Node p;
        path.Add(n);
        p = n.GetParent();
        if (p != null) {
            if(p.GetTargetType() == TargetType.Normal)
                p.GetComponent<MeshRenderer>().material = pathMat;
            Debug.Log(n.name + " Parent: " + p.name);
            CreatePath(p);
        }
    }
}
