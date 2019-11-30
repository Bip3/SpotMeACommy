using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] allSpawns;
    public float respawnTimer = 3.0f;
    private float timer;
    public bool timing = false;

    public int numSpawn = 6;

    LevelManager level;
    void Start()
    {
        level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        timer = respawnTimer;
        spawn(numSpawn);
    }

    void Update()
    {
        if (timing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                spawn(1);
                timer = respawnTimer;
            }
        }
    }
    void spawn(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector2 newPos = new Vector2(Random.Range(level.bounds.x + 1, level.bounds.y - 1), Random.Range(-3.2f, 0));
            int rand = Random.Range(0, allSpawns.Length);
            Instantiate(allSpawns[rand], newPos, Quaternion.identity);

            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                allSpawns[rand].GetComponent<PeopleAI>().isCommunist = true;
            }
            else
            {
                allSpawns[rand].GetComponent<PeopleAI>().isCommunist = false;
            }
            
        }
    }
}
