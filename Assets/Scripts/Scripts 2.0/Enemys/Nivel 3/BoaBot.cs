using UnityEngine;
using System.Collections;
using Globales;

public class BoaBot : MonoBehaviour {

	public int Health = 50;

	public float Speed =2;
	public float CountUp;
	public float CountDown;

	public GameObject DistanceAtack;

	bool Detected;

	Vector2 WalkDistance;
	Vector3 Pos;

	Animator Anin;
	
	void Start () 
	{
		Pos = transform.position;
		Anin = GetComponent<Animator> ();
	}

	void Update () 
	{
		DetectedPlayer ();

		if(Detected)
		{
			Anin.SetBool ("Detected", true);
			transform.position = new Vector2 (transform.position.x, transform.position.y + 5);
			Invoke ("Decenso", 1f);
		}
	}

	void Decenso()
	{
		Anin.SetBool ("Detected",false);
		transform.position = new Vector2(transform.position.x, transform.position.y - 5);
	}

	//Detectar jugador
	void DetectedPlayer()
	{
		Debug.DrawLine (transform.position,DistanceAtack.transform.position,Color.blue);
		Detected = Physics2D.Linecast (transform.position,DistanceAtack.transform.position, 1 << LayerMask.NameToLayer("Player"));
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
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Ice")
		{
			Health -= GameController.IceAndFire;
			Destroy(Other.gameObject);
		}

		if(Other.gameObject.tag == "Tornado")
		{
			Health -= GameController.Tornadito;
			Destroy(Other.gameObject);
		}
	}
		
}
