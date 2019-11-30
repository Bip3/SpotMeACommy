using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public bool canPressGameButtons;
    public void detain()
    {
        if (canPressGameButtons)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().selectStore.GetComponent<PeopleAI>().detain();
    }
    public void release()
    {
        if (canPressGameButtons)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().selectStore.GetComponent<PeopleAI>().release();
    }
    public void restart()
    {
        SceneManager.LoadScene(0);
    }
}
