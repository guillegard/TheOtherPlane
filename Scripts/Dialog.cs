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

    public GameObject playerController;
    public GameObject character;
    public GameObject iBG;
    public GameObject textBG;
    public GameObject aBG;

    public GameObject[] triggers;
    int triggerIndex = 0;

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
        foreach(GameObject trigger in triggers)
        {
            trigger.SetActive(false);
        }
        StartDialog(index, finIndex,true);
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
            playerController.GetComponent<PlayerController>().enabled = true;
            iBG.SetActive(false);
            textBG.SetActive(false);
            aBG.SetActive(false);
        }
    }

    public void StartDialog(int ini, int fin, bool isTrigger)
    {
        if (triggerIndex < triggers.Length) 
            triggers[triggerIndex].SetActive(true);
        if(isTrigger)
            triggerIndex++;
        iBG.SetActive(true);
        textBG.SetActive(true);
        aBG.SetActive(true);
        character.GetComponent<Animator>().SetBool("isMoving", false);
        index = ini;
        finIndex = fin;
        playerController.GetComponent<PlayerController>().enabled = false;
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
