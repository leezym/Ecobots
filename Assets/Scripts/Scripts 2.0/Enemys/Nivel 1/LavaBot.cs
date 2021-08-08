 using UnityEngine;
using System.Collections;
using Globales;

public class LavaBot : dropItems {

	public float Speed = 8;
	public float Health = 50;

	public GameObject Target;
	public GameObject DistanceAtack;
	public GameObject Explosion;

	bool Detected;
	bool Forw = true;
	bool Embestida = false;
	bool Disable = false;
    bool Mecanicas = true;
	bool Drop = true;
 	
	public float Dis,EmbestidaTime = 0;

	Shooting Shoot;
	Vector3 Move;
	CircleCollider2D Coll;
	SpriteRenderer sprite;


	void Start()
	{
		Target = GameObject.FindGameObjectWithTag ("Player");
		Shoot = Target.GetComponent<Shooting> ();
		Coll = GetComponent<CircleCollider2D> ();
		sprite = GetComponent<SpriteRenderer> ();
	}

	void Update()
	{
		startDrop ();

        if(Mecanicas)
        {
            CalculoDistancia();
            RotationEnemy();

            if (!Disable)
            {
                DetectedPlayer();
            }

            if (Detected)
            {
                Embestida = true;
            }

            if (Embestida == true)
            {
                Embestir();
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

	IEnumerator Muerte()
	{
		//Coll.isTrigger = true;
		Coll.enabled = false;
		sprite.enabled = false;
		Explosion.SetActive(true);
	
		yield return new WaitForSeconds (2.5f);

		Destroy (gameObject);
	}

	//Detectar Jugador
	void DetectedPlayer()
	{
		Debug.DrawLine (transform.position,DistanceAtack.transform.position,Color.blue);
		Detected = Physics2D.Linecast (transform.position,DistanceAtack.transform.position, 1 << LayerMask.NameToLayer("Player"));
	}

	//Calcular Distancia
	void CalculoDistancia()
	{
		if(Target != null)
		{
			Dis = (Target.transform.position.x - transform.position.x);	
		}
	}


	//Rotacion de Enemigo
	void RotationEnemy()
	{
		if(Forw)
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
	}

	//Embestida
	void Embestir()
	{
		if(EmbestidaTime < 2.0)
		{
				Move.x = Speed * Time.deltaTime;
				transform.Translate (Move);
				EmbestidaTime += Time.deltaTime;
		}
		Invoke("DesactivarEmbestida",2);
	}

	//Desactivar Embestida
	void DesactivarEmbestida()
	{
		Embestida = false;
		EmbestidaTime = 0;
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
			Destroy (gameObject);
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
			Forw = false;
		}
	}

	void OnTriggerExit2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Forw = true;
		}
	}
}
