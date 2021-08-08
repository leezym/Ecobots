using UnityEngine;
using System.Collections;

public class RaycastController : MonoBehaviour {

	public const float skinWidth = .015f;

	public int HorizontalRayCount = 4;
	public int VerticalRayCount = 4;
	LayerMask collisionMask;

	[HideInInspector]
	public float HorizontalRaySpacing;
	[HideInInspector]
	public float VerticalRaySpacing;
	
	[HideInInspector]
	public BoxCollider2D collider;
	public RaycastOgins raycastorigins;

	public virtual void Start()
	{
		collider = GetComponent<BoxCollider2D>();
		CalculateRaySpacing ();
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
}
