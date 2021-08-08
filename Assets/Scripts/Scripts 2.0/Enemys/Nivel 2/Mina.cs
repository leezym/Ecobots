using UnityEngine;
using System.Collections;
using Globales;

public class Mina : MonoBehaviour {

    public GameObject Explosion;

	CircleCollider2D Coll;

    float Health = 5;

    void Start() 
    {
		Coll = GetComponent<CircleCollider2D>();
    }

    void Update() 
    {
        if (Health < 1)
        {
            StartCoroutine(Muerte());
        }
    }

    //Efecto de Muerte
    IEnumerator Muerte()
    {
        Coll.isTrigger = true;
        Explosion.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
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
            Destroy(gameObject);
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
}
