using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{
    private float time;
    private Canvas canvas;
    private GameObject player;
    private GameObject plane;
    private GameObject obstacle;
    private GameObject wallL;
    private GameObject wallR;
    private GameObject wallF;
    private GameObject wallB;
    private GameObject spawner;
    private GameObject counter;
    private GameObject label;
    private GameObject title;
    private GameObject description;
    private GameObject gameover;
    private GameObject collector;
    private GameObject playerInstance;
    private List<GameObject> levelAssetInstances;
    private GameObject spawnerInstance;
    private GameObject counterInstance;
    private GameObject labelInstance;
    private GameObject titleInstance;
    private GameObject descriptionInstance;
    private GameObject gameoverInstance;
    private GameObject collectorInstance;
    private System.Random rnd;
    private bool hasStarted;
    private bool gameOver;
    private Canvas cnvs;

    // Use this for initialization
    void Start ()
    {
        time = 240;
        gameOver = false;
        rnd = new System.Random();
        levelAssetInstances = new List<GameObject>();
        GameObject es = new GameObject("EventSystem", typeof(EventSystem));
        es.AddComponent<StandaloneInputModule>();

        GameObject c = new GameObject()
        {
            name = "Canvas"
        };
        c.transform.parent = transform.root;
        canvas = c.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        CanvasScaler cs = c.AddComponent<CanvasScaler>();
        cs.scaleFactor = 1.0f;
        cs.dynamicPixelsPerUnit = 10.0f;
        GraphicRaycaster gr = c.AddComponent<GraphicRaycaster>();
        c.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        c.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);

        
        bool bWorldPosition = true;
        c.GetComponent<RectTransform>().SetParent(transform.root, bWorldPosition);
        c.transform.localPosition = new Vector3(0f, 0f, 0f);

        c.transform.localScale = new Vector3(
            1.0f / this.transform.localScale.x * .7f,
            1.0f / this.transform.localScale.y * .7f,
            1.0f / this.transform.localScale.z * .7f
            );

        // Load Assets
        player = Resources.Load("Player/Player") as GameObject;
        plane = Resources.Load("Level/Plane") as GameObject;
        obstacle = Resources.Load("Level/Obstacle") as GameObject;
        wallL = Resources.Load("Level/WallLeft") as GameObject;
        wallR = Resources.Load("Level/WallRight") as GameObject;
        wallF = Resources.Load("Level/WallFront") as GameObject;
        wallB = Resources.Load("Level/WallBack") as GameObject;
        spawner = Resources.Load("Spawner/Spawner") as GameObject;
        collector = Resources.Load("Collector/Collector") as GameObject;
        counter = Resources.Load("Counter/Counter") as GameObject;
        label = Resources.Load("UI/Score") as GameObject;
        description = Resources.Load("UI/Description") as GameObject;
        title = Resources.Load("UI/Title") as GameObject;
        gameover = Resources.Load("UI/GameOver") as GameObject;


        // Instantiate Assets
        CreateLevel();
        labelInstance = (GameObject)Instantiate(label, label.transform.position, label.transform.rotation);
        labelInstance.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        counterInstance = (GameObject)Instantiate(counter, counter.transform.position, counter.transform.rotation);
        titleInstance = (GameObject)Instantiate(title, title.transform.position, title.transform.rotation);
        titleInstance.transform.SetParent(c.transform);
        descriptionInstance = (GameObject)Instantiate(description, description.transform.position, description.transform.rotation);
        descriptionInstance.transform.SetParent(c.transform);

        // Set Camera for Canvas from instantiated Player asset.
        cnvs = GetComponent<Canvas>();
    }

    public float GetTime()
    {
        return this.time;
    }

    private void CreateLevel()
    {
        CreateStart();
        CreateMaze();
        CreateEnd();
    }

    private void CreateStart()
    {
        levelAssetInstances.Add((GameObject)Instantiate(plane, plane.transform.position, plane.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(plane, plane.transform.position + new Vector3(0, 0, 10.0f), plane.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(wallR, wallR.transform.position, wallR.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(wallF, wallF.transform.position, wallF.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(wallB, wallB.transform.position, wallB.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(wallF, wallF.transform.position + new Vector3(0, 0, 10.0f), wallF.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(wallB, wallB.transform.position + new Vector3(0, 0, 10.0f), wallB.transform.rotation));
        spawnerInstance = (GameObject)Instantiate(spawner, spawner.transform.position + new Vector3(0, 0, 10.0f), spawner.transform.rotation);
    }

    private void CreateMaze()
    {
        for(int z = 0; z < 10; z++)
        {
            levelAssetInstances.Add((GameObject)Instantiate(obstacle, obstacle.transform.position + new Vector3(z * (float)Math.Sin(z), 0, (20.0f + z * 10.0f)) + new Vector3(rnd.Next(1, 5), 0, rnd.Next(1, 5)) + new Vector3(-2.5f, 0, -2.5f), obstacle.transform.rotation));
            levelAssetInstances.Add((GameObject)Instantiate(plane, plane.transform.position + new Vector3(z * (float) Math.Sin(z), 0, (20.0f + z * 10.0f)), plane.transform.rotation));
        }
    }

    private void CreateEnd()
    {
        levelAssetInstances.Add((GameObject)Instantiate(wallF, wallF.transform.position + new Vector3(0, 0, 120.0f), wallF.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(wallB, wallB.transform.position + new Vector3(0, 0, 120.0f), wallB.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(plane, plane.transform.position + new Vector3(0, 0, 120.0f), plane.transform.rotation));
        levelAssetInstances.Add((GameObject)Instantiate(wallL, wallL.transform.position + new Vector3(0, 0, 120.0f), wallL.transform.rotation));
        collectorInstance = (GameObject)Instantiate(collector, collector.transform.position + new Vector3(0, 0, 120.0f), collector.transform.rotation);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButton("Submit"))
        {
            hasStarted = true;
        }
        if (Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (hasStarted)
        {
            if (playerInstance == null)
            {
                playerInstance = (GameObject)Instantiate(player, player.transform.position, player.transform.rotation);
                canvas.worldCamera = playerInstance.GetComponent<Camera>();
            }
            if (gameoverInstance != null)
            {
                Destroy(gameoverInstance);
            }
            if (this.time > 0)
                this.time = this.time - Time.deltaTime;
            else
                gameOver = true;
            if (playerInstance.transform.position.y <= -1)
                gameOver = true;
            Destroy(titleInstance);
            Destroy(descriptionInstance);
        }
        if (gameOver)
        {
            hasStarted = false;
            gameoverInstance = (GameObject)Instantiate(gameover, gameover.transform.position, gameover.transform.rotation);
        }
    }
}
