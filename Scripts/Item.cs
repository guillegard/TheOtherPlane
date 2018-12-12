using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum Type
    {
        Key, Door, Chest, Special
    }

    public Type type;
    public GameObject contains;
    public bool hasDialog;
    public int ini;
    public int fin;
    public GameObject dialogManager;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    public GameObject OpenChest()
    {
        if (hasDialog)
        {
            dialogManager.GetComponent<Dialog>().StartDialog(ini, fin, false);

        }
        return contains;
    }

}
