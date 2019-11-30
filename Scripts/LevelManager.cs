using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Vector2 bounds;

    public string[] regularNames;
    public string[] regularNationalities;

    public string[] communistNames;
    public string[] communistNationalities;

    public int totalPossible;
    public int correct;
    public int incorrect;

    public GameObject selectStore;
    void Start()
    {
        totalPossible = GameObject.Find("Spawner").GetComponent<Spawner>().numSpawn;
        bounds = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0,0)).x, Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x);
    }
    void Update()
    {
        
    }
    public string newRegName()
    {
        return regularNames[Random.Range(0, regularNames.Length)];
    }

    public string newCommunistName()
    {
        return communistNames[Random.Range(0, communistNames.Length)];
    }

    public string newCommunistNationality()
    {
        return communistNationalities[Random.Range(0, communistNationalities.Length)];
    }
    public string newRegNationality()
    {
        return regularNationalities[Random.Range(0, regularNationalities.Length)];
    }

    public void endGame()
    {
        GameManager.correct = correct;
        GameManager.incorrect = incorrect;
        GameManager.total = totalPossible;
        SceneManager.LoadScene(2);
        
    }
}
