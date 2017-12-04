using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    public GameObject controller;
    private Text followerCountLabel;
    private List<GameObject> instances;
    private string text = "Score: {0}\tTime Left: {1}:{2:00}";
    private CollectorScript cs;
    private ControllerScript cntrllr;

    // Use this for initialization
    void Start ()
    {
        instances = new List<GameObject>();
        followerCountLabel = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        cntrllr = GameObject.FindGameObjectWithTag("Controller").GetComponent<ControllerScript>();
        cs = GameObject.FindGameObjectWithTag("Collector").GetComponent<CollectorScript>();
        UpdateUILabel();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateUILabel();
    }

    internal void Add(GameObject go)
    {
        instances.Add(go);
    }

    internal void Remove(GameObject go)
    {
        instances.Remove(go);
    }

    private void UpdateUILabel()
    {
        followerCountLabel.text = String.Format(text, cs.GetScore(), (int) cntrllr.GetTime() / 60, (int) cntrllr.GetTime() % 60);
    }

    internal int GetCount()
    {
        return instances.Count;
    }
}
