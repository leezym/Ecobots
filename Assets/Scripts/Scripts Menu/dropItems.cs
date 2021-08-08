using UnityEngine;
using System.Collections;
using Globales;

public class dropItems : MonoBehaviour {

	float posX,posY, posZ;
	int rnd, rnd2;
	GameObject SaludL, SaludS,MuniL, MuniS;

	public void startDrop()
	{
		MuniL = Resources.Load ("municionL") as GameObject;
		MuniS = Resources.Load ("municionS") as GameObject;
		SaludL = Resources.Load ("healthL") as GameObject;
		SaludS = Resources.Load ("healthS") as GameObject;

		posX = gameObject.transform.position.x;
		posY = gameObject.transform.position.y;
		posZ = gameObject.transform.position.z;
		rnd = Random.Range (1,101);
		rnd2 = Random.Range (1,101);
	}

	public void randomDrop()
	{
		if (rnd > 0 && rnd < 16) 
		{
			if (rnd2 > 0 && rnd2 < 36)
			{
				Instantiate(SaludL,new Vector3(posX,posY,posZ),Quaternion.identity);
				Debug.Log ("saludsota");
			}else if (rnd2 > 35 && rnd2 < 101){
				Instantiate(SaludS,new Vector3(posX,posY,posZ),Quaternion.identity);
				Debug.Log ("saludsita");
			}
		}
			
		if (rnd > 15 && rnd < 36)
		{
			if (rnd2 > 0 && rnd2 < 36)
			{
				Instantiate(MuniL,new Vector3(posX,posY,posZ),Quaternion.identity);
				Debug.Log ("munisota");
			}else if (rnd2 > 35 && rnd2 < 101) {
				Instantiate(MuniS,new Vector3(posX,posY,posZ),Quaternion.identity);
				Debug.Log ("munisita");
			}
		}
	}
}