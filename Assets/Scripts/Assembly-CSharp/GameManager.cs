using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public bool interactable = true;

	public static GameManager singleton;

	public bool PlayOnMobile;

	public PlayerSystem myPlayerSystem;

	public MouseOrbitImproved myCamSystem;

	public TimeManager myTimeManager;

	public int score;

	public Text scoreText;

	public Text scoreText_Shadow;

	private int allStars;

	public int stars;

	public Text starsText;

	public Text starsText_Shadow;

	public ScaleManagerScript _backpackScaleManager;

	public Renderer _faceRenderer;

	public Texture2D _origFace;

	public Texture2D _shockedFace;

	[Header("Level Elements")]
	public int LevelID;

	public AudioHighPassFilter musicFilter;

	public GameObject LevelStartScreen;

	public GameObject LevelGoalScreen;

	public GameObject RankScreen;

	public Animator blackScreenAnimator;

	public Animator deathScreenAnimator;

	public RectTransform deathAnimationRect;

	public Image blackScreen;

	public Vector3 _respawn;

	public Quaternion _rerotation;

	public GameObject specialLevelReset;

	private GameObject[] allBees;

	private GameObject[] allCranes;

	private GameObject[] allMovers;

	[Header("Game Settings")]
	public MusicManager myMusicManager;

	public GameObject PauseMenu;

	public MouseOrbitImproved myMouseScript;

	public Slider CameraSensitivtySlider;

	private bool isDead;

	[Header("Game Sounds")]
	public AudioSource starSound;

	public bool GameRunning { get; private set; }

	private void Awake()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			PlayOnMobile = true;
		}
		if (singleton == null)
		{
			singleton = this;
		}
		else if (singleton != this)
		{
			Object.Destroy(base.gameObject);
		}
		allBees = GameObject.FindGameObjectsWithTag("BeeEnemy");
		allCranes = GameObject.FindGameObjectsWithTag("Crane");
		allMovers = GameObject.FindGameObjectsWithTag("Mover");
		StartGame();
	}

	public void StartGame()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		GameRunning = true;
		LevelStartScreen.SetActive(value: true);
		blackScreenAnimator.SetBool("isOpen", value: true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Star");
		allStars = array.Length;
		if (stars < 10)
		{
			starsText.text = "0" + stars + " / " + allStars;
			starsText_Shadow.text = "0" + stars + " / " + allStars;
		}
		else
		{
			starsText.text = stars + " / " + allStars;
			starsText_Shadow.text = stars + " / " + allStars;
		}
		_respawn = myPlayerSystem.transform.position;
		_rerotation = myPlayerSystem._playermodel.rotation;
		UpdateScore();
	}

	public void ChangeSensitivitySettings()
	{
		float num = CameraSensitivtySlider.value;
		num *= 10f;
		myMouseScript.xSpeed = num;
		myMouseScript.ySpeed = num * 2f;
		int x = (int)CameraSensitivtySlider.value;
		GamePadScript.instance.SetInt("CameraSensitivity", x);
	}

	public void FixedUpdate()
	{
		if (!GameRunning && blackScreen.color.a == 1f)
		{
			if (LevelID == 9)
			{
				SceneManager.LoadScene(16);
			}
			else
			{
				SceneManager.LoadScene(3);
			}
		}
		else if (Input.GetMouseButtonDown(0) && myPlayerSystem.IamActive)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	public void EndGame(bool Victory)
	{
		ChangeGame(running: false);
		if (specialLevelReset != null)
		{
			specialLevelReset.SetActive(value: true);
		}
		myMusicManager.PlayJingle();
		myPlayerSystem.IamActive = false;
		myPlayerSystem.EndOfLevel();
		if (Victory)
		{
			myTimeManager.timerIsRunning = false;
			if (myPlayerSystem.characterID == 0)
			{
				RankScreen.SetActive(value: true);
				myTimeManager.CheckForBestRank();
			}
			if (stars == allStars)
			{
				SaveAllStars();
			}
			GamePadScript.instance.SavingRoutine();
			LevelGoalScreen.SetActive(value: true);
			StartCoroutine(PauseBeforeLevelEnd(6f));
		}
	}

	private void SaveAllStars()
	{
		if (LevelID == 1)
		{
			GamePadScript.instance.SetInt("1_1AllStars", 1);
		}
		else if (LevelID == 2)
		{
			GamePadScript.instance.SetInt("1_2AllStars", 1);
		}
		else if (LevelID == 3)
		{
			GamePadScript.instance.SetInt("2_1AllStars", 1);
		}
		else if (LevelID == 4)
		{
			GamePadScript.instance.SetInt("2_2AllStars", 1);
		}
		else if (LevelID == 5)
		{
			GamePadScript.instance.SetInt("3_1AllStars", 1);
		}
		else if (LevelID == 6)
		{
			GamePadScript.instance.SetInt("3_2AllStars", 1);
		}
		else if (LevelID == 7)
		{
			GamePadScript.instance.SetInt("4_1AllStars", 1);
		}
		else if (LevelID == 8)
		{
			GamePadScript.instance.SetInt("4_2AllStars", 1);
		}
		else if (LevelID == 9)
		{
			GamePadScript.instance.SetInt("5_1AllStars", 1);
		}
	}

	private IEnumerator PauseBeforeLevelEnd(float coolDown)
	{
		float waitTime = Time.realtimeSinceStartup + coolDown;
		yield return new WaitWhile(() => Time.realtimeSinceStartup < waitTime);
		blackScreenAnimator.SetBool("isOpen", value: false);
	}

	public void ChangeGame(bool running)
	{
		GameRunning = running;
	}

	private void Update()
	{
		if (interactable && !isDead && GamePadScript.instance.startButton() && myPlayerSystem.IamActive)
		{
			PauseGame(pauseIt: true);
		}
		else if (interactable && !isDead && GamePadScript.instance.startButton() && PauseMenu.activeSelf)
		{
			PauseGame(pauseIt: false);
		}
	}

	public void ResetGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void PauseGame(bool pauseIt)
	{
		if (pauseIt)
		{
			myTimeManager.timerIsRunning = false;
			ChangeGame(running: false);
			ActivateAllEnemies(pausingThem: false);
			myPlayerSystem.IamActive = false;
			PauseMenu.SetActive(value: true);
		}
		else if (!pauseIt)
		{
			myTimeManager.timerIsRunning = true;
			ChangeGame(running: true);
			ActivateAllEnemies(pausingThem: true);
			myPlayerSystem.IamActive = true;
			PauseMenu.SetActive(value: false);
		}
	}

	private void ActivateAllEnemies(bool pausingThem)
	{
		GameObject[] array = allBees;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<BeeScript>().activeBee = pausingThem;
		}
		array = allCranes;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<CraneScript>()._paused = !pausingThem;
		}
		array = allMovers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<MovingPlatformScript>().iamActive = pausingThem;
		}
	}

	public void GameFreeze(float coolDown)
	{
		myTimeManager.GameFreezeMoment(coolDown);
	}

	public void FaceChange(int newface)
	{
		if (newface == 0)
		{
			_faceRenderer.material.mainTexture = _origFace;
		}
		if (newface == 1)
		{
			_faceRenderer.material.mainTexture = _shockedFace;
		}
	}

	public void CollectStar()
	{
		stars++;
		CollectPoint();
		score += 100;
		UpdateScore();
	}

	public void CollectPoint()
	{
		_backpackScaleManager.newScale = new Vector3(0.006f, 0.006f, 0.006f);
		myPlayerSystem.StarEffect.Play();
		starSound.Play();
		if (stars < 10)
		{
			starsText.text = "0" + stars + " / " + allStars;
			starsText_Shadow.text = "0" + stars + " / " + allStars;
		}
		else
		{
			starsText.text = stars + " / " + allStars;
			starsText_Shadow.text = stars + " / " + allStars;
		}
	}

	public void UpdateScore()
	{
		if (score < 10)
		{
			scoreText.text = "00000" + score;
			scoreText_Shadow.text = "00000" + score;
		}
		else if (score < 100)
		{
			scoreText.text = "0000" + score;
			scoreText_Shadow.text = "0000" + score;
		}
		else if (score < 1000)
		{
			scoreText.text = "000" + score;
			scoreText_Shadow.text = "000" + score;
		}
		else if (score < 10000)
		{
			scoreText.text = "00" + score;
			scoreText_Shadow.text = "00" + score;
		}
		else if (score < 100000)
		{
			scoreText.text = "0" + score;
			scoreText_Shadow.text = "0" + score;
		}
		else
		{
			scoreText.text = string.Concat(score);
			scoreText_Shadow.text = string.Concat(score);
		}
	}

	public void PlayerStars()
	{
		myPlayerSystem.StarEffect.Play();
	}

	public void PlayerDeath()
	{
		if (!isDead)
		{
			if (score > 0)
			{
				score -= 100;
				UpdateScore();
			}
			PauseGame(pauseIt: true);
			PauseMenu.SetActive(value: false);
			myPlayerSystem.DeathAnimation();
			isDead = true;
			StartCoroutine(DeathAnimationBeforeRespawn(1f));
		}
	}

	private IEnumerator DeathAnimationBeforeRespawn(float coolDown)
	{
		float waitTime = Time.realtimeSinceStartup + coolDown;
		yield return new WaitWhile(() => Time.realtimeSinceStartup < waitTime);
		deathScreenAnimator.SetBool("isOpen", value: false);
		StartCoroutine(WaitForDeathAnimation());
	}

	private IEnumerator WaitForDeathAnimation()
	{
		yield return new WaitWhile(() => deathAnimationRect.localScale.x > 1f);
		deathScreenAnimator.SetBool("isOpen", value: true);
		myPlayerSystem.transform.position = _respawn;
		myPlayerSystem._playermodel.rotation = _rerotation;
		myPlayerSystem._playermodel.position = new Vector3(myPlayerSystem.transform.position.x, myPlayerSystem.transform.position.y - 1.1f, myPlayerSystem.transform.position.z);
		myPlayerSystem.ImportantSquish();
		myPlayerSystem.pushDirection = Vector3.zero;
		myPlayerSystem.FootSounds.SetActive(value: true);
		myPlayerSystem._playeranimation.Play("Idle");
		myCamSystem.ForceCameraCenter();
		isDead = false;
		if (specialLevelReset != null)
		{
			specialLevelReset.SetActive(value: true);
		}
		PauseGame(pauseIt: false);
	}

	public void DistortMusic(int mode)
	{
		if (musicFilter.enabled && mode == 0)
		{
			musicFilter.enabled = false;
		}
		else if (!musicFilter.enabled && mode == 1)
		{
			musicFilter.enabled = true;
		}
	}

	public void ChangeMusic()
	{
		myMusicManager.ChangeTheSoundtrack();
	}
}
