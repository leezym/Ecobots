using UnityEngine;
using System.Collections;

public class DestructorBullet : MonoBehaviour {

	void OnTriggerCollision2D(Collision2D Other)
	{
		if(Other.gameObject.layer == LayerMask.NameToLayer("Bala") || Other.gameObject.layer == LayerMask.NameToLayer("BalaEnemy"))
		{
			Destroy (Other.gameObject);
		}
	}
}
