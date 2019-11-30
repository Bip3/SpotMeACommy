using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleAI : MonoBehaviour
{
    string myName;
    string nationality;

    int age;

    public int dir = 1;
    public float speed = 2;
    private float speedStore = 0;
    public bool selected;
    private AIStates aiStates;
    public DialogueBoxQueue txtBox;

    public Vector2 walkTimes;
    private float walkTimer = 0.5f;

    public Vector2 waitTime;
    private float waitTimer = 0.5f;

    private float reboundTimer = 1;

    Animator anim;
    LevelManager level;

    public bool isCommunist;

    SpriteRenderer renderer;
    void Start()
    {
        
        txtBox = GameObject.Find("Panel").GetComponent<DialogueBoxQueue>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        chooseDir();

        level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        calculate();
        renderer.sortingOrder = (int)(renderer.transform.position.y * -100);


    }
    void calculate()
    {
        age = Random.Range(16, 75);
        if (isCommunist)
        {
            myName = level.newCommunistName();
            nationality = level.newCommunistNationality();
        }
        else
        {
            myName = level.newRegName();
            nationality = level.newRegNationality();
        }
    }
    void Update()
    {
        if (reboundTimer < 1)
        {
            reboundTimer += Time.deltaTime;
        }
        if (transform.position.x < level.bounds.x || transform.position.x > level.bounds.y)
        {
            rebound();
        }

        transform.localScale = new Vector3(dir, transform.localScale.y, transform.localScale.z);
        switch (aiStates)
        {
            case AIStates.walking:
                walkTimer -= Time.deltaTime;
                if (walkTimer <= 0)
                {
                    StopAI();
                }
                transform.Translate(Vector3.right * dir * Time.deltaTime * speed);
                break;
            case AIStates.stopped:
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                    chooseDir();
                break;
            case AIStates.detained:

                break;
        }
    }
    public void selectMe()
    {
        aiStates = AIStates.detained;
        selected = true;
        txtBox.queueString("My name is " + myName + ". I'm " + age + " years old, and I'm " + nationality);
        anim.SetBool("Idle", true);
    }
    void StopAI()
    {
        aiStates = AIStates.stopped;
        waitTimer = Random.Range(waitTime.x, waitTime.y);
        anim.SetBool("Idle", true);
    }
    void chooseDir()
    {
        speed = Random.Range(1.5f, 3.0f);
        walkTimer = Random.Range(walkTimes.x, walkTimes.y);
        int check = Random.Range(1, 3);
        if (check == 1)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        aiStates = AIStates.walking;
        anim.SetBool("Idle", false);
    }
    void rebound()
    {
        if (reboundTimer >= 1)
        {
            dir *= -1;
            reboundTimer = 0;
        }

    }

    public void release()
    {
        txtBox.startFadeOut();
        if (isCommunist)
            level.incorrect += 1;
        else
            level.correct += 1;


        
        StartCoroutine(walkout());
    }
    public void detain()
    {
        txtBox.startFadeOut();
        if (isCommunist)
            level.correct += 1;
        else
            level.incorrect += 1;
        StartCoroutine(walkout());
    }
    IEnumerator walkout()
    {
        GetComponentInChildren<CircleCollider2D>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        while (GetComponentInChildren<SpriteRenderer>().color.a > 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(GetComponentInChildren<SpriteRenderer>().color.r, GetComponentInChildren<SpriteRenderer>().color.g, GetComponentInChildren<SpriteRenderer>().color.b, GetComponentInChildren<SpriteRenderer>().color.a - .01f);
            yield return new WaitForSeconds(0.008f);
        }
        GameObject.Find("Cursor").GetComponent<cursorScript>().selected = false;



        if (level.correct + level.incorrect >= level.totalPossible)
        {
            level.endGame();
        }
    }
    private enum AIStates
    {
        walking,
        stopped,
        detained
    }
}
