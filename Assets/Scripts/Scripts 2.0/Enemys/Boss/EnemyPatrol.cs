using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {

	public Transform [] PatrolPoints;
	public float Speed;
	public int currentPoint;
	public bool patrol =true;
	public Vector3 Target;
	public Vector3 MoveDirection;
	public Vector3 Velocity;
	
	
	void Update()
	{
		if(currentPoint < PatrolPoints.Length)
		{
			Target = PatrolPoints [currentPoint].position;
			MoveDirection = Target-transform.position;
			Velocity = GetComponent<Rigidbody>().velocity;
			
			if(MoveDirection.magnitude < 1)
			{
				currentPoint++;
			}
			else
			{
				Velocity=MoveDirection.normalized*Speed;
			}
		}
		else
		{
			if(patrol)
			{
				currentPoint=0;
			}
			else
			{
				Velocity=Vector3.zero;
			}
		}
		GetComponent<Rigidbody>().velocity = Velocity;
		transform.LookAt(Target);
	}
}	