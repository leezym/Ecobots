using UnityEngine;
using System.Collections;
using Globales;

public class MadreAgua : MonoBehaviour {

	public float BulletSpeed;
	public float Speed;
    public float TornadoSpeed;

	public Rigidbody2D Bullet, Torn,BigBullet;

	public GameObject Explosion;
	public GameObject SpawnTornado;
	public GameObject SpawnTornado2;
	public GameObject[] Posiciones;
	public Transform[] SpawnShoot;

	float Dis = 0;
	int Control = 0;

	Vector3 move;
	public GameObject Player, Enemigo;
	public Animator Ani;
	public Army_Menu arm;

	void Start() 
	{
        arm = GameObject.Find("ScriptCanvas").GetComponent<Army_Menu>();

		if (GameController.muerto2)
		{
			Destroy(gameObject);
		}

		Enemigo = GameObject.FindWithTag("Boss");
		Player = GameObject.Find ("Personaje(Clone)");
        Ani = GetComponent<Animator>();
	}

	void Update()
	{
		if(Player == null)
		{
			Player = GameObject.Find ("Personaje(Clone)");
		}

		if(GameController.data.sliderHealthBoss.value == GameController.data.sliderHealthBoss.minValue)
		{
			StartCoroutine (Muerte ());
		}

		if(GameController.Mecanicas)
		{
			if (Control == 0)
			{
				StartCoroutine(Boss());
				Control++;
			}
		}

        Destroy(GameObject.Find("ShootBubble(Clone)"), 5f);
        Destroy(GameObject.Find("Torbellino(Clone)"), 2f);
	}

	IEnumerator Boss() 
	{
		while (GameController.Mecanicas)
		{
			//Posicionamiento Punto1
			while (transform.position.x != Posiciones[0].transform.position.x)
			{
				Ani.SetBool ("Idle",true);
				//				Ani.SetBool ("Meteoros",false);
				transform.position = new Vector3(Posiciones[0].transform.position.x, Posiciones[0].transform.position.y, Posiciones[0].transform.position.z) ;
				//transform.Translate(new Vector3(Posiciones[1].transform.position.x, Posiciones[1].transform.position.y, Posiciones[1].transform.position.z));  
			}

			yield return new WaitForSeconds(2);

			CalculoDistancia();

			yield return new WaitForSeconds(.5f);

			RotationEnemy();

			yield return new WaitForSeconds(.7f);

			//Contador de Disparos
			int NumShoot = 0;
			while (NumShoot < 3)
			{
				Ani.SetBool ("Disparo",true);
			    Ani.SetBool ("Idle",false);
				Rigidbody2D Shoting;
				Shoting = (Rigidbody2D)Instantiate(Bullet, SpawnShoot[Random.Range(0, 3)].position, Quaternion.identity);
				Shoting.velocity = transform.right * BulletSpeed;

				NumShoot++;

				yield return new WaitForSeconds(.7f);
			}

			Ani.SetBool ("Idle",true);
            Ani.SetBool("Disparo", false);

			//Reiniciar Contador
			if (NumShoot != 0)
			{
				NumShoot = 0;
			}

			yield return new WaitForSeconds(3.5f);

			//Posicionamiento Punto2
			while (transform.position.x != Posiciones[1].transform.position.x)
			{
				transform.position = new Vector3(Posiciones[1].transform.position.x, Posiciones[1].transform.position.y, Posiciones[1].transform.position.z); 
			}

			yield return new WaitForSeconds(2);

			CalculoDistancia();

			yield return new WaitForSeconds(.5f);

			RotationEnemy();

			yield return new WaitForSeconds(1);

			//Contador de Disparos
			while (NumShoot < 3)
			{
				Ani.SetBool ("Disparo",true);
				Ani.SetBool ("Idle",false);
				Rigidbody2D Shoting;
				Shoting = (Rigidbody2D)Instantiate(Bullet, SpawnShoot[Random.Range(0, 3)].position, Quaternion.identity);
				Shoting.velocity = transform.right * BulletSpeed;

				NumShoot++;

				yield return new WaitForSeconds(.7f);
			}

			Ani.SetBool ("Idle",true);
			Ani.SetBool ("Disparo",false);

			//Reiniciar Contador
			if (NumShoot != 0)
			{
				NumShoot = 0;
			}

			yield return new WaitForSeconds(3.5f);

			//Posicionamiento Punto1
			while (transform.position.x != Posiciones[0].transform.position.x)
			{
				Ani.SetBool ("Idle",true);
				Ani.SetBool ("Disparo",false);
				transform.position = new Vector3(Posiciones[0].transform.position.x, Posiciones[0].transform.position.y, Posiciones[0].transform.position.z);
			}

			yield return new WaitForSeconds(1);

			CalculoDistancia();

			yield return new WaitForSeconds(.5f);

			RotationEnemy();

			yield return new WaitForSeconds(.5f);

			//Contador de Disparos
			while (NumShoot < 3)
			{
				Ani.SetBool ("Disparo",true);
				Ani.SetBool ("Idle",false);

				Rigidbody2D Shoting;
				Shoting = (Rigidbody2D)Instantiate(BigBullet, SpawnShoot[1].position, Quaternion.identity);
				Shoting.velocity = transform.right * BulletSpeed;

				NumShoot++;

				yield return new WaitForSeconds(.7f);
			}

			Ani.SetBool ("Idle",true);
			Ani.SetBool ("Disparo",false);

			//Reiniciar Contador
			if (NumShoot != 0)
			{
				NumShoot = 0;
			}

			yield return new WaitForSeconds(1);

			//Posicionamiento Punto2
			while (transform.position.x != Posiciones[1].transform.position.x)
			{
				transform.position = new Vector3(Posiciones[1].transform.position.x, Posiciones[1].transform.position.y, Posiciones[1].transform.position.z);
			}

			yield return new WaitForSeconds(1);

			CalculoDistancia();

			yield return new WaitForSeconds(.5f);

			RotationEnemy();

			yield return new WaitForSeconds(.5f);

			float TimeShoot = 0;

			while (TimeShoot < 1)
			{
				Ani.SetBool ("Tornado",true);
				Ani.SetBool ("Idle",false);

				Rigidbody2D tornado;
				tornado = (Rigidbody2D)Instantiate(Torn,SpawnTornado2.transform.position, Quaternion.identity);
				tornado.velocity = transform.right * TornadoSpeed;
				TimeShoot++;
			}

			if (TimeShoot != 0)
			{
				TimeShoot = 0;
			}

			yield return new WaitForSeconds(3);

			//Posicionamiento Punto1
			while (transform.position.x != Posiciones[0].transform.position.x)
			{
				Ani.SetBool ("Idle",true);
				Ani.SetBool ("Tornado",false);
				transform.position = new Vector3(Posiciones[0].transform.position.x, Posiciones[0].transform.position.y, Posiciones[0].transform.position.z);
			}

			yield return new WaitForSeconds(1);

			CalculoDistancia();

			yield return new WaitForSeconds(.5f);

			RotationEnemy();

			yield return new WaitForSeconds(.5f);



			while (TimeShoot < 1)
			{
				Ani.SetBool ("Tornado",true);
				Ani.SetBool ("Idle",false);

				Rigidbody2D tornado;

				tornado = Instantiate(Torn, SpawnTornado.transform.position,SpawnTornado2.transform.rotation) as Rigidbody2D;
				tornado.velocity = -transform.right * TornadoSpeed;

				TimeShoot++;
			}
		}
		yield return new WaitForSeconds(3f);
	}

