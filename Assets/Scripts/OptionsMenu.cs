using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public GameManager myGameManager;

	public GameObject ParentMenu;

	public RectTransform ToreeCursor;

	public Slider MusicVolumeSlider;

	public Slider CameraSensitivtySlider;

	public Toggle XAxisToggle;

	public Toggle YAxisToggle;

	public Toggle NewOSTToggle;

	private float horizontallInput;

	private float verticalInput;

	private float inputDeadZone = 1f;

	private int currentPos;

	private float settingcooldown = -0.1f;

	private bool pressedVertical;

	public AudioSource selectSound;

	private void Start()
	{
		myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void CheckForToggleSaves()
	{
		int @int = GamePadScript.instance.GetInt("CameraXInverted");
		int int2 = GamePadScript.instance.GetInt("CameraYInverted");
		int int3 = GamePadScript.instance.GetInt("BonusOST");
		if (@int == 1)
		{
			XAxisToggle.isOn = true;
		}
		else
		{
			XAxisToggle.isOn = false;
		}
		if (int2 == 1)
		{
			YAxisToggle.isOn = true;
		}
		else
		{
			YAxisToggle.isOn = false;
		}
		CameraSensitivtySlider.value = GamePadScript.instance.GetInt("CameraSensitivity");
		MusicVolumeSlider.value = GamePadScript.instance.GetInt("MusicVolume");
		if (int3 == 1)
		{
			NewOSTToggle.isOn = true;
		}
		else
		{
			NewOSTToggle.isOn = false;
		}
	}

	public void ChangeSensitivitySettings()
	{
		_ = CameraSensitivtySlider.value;
		int x = (int)CameraSensitivtySlider.value;
		GamePadScript.instance.SetInt("CameraSensitivity", x);
	}

	private void OnEnable()
	{
		currentPos = 0;
		CheckForToggleSaves();
	}

	private void Update()
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
		Debug.Log(horizontallInput + "  " + verticalInput);
		ChoseMenuPoint();
		ChangeSliders();
		if (GamePadScript.instance.jumpButton() || Input.GetButtonDown("KeyboardUse"))
		{
			DoMenuActivities();
		}
		if (GamePadScript.instance.cancelButton())
		{
			ParentMenu.SetActive(value: true);
			base.gameObject.SetActive(value: false);
		}
	}

	private void ChoseMenuPoint()
	{
		if (verticalInput > 0.2f && !pressedVertical)
		{
			selectSound.Play();
			currentPos--;
			pressedVertical = true;
		}
		else if (verticalInput < -0.2f && !pressedVertical)
		{
			selectSound.Play();
			currentPos++;
			pressedVertical = true;
		}
		else if (verticalInput < 0.2f && verticalInput > -0.2f)
		{
			pressedVertical = false;
		}
		if (currentPos == -1)
		{
			currentPos = 5;
		}
		else if (currentPos == 6)
		{
			currentPos = 0;
		}
		int num = 0;
		if (currentPos == 0)
		{
			num = 99;
		}
		else if (currentPos == 1)
		{
			num = 41;
		}
		else if (currentPos == 2)
		{
			num = -17;
		}
		else if (currentPos == 3)
		{
			num = -63;
		}
		else if (currentPos == 4)
		{
			num = -104;
		}
		else if (currentPos == 5)
		{
			num = -144;
		}
		ToreeCursor.anchoredPosition = new Vector2(0f, num);
	}

	private void ChangeSliders()
	{
		if (horizontallInput > 0.2f && settingcooldown < -0f)
		{
			if (currentPos == 0)
			{
				MusicVolumeSlider.value += 1f;
			}
			else if (currentPos == 1)
			{
				CameraSensitivtySlider.value += 1f;
			}
			settingcooldown = 0.1f;
		}
		else if (horizontallInput < -0.2f && settingcooldown < -0f)
		{
			if (currentPos == 0)
			{
				MusicVolumeSlider.value -= 1f;
			}
			else if (currentPos == 1)
			{
				CameraSensitivtySlider.value -= 1f;
			}
			settingcooldown = 0.1f;
		}
		if (settingcooldown > 0f)
		{
			settingcooldown -= 1f * Time.deltaTime;
		}
		if (currentPos == 0)
		{
			_ = MusicVolumeSlider.value;
			GamePadScript.instance.SetInt("MusicVolume", (int)MusicVolumeSlider.value);
		}
		else
		{
			ChangeSensitivitySettings();
		}
	}

	private void DoMenuActivities()
	{
		if (currentPos == 2)
		{
			if (XAxisToggle.isOn)
			{
				XAxisToggle.isOn = false;
			}
			else
			{
				XAxisToggle.isOn = true;
			}
			GamePadScript.instance.SetInt("CameraXInverted", XAxisToggle.isOn ? 1 : 0);
		}
		else if (currentPos == 3)
		{
			if (YAxisToggle.isOn)
			{
				YAxisToggle.isOn = false;
			}
			else
			{
				YAxisToggle.isOn = true;
			}
			GamePadScript.instance.SetInt("CameraYInverted", YAxisToggle.isOn ? 1 : 0);
		}
		else if (currentPos == 4)
		{
			if (NewOSTToggle.isOn)
			{
				NewOSTToggle.isOn = false;
			}
			else
			{
				NewOSTToggle.isOn = true;
			}
			GamePadScript.instance.SetInt("BonusOST", NewOSTToggle.isOn ? 1 : 0);
			myGameManager.ChangeMusic();
		}
		else if (currentPos == 5)
		{
			ParentMenu.SetActive(value: true);
			base.gameObject.SetActive(value: false);
		}
	}
}
