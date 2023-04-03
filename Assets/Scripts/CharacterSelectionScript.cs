using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionScript : MonoBehaviour
{
	public Transform CharacterWheel;

	private int currentChar;

	private float inputcooldown = -0.1f;

	public GameObject Macbat;

	public GameObject Macbat_Shadow;

	public GameObject Tasty;

	public GameObject Tasty_Shadow;

	public Animation _character1animation;

	public Animation _character2animation;

	public Animation _character3animation;

	public Animation _character2shadowanimation;

	public Animation _character3shadowanimation;

	public Text InfoPanelName;

	public Text InfoPanelName_Shadow;

	public Text InfoPanelFeature;

	private float horizontallInput;

	private float inputDeadZone = 1f;

	private int weOwnMacbat;

	private int weOwnTasty;

	public AudioSource selectSound;

	public AudioSource DontOwn;

	private bool jumpToNext;

	private bool jumpToLast;

	public Image blackScreen;

	public Animator blackScreenAnimator;

	private void Start()
	{
		_character1animation["Run"].speed = 0.25f;
		_character1animation.Play("Run");
		_character2animation["Run"].speed = 0.25f;
		_character2animation.Play("Run");
		_character3animation["Run"].speed = 0.25f;
		_character3animation.Play("Run");
		_character2shadowanimation["Run"].speed = 0.25f;
		_character2shadowanimation.Play("Run");
		_character3shadowanimation["Run"].speed = 0.25f;
		_character3shadowanimation.Play("Run");
		weOwnMacbat = 0;
		if (GamePadScript.instance.HasKey("GotMacbat"))
		{
			weOwnMacbat = GamePadScript.instance.GetInt("GotMacbat");
		}
		if (weOwnMacbat == 1)
		{
			Macbat_Shadow.SetActive(value: false);
			Macbat.SetActive(value: true);
		}
		else
		{
			Macbat_Shadow.SetActive(value: true);
			Macbat.SetActive(value: false);
		}
		weOwnTasty = 0;
		if (GamePadScript.instance.HasKey("GotTasty"))
		{
			weOwnTasty = GamePadScript.instance.GetInt("GotTasty");
		}
		if (weOwnTasty == 1)
		{
			Tasty_Shadow.SetActive(value: false);
			Tasty.SetActive(value: true);
		}
		else
		{
			Tasty_Shadow.SetActive(value: true);
			Tasty.SetActive(value: false);
		}
		if (GamePadScript.instance.HasKey("ChosenCharacter"))
		{
			currentChar = GamePadScript.instance.GetInt("ChosenCharacter");
		}
		int num = 0;
		if (currentChar == 0)
		{
			num = 0;
		}
		else if (currentChar == 1)
		{
			num = 120;
		}
		else if (currentChar == 2)
		{
			num = 240;
		}
		Quaternion localRotation = Quaternion.Euler(0f, num, 0f);
		CharacterWheel.localRotation = localRotation;
	}

	private void Update()
	{
		if (!jumpToLast && !jumpToNext)
		{
			horizontallInput = GamePadScript.instance.xAxis();
			if (horizontallInput < inputDeadZone && horizontallInput > 0f - inputDeadZone)
			{
				horizontallInput = 0f;
			}
			ChangeCharacter();
			ManageCharacterWheel();
			if (GamePadScript.instance.jumpButton() || Input.GetButtonDown("KeyboardUse"))
			{
				ChoseCharacter();
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
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		else if (jumpToLast && blackScreen.color.a == 1f)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}

	private void ChangeCharacter()
	{
		if (horizontallInput > 0.2f && inputcooldown < -0f)
		{
			selectSound.Play();
			currentChar++;
			inputcooldown = 0.25f;
			horizontallInput = 0f;
		}
		else if (horizontallInput < -0.2f && inputcooldown < -0f)
		{
			selectSound.Play();
			currentChar--;
			inputcooldown = 0.25f;
			horizontallInput = 0f;
		}
		if (currentChar < 0)
		{
			currentChar = 2;
		}
		else if (currentChar > 2)
		{
			currentChar = 0;
		}
		if (inputcooldown == 0.25f)
		{
			ChangeInfoPanel();
		}
		if (inputcooldown > 0f)
		{
			inputcooldown -= 1f * Time.deltaTime;
		}
	}

	private void ManageCharacterWheel()
	{
		float y = 0f;
		if (currentChar == 0)
		{
			y = 0f;
		}
		else if (currentChar == 1)
		{
			y = 120f;
		}
		else if (currentChar == 2)
		{
			y = 240f;
		}
		Quaternion b = Quaternion.Euler(0f, y, 0f);
		CharacterWheel.localRotation = Quaternion.Lerp(CharacterWheel.localRotation, b, Time.deltaTime * 5f);
	}

	private void ChangeInfoPanel()
	{
		if (currentChar == 0)
		{
			InfoPanelName.text = "Toree";
			InfoPanelName_Shadow.text = "Toree";
			InfoPanelFeature.text = "- none";
		}
		else if (currentChar == 1)
		{
			if (weOwnMacbat == 1)
			{
				InfoPanelName.text = "Macbat";
				InfoPanelName_Shadow.text = "Macbat";
				InfoPanelFeature.text = "- endless jumps";
			}
			else
			{
				InfoPanelName.text = "???";
				InfoPanelName_Shadow.text = "???";
				InfoPanelFeature.text = "- ???";
			}
		}
		else if (currentChar == 2)
		{
			if (weOwnTasty == 1)
			{
				InfoPanelName.text = "Tasty";
				InfoPanelName_Shadow.text = "Tasty";
				InfoPanelFeature.text = "- higher speed";
			}
			else
			{
				InfoPanelName.text = "???";
				InfoPanelName_Shadow.text = "???";
				InfoPanelFeature.text = "- ???";
			}
		}
	}

	private void ChoseCharacter()
	{
		if (currentChar == 0)
		{
			GamePadScript.instance.SetInt("ChosenCharacter", 0);
			jumpToNext = true;
			blackScreenAnimator.SetBool("isOpen", value: false);
		}
		else if (currentChar == 1)
		{
			if (weOwnMacbat == 1)
			{
				GamePadScript.instance.SetInt("ChosenCharacter", 1);
				jumpToNext = true;
				blackScreenAnimator.SetBool("isOpen", value: false);
			}
			else
			{
				DontOwn.Play();
			}
		}
		else if (currentChar == 2)
		{
			if (weOwnTasty == 1)
			{
				GamePadScript.instance.SetInt("ChosenCharacter", 2);
				jumpToNext = true;
				blackScreenAnimator.SetBool("isOpen", value: false);
			}
			else
			{
				DontOwn.Play();
			}
		}
	}
}
