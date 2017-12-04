using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour {

    private int score;

	// Use this for initialization
	void Start () {
        score = 0;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void raiseScore()
    {
        this.score = this.score + 1;
    }

    public int GetScore()
    {
        return this.score;
    }
}
