using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public enum Type
    {
        Key, Door, Chest, Special, Potion, SpiritPotion
    }

    public Type type;
    public Item contains;
    public bool hasDialog;
    public int ini;
    public int fin;
    public GameObject dialogManager;
    public GameObject openChest;
    public string itemName;
    public bool isTriggerTrigger;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    public Item OpenChest()
    {
        if (hasDialog)
        {
            dialogManager.GetComponent<Dialog>().StartDialog(ini, fin, false, isTriggerTrigger);
        }
        openChest.SetActive(true);
        return contains;
    }

    public void spiritDialog()
    {
        if(hasDialog)
            dialogManager.GetComponent<Dialog>().StartDialog(ini, fin, false, isTriggerTrigger);
    }

}
