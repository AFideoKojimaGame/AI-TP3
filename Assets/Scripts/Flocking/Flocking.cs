using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour {

    List<Boid> flock = new List<Boid>();
    Transform target;

	// Use this for initialization
	void Start () {
        GameObject[] boidobjs = GameObject.FindGameObjectsWithTag("Boid");
        if(boidobjs.Length > 0) {
            for(int i = 0; i < boidobjs.Length; i++) {
                AddBoid(boidobjs[i].GetComponent<Boid>());
            }
        }

        target = GameObject.Find("Target").transform;
        SetTarget(target.position);
    }
	
	// Update is called once per frame
	void Update () {
        SetTarget(target.position);
        Adjacents();
	}

    void SetTarget(Vector3 target) {
        for(int i = 0; i < flock.Count; i++) {
            flock[i].target = target;
        }
    }

    public void AddBoid(Boid b) {
        flock.Add(b);
    }

    void Adjacents() {
        if(flock.Count > 0) {
            Boid a;
            Boid b;
            float dist;

            for (int h = 0; h < flock.Count; h++) {
                flock[h].ClearNeighbors();
            }

            for (int i = 0; i < flock.Count; i++) {
                a = flock[i];
                for (int j = 0; j < flock.Count; j++) {
                    b = flock[j];
                    dist = Vector3.Distance(a.transform.position, b.transform.position);
                    if(dist < 100) {
                        a.AddNeighbor(b);
                    }
                }
            }
        }
    }
}
