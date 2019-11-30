using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    public Text correct;
    public Text incorrect;
    public Text results;
    // Start is called before the first frame update
    void Start()
    {
        correct.text = "Correct: " + GameManager.correct;
        incorrect.text = "Incorrect: " + GameManager.incorrect;
        float calc = (float)GameManager.correct / ((float)GameManager.incorrect + (float)GameManager.correct) * 100;
        results.text = "Results: " + (int)calc + "%";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
