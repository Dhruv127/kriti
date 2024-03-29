
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	static GameManager current;

	public float deathSequenceDuration = 1.5f;	
	public int fearcount;
						
	Door lockedDoor;							
	SceneFader sceneFader;						

	int numberOfDeaths;	
	int fearKilled;						
	float totalGameTime;						
	bool isGameOver;							


	void Awake()
	{
		if (current != null && current != this)
		{
			Destroy(gameObject);
			return;
		}

		current = this;


		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		if (isGameOver)
			return;

		totalGameTime += Time.deltaTime;
		UIManager.UpdateTimeUI(totalGameTime);
	}

	public static bool IsGameOver()
	{
		if (current == null)
			return false;

		return current.isGameOver;
	}

	public static void RegisterSceneFader(SceneFader fader)
	{
		if (current == null)
			return;

		current.sceneFader = fader;
	}

	public static void RegisterDoor(Door door)
	{
		if (current == null)
			return;

		current.lockedDoor = door;
	}

	

	

	public static void PlayerDied()
	{
		if (current == null)
			return;

		current.numberOfDeaths++;
		UIManager.UpdateDeathUI(current.numberOfDeaths);

		current.fearKilled=0;
		UIManager.Updatfear(current.fearKilled);

		if(current.sceneFader != null)
			current.sceneFader.FadeSceneOut();

		current.Invoke("RestartScene", current.deathSequenceDuration);
	}
	public static void fearkilled()
	{
		if (current == null)
			return;
		
		Debug.Log(current.fearKilled);

		current.fearKilled++;

		if (current.fearKilled == current.fearcount)
			current.lockedDoor.Open();

		
		UIManager.Updatfear(current.fearKilled);

		if(current.sceneFader != null)
			current.sceneFader.FadeSceneOut();
	}

	public static void PlayerWon()
	{
		if (current == null)
			return;

		current.isGameOver = true;

		UIManager.DisplayGameOverText();
		AudioManager.PlayWonAudio();
	}

	void RestartScene()
	{

		AudioManager.PlaySceneRestartAudio();

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}
}
