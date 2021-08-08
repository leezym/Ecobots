using UnityEngine;
using System.Collections;
using Globales;

public class EspectroEnergia : dropItems {

	public GameObject Target;
	public GameObject Explocion;

	public float Speed;
	public float Health = 50;

	bool Detected;
	bool Embestir = false;
	bool Mecanicas = true;
	bool Drop = true;

	float EmbestidaTime;
	float Dis;

	Vector3 Move;

	Animator Anim;

	BoxCollider2D Coll;

	void Start()
	{
		Target = GameObject.FindWithTag ("Player");
		Anim = GetComponent<Animator>();
		Coll = GetComponent<BoxCollider2D> ();
	}

	void Update()
	{
		startDrop ();

		if(Mecanicas)
		{
			CalculoDistancia (); 
			RotationEnemy();

			if(Embestir)
			{
				Anim.SetBool ("Atack", true);
				Embestida();
			}

			if(!Embestir)
			{
				Anim.SetBool ("Atack", false);
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
		Explocion.SetActive(true);

		yield return new WaitForSeconds (2.5f);

		Destroy (gameObject);
	}

	void CalculoDistancia()
	{
		Dis = (Target.transform.position.x - transform.position.x);
	}
	
	void Embestida()
	{
		if(EmbestidaTime < 3.0f)
		{
			Move.x = Speed * Time.deltaTime;
			transform.Translate(Move);
			EmbestidaTime += Time.deltaTime;
		}
		if(EmbestidaTime > 3.0f)
		{
			EmbestidaTime = 0;
		}
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


		if(Other.gameObject.tag == "Player")
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


	void OnTriggerEnter2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Embestir = true;
		}
	}

	void OnTriggerExit2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Embestir = false;
		}
	}
}
