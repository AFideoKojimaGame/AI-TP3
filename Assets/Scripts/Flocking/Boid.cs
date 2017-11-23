using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    public Vector3 target;

    Vector3 direction;

    public float range = 2;
    float dist;

    List<Boid> adjacentBoids = new List<Boid>();

    public Vector3 vel = Vector3.zero;

    float separationWeight = 1;
    float alignmentWeight = 1;
    float cohesionWeight = 1;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {

        ComputeVelocity();

        dist = Vector3.Distance(transform.position, target);

            Vector2 res = transform.up + vel.normalized;
            transform.Translate(res * Time.deltaTime * 5, Space.World);

            direction = target - transform.position;

            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                  Quaternion.AngleAxis(angle, transform.forward),
                                  Time.deltaTime * 5);
        
    }

    public void AddNeighbor(Boid b) {
        adjacentBoids.Add(b);
    }

    public void ClearNeighbors() {
        adjacentBoids.Clear();
    }

    public void ComputeVelocity() {
        Vector3 alignment = ComputeAlignment();
        Vector3 cohesion = ComputeCohesion();
        Vector3 separation = ComputeSeparation();

        vel = (cohesion * cohesionWeight + separation * separationWeight + alignment * alignmentWeight) / 3;
    }

    Vector3 ComputeAlignment() {
        Vector3 v = Vector3.zero;
        for(int i = 0; i < adjacentBoids.Count; i++) {
            v += adjacentBoids[i].transform.up;
        }

        if (adjacentBoids.Count > 0) {
            v /= adjacentBoids.Count;
        }

        v.Normalize();

        return v;
    }

    Vector3 ComputeCohesion() {
        Vector3 v = Vector3.zero;
        for (int i = 0; i < adjacentBoids.Count; i++) {
            v += adjacentBoids[i].transform.position;
        }

        if (adjacentBoids.Count > 0) {
            v /= adjacentBoids.Count;
        }

        v -= transform.position;

        v.Normalize();

        return v;
    }

    Vector3 ComputeSeparation() {
        Vector3 v = Vector3.zero;
        for (int i = 0; i < adjacentBoids.Count; i++) {
            v += adjacentBoids[i].transform.position - transform.position;
        }

        if (adjacentBoids.Count > 0) {
            v /= adjacentBoids.Count;
        }

        v *= -1;

        v.Normalize();

        return v;
    }
}