	void OnCollisionEnter2D(Collision2D Other)
	{
		if(Other.gameObject.tag == "Bullet")
		{
			GameController.data.sliderHealthBoss.value -= GameController.DisparoBase; 
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Electric" )
		{
			GameController.data.sliderHealthBoss.value -= GameController.ElectricShoot;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Energy")
		{
			GameController.data.sliderHealthBoss.value -= GameController.EnergyBall;
			Destroy(Other.gameObject);
		}
	}


	public void OnTriggerStay2D(Collider2D Other)
	{
		if(Other.gameObject.tag == "Fire")
		{
			GameController.data.sliderHealthBoss.value -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Ice")
		{
			GameController.data.sliderHealthBoss.value -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Tornado")
		{
			GameController.data.sliderHealthBoss.value -= GameController.Tornadito;
			Destroy(Other.gameObject);
		}
	}

	public void CalculoDistancia()
	{
		Dis = (Player.transform.position.x - Enemigo.transform.position.x);
	}

	public void RotationEnemy()
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

	public void RotationTornado()
	{
		if(Dis > 0)
		{
			SpawnTornado.transform.eulerAngles = new Vector2 (0,180);			
		}
		if(Dis < 0)
		{
			SpawnTornado.transform.eulerAngles = new Vector2 (0,0);
		}
	}

	IEnumerator Muerte()
	{
        GameController.Mecanicas = false;
		GameController.ActiveInputs = false;
        GameController.isActive = true;
        GameController.isActiveBossDead = true;
   		Explosion.SetActive(true);        

		yield return new WaitForSeconds(5f);

		Destroy(gameObject);
		Application.LoadLevel("Menu");
		GameController.muerto2 = true;
		GameController.lvl = 0;
		GameController.isActive = false;
		GameController.isActiveBoss = false;
		GameController.isActiveBossDead = false;
		GameController.data.gameArma.enabled = false;
		GameController.data.gameLife.enabled = false;
		GameController.data.HUD_canvas.enabled = false;
		GameController.data.Enemy_canvas.enabled = true;
		arm.Resetear();
		arm.PlaySound();
	}

}