using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit1 : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "GrabberU" || collision.name == "GrabberD" || collision.name == "GrabberL" || collision.name == "GrabberR" || collision.name == "Player")
            return;
        if(collision.name.Length >= 7 && collision.name.Substring(0, 7) == "Trigger")
        {
            return;
        }
        if(collision.gameObject.GetComponent<Enemy>() != null)
        {
            
        }
        Destroy(this.gameObject);
    }
}
