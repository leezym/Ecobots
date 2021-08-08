using UnityEngine;
using System.Collections;

public class Capa : MonoBehaviour {

	public LayerMask collisionMask;

	void Udate()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position,Vector2.right,1.5f,collisionMask);
	}
}
