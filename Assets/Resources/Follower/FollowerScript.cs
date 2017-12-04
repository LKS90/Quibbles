using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour {

    public GameObject controller;
    public float speed;
    public float attractionRadius;
    private Rigidbody rb;
    private CounterScript cnt;
    private GameObject target;
    private GameObject goal;
    private CollectorScript cs;
    private Renderer rendSpawner;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        cnt = GameObject.FindGameObjectWithTag("Counter").GetComponent<CounterScript>();
        target = GameObject.FindGameObjectWithTag("Player");
        goal = GameObject.FindGameObjectWithTag("Collector");
        cs = GameObject.FindGameObjectWithTag("Collector").GetComponent<CollectorScript>();
        rendSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        float distanceGoal = Vector3.Distance(goal.transform.position, this.transform.position);
        if (distanceGoal < 5)
            target = goal;
        float distance = Vector3.Distance(target.transform.position, this.transform.position);
        Vector3 direction = (target.transform.position - this.transform.position);
        if (distance <= attractionRadius)
        {
            rb.AddForce(speed * direction * ((distance*distance * 0.3f) + 1));
        }

        if (this.transform.position.y <= -1 || distanceGoal < .65)
        {
            if (distanceGoal <= .65)
                cs.raiseScore();
            Destroy(this.gameObject, 0);
            cnt.Remove(this.gameObject);
            SphereSpawner.SetColorWhite(rendSpawner);
        }		
	}
}
