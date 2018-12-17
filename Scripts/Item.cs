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
    public GameObject openChest;
    public string itemName;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    public string OpenChest()
    {
        if (hasDialog)
        {
            dialogManager.GetComponent<Dialog>().StartDialog(ini, fin, false);

        }
        openChest.SetActive(true);
        return contains.name;
    }

    public void spiritDialog()
    {
        if(hasDialog)
            dialogManager.GetComponent<Dialog>().StartDialog(ini, fin, false);
    }

}
