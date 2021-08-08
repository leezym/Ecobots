using UnityEngine;
using System.Collections;


[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour 
{
	public const float skinWidth = .005f;
	
	public int HorizontalRayCount = 4;
	public int VerticalRayCount = 4;
	public LayerMask collisionMask;
	
	[HideInInspector]
	public float HorizontalRaySpacing;
	[HideInInspector]
	public float VerticalRaySpacing;
	
	[HideInInspector]
	public BoxCollider2D collider;
	public RaycastOgins raycastorigins;

	float maxClimbAngle = 80;
	float maxDecendAngle = 80;
	PJ Lado;

	public CollisionInfo collisions;

	void Awake()
	{
		Lado = GetComponent<PJ>();
		collisions.FaceDir = 1;
	}

//	public override void Start()
//	{
//		base.Start ();
//	}

	public virtual void Start()
	{
		collider = GetComponent<BoxCollider2D>();
		CalculateRaySpacing ();
	}

    void Update() 
    {
        if(collider == null)
        {
            collider = GetComponent<BoxCollider2D>();
        }
    }


	public void Move(Vector3 velocity, Vector3 AirJump)
	{

		UpdateRaycastOrigins ();
		collisions.Reset ();
		collisions.velocityOld = velocity;

		if(velocity.y < 0)
		{
			DecendSlope(ref velocity);
		}

		if(velocity.x != 0)
		{
			collisions.FaceDir = (int)Mathf.Sign (velocity.x);
		}

		HorizontalCollision(ref velocity);

		if(velocity.y != 0)
		{
			VerticalCollision (ref velocity);
		}

		transform.Translate (velocity);
		transform.Translate (AirJump);
	}

	void HorizontalCollision(ref Vector3 velocity)
	{
		float DirectionX = Lado.Dir;
		float RayLenght = Mathf.Abs (velocity.x) + skinWidth;

		if(Mathf.Abs(velocity.x)< skinWidth)
		{
			RayLenght = 2 * skinWidth;
		}
		
		for(int i=0; i<HorizontalRayCount; i++)
		{
			Vector2 RayOrigin = (DirectionX == -1)?raycastorigins.BottomLeft : raycastorigins.BottomRight;
			RayOrigin += Vector2.up * HorizontalRaySpacing * i;
			RaycastHit2D hit = Physics2D.Raycast(RayOrigin, Vector2.right * DirectionX, RayLenght, collisionMask);

			Debug.DrawRay(RayOrigin, Vector2.right * DirectionX * RayLenght, Color.red);

			if (hit)
			{
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

				if(i == 0 && slopeAngle <= maxClimbAngle)
				{
					if(collisions.descendingSlope)
					{
						collisions.descendingSlope = false;
						velocity = collisions.velocityOld;
					}
					float distanceToSlopeStart = 0;
					if(slopeAngle != collisions.slopeAngleOld)
					{
						distanceToSlopeStart = hit.distance-skinWidth;
						velocity.x -= distanceToSlopeStart * DirectionX;
					}
					ClimbSlope(ref velocity, slopeAngle);
					velocity.x += distanceToSlopeStart * DirectionX;
				}

				if(!collisions.ClimbingSlope || slopeAngle > maxClimbAngle)
				{
					velocity.x = (hit.distance - skinWidth) * DirectionX;
					RayLenght = hit.distance;

					if(collisions.ClimbingSlope)
					{
						velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
					}

					collisions.left = DirectionX == -1;
					collisions.right = DirectionX == 1;
				}
			}
		}
	}


	void VerticalCollision(ref Vector3 velocity)
	{
		float DirectionY = Mathf.Sign (velocity.y);
		float RayLenght = Mathf.Abs (velocity.y) + skinWidth;

		for(int i=0; i<VerticalRayCount; i++)
		{
			Vector2 RayOrigin = (DirectionY == -1)?raycastorigins.BottomLeft : raycastorigins.TopLeft;
			RayOrigin += Vector2.right * VerticalRaySpacing * i;
			RaycastHit2D hit = Physics2D.Raycast(RayOrigin, Vector2.up * DirectionY, RayLenght, collisionMask);

			Debug.DrawRay(RayOrigin, Vector2.up * DirectionY * RayLenght, Color.red);

			if (hit)
			{
				velocity.y = (hit.distance - skinWidth) * DirectionY;
				RayLenght = hit.distance;

				if(collisions.ClimbingSlope)
				{
					velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Sin(velocity.x));
				}

				collisions.below = DirectionY == -1;
				collisions.above = DirectionY == 1;
			}
		}
		if(collisions.ClimbingSlope)
		{
			float directionX = Mathf.Sign(velocity.x);
			RayLenght = Mathf.Abs(velocity.x) + skinWidth;
			Vector2 rayOrigin = ((directionX == -1)?raycastorigins.BottomLeft:raycastorigins.BottomRight) + Vector2.up * velocity.y;
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.right * directionX,RayLenght,collisionMask);
			
			if(hit)
			{
				float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
				if(slopeAngle != collisions.slopeAngle)
				{
					velocity.x = (hit.distance - skinWidth) * directionX;
					collisions.slopeAngle = slopeAngle;
				}
			}
		}
	}

	void ClimbSlope(ref Vector3 velocity, float slopeAngle)
	{
		float moveDistance = Mathf.Abs (velocity.x);
		float climbVelocityY = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDistance;

		if (velocity.y <= climbVelocityY) {
			velocity.y = climbVelocityY;
			velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
			collisions.below = true;
			collisions.ClimbingSlope = true;
			collisions.slopeAngle = slopeAngle;
		}
	}

	void DecendSlope(ref Vector3 velocity)
	{
		float directionX = Mathf.Sign (velocity.x);
		Vector2 rayOrigin = (directionX == -1)?raycastorigins.BottomRight : raycastorigins.BottomLeft;
		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, Mathf.Infinity,collisionMask);

		if(hit)
		{
			float slopeAngle =Vector2.Angle(hit.normal, Vector2.up);
			if(slopeAngle != 0 && slopeAngle <= maxDecendAngle)
			{
				if(Mathf.Sign(hit.normal.x) == directionX)
				{
					if(hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
					{
						float moveDistance = Mathf.Abs(velocity.x);
						float decendVelocity = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
						velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
						velocity.y -= decendVelocity;

						collisions.slopeAngle = slopeAngle;
						collisions.descendingSlope = true;
						collisions.below = true;
					}
				}
			}
		}
	}

	public void UpdateRaycastOrigins()
	{
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);
		
		raycastorigins.BottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastorigins.BottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastorigins.TopLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastorigins.TopRight = new Vector2 (bounds.max.x, bounds.max.y);
	}
	
	public void CalculateRaySpacing()
	{
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);
		
		HorizontalRayCount = Mathf.Clamp (HorizontalRayCount, 2, int.MaxValue);
		VerticalRayCount = Mathf.Clamp (VerticalRayCount, 2, int.MaxValue);
		
		HorizontalRaySpacing = bounds.size.y / (HorizontalRayCount - 1);
		VerticalRaySpacing = bounds.size.y / (VerticalRayCount - 1);
	}

	public struct RaycastOgins
	{
		public Vector2 TopLeft, TopRight;
		public Vector2 BottomLeft, BottomRight;
	}
	
	public struct CollisionInfo
	{
		public bool above, below;
		public bool left, right;
		public bool ClimbingSlope;
		public bool descendingSlope;
		public float slopeAngle, slopeAngleOld;
		public int FaceDir;
		public Vector3 velocityOld;
		
		public void Reset()
		{
			above = below = false;
			left = right = false;
			ClimbingSlope = false;
			descendingSlope = false;
			
			slopeAngleOld = slopeAngle;
			slopeAngle = 0;
		}
	}
}
