using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour {

    public GameObject instructionImage;
    public GameObject fire;
    public Character player;
    public ParticleSystem p1;
    public ParticleSystem p2;
    public ParticleSystem p3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Fire()
    {
        fire.SetActive(true);
        instructionImage.SetActive(true);
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            ParticleSystem.EmissionModule emissionModule1 = p1.emission;
            ParticleSystem.EmissionModule emissionModule2 = p2.emission;
            ParticleSystem.EmissionModule emissionModule3 = p3.emission;
            //Debug.Log(i);
            emissionModule1.rateOverTime = 50 * (1 - i);
            emissionModule2.rateOverTime = 50 * (1 - i);
            emissionModule3.rateOverTime = 50 * (1 - i);
            instructionImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.07f);
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(fire);
        this.gameObject.SetActive(false);
    }


}
