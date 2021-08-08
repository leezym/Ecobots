using UnityEngine;
using System.Collections;
using Globales;

public class CobraBot : dropItems {

	//Variables Publicas
	public int Health = 50;

	public float BulletSpeed;

	public GameObject player;
	public GameObject SpawnBullet;
	public GameObject DistanceAtack;
	public GameObject Explosion;

	public Rigidbody2D Bullet;

	//Variables privadas

	float LeftDir,RightDir;
	float Speed = 1.5f;   
	float CountShoot, TimeShoot;

	Animator Anim;
	Vector2 WalkDistance;
	BoxCollider2D Coll;

	bool Detected,Atack,Patrulla;
    bool Mecanicas = true;
	bool Drop = true;

	void Start () 
	{
		Anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		Coll = GetComponent<BoxCollider2D> ();
		LeftDir = transform.position.x - 2.5f;
		RightDir = transform.position.x + 2.5f;
	}
	

	void Update () 
	{
		startDrop ();

        if(Mecanicas)
        {
            DetectedPlayer();

            if (Detected)
            {
                Anim.SetBool("Deteccion", true);
                Atack = true;
                Patrulla = false;
            }

            if (!Detected)
            {
                Anim.SetBool("Deteccion", false);
                Atack = false;
                Patrulla = true;
            }

            //Mecanicas
            if (Atack == true)
            {
                Disparo();
            }

            if (Patrulla = true)
            {
                patrulla();
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
		Destroy(GameObject.Find("Bala"),3f);
	}

	//Efecto de Muerte
	IEnumerator Muerte()
	{
		Coll.isTrigger = true;
		Atack = false;
		Patrulla = false;
		Explosion.SetActive(true);

		yield return new WaitForSeconds (2.5f);

		Destroy (gameObject);
	}

	//Detectar jugador
	void DetectedPlayer()
	{
		Debug.DrawLine (SpawnBullet.transform.position,DistanceAtack.transform.position,Color.blue);
		Detected = Physics2D.Linecast (SpawnBullet.transform.position,DistanceAtack.transform.position, 1 << LayerMask.NameToLayer("Player"));
	}

	//Mecanica de pratrulla
	void patrulla()
	{
		WalkDistance.x = Speed * Time.deltaTime;
		if(transform.position.x >= RightDir)
		{
			transform.eulerAngles = new Vector2 (0, -180);
		} 
		if(transform.position.x <= LeftDir)
		{
			transform.eulerAngles = new Vector2 (0, 0);
		}
		
		transform.Translate (WalkDistance);
	}

	//Mecanica de disparo
	void Disparo()
	{	
		if(CountShoot <=3.5f )
		{
			if(TimeShoot <= 0)
			{
				Rigidbody2D Shoting;
				Shoting = Instantiate(Bullet,SpawnBullet.transform.position,SpawnBullet.transform.rotation) as Rigidbody2D;
				Shoting.velocity = transform.right * BulletSpeed;
				TimeShoot = 1;
			}
			CountShoot += Time.deltaTime;
			TimeShoot -= Time.deltaTime;
		}
		if(CountShoot > 3.5f)
		{
			CountShoot = 0;
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
