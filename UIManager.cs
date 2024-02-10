
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	static UIManager current;

	public TextMeshProUGUI fearKilledText;			
	public TextMeshProUGUI timeText;		
	public TextMeshProUGUI deathText;		
	public TextMeshProUGUI gameOverText;	


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


	public static void Updatfear(int fearkilled)
	{
		if (current == null)
			return;

		current.fearKilledText.text = fearkilled.ToString();
	}

	public static void UpdateTimeUI(float time)
	{
		if (current == null)
			return;

		int minutes = (int)(time / 60);
		float seconds = time % 60f;

		current.timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
	}

	public static void UpdateDeathUI(int deathCount)
	{
		if (current == null)
			return;

		current.deathText.text = deathCount.ToString();
	}

	public static void DisplayGameOverText()
	{
		if (current == null)
			return;

		current.gameOverText.enabled = true;
	}
}
