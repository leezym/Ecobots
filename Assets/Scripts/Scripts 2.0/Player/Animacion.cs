using UnityEngine;
using System.Collections;

public class Animacion : MonoBehaviour {

	PJ player;
	int Jump = Animator.StringToHash("Jump");
	public Animator Anim;
	Shooting shoot;
    Controller2D Control;

	void Start () 
	{
		player = GameObject.FindWithTag ("Player").GetComponent<PJ> ();
		shoot = GameObject.FindWithTag ("Player").GetComponent<Shooting> ();
		Anim = GetComponent<Animator>();
        Control = GameObject.FindWithTag("Player").GetComponent<Controller2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		Anim.SetFloat ("Speed",Mathf.Abs(player.input.x));
		Anim.SetBool ("Shoot", shoot.Shoot);
		Anim.SetBool("Ground",player.Ground);

		if(player.targetVelocityX > 0.1f && Input.GetKey(KeyCode.X) == true)
		{
			Anim.SetFloat ("Speed",player.targetVelocityX);
			Anim.SetBool ("Shoot", true);
		}
	}
}
