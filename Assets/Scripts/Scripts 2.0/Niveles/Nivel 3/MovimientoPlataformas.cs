using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovimientoPlataformas : RaycastController {

	public LayerMask PassemenrMask;
	public Vector3[] LocalWayPoints;
	public float Speed;
	public bool Cyclic;
	int FromWayPoints;
	float PorcentajeEntrePuntos;
	Vector3[] GlobalWayPoints;



	public override void Start () 
	{
		base.Start ();
		GlobalWayPoints = new Vector3[LocalWayPoints.Length];
		for(int i = 0; i < LocalWayPoints.Length; i++)
		{
			GlobalWayPoints[i] = LocalWayPoints[i] + transform.position;
		}
	}

	void Update () 
	{
		UpdateRaycastOrigins ();
		Vector3 Velocity = CalcularMovimientoPlataforma();
		MovePassengers (Velocity);
		transform.Translate (Velocity);
	}

	Vector3 CalcularMovimientoPlataforma()
	{
		FromWayPoints %= GlobalWayPoints.Length;
		int ToWayPointIndex = (FromWayPoints + 1)%GlobalWayPoints.Length;
		float DistanciaEntrePuntos = Vector3.Distance (GlobalWayPoints[FromWayPoints],GlobalWayPoints[ToWayPointIndex]);
		PorcentajeEntrePuntos += Time.deltaTime * Speed/DistanciaEntrePuntos;

		Vector3 NewPos = Vector3.Lerp (GlobalWayPoints[FromWayPoints],GlobalWayPoints[ToWayPointIndex],PorcentajeEntrePuntos);

		if(PorcentajeEntrePuntos >= 1)
		{
			PorcentajeEntrePuntos = 0;
			FromWayPoints ++;
			if(!Cyclic)
			{
				if(FromWayPoints >= GlobalWayPoints.Length - 1)
				{
					FromWayPoints = 0;
					System.Array.Reverse(GlobalWayPoints);
				}
			}
		}

		return NewPos - transform.position;
	}

	void MovePassengers(Vector3 velocity)
	{
		HashSet<Transform> movePassenger = new HashSet<Transform> ();

		float DirectionX = Mathf.Sign (velocity.x);
		float DirectionY = Mathf.Sign (velocity.y);

		//Movimiento Vertical
		if(velocity.y != 0)
		{
			float RayLenght = Mathf.Abs (0.5f) + skinWidth;
			
			for(int i=0; i<VerticalRayCount; i++)
			{
				Vector2 RayOrigin = (DirectionY == -1)?raycastorigins.BottomLeft : raycastorigins.TopLeft;
				RayOrigin += Vector2.right * (VerticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(RayOrigin, Vector2.up * DirectionY, RayLenght, PassemenrMask);

				if(hit)
				{
					if(!movePassenger.Contains(hit.transform))
					{
						movePassenger.Add(hit.transform);
						float PushX = (DirectionY == 1)?velocity.x:0; 
						float PushY = velocity.y - (hit.distance - skinWidth) * DirectionY;
						
						hit.transform.Translate(new Vector3(PushX,PushY));
					}
				}
			}
		}

		//Movimiento Horizontal
		if(velocity.x !=0)
		{
			float RayLenght = Mathf.Abs (velocity.x) + skinWidth;

			for(int i=0; i<HorizontalRayCount; i++)
			{
				Vector2 RayOrigin = (DirectionX == -1)?raycastorigins.BottomLeft : raycastorigins.BottomRight;
				RayOrigin += Vector2.up * HorizontalRaySpacing * i;
				RaycastHit2D hit = Physics2D.Raycast(RayOrigin, Vector2.right * DirectionX, RayLenght, PassemenrMask);

				if(hit)
				{
					if(!movePassenger.Contains(hit.transform))
					{
						movePassenger.Add(hit.transform);
						float PushX = velocity.x - (hit.distance - skinWidth) * DirectionX;
						float PushY = 0;
						
						hit.transform.Translate(new Vector3(PushX,PushY));
					}
				}
			}
		}

		//Movimiento Horizontal o hacia Abajo
		if(DirectionY == -1 || velocity.y == 0 && velocity.x != 0)
		{
			float RayLenght = skinWidth * 2 ;
			
			for(int i=0; i<VerticalRayCount; i++)
			{
				Vector2 RayOrigin = raycastorigins.TopLeft + Vector2.right * (VerticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(RayOrigin, Vector2.up, RayLenght, PassemenrMask);
				
				if(hit)
				{
					if(!movePassenger.Contains(hit.transform))
					{
						movePassenger.Add(hit.transform);
						float PushX = velocity.x;
						float PushY = velocity.y;
						
						hit.transform.Translate(new Vector3(PushX,PushY));
					}
				}
			}
		}
	}

	void OnDrawGizmos()
	{
		if(LocalWayPoints != null)
		{
			Gizmos.color = Color.blue;
			float Size = .3f;

			for(int i = 0; i < LocalWayPoints.Length; i++)
			{
				Vector3 globalWayPoints = (Application.isPlaying)?GlobalWayPoints[i] : LocalWayPoints[i] + transform.position;
				Gizmos.DrawLine(globalWayPoints - Vector3.up * Size, globalWayPoints + Vector3.up * Size);
				Gizmos.DrawLine(globalWayPoints - Vector3.left * Size, globalWayPoints + Vector3.left * Size);
			}
		}
	}
}