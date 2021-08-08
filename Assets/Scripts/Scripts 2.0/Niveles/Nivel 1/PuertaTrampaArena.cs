using UnityEngine;
using System.Collections;

public class PuertaTrampaArena : MonoBehaviour {

	public int Count = 0;
	public GameObject Puerta;

	void Update()
	{
		if(Count == 5)
		{
			Puerta.transform.Translate(10,0,0);
		}
	}
}
