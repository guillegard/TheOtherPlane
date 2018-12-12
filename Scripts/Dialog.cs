using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour {

    public TextMeshProUGUI text;
    public string[] sentences;
    public string[] a1_sentences;
    public string[] a2_sentences;
    public int index;
    public int finIndex;
    public Color color;
    public float speed;

    public GameObject a1;
    public GameObject a2;

    public GameObject player;
    public GameObject iBG;
    public GameObject textBG;
    public GameObject aBG;

    IEnumerator Type()
    {
        foreach(char l in sentences[index].ToCharArray())
        {
            text.text += l;
            yield return new WaitForSeconds(speed);
        }
    }

	// Use this for initialization
	void Start () {
        StartDialog(index, finIndex);
	}

    public void Next()
    {
        a1.SetActive(false);
        a2.SetActive(false);

        if (index < finIndex - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            text.text = "";
            player.GetComponent<PlayerController>().enabled = true;
            iBG.SetActive(false);
            textBG.SetActive(false);
            aBG.SetActive(false);
        }
    }

    public void StartDialog(int ini, int fin)
    {
        index = ini;
        finIndex = fin;
        player.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(Type());
    }
	
	// Update is called once per frame
	void Update () {
		if(text.text == sentences[index])
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a1.GetComponent<TextMeshProUGUI>().text = a1_sentences[index];
            a2.GetComponent<TextMeshProUGUI>().text = a2_sentences[index];
        }
	}
}
