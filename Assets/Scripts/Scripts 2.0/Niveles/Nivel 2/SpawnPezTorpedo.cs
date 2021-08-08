using UnityEngine;
using System.Collections;

public class SpawnPezTorpedo : MonoBehaviour {

	public GameObject Pez, PJ;
	bool Lanzamiento;
	public float tiempo = 0, Dis;

	void Start()
	{
        PJ = GameObject.Find("Personaje(Clone)");
        Pez = Resources.Load("PezTorpedo") as GameObject;
	}

	void Update()
	{
		if(PJ == null)
        {
            PJ = GameObject.Find("Personaje(Clone)");
        }
        
        CalculoDistancia ();

		if(Lanzamiento)
		{
			tiempo += Time.deltaTime;

			if(tiempo > 1f)
			{
				GameObject ShootingPez = Instantiate (Pez,transform.position,transform.rotation) as GameObject;
				tiempo = 0;
			}
		}
        Destroy(GameObject.Find("PezTorpedo(Clone)"), 2f);
	}
	
	void CalculoDistancia()
	{
		Dis = (PJ.transform.position.x - transform.position.x);
	}

	void OnTriggerStay2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Lanzamiento = true;
		}
	}

	void OnTriggerExit2D(Collider2D Tar)
	{
		if(Tar.gameObject.tag == "Player")
		{
			Lanzamiento = false;
		}
	}
}
