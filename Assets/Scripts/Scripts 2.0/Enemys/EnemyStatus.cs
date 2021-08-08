using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {

	// Variables Publicas
	public float Vit = 100 ;
	private Shooting Daño ;
	public Transform DistanceAtack;
	public bool Detected = false;

//	private void Awake () 
//	{
//		Daño = GetComponent<Shooting>();
//	}

	void DetectedPlayer()
	{
		Debug.DrawLine (transform.position,DistanceAtack.position,Color.blue);
		Detected = Physics2D.Linecast (transform.position,DistanceAtack.position, 1 << LayerMask.NameToLayer("Player"));
	}

	void Update()
	{
		DetectedPlayer ();

		if(Detected)
		{
			Debug.Log("Golpe");
		}

	}

	void ChangeHP(float HP)
	{
		Vit += HP;
		if (Vit <= 0) 
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D Target)
	{

		if(Target.gameObject.tag == "Bullet")
		{
			Debug.Log ("Me pego");
			//ChangeHP(15);
			Destroy(Target.gameObject);
//			if(Daño.LVL == 0)
//			{
//				Debug.Log("nivel 0");
//			}
		}
	}
}
