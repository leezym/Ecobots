using UnityEngine;
using System.Collections;
using Globales;

public class PezTorpedo : MonoBehaviour {

    GameObject PJ;

	Vector3 Move;

	public float Health = 10;

    bool cambio = true;
    bool Forw;

    public SpawnPezTorpedo Spawn;

	void Start()
	{
		Spawn = GameObject.FindWithTag("Spawn").GetComponent<SpawnPezTorpedo>();
	}
	
	void Update () 
	{
		Elevar ();
		Invoke ("Lanzamiento",0.6f);
		Destroy (gameObject,4);
	}
		
	void Elevar()
	{
		if(cambio == true)
		{
			Move = new Vector3 (0,10,0) * Time.deltaTime;
			transform.Translate (Move);
		}
	}

	void Lanzamiento()
	{
		if(Spawn.Dis > 0)
		{
			cambio = false;
			Move = new Vector3 (20,0,0) * Time.deltaTime * 2;
            transform.eulerAngles = new Vector2(0, 0);
			transform.Translate (Move);
		}

		if(Spawn.Dis <0)
		{
			cambio = false;
			Move = new Vector3 (20,0,0) * Time.deltaTime * 2;
            transform.eulerAngles = new Vector2(0, 180);
			transform.Translate (Move);
		}
	}

	//Collisiones
	void OnCollisionEnter2D(Collision2D Other)
	{
		if(Other.gameObject.tag == "Bullet")
		{
			Health -= GameController.DisparoBase; 
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Electric" )
		{
			Health -= GameController.ElectricShoot;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Energy")
		{
			Health -= GameController.EnergyBall;
			Destroy(Other.gameObject);
		}

		if (Other.gameObject.tag == "Player")
		{
			Destroy(gameObject);
		}
	}


	public void OnTriggerStay2D(Collider2D Other)
	{
		if(Other.gameObject.tag == "Fire")
		{
			Health -= GameController.IceAndFire;
		}

		if(Other.gameObject.tag == "Ice")
		{
			Health -= GameController.IceAndFire;
		}

		if(Other.gameObject.tag == "Tornado")
		{
			Health -= GameController.Tornadito;
			Destroy(Other.gameObject);
		}
	}
}
