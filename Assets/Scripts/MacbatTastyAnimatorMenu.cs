using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MacbatTastyAnimatorMenu : MonoBehaviour
{
	public bool isTasty;

	public Animation _character3animation;

	private bool jumpToLast;

	public Image blackScreen;

	public Animator blackScreenAnimator;

	private void Start()
	{
		_character3animation["Run"].speed = 0.25f;
		_character3animation.Play("Run");
		if (!isTasty)
		{
			GamePadScript.instance.SetInt("GotMacbat", 1);
		}
		else
		{
			GamePadScript.instance.SetInt("GotTasty", 1);
		}
	}

	private void Update()
	{
		if (!jumpToLast && GamePadScript.instance.jumpButton())
		{
			JumpBack();
		}
	}

	public void FixedUpdate()
	{
		if (jumpToLast && blackScreen.color.a == 1f)
		{
			SceneManager.LoadScene(3);
		}
	}

	private void JumpBack()
	{
		jumpToLast = true;
		blackScreenAnimator.SetBool("isOpen", value: false);
	}
}
