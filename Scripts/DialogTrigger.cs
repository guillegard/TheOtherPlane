using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public GameObject dialogManager;
    public int ini;
    public int fin;
    public bool isInstruction;
    public Instructions instruction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Colision");
        if (collision.gameObject.name == "Player")
        {
            if (isInstruction)
            {
                StartCoroutine(instruction.Fire());
            }
            else
            {
                dialogManager.GetComponent<Dialog>().StartDialog(ini, fin, true, true);
                Destroy(this.gameObject);
            }
        }
    }
}
