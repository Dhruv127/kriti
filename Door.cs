
using UnityEngine;

public class Door : MonoBehaviour
{
	Animator anim;			
	int openParameterID;	


	void Start()
	{
		anim = GetComponent<Animator>();

		openParameterID = Animator.StringToHash("Open");

		GameManager.RegisterDoor(this);
	}

	public void Open()
	{
		anim.SetTrigger(openParameterID);
		AudioManager.PlayDoorOpenAudio();
	}
}
