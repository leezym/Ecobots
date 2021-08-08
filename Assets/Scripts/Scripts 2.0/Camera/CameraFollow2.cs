using UnityEngine;
using System.Collections;

public class CameraFollow2 : MonoBehaviour {

	public Transform Target;
    public float Dumping = 1;
    public float LookAheadFactor = 3;
    public float LookAheadReturnSpeed = 0.5f;
    public float LookAheadMoveThreshold = 0.1f;
    public float YPosRestriction = -1;

    float OffSetZ;
    Vector3 LastTargerPosition;
    Vector3 CurrentVelocity;
    Vector3 LookAheadPos;

	float NextTimeToSearch = 0;

    void Start() 
    {
        if (Target != null)
        {
            Target = transform.Find("JoeZ(Clone)");
        }
		
		//InstancePlayerCamera();
    }

    void Update() 
    {
		if(Target == null)
		{
			InstancePlayerCamera ();
		}
    }

	void LateUpdate()
	{
		if(Target == null)
		{
			FindPlayer();
			return;
		}

		float XMoveDelta = (Target.position - LastTargerPosition).x;
		bool UpdateLoockAheadTarget = Mathf.Abs(XMoveDelta) > LookAheadMoveThreshold;

		if (UpdateLoockAheadTarget)
		{
			LookAheadPos = LookAheadFactor * Vector3.right * Mathf.Sign(XMoveDelta);
		}
		else 
		{
			LookAheadPos = Vector3.MoveTowards(LookAheadPos, Vector3.zero, Time.deltaTime * LookAheadReturnSpeed);
		}

		Vector3 AheadTargetPos = Target.position + LookAheadPos + Vector3.forward * OffSetZ;
		Vector3 NewPos = Vector3.SmoothDamp(transform.position, AheadTargetPos, ref CurrentVelocity,Dumping);

		NewPos = new Vector3(NewPos.x, Mathf.Clamp(NewPos.y, YPosRestriction, Mathf.Infinity), NewPos.z);

		transform.position = NewPos;
		LastTargerPosition = Target.position;
	}

	public void InstancePlayerCamera()
	{
		//Target = transform.Find ("JoeZ(Clone)");
        if (Target != null)
        {
            LastTargerPosition = Target.position;
            OffSetZ = (transform.position - Target.position).z;
            transform.parent = null;
        }
		
		
	}

    void FindPlayer() 
    {
		if(NextTimeToSearch <= Time.deltaTime)
		{
			GameObject SearchReasult = GameObject.FindWithTag ("Player");

			if(SearchReasult != null)
			{
				Target = SearchReasult.transform;
				NextTimeToSearch = Time.time + 0.5f;
			}
		}
    }
}
