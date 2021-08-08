using UnityEngine;
using System.Collections;
using Globales;
using UnityEngine.UI;

[RequireComponent (typeof (Controller2D))]
public class PJ : MonoBehaviour 
{
	public Slider[] sliderDash;
	public Vector2 WallJump;//Distancia que salta el jugador en la pared
	public Vector2 input;
	public Vector2 WallLeap;//Distancia que salte el jugador al alejarse de la pared
	public float SpeedSlideMax = 3, WallTimeStick = .25f, Dir, timeToJumpApex = .4f, jumpHeight = 4, targetVelocityX;
	public bool Ground;

	float TimeToWallInStick;
	float accelerationTimeAirBone;
	float accelerationTimeGround;
	float moveSpeed = 6;
	float Acceleration = 15;
	float NumJump;

	float gravity;
	float jumpVelocity;
	bool DobleJump = false;
	Vector3 velocity;
	Vector3 AirJump;
	Vector3 DobleJumpHeight;
	float velocityXSmoothing;

	Animator Anim;

	Controller2D controller;

	void Start () 
	{
		Anim = GetComponent<Animator> ();
		controller = GetComponent<Controller2D> ();
		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;

		sliderDash = FindObjectsOfType (typeof(Slider)) as Slider[];
	}

	void Update()
	{
		if(GameController.ActiveInputs)
		{
			input = new Vector2 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

			if(input.x >0f)
			{
				transform.eulerAngles = new Vector2 (0,0);
				input = input;
				Dir = 1;
			}
			if(input.x < 0f)
			{
				transform.eulerAngles = new Vector2 (0,180);
				input.x = -input.x ;
				Dir = -1;
			}

			int WallDirX = (controller.collisions.left) ? -1 : 1;

			targetVelocityX = input.x * moveSpeed;
			float targetAccelerationX = input.x * Acceleration;
			velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGround : accelerationTimeAirBone);

			bool WallSliding = false;

			//Detectar las coliciones de laterales para que el personaje se deslice en las paredes
			if((controller.collisions.left || controller.collisions.right) && !controller.collisions.above && velocity.y < 0)
			{
				WallSliding = true;

				if(velocity.y < -SpeedSlideMax)
				{
					velocity.y =- SpeedSlideMax;
				}

				if(TimeToWallInStick > 0)
				{
					velocityXSmoothing =0;
					velocity.x = 0;
					if(input.x != WallDirX && input.x !=0)
					{
						TimeToWallInStick -= Time.deltaTime;
					}else 
					{
						TimeToWallInStick = WallTimeStick;
					}
				}else
				{
					TimeToWallInStick = WallTimeStick;
				}
			}

			//Detectar las colisiones de arriba y abajo
			if(controller.collisions.above || controller.collisions.below)
			{
				velocity.y = 0;
			}

			if(controller.collisions.below)
			{
				Ground = true;
				DobleJump =false;
				NumJump = 1;
			}

			if(!controller.collisions.below)
			{
				DobleJump = true;
				Ground = false;
			}

			// Permite que el personaje salte sobre cualquier superficie
			if(Input.GetKeyDown(KeyCode.V))
			{
				if(controller.collisions.below && DobleJump == false)
				{
					velocity.y = jumpVelocity;
				}

				if(!controller.collisions.below && DobleJump == true && NumJump > 0)
				{
					velocity.y = jumpVelocity;
					DobleJumpHeight = velocity * Time.deltaTime;
					transform.Translate(DobleJumpHeight);
					NumJump = 0;
				}

				if(WallSliding)
				{
					if(WallDirX == input.x)
					{
						velocity.x = -WallDirX * WallJump.x;
						velocity.y = WallJump.y;
					}//else if(input.x == 0)
					//				{
					//					Velocity.x =- WallDirX * WallJumpOff.x;
					//					Velocity.y = WallJumpOff.y;
					//				}else
					{
						velocity.x = -WallDirX * WallLeap.x;
						velocity.y = WallLeap.y;
					}
				}		
			}

			for (int i = 0; i < sliderDash.Length; i++) 
			{
				if (sliderDash[i].name == "SliderDash") 
				{
					if (Input.GetKey (KeyCode.Z) && sliderDash[i].value > 0) 
					{
						velocity.x = Mathf.SmoothDamp (velocity.x, targetAccelerationX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGround : accelerationTimeAirBone);
						sliderDash[i].value -= Time.deltaTime;
					}

					if (!Input.GetKey (KeyCode.Z) && sliderDash[i].value < 3) 
					{
						sliderDash[i].value += Time.deltaTime;
					}

					velocity.y += gravity * Time.deltaTime;
					controller.Move (velocity * Time.deltaTime, AirJump * Time.deltaTime);
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.name == "bossDialogue") {
			GameController.isActiveBoss = true;
			GameController.isActive = true;
			GameController.ActiveInputs = false;
		}
	}

}