using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Controller2D Target;
	public Vector2 FocusAreaSize;
	public float VerticalOffSet;
	FocusArea focusarea;
    
	void Start()
	{
        if(Target != null)
        {
            atacharCamara();
        }
	}

    public void atacharCamara() 
    {
		if(Target == null)
		{
			Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller2D>();
			focusarea = new FocusArea(Target.collider.bounds, FocusAreaSize);
		}
    }

	void LateUpdate()
	{
        if(Target== null)
        {
            atacharCamara();
        }
		focusarea.Update (Target.collider.bounds);
		Vector2 FocusPos = focusarea.Center + Vector2.up * VerticalOffSet;
		transform.position = (Vector3)FocusPos + Vector3.forward * -10;
	}

	void OnDrawGizmos()
	{
//		Gizmos.color = new Color (1,0,0,.5f);
//		Gizmos.DrawCube (focusarea.Center, FocusAreaSize);
	}

	struct FocusArea
	{
		public Vector2 Center;
		public Vector2 Velocity;
		float Left,Right;
		float Top,Bottom;

		public FocusArea(Bounds TargetBounds, Vector2 Size)
		{
			Left = TargetBounds.center.x - Size.x/2;
			Right = TargetBounds.center.x + Size.x/2;
			Bottom = TargetBounds.min.y;
			Top = TargetBounds.min.y + Size.y;
			Velocity = Vector2.zero;
			Center = new Vector2((Left + Right)/2,(Top+Bottom)/2);
		}

		public void Update(Bounds TargetBounds)
		{
			float ShiftX = 0;
			if(TargetBounds.min.x < Left)
			{
				ShiftX = TargetBounds.min.x - Left;
			}else if (TargetBounds.max.x > Right)
			{
				ShiftX = TargetBounds.max.x - Right;
			}
			Left += ShiftX;
			Right += ShiftX;

			float ShiftY = 0;
			if(TargetBounds.min.y < Bottom)
			{
				ShiftY = TargetBounds.min.y - Bottom;
			}else if(TargetBounds.max.y > Top)
			{
				ShiftY = TargetBounds.max.y - Top;
			}
			Top += ShiftY;
			Bottom += ShiftY;

			Center = new Vector2((Left + Right)/2,(Top+Bottom)/2);
			Velocity = new Vector2 (ShiftX, ShiftY);
		}
	}

}
