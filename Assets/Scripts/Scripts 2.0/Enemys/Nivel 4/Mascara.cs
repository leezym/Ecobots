using UnityEngine;
using System.Collections;
using Globales;

public class Mascara : dropItems {

	//Variables Publicas
	public float BulletSpeed;
	public float Health = 50;

	public Rigidbody2D Bullet;

	public GameObject SpawnBullet;
	public GameObject player;
	public GameObject DistanceAtack;
	public GameObject Explosion;

	float Speed = 1.5f;
	float LeftDir, RightDir;
	float CountShoot, TimeShoot;

	bool Detected,Atack;
	bool Mecanicas = true;
	bool Drop = true;

	private Vector2 WalkDistance;
	
	void Start () 
	{
		player = GameObject.FindWithTag ("Player");
		LeftDir = transform.position.x - 2.5f;
		RightDir = transform.position.x + 2.5f;
	}
	
	// Patrullaje
	void Update () 
	{
		startDrop ();

		if(player == null)
		{
			player = GameObject.FindWithTag ("Player");
		}

		if(Mecanicas)
		{
			DetectedPlayer ();
			if (Detected)
			{
				Disparo ();
			}

			if (!Detected) 
			{
				Patrulla ();
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

			Destroy(gameObject);
		}
		Destroy(GameObject.Find("BulletMascara(Clone)"),3f);
	}
		
	void DetectedPlayer()
	{
		Debug.DrawLine (SpawnBullet.transform.position,DistanceAtack.transform.position,Color.blue);
		Detected = Physics2D.Linecast (SpawnBullet.transform.position,DistanceAtack.transform.position, 1 << LayerMask.NameToLayer("Player"));
	}
	
	void Patrulla()
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
