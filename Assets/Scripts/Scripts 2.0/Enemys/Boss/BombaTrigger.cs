using UnityEngine;
using System.Collections;
using Globales;

public class BombaTrigger : MonoBehaviour {

	public GameObject Explosion;

	void Start()
	{
		Invoke ("Active",1);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameController.data.sliderHealth.value -= GameController.Boom * Time.deltaTime;
			GameController.data.sliderHealthP.value -= GameController.Boom * Time.deltaTime;
		}	
	}

	void Active()
	{
		Explosion.SetActive (true);
	}
}