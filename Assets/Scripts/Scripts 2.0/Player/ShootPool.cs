using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootPool : MonoBehaviour {

	public GameObject Bullet;
	public int Amount;

	private List<GameObject> available = new List<GameObject> ();

	void Awake()
	{
		InstantiateBullet ();
	}


	void InstantiateBullet()
	{
		for(int i =0; i<Amount; i++)
		{
			GameObject go = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
			go.SendMessage("SetInitialState",SendMessageOptions.RequireReceiver);
			available.Add(go);
		}

	}

	public GameObject GetGameObject()
	{
		if (available.Count > 0) {
			GameObject go = available [0];
			go.SendMessage ("SetAwakeState", SendMessageOptions.RequireReceiver);
			available.RemoveAt (0);
			return go;
		} else 
		{
			GameObject go = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
			go.SendMessage("SetAwakeState", SendMessageOptions.RequireReceiver);
			return go;
		}
	}

	public void ReleaseGameObject(GameObject go)
	{
		go.SendMessage ("SetInitialState", SendMessageOptions.RequireReceiver);
		available.Add (go);
	}
}
