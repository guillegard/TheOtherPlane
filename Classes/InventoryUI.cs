using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour {
    public TextMeshProUGUI[] text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Coins
        text[0].text = LevelController.coins + "";
        //Potion
        text[1].text = Potion.quantity + "";
    }
}
