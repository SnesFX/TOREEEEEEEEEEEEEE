using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	private float minutes;

	private float seconds;

	public float UltrabestMinutes;

	public float UltrabestSeconds;

	public float bestMinutes;

	public float bestSeconds;

	public float secondbestMinutes;

	public float secondbestSeconds;

	public Image rankingImage;

	public Sprite ARankSmol;

	public Sprite BRankSmol;

	public Sprite CRankSmol;

	public Sprite SRankSmol;

	private float lastTimeScale;

	private float timeRemaining;

	public bool timerIsRunning;

	public Text timeText;

	public Text timeText_Shadow;

	public void Update()
	{
		if (timerIsRunning)
		{
			TimerControl();
		}
	}

	private void TimerControl()
	{
		if (timeRemaining < 10000f)
		{
			timeRemaining += Time.deltaTime;
			DisplayTime(timeRemaining);
		}
		else
		{
			Debug.Log("Time has run out!");
			timeRemaining = 0f;
			timerIsRunning = false;
		}
	}

	private void DisplayTime(float timeToDisplay)
	{
		timeToDisplay += 1f;
		minutes = Mathf.FloorToInt(timeToDisplay / 60f);
		seconds = Mathf.FloorToInt(timeToDisplay % 60f);
		timeText.text = $"{minutes:00}:{seconds:00}";
		timeText_Shadow.text = $"{minutes:00}:{seconds:00}";
	}

	public void GameFreezeMoment(float coolDown)
	{
		if (Time.timeScale != 0f)
		{
			lastTimeScale = Time.timeScale;
			Time.timeScale = 0f;
			StartCoroutine(PauseThanCrash(coolDown));
		}
	}

	private IEnumerator PauseThanCrash(float coolDown)
	{
		float waitTime = Time.realtimeSinceStartup + coolDown;
		yield return new WaitWhile(() => Time.realtimeSinceStartup < waitTime);
		Time.timeScale = lastTimeScale;
		Debug.Log("Done Once");
	}

	public void CheckForBestRank()
	{
		if (minutes < UltrabestMinutes)
		{
			rankingImage.sprite = SRankSmol;
			SaveOurRanking(4);
		}
		else if (minutes == UltrabestMinutes && seconds <= UltrabestSeconds)
		{
			rankingImage.sprite = SRankSmol;
			SaveOurRanking(4);
		}
		else if (minutes < bestMinutes)
		{
			rankingImage.sprite = ARankSmol;
			SaveOurRanking(3);
		}
		else if (minutes == bestMinutes && seconds <= bestSeconds)
		{
			rankingImage.sprite = ARankSmol;
			SaveOurRanking(3);
		}
		else if (minutes < secondbestMinutes)
		{
			rankingImage.sprite = BRankSmol;
			SaveOurRanking(2);
		}
		else if (minutes == secondbestMinutes && seconds <= secondbestSeconds)
		{
			rankingImage.sprite = BRankSmol;
			SaveOurRanking(2);
		}
		else
		{
			rankingImage.sprite = CRankSmol;
			SaveOurRanking(1);
		}
	}

	private void SaveOurRanking(int newRanking)
	{
		if (GameManager.singleton.LevelID == 1)
		{
			if (GamePadScript.instance.HasKey("Level1Act1Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level1Act1Rank"))
				{
					GamePadScript.instance.SetInt("Level1Act1Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level1Act1Rank", newRanking);
			}
		}
		else if (GameManager.singleton.LevelID == 2)
		{
			if (GamePadScript.instance.HasKey("Level1Act2Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level1Act2Rank"))
				{
					GamePadScript.instance.SetInt("Level1Act2Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level1Act2Rank", newRanking);
			}
		}
		else if (GameManager.singleton.LevelID == 3)
		{
			if (GamePadScript.instance.HasKey("Level2Act1Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level2Act1Rank"))
				{
					GamePadScript.instance.SetInt("Level2Act1Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level2Act1Rank", newRanking);
			}
		}
		else if (GameManager.singleton.LevelID == 4)
		{
			if (GamePadScript.instance.HasKey("Level2Act2Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level2Act2Rank"))
				{
					GamePadScript.instance.SetInt("Level2Act2Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level2Act2Rank", newRanking);
			}
		}
		else if (GameManager.singleton.LevelID == 5)
		{
			if (GamePadScript.instance.HasKey("Level3Act1Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level3Act1Rank"))
				{
					GamePadScript.instance.SetInt("Level3Act1Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level3Act1Rank", newRanking);
			}
		}
		else if (GameManager.singleton.LevelID == 6)
		{
			if (GamePadScript.instance.HasKey("Level3Act2Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level3Act2Rank"))
				{
					GamePadScript.instance.SetInt("Level3Act2Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level3Act2Rank", newRanking);
			}
		}
		else if (GameManager.singleton.LevelID == 7)
		{
			if (GamePadScript.instance.HasKey("Level4Act1Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level4Act1Rank"))
				{
					GamePadScript.instance.SetInt("Level4Act1Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level4Act1Rank", newRanking);
			}
		}
		else if (GameManager.singleton.LevelID == 8)
		{
			if (GamePadScript.instance.HasKey("Level4Act2Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level4Act2Rank"))
				{
					GamePadScript.instance.SetInt("Level4Act2Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level4Act2Rank", newRanking);
			}
		}
		else
		{
			if (GameManager.singleton.LevelID != 9)
			{
				return;
			}
			if (GamePadScript.instance.HasKey("Level5Act1Rank"))
			{
				if (newRanking > GamePadScript.instance.GetInt("Level5Act1Rank"))
				{
					GamePadScript.instance.SetInt("Level5Act1Rank", newRanking);
				}
			}
			else
			{
				GamePadScript.instance.SetInt("Level5Act1Rank", newRanking);
			}
		}
	}
}
