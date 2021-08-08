using UnityEngine;
using System.Collections;
using Globales;

public class Llorona : MonoBehaviour {

    public GameObject SpawnShoot;
    public GameObject Estalactitas;
    public GameObject Explosion;
    
    public Rigidbody2D Bullet;
    public Rigidbody2D BigShoot;

    public float SpeedBullet;

    public GameObject[] Posiciones;
    public GameObject[] PosEstalactitas;

    public Animator Anim;

    public Army_Menu arm;

    GameObject Player;

    int Control = 0;

    float Dis = 0;

    void Start()
    {
        arm = GameObject.Find("ScriptCanvas").GetComponent<Army_Menu>();
		Anim = GetComponent<Animator> ();

        if (GameController.muerto5)
        {
            Destroy(gameObject);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        if (GameController.data.sliderHealthBoss.value == GameController.data.sliderHealthBoss.minValue)
        {
            StartCoroutine(Muerte());
        }

        if (GameController.Mecanicas)
        {
            if (Control == 0)
            {
                StartCoroutine(Boss());
                Control++;
            }
        }

        Destroy(GameObject.Find("Shoot3(Clone)"), 5f);
    }

    IEnumerator Boss() 
    {
        while (GameController.Mecanicas)
        {
            while (transform.position.x != Posiciones[0].transform.position.x)
            {
                transform.position = new Vector3(Posiciones[0].transform.position.x, Posiciones[0].transform.position.y, Posiciones[0].transform.position.z);
            }

            yield return new WaitForSeconds(2);

            CalculoDistancia();

            yield return new WaitForSeconds(.5f);

            RotationEnemy();

            yield return new WaitForSeconds(1);

            //Contador de Disparos
            int NumShoot = 0;
            while (NumShoot < 5)
            {
                Rigidbody2D Shoting;
                Shoting = (Rigidbody2D)Instantiate(Bullet, SpawnShoot.transform.position, Quaternion.identity);
                Shoting.velocity = transform.right * SpeedBullet;

                NumShoot++;

                yield return new WaitForSeconds(.7f);
            }

            if (NumShoot != 0)
            {
                NumShoot = 0;
            }

            yield return new WaitForSeconds(2f);

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
           
            while (NumShoot < 5)
            {
                Rigidbody2D Shoting;
                Shoting = (Rigidbody2D)Instantiate(Bullet, SpawnShoot.transform.position, Quaternion.identity);
                Shoting.velocity = transform.right * SpeedBullet;

                NumShoot++;

                yield return new WaitForSeconds(.7f);
            }

            if (NumShoot != 0)
            {
                NumShoot = 0;
            }

            yield return new WaitForSeconds(2f);

            while (transform.position.x != Posiciones[0].transform.position.x)
            {
                transform.position = new Vector3(Posiciones[0].transform.position.x, Posiciones[0].transform.position.y, Posiciones[0].transform.position.z);
            }

            yield return new WaitForSeconds(2);

            CalculoDistancia();

            yield return new WaitForSeconds(.5f);

            RotationEnemy();

            yield return new WaitForSeconds(1);

            for (int i = 0; i < PosEstalactitas.Length; i++)
            {
                Estalactitas = Instantiate(Estalactitas, PosEstalactitas[i].transform.position, Quaternion.identity) as GameObject;
                Estalactitas.transform.Translate(0, 0, 0);
                Destroy(Estalactitas.gameObject, 3f);
            }

            yield return new WaitForSeconds(2);

            while (transform.position.x != Posiciones[1].transform.position.x)
            {
                transform.position = new Vector3(Posiciones[1].transform.position.x, Posiciones[1].transform.position.y, Posiciones[1].transform.position.z);
            }

            yield return new WaitForSeconds(2);

            CalculoDistancia();

            yield return new WaitForSeconds(.5f);

            RotationEnemy();

            yield return new WaitForSeconds(1);

            Rigidbody2D Shoting1;
            Shoting1 = (Rigidbody2D)Instantiate(BigShoot, SpawnShoot.transform.position, Quaternion.identity);
            Shoting1.velocity = transform.right * SpeedBullet;

            yield return new WaitForSeconds(2);
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
        GameController.muerto5 = true;
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

    public void CalculoDistancia()
    {
        Dis = (Player.transform.position.x - transform.position.x);
    }

    public void RotationEnemy()
    {
        if (Dis > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Dis < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "Bullet")
        {
            GameController.data.sliderHealthBoss.value -= GameController.DisparoBase;
            Destroy(Other.gameObject);
        }

        if (Other.gameObject.tag == "Electric")
        {
            GameController.data.sliderHealthBoss.value -= GameController.ElectricShoot;
            Destroy(Other.gameObject);
        }

        if (Other.gameObject.tag == "Energy")
        {
            GameController.data.sliderHealthBoss.value -= GameController.EnergyBall;
            Destroy(Other.gameObject);
        }
    }


    public void OnTriggerStay2D(Collider2D Other)
    {
        if (Other.gameObject.tag == "Fire")
        {
            GameController.data.sliderHealthBoss.value -= GameController.IceAndFire;
        }

        if (Other.gameObject.tag == "Ice")
        {
            GameController.data.sliderHealthBoss.value -= GameController.IceAndFire;
        }

        if (Other.gameObject.tag == "Tornado")
        {
            GameController.data.sliderHealthBoss.value -= GameController.Tornadito;
            Destroy(Other.gameObject);
        }
    }
}
