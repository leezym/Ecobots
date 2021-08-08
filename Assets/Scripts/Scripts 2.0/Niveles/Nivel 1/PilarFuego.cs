using UnityEngine;
using System.Collections;
using Globales;

public class PilarFuego : MonoBehaviour {

    public GameObject Fuego;
    float Daño = 2;
	float TimePlayer = 0;

	void Update()
	{
		if(TimePlayer > 2)
		{
            Fuego.SetActive(true);
		}
	}

	void OnTriggerStay2D(Collider2D Coll)
	{
		if(Coll.gameObject.tag == "Player")
		{
			TimePlayer += 1 * Time.deltaTime;

            if(TimePlayer > 2)
            {
                Debug.Log("Daño");
                GameController.data.sliderHealth.value -= Daño *  Time.deltaTime;
                GameController.data.sliderHealthP.value -= Daño * Time.deltaTime;
            }
		}
	}

	void OnTriggerExit2D(Collider2D Coll)
	{
		if(Coll.gameObject.tag == "Player")
		{
			TimePlayer = 0;
            Fuego.SetActive(false);
		}
	}
}
