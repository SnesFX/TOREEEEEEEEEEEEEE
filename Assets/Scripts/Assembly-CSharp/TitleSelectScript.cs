using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSelectScript : MonoBehaviour
{
	public GameObject ChildMenu;

	public RectTransform ToreeCursor;

	private float verticalInput;

	private int currentPos;

	private bool pressedVertical;

	public AudioSource selectSound;

	private bool jumpToNext;

	private bool jumpToLast;

	public Image blackScreen;

	public Animator blackScreenAnimator;

	private void Start()
	{
	}

	private void Update()
	{
		if (!jumpToLast && !jumpToNext)
		{
			verticalInput = 0f - GamePadScript.instance.yAxis();
			ChoseMenuPoint();
			if (GamePadScript.instance.jumpButton() || Input.GetButtonDown("KeyboardUse"))
			{
				DoMenuActivities();
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
			currentPos = 0;
		}
		else if (currentPos == 3)
		{
			currentPos = 2;
		}
		int num = 0;
		if (currentPos == 0)
		{
			num = 118;
		}
		else if (currentPos == 1)
		{
			num = 0;
		}
		else if (currentPos == 2)
		{
			num = -120;
		}
		ToreeCursor.anchoredPosition = new Vector2(6f, num);
	}

	private void DoMenuActivities()
	{
		if (currentPos == 0)
		{
			MenuPointClick(0);
		}
		else if (currentPos == 1)
		{
			MenuPointClick(1);
		}
		else if (currentPos == 2)
		{
			MenuPointClick(2);
		}
	}

	public void MenuPointClick(int menupoint)
	{
		switch (menupoint)
		{
		case 0:
			jumpToNext = true;
			blackScreenAnimator.SetBool("isOpen", value: false);
			break;
		case 1:
			ChildMenu.SetActive(value: true);
			base.gameObject.SetActive(value: false);
			break;
		case 2:
			Application.Quit();
			break;
		}
	}
}
