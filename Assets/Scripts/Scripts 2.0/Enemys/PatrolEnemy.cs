using UnityEngine;
using System.Collections;

public class PatrolEnemy : MonoBehaviour {

	//Variables Publicas
	public float Speed = 1.5f;
	public float LeftDir ;
	public float RightDir ;
	//Variables privadas
	private Vector2 WalkDistance;

	void Start () 
	{
		LeftDir = transform.position.x - 2.5f;
		RightDir = transform.position.x + 2.5f;
	}
	
	// Patrullaje
	void Update () 
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
}
