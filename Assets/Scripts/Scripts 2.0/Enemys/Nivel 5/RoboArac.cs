using UnityEngine;
using System.Collections;
using Globales;

public class RoboArac : dropItems {

	public float BulletSpeed;
    
    public int Health = 50;
	
    public Rigidbody2D Bullet;
	
    public GameObject player,Explosion;
	public GameObject[] SpawnBullet;

	//Variables privadas

	float Speed = 1.5f;
    float LeftDir, RightDir;
    float CountShoot, TimeShoot,Dis;
	
    bool Atack;
	bool Mecanicas = true;
	bool Drop = true;

    BoxCollider2D Coll;
	
    private Vector2 WalkDistance;
	
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
        Coll = GetComponent<BoxCollider2D>();
		LeftDir = transform.position.x - 2.5f;
		RightDir = transform.position.x + 2.5f;
	}
	
	// Patrullaje
	void Update () 
	{
		startDrop ();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

		if(Mecanicas)
		{
			CalculoDistancia();

			if(Atack)
			{
				RotationEnemy();
				Invoke("Disparo",0.5f);
			}

			if(!Atack)
			{
				Patrulla();
			}
		}
       
		if(Health <=0)
		{
			Mecanicas = false;

			if(Drop)
			{
				randomDrop ();
				Drop = false;
			}

			Destroy(gameObject);
		}
		Destroy(GameObject.Find("BulletAracnido(Clone)"),3f);
	}

    IEnumerator Muerte()
    {
        Coll.isTrigger = true;
        Explosion.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
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

				if(Dis < 0)
				{
					for( int i = 0; i < SpawnBullet.Length; i ++)
					{
						Shoting = Instantiate(Bullet,SpawnBullet[i].transform.position,Quaternion.identity) as Rigidbody2D;
						Shoting.velocity = new Vector2 (-10,-30) * BulletSpeed;
					}
				}

				if(Dis > 0)
				{
					for( int i = 0; i < SpawnBullet.Length; i ++)
					{
						Shoting = Instantiate(Bullet,SpawnBullet[i].transform.position,Quaternion.identity) as Rigidbody2D;
						Shoting.velocity = new Vector2 (10,-30) * BulletSpeed;
					}
				}
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
	void CalculoDistancia()
	{
		Dis = (player.transform.position.x - transform.position.x);
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
        if (Other.gameObject.tag == "Bullet")
        {
            Health -= GameController.DisparoBase;
            Destroy(Other.gameObject);
        }

        if (Other.gameObject.tag == "Electric")
        {
            Health -= GameController.ElectricShoot;
            Destroy(Other.gameObject);
        }

        if (Other.gameObject.tag == "Energy")
        {
            Health -= GameController.EnergyBall;
            Destroy(Other.gameObject);
        }
    }


    public void OnTriggerStay2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "Fire")
        {
            Health -= GameController.IceAndFire;
        }

        if (Other.gameObject.tag == "Ice")
        {
            Health -= GameController.IceAndFire;
        }

        if (Other.gameObject.tag == "Tornado")
        {
            Health -= GameController.Tornadito;
            Destroy(Other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D Tar)
    {
        if (Tar.gameObject.tag == "Player")
        {
            Atack = true;
			transform.Translate(new Vector2(0, -5));
        }
    }

    void OnTriggerExit2D(Collider2D Tar)
    {
        if (Tar.gameObject.tag == "Player")
        {
            Atack = false;
            transform.Translate(new Vector2(0, 5));
        }
    }
}
