using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelStore : MonoBehaviour
{
    public Button releaseButton;
    public Text myText;
    public Button detainButton;

    public Text detainText;
    public Text releaseText;
    void Update()
    {
        Color textColor = new Color(myText.color.r, myText.color.g, myText.color.b, GetComponent<Image>().color.a);
        releaseButton.GetComponent<Image>().color = GetComponent<Image>().color;
        myText.color = textColor;
        detainButton.GetComponent<Image>().color = GetComponent<Image>().color;
        releaseText.color = textColor;
        detainText.color = textColor;

        if (GetComponent<Image>().color.a >= .8f)
        {
            GameObject.Find("ButtonManager").GetComponent<ButtonManager>().canPressGameButtons = true;
            GameObject.Find("ButtonManager").GetComponent<ButtonManager>().canPressGameButtons = true;
        }
        else
        {
            GameObject.Find("ButtonManager").GetComponent<ButtonManager>().canPressGameButtons = false;
            GameObject.Find("ButtonManager").GetComponent<ButtonManager>().canPressGameButtons = false;
        }
    }
}
