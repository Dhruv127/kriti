
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public GameObject deathVFXPrefab;	
	bool isAlive = true;				
	int trapsLayer;
	public PlayerMovement playerMovement;
	
	void Start()
	{
		trapsLayer = LayerMask.NameToLayer("Traps");
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != trapsLayer || !isAlive)
			return;
		Debug.Log(playerMovement==null);
		if(playerMovement!=null&&playerMovement.playerhasorb)
		{
			
			Instantiate(deathVFXPrefab, transform.position, transform.rotation);
			collision.gameObject.SetActive(false);
			GameManager.PlayerWon();
			return ;
		}
		isAlive = false;

		if (collision.gameObject.tag != "air"&&(collision.gameObject.tag!="FinalBoss"))
		Instantiate(deathVFXPrefab, transform.position, transform.rotation);

		if(collision.gameObject.tag!="FinalBoss")
		gameObject.SetActive(false);

		if(gameObject.tag!="Enemy"&&(collision.gameObject.tag!="FinalBoss"))
		GameManager.PlayerDied();
		else
		GameManager.fearkilled();

		AudioManager.PlayDeathAudio();
	}
}
