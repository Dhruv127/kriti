
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	PlayerMovement movement;
	Enemy movement2;	
	Rigidbody2D rigidBody;	
	PlayerInput input;		
	Animator anim;			

	int hangingParamID;		
	int groundParamID;		
	int crouchParamID;		
	int speedParamID;		
	int fallParamID;
	
	Transform parent;		


	void Start()
	{
		hangingParamID = Animator.StringToHash("isHanging");
		groundParamID = Animator.StringToHash("isOnGround");
		crouchParamID = Animator.StringToHash("isCrouching");
		speedParamID = Animator.StringToHash("speed");
		fallParamID = Animator.StringToHash("verticalVelocity");

		parent = transform.parent;

		if(parent.tag=="Enemy")
		movement2   = parent.GetComponent<Enemy>();

		movement	= parent.GetComponent<PlayerMovement>();
		rigidBody	= parent.GetComponent<Rigidbody2D>();
		input		= parent.GetComponent<PlayerInput>();
		anim		= GetComponent<Animator>();
		
		if(movement == null || rigidBody == null || input == null || anim == null)
		{
			Debug.LogError("A needed component is missing from the player");
			Destroy(this);
		}
	}

	void Update()
	{
		if(parent.tag=="Enemy")
		{
			anim.SetBool(groundParamID, movement2.isOnGround);
			anim.SetFloat(speedParamID, Mathf.Abs(movement2.followspeed));
		}
		else
		{
		anim.SetBool(hangingParamID, movement.isHanging);
		anim.SetBool(groundParamID, movement.isOnGround);
		anim.SetBool(crouchParamID, movement.isCrouching);
		anim.SetFloat(fallParamID, rigidBody.velocity.y);

		anim.SetFloat(speedParamID, Mathf.Abs(input.horizontal));
		}
	}

	public void StepAudio()
	{
		AudioManager.PlayFootstepAudio();
	}

	public void CrouchStepAudio()
	{
		AudioManager.PlayCrouchFootstepAudio();
	}
}
