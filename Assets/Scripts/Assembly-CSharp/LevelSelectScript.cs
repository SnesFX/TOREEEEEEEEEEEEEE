using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
{
	private int currentLevel;

	public RectTransform ToreeCursor;

	public RectTransform[] levelFrames;

	public GameObject[] levelMaps;

	public GameObject currentMap;

	public Text LevelName1;

	public Text LevelName2;

	public Image finalLevelArt1;

	public Image finalLevelArt2;

	private bool LastLevelAcces;

	private float horizontallInput;

	private float verticalInput;

	private float inputDeadZone = 1f;

	private float inputcooldown = -0.1f;

	public AudioSource selectSound;

	private bool jumpToNext;

	private bool jumpToLast;

	public Image blackScreen;

	public Animator blackScreenAnimator;

	private void Start()
	{
		CheckIfLastLevelIsOkay();
		JumpToLevel();
		if (GamePadScript.instance.HasKey("GotTasty") && GamePadScript.instance.GetInt("GotTasty") == 0)
		{
			CheckIfWeGetTasty();
		}
		if (GamePadScript.instance.HasKey("GotMacbat") && GamePadScript.instance.GetInt("GotMacbat") == 0)
		{
			CheckIfWeGetMacbat();
		}
	}

	private void CheckIfLastLevelIsOkay()
	{
		int num = 0;
		if (GamePadScript.instance.HasKey("Level1Act1Rank") && GamePadScript.instance.GetInt("Level1Act1Rank") > 0)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level1Act2Rank") && GamePadScript.instance.GetInt("Level1Act2Rank") > 0)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level2Act1Rank") && GamePadScript.instance.GetInt("Level2Act1Rank") > 0)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level2Act2Rank") && GamePadScript.instance.GetInt("Level2Act2Rank") > 0)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level3Act1Rank") && GamePadScript.instance.GetInt("Level3Act1Rank") > 0)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level3Act2Rank") && GamePadScript.instance.GetInt("Level3Act2Rank") > 0)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level4Act1Rank") && GamePadScript.instance.GetInt("Level4Act1Rank") > 0)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level4Act2Rank") && GamePadScript.instance.GetInt("Level4Act2Rank") > 0)
		{
			num++;
		}
		Debug.Log(string.Concat(num));
		if (num == 8)
		{
			LastLevelAcces = true;
			finalLevelArt1.color = new Color32(byte.MaxValue, byte.MaxValue, 225, byte.MaxValue);
			finalLevelArt2.color = new Color32(byte.MaxValue, byte.MaxValue, 225, 150);
		}
	}

	private void CheckIfWeGetTasty()
	{
		int num = 0;
		if (GamePadScript.instance.HasKey("Level1Act1Rank") && GamePadScript.instance.GetInt("Level1Act1Rank") > 2)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level1Act2Rank") && GamePadScript.instance.GetInt("Level1Act2Rank") > 2)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level2Act1Rank") && GamePadScript.instance.GetInt("Level2Act1Rank") > 2)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level2Act2Rank") && GamePadScript.instance.GetInt("Level2Act2Rank") > 2)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level3Act1Rank") && GamePadScript.instance.GetInt("Level3Act1Rank") > 2)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level3Act2Rank") && GamePadScript.instance.GetInt("Level3Act2Rank") > 2)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level4Act1Rank") && GamePadScript.instance.GetInt("Level4Act1Rank") > 2)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level4Act2Rank") && GamePadScript.instance.GetInt("Level4Act2Rank") > 2)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level5Act1Rank") && GamePadScript.instance.GetInt("Level5Act1Rank") > 2)
		{
			num++;
		}
		if (num >= 9)
		{
			SceneManager.LoadScene(15);
		}
	}

	private void CheckIfWeGetMacbat()
	{
		int num = 0;
		if (GamePadScript.instance.HasKey("1_1AllStars") && GamePadScript.instance.GetInt("1_1AllStars") == 1)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("1_2AllStars") && GamePadScript.instance.GetInt("1_2AllStars") == 1)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("2_1AllStars") && GamePadScript.instance.GetInt("2_1AllStars") == 1)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("2_2AllStars") && GamePadScript.instance.GetInt("2_2AllStars") == 1)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("3_1AllStars") && GamePadScript.instance.GetInt("3_1AllStars") == 1)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("3_2AllStars") && GamePadScript.instance.GetInt("3_2AllStars") == 1)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("4_1AllStars") && GamePadScript.instance.GetInt("4_1AllStars") == 1)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("4_2AllStars") && GamePadScript.instance.GetInt("4_2AllStars") == 1)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("5_1AllStars") && GamePadScript.instance.GetInt("5_1AllStars") == 1)
		{
			num++;
		}
		if (num == 9)
		{
			SceneManager.LoadScene(14);
		}
	}

	private void Update()
	{
		if (!jumpToLast && !jumpToNext)
		{
			horizontallInput = GamePadScript.instance.xAxis();
			verticalInput = 0f - GamePadScript.instance.yAxis();
			if (horizontallInput < inputDeadZone && horizontallInput > 0f - inputDeadZone)
			{
				horizontallInput = 0f;
			}
			if (verticalInput < inputDeadZone && verticalInput > 0f - inputDeadZone)
			{
				verticalInput = 0f;
			}
			ChoseLevel();
			if (GamePadScript.instance.jumpButton() || Input.GetButtonDown("KeyboardUse"))
			{
				DoMenuActivities();
			}
			if (GamePadScript.instance.cancelButton())
			{
				jumpToLast = true;
				blackScreenAnimator.SetBool("isOpen", value: false);
			}
		}
	}

	public void FixedUpdate()
	{
		if (jumpToNext && blackScreen.color.a == 1f)
		{
			SceneManager.LoadScene(currentLevel + 4);
		}
		else if (jumpToLast && blackScreen.color.a == 1f)
		{
			SceneManager.LoadScene(2);
		}
	}

	private void ChoseLevel()
	{
		if (verticalInput > 0.2f && inputcooldown < -0f)
		{
			currentLevel--;
			inputcooldown = 0.25f;
		}
		else if (verticalInput < -0.2f && inputcooldown < -0f)
		{
			currentLevel++;
			inputcooldown = 0.25f;
		}
		else if (horizontallInput > 0.2f && inputcooldown < -0f)
		{
			currentLevel += 2;
			inputcooldown = 0.25f;
		}
		else if (horizontallInput < -0.2f && inputcooldown < -0f)
		{
			currentLevel -= 2;
			inputcooldown = 0.25f;
		}
		if (currentLevel < 0)
		{
			currentLevel = 0;
		}
		else if (LastLevelAcces && currentLevel > 8)
		{
			currentLevel = 8;
		}
		else if (!LastLevelAcces && currentLevel > 7)
		{
			currentLevel = 7;
		}
		else if (inputcooldown == 0.25f)
		{
			selectSound.Play();
		}
		if (inputcooldown == 0.25f)
		{
			JumpToLevel();
		}
		if (inputcooldown > 0f)
		{
			inputcooldown -= 1f * Time.deltaTime;
		}
	}

	private void JumpToLevel()
	{
		ToreeCursor.anchoredPosition = levelFrames[currentLevel].anchoredPosition;
		string myName = levelFrames[currentLevel].gameObject.GetComponent<LevelFrameScript>().myName;
		LevelName1.text = myName;
		LevelName2.text = myName;
		currentMap.SetActive(value: false);
		levelMaps[currentLevel].SetActive(value: true);
		currentMap = levelMaps[currentLevel];
	}

	private void DoMenuActivities()
	{
		jumpToNext = true;
		blackScreenAnimator.SetBool("isOpen", value: false);
	}
}
