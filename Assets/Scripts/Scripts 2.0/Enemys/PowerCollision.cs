using UnityEngine;
using System.Collections;
using Globales;

public class PowerCollision : MonoBehaviour {

	public int VidaEnemy;
	public CobraBot CobraVida;

	void Start()
	{
		CobraVida = GameObject.FindWithTag("Cobra").GetComponent<CobraBot>();

		if(this.gameObject.tag == "Cobra")
		{
			VidaEnemy = CobraVida.Health;
		}
	}

	void Update()
	{
		if(this.gameObject.tag == "Cobra")
		{
			CobraVida.Health -= VidaEnemy;
		}
	}

	void OnCollisionEnter2D(Collision2D Other)
	{
		if(Other.gameObject.tag == "Bullet")
		{
			VidaEnemy -= GameController.DisparoBase; 
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Electric" )
		{
			VidaEnemy -= GameController.ElectricShoot;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Energy")
		{
			VidaEnemy -= GameController.EnergyBall;
			Destroy(Other.gameObject);
		}
	}


	public void OnTriggerStay2D(Collider2D Other)
	{
		if(Other.gameObject.tag == "Fire")
		{
			VidaEnemy -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Ice")
		{
			VidaEnemy -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Tornado")
		{
			VidaEnemy -= GameController.Tornadito;
			Destroy(Other.gameObject);
		}
	}
}
