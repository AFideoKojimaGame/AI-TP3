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

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {

        ComputeVelocity();

        dist = Vector3.Distance(transform.position, target);
        Debug.Log(dist);
        if (dist > 1) {
            transform.Translate(Vector2.up * Time.deltaTime * 5);
            direction = target - transform.position;
            direction.x += vel.x;
            direction.y += vel.y;
            //transform.rotation = Quaternion.Slerp(transform.rotation,
            //                                      Quaternion.LookRotation(direction, Vector3.forward),
            //                                      0.05f);

            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;


            transform.rotation = Quaternion.Slerp(transform.rotation,
                                  Quaternion.AngleAxis(angle, transform.forward),
                                  0.05f);
        }
    }

    public void AddNeighbor(Boid b) {
        adjacentBoids.Add(b);
    }

    public void ClearNeighbors() {
        adjacentBoids.Clear();
        //adjacentBoids = null;
        //adjacentBoids = new List<Boid>();
    }

    public void ComputeVelocity() {
        Vector3 alignment = ComputeAlignment();
        Vector3 cohesion = ComputeCohesion();
        Vector3 separation = ComputeSeparation();

        vel.x = (cohesion.x + separation.x + alignment.x) / 3;
        vel.y = (cohesion.y + separation.y + alignment.y) / 3;

        vel.Normalize();
    }

    Vector3 ComputeAlignment() {
        Vector3 v = Vector3.zero;
        for(int i = 0; i < adjacentBoids.Count; i++) {
            v.x += adjacentBoids[i].vel.x;
            v.y += adjacentBoids[i].vel.y;
        }

        if (adjacentBoids.Count > 0) {
            v.x /= adjacentBoids.Count;
            v.y /= adjacentBoids.Count;
        }

        v.Normalize();

        return v;
    }

    Vector3 ComputeCohesion() {
        Vector3 v = Vector3.zero;
        for (int i = 0; i < adjacentBoids.Count; i++) {
            v.x += adjacentBoids[i].transform.position.x;
            v.y += adjacentBoids[i].transform.position.y;
        }

        if (adjacentBoids.Count > 0) {
            v.x /= adjacentBoids.Count;
            v.y /= adjacentBoids.Count;
        }

        v.x = v.x - transform.position.x;
        v.y = v.y - transform.position.y;

        v.Normalize();

        return v;
    }

    Vector3 ComputeSeparation() {
        Vector3 v = Vector3.zero;
        for (int i = 0; i < adjacentBoids.Count; i++) {
            v.x += adjacentBoids[i].transform.position.x - transform.position.x;
            v.y += adjacentBoids[i].transform.position.y - transform.position.y;
        }

        if (adjacentBoids.Count > 0)
        {
            v.x /= adjacentBoids.Count;
            v.y /= adjacentBoids.Count;
        }

        v.x *= -1;
        v.y *= -1;

        v.Normalize();

        return v;
    }
}
