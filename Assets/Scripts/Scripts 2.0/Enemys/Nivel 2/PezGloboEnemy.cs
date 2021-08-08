using UnityEngine;
using System.Collections;
using Globales;

public class PezGloboEnemy : dropItems {

	public dropItems drop;

    public float Health = 50;

	public GameObject Tornado;
	public GameObject Explosion; 
	public GameObject Player;

	float Dis;

	Vector3 Move;
	BoxCollider2D Coll;
	Animator Anim;

	bool Empuje;
	bool Mecanicas = true;
	bool Drop = true;

	void Start()
	{
		Anim = GetComponent<Animator> ();
		Player = GameObject.FindWithTag ("Player");
		Coll = GetComponent<BoxCollider2D> ();
	}

	void Update()
	{
		startDrop ();

		if (Player == null) 
		{
			Player = GameObject.FindWithTag ("Player");
		}

		if(Mecanicas)
		{
			CalculoDistancia ();
			RotationEnemy ();

			if(Empuje == true )
			{
				Anim.SetBool ("Active", true);
				Tornado.SetActive(true);
			}

			if(Empuje == false)
			{
				Anim.SetBool ("Active", false);
				Invoke("Desactivar",1.5f);
			}
		}
			
		if(Health < 1)
		{
			Mecanicas = false;

			if(Drop)
			{
				randomDrop ();
				Drop = false;
			}

			StartCoroutine (Muerte ());
		}
	}

	//Efecto de Muerte
	IEnumerator Muerte()
	{
		Coll.isTrigger = true;
		Explosion.SetActive(true);

		yield return new WaitForSeconds (2.5f);

		Destroy (gameObject);
	}
		
	void CalculoDistancia()
	{
		Dis = (Player.transform.position.x - transform.position.x);
	}

	void Desactivar()
	{
		Tornado.SetActive(false);
	}

	void RotationEnemy()
	{
		if(Dis > 0)
		{
			transform.eulerAngles = new Vector2 (0,0);
		}
		if(Dis < 0)
		{
			transform.eulerAngles = new Vector2 (0,180);
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
    }

	public void OnTriggerStay2D(Collider2D Other)
	{
		if (Other.gameObject.tag == "Fire")
		{
			Health -= GameController.IceAndFire * 0.1f;
		}

		if (Other.gameObject.tag == "Ice")
		{
			Health -= GameController.IceAndFire;
		}

		if(Other.gameObject.tag == "Tornado")
		{
			Health = GameController.Tornadito;
			Destroy(Other.gameObject);
		}
	}
		
	void OnTriggerEnter2D(Collider2D Other)
	{
		if(Other.gameObject.tag == "Player")
		{
			Empuje = true;	
		}
	}

	void OnTriggerExit2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Empuje = false;
			Invoke("Desactivar",1.5f);
		}
	}
}
