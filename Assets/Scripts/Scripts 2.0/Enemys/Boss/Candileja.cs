using UnityEngine;
using System.Collections;
using Globales;

public class Candileja : MonoBehaviour {
	
	public float BulletSpeed;
	public float Speed;

	public Rigidbody2D Bullet, Meteo;

	public GameObject Meteoro;
	public GameObject Llamarada;
	public GameObject Explosion;
	public GameObject[] SpawnMeteo;
	public GameObject[] Posiciones;

	public Transform[] SpawnShoot;

	float Dis = 0;
	float EmbestidaTime = 0;

	Vector3 move;
	public GameObject Player, Enemigo;
	public Animator Ani;
    int Control = 0;
	public Army_Menu arm;

	void Start() 
	{
		arm = GameObject.Find ("ScriptCanvas").GetComponent<Army_Menu> ();

		if (GameController.muerto1) {
			Destroy(gameObject);
		}

		Enemigo = GameObject.FindWithTag("Boss");
        Player = GameObject.FindGameObjectWithTag("Player");
		SpawnMeteo = GameObject.FindGameObjectsWithTag("SpawnMeteoro");
		Meteoro = GameObject.Find("Meteoros");
	}

	void Update()
	{
		if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
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

        Destroy(GameObject.Find("ShootBoss1(Clone)"), 5f);
	}

	IEnumerator Boss() 
	{
        while (GameController.Mecanicas)
		{
            Ani.SetBool("Idle", true);
            Ani.SetBool("Meteoros", false);
            //Posicionamiento Punto1
			while (transform.position.x != Posiciones[0].transform.position.x)
			{
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
			while (NumShoot < 8)
			{
				Ani.SetBool ("Ataque",true);
				Ani.SetBool ("Idle",false);
				Rigidbody2D Shoting;
				Shoting = (Rigidbody2D)Instantiate(Bullet, SpawnShoot[Random.Range(0, 3)].position, Quaternion.identity);
				Shoting.velocity = transform.right * BulletSpeed;

				NumShoot++;

				yield return new WaitForSeconds(.7f);
			}

			Ani.SetBool ("Idle",true);
			Ani.SetBool ("Ataque",false);

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
			while (NumShoot < 8)
			{
				Ani.SetBool ("Ataque",true);
				Ani.SetBool ("Idle",false);
				Rigidbody2D Shoting;
				Shoting = (Rigidbody2D)Instantiate(Bullet, SpawnShoot[Random.Range(0, 3)].position, Quaternion.identity);
				Shoting.velocity = transform.right * BulletSpeed;

				NumShoot++;

				yield return new WaitForSeconds(.7f);
			}

			Ani.SetBool ("Idle",true);
			Ani.SetBool ("Ataque",false);

			//Reiniciar Contador
			if (NumShoot != 0)
			{
				NumShoot = 0;
			}

			yield return new WaitForSeconds(2);

			CalculoDistancia();

			yield return new WaitForSeconds(.5f);

			RotationEnemy();

			yield return new WaitForSeconds(.5f);

			//Embestida 
			while (EmbestidaTime < 2.0f)
			{
				Ani.SetBool ("Embestida",true);
				Ani.SetBool ("Ataque",false);
				Ani.SetBool ("Idle",false);

				Llamarada.SetActive(true);
				move.x = Speed * Time.deltaTime;
				transform.Translate(move);
				EmbestidaTime += Time.deltaTime;
				yield return new WaitForSeconds(.001f);

			}
			if (EmbestidaTime > 2.0f)
			{
				EmbestidaTime = 0;
				Llamarada.SetActive(false);
			}

			yield return new WaitForSeconds(2);

            Ani.SetBool("Idle", true);
            Ani.SetBool("Embestida", false);

            //Posicionamiento Punto3
			while (transform.position.x != Posiciones[2].transform.position.x)
			{
				transform.position = new Vector3(Posiciones[2].transform.position.x, Posiciones[2].transform.position.y, Posiciones[2].transform.position.z);
			}

			yield return new WaitForSeconds(2);

			CalculoDistancia();

			yield return new WaitForSeconds(.5f);

			RotationEnemy();

			yield return new WaitForSeconds(.5f);

			//Contador de Disparos
			while (NumShoot < 8)
			{
				Ani.SetBool ("Ataque",true);
				Ani.SetBool ("Idle",false);

				Rigidbody2D Shoting;
				Shoting = (Rigidbody2D)Instantiate(Bullet, SpawnShoot[Random.Range(0, 3)].position, Quaternion.identity);
				Shoting.velocity = transform.right * BulletSpeed;

				NumShoot++;

				yield return new WaitForSeconds(.7f);
			}

			Ani.SetBool ("Idle",true);
			Ani.SetBool ("Ataque",false);

			//Reiniciar Contador
			if (NumShoot != 0)
			{
				NumShoot = 0;
			}

			yield return new WaitForSeconds(2);

			//Posicionamiento Punto4
			while (transform.position.x != Posiciones[3].transform.position.x)
			{
				transform.position = new Vector3(Posiciones[3].transform.position.x, Posiciones[3].transform.position.y, Posiciones[3].transform.position.z);
			}

			yield return new WaitForSeconds(2);

			float TimeShoot = 0;
			while (TimeShoot < 10)
			{
				Ani.SetBool ("Meteoros",true);
				Ani.SetBool ("Idle",false);

				Rigidbody2D Meteoro1;
				Rigidbody2D Meteoro2;
				Rigidbody2D Meteoro3;

				Meteoro1 = Instantiate(Meteo, SpawnMeteo[0].transform.position, SpawnMeteo[0].transform.rotation) as Rigidbody2D;
				Meteoro1.AddForce(new Vector2(0, -1) * 5f * Time.deltaTime);
				Meteoro2 = Instantiate(Meteo, SpawnMeteo[1].transform.position, SpawnMeteo[0].transform.rotation) as Rigidbody2D;
				Meteoro2.AddForce(new Vector2(0, -1) * 5f * Time.deltaTime);
				Meteoro3 = Instantiate(Meteo, SpawnMeteo[2].transform.position, SpawnMeteo[0].transform.rotation) as Rigidbody2D;
				Meteoro3.AddForce(new Vector2(0, -1) * 5f * Time.deltaTime);
				TimeShoot++;

				yield return new WaitForSeconds(1);

				/*while (TimeShoot < 5)
                {
                    move.x = 5 * Time.deltaTime;
                    transform.Translate(move);
                }*/
			}

			yield return new WaitForSeconds(2);
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
		GameController.muerto1 = true;
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