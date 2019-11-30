using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxQueue : MonoBehaviour
{
    public Text textBox;
    public bool faded;
    List<char[]> dialogueList = new List<char[]>();
    public void queueString(string input)
    {
        StopAllCoroutines();
        textBox.text = "";
        char[] letters = input.ToCharArray();
        if (!faded)
        {
            StartCoroutine(panelFadeIn(letters));
        }
    }
    public IEnumerator panelFadeIn(char[] letters)
    {
        while (GetComponent<Image>().color.a < .8f)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, GetComponent<Image>().color.a + .01f);
            yield return new WaitForSeconds(.008f);
        }
        faded = true;
        StartCoroutine(queue(letters));
        StopCoroutine(panelFadeOut());

    }

    IEnumerator panelFadeOut()
    {
        while (GetComponent<Image>().color.a > 0)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, GetComponent<Image>().color.a - .01f);
            yield return new WaitForSeconds(.008f);
        }
        faded = false;
    }
    public void startFadeOut()
    {
        StartCoroutine(panelFadeOut());
    }

    public IEnumerator queue(char[] letters)
    {
        for (int i = 0; i < letters.Length; i++)
        {
            textBox.text = textBox.text + letters[i];
            yield return new WaitForSeconds(.05f);
        }
    }
}
