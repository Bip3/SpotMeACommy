using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class transitionScript : MonoBehaviour
{
    public float timer = 4.0f;
    void Start()
    {
        
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
