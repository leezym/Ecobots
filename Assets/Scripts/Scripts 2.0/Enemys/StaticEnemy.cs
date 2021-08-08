using UnityEngine;
using System.Collections;

public class StaticEnemy : MonoBehaviour {

	public Transform Player;
	public Transform DistanceAtack;
	public bool Detected = false;
	

	// Update is called once per frame
	void Update () 
	{
		DetectedPlayer ();
	}

	void OnTriggerStay2D(Collider2D Target)
	{
		if(Target.tag == "Player")
		{
			if(Player.position.x < transform.position.x)
			{
				transform.eulerAngles = new Vector2 (0,0);
			}
			if(Player.position.x > transform.position.x)
			{
				transform.eulerAngles = new Vector2 (0,180);
			}
		}
	}

	void DetectedPlayer()
	{
		Debug.DrawLine (transform.position,DistanceAtack.position,Color.blue);
		Detected = Physics2D.Linecast (transform.position,DistanceAtack.position, 1 << LayerMask.NameToLayer("Player"));
	}

	
}
