using UnityEngine;
using System.Collections;
using Globales;


public class NoRobot : dropItems {

	public Transform DistanceAtack;
	
    public float Speed = 15;
    public float Health = 50;
	
    public GameObject Target;
    public GameObject Explosion;
	
    Vector3 Move;

    BoxCollider2D Coll;

    bool Detected;
    bool Forw = true;
    bool Embestida = false;
    bool Mecanicas = true;
	bool Drop = true;

	float Dis,EmbestidaTime = 0;

	void Start()
	{
		Target = GameObject.FindGameObjectWithTag ("Player");
        Coll = GetComponent<BoxCollider2D>();
	}
	
	void DetectedPlayer()
	{
		Debug.DrawLine (transform.position,DistanceAtack.position,Color.blue);
		Detected = Physics2D.Linecast (transform.position,DistanceAtack.position, 1 << LayerMask.NameToLayer("Player"));
	}
	
	void Update()
	{
		startDrop ();

        if(Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
        }

		if(Mecanicas)
        {
            CalculoDistancia();
            RotationEnemy();
            DetectedPlayer();

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

            StartCoroutine(Muerte());
		}
	}

    //Efecto de Muerte
    IEnumerator Muerte()
    {
        Coll.isTrigger = true;
        Explosion.SetActive(true);
        Mecanicas = false;

        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
    }
	
	void CalculoDistancia()
	{
		Dis = (Target.transform.position.x - transform.position.x);
	}
	
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
	
	void DesactivarEmbestida()
	{
		Embestida = false;
		EmbestidaTime = 0;
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

        if(Other.gameObject.tag == "Player")
        {
            StartCoroutine(Muerte());
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
