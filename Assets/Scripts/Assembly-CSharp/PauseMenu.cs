using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public GameObject ChildMenu;

	public RectTransform ToreeCursor;

	private float horizontallInput;

	private float verticalInput;

	private int currentPos;

	private float settingcooldown = -0.1f;

	private bool pressedVertical;

	public AudioSource selectSound;

	private bool jumpToNext;

	private bool jumpToLast;

	public Image blackScreen;

	public Animator blackScreenAnimator;

	private void Start()
	{
	}

	private void OnEnable()
	{
		currentPos = 0;
		Cursor.lockState = CursorLockMode.None;
	}

	private void Update()
	{
		if (!jumpToLast && !jumpToNext)
		{
			horizontallInput = GamePadScript.instance.xAxis();
			verticalInput = 0f - GamePadScript.instance.yAxis();
			ChoseMenuPoint();
		}
	}

	public void FixedUpdate()
	{
		if ((!jumpToNext || blackScreen.color.a != 1f) && jumpToLast && blackScreen.color.a == 1f)
		{
			SceneManager.LoadScene(3);
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
			currentPos = 3;
		}
		else if (currentPos == 4)
		{
			currentPos = 0;
		}
		int num = 0;
		if (currentPos == 0)
		{
			num = 97;
		}
		else if (currentPos == 1)
		{
			num = 31;
		}
		else if (currentPos == 2)
		{
			num = -33;
		}
		else if (currentPos == 3)
		{
			num = -97;
		}
		ToreeCursor.anchoredPosition = new Vector2(0f, num);
		if (GamePadScript.instance.jumpButton() || Input.GetButtonDown("KeyboardUse"))
		{
			DoMenuActivities();
		}
		if (GamePadScript.instance.cancelButton())
		{
			GameManager.singleton.PauseGame(pauseIt: false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	private void DoMenuActivities()
	{
		if (currentPos == 0)
		{
			GameManager.singleton.PauseGame(pauseIt: false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (currentPos == 1)
		{
			ChildMenu.SetActive(value: true);
			base.gameObject.SetActive(value: false);
		}
		else if (currentPos == 2)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else if (currentPos == 3)
		{
			jumpToLast = true;
			blackScreenAnimator.SetBool("isOpen", value: false);
		}
	}
}
