using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class cursorScript : MonoBehaviour
{
    Player player;
    public int playerId;
    public float speed = .2f;
    public GameObject selectPoint;

    public DialogueBoxQueue txtBox;

    LevelManager level;

    Vector3 newPos;
    public Vector2 vertBounds;
    public Vector2 horizBounds;

    public bool selected;
    void Start()
    {
        vertBounds = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y, Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y);
        horizBounds = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x);
        player = ReInput.players.GetPlayer(playerId);
        level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    void Update()
    {
        if (!selected)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1);
            UpdatePosition();
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0);
        }

        if (player.GetButtonDown("Select"))
        {
            press();
        }
    }
    void UpdatePosition()
    {
        newPos = new Vector3(transform.position.x + player.GetAxis("MoveHorizontal") * speed, transform.position.y + player.GetAxis("MoveVertical") * speed, transform.position.z);

        if (newPos.x < horizBounds.x)
        {
            newPos = new Vector3(horizBounds.x, newPos.y, newPos.z);
        }
        if (newPos.x > horizBounds.y)
        {
            newPos = new Vector3(horizBounds.y, newPos.y, newPos.z);
        }
        if (newPos.y < vertBounds.x)
        {
            newPos = new Vector3(newPos.x, vertBounds.x, newPos.z);
        }
        if (newPos.y > vertBounds.y)
        {
            newPos = new Vector3(newPos.x, vertBounds.y, newPos.z);
        }
        transform.position = newPos;
    }
    public void press()
    {
        RaycastHit2D hit = Physics2D.Raycast(selectPoint.transform.position, Vector3.forward);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<PeopleAI>())
            {
                if (!selected)
                {
                    hit.collider.GetComponent<PeopleAI>().selectMe();
                    level.selectStore = hit.collider.gameObject;
                    selected = true;
                }
            }
        }
    }
}
