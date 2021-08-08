using UnityEngine;
using System.Collections;

public class SubirArena : MonoBehaviour {

	public bool Activar = false;
	public GameObject Arena;
	public GameObject puerta;
	float Speed = 2.5f;
	Vector3 Move;
	public int Kills;

	void Update()
	{
		Move.y = 0.5f * Time.deltaTime;

		if(Activar)
		{
			Arena.transform.Translate(Move);
		}

		if(Kills > 1)
		{
			puerta.transform.Translate(0, 10, 0);
			Invoke("transladarPuerta", 0.5f);
		}

	}

	void transladarPuerta()
	{
		puerta.transform.Translate(0, -10, 0);
		Kills = 0;
	}

	void OnTriggerEnter2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Activar = true;
		}
	}
}
