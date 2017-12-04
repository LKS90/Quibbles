using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public float height;
    public int limit;
    private GameObject follower;
    private CounterScript cnt;
    private System.Random rnd;
    private Renderer rend;

    // Use this for initialization
    void Start ()
    {
        follower = Resources.Load("Follower/Follower") as GameObject;
        
        cnt = GameObject.FindGameObjectWithTag("Counter").GetComponent<CounterScript>();
        rnd = new System.Random();
        rend = GetComponent<Renderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (cnt.GetCount() < limit)
            {
                for (int i = 0; i < (limit - cnt.GetCount()) / 100 + 1; i++)
                {
                    Vector3 spawnPosition = this.transform.position + new Vector3(rnd.Next(1, 5), height, rnd.Next(1, 5)) + new Vector3(-2.5f, 0, -2.5f);
                    GameObject go = (GameObject)Instantiate(follower, spawnPosition, this.transform.rotation);
                    cnt.Add(go);
                }
                rend.material.SetColor("_Color", Color.green);
            }
        }

    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if (cnt.GetCount() == limit)
        {
            rend.material.SetColor("_Color", Color.red);
        }
        else
        {
            rend.material.SetColor("_Color", Color.white);
        }
    }

    internal static void SetColorWhite(Renderer rend)
    {
        rend.material.SetColor("_Color", Color.white);
    }

    // Update is called once per frame
    void Update ()
    {
    }
}
