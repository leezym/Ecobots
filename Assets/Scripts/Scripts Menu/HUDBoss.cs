using UnityEngine;
using System.Collections;
using Globales;
using UnityEngine.UI;

public class HUDBoss : MonoBehaviour {

	public Texture[] itemBoss;

	void Update(){
		ItemBoss ();
	}

	void ItemBoss()
	{	

		for (int i=1;i<itemBoss.Length -1; i++)
		{
			if (GameController.lvl == i) 
			{
				gameObject.GetComponent<RawImage>().texture = itemBoss [i];
			}			
		}
	}
}
