using UnityEngine;
using System.Collections;

public class ShootEnemy : MonoBehaviour {

	// Variables Publicas
	public int Damage = 1;
	public float BulletSpeed = 0.1f;
	public GameObject SpawnBullet;
	public Rigidbody2D Bullet;
	public Transform Target;
	//Variables Privadas
	private float TimeFire = 1.5f;
	
	void OnTriggerStay2D(Collider2D Detectar)
	{
		if(Detectar.tag == "Player")
		{
			Debug.Log("Entro");
			if(TimeFire <=0)
			{
				Debug.Log("Entra");
				Rigidbody2D Shoting;
				Shoting = Instantiate(Bullet,SpawnBullet.transform.position,SpawnBullet.transform.rotation) as Rigidbody2D;
				Shoting.velocity = transform.TransformDirection(Vector2.left * BulletSpeed);
				TimeFire = 1.5f;
			}
			Vector2 Point = new Vector2(Target.transform.position.x, Target.transform.position.y);
			transform.LookAt (new Vector3(Point.x,Point.y,0));
		}
		TimeFire -= Time.deltaTime;
	}

}
