using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThankYouScript : MonoBehaviour
{
	public bool InitiateJump;

	public bool jumpToLast;

	public Image blackScreen;

	public Animator blackScreenAnimator;

	private void Start()
	{
	}

	private void Update()
	{
		if (InitiateJump && !jumpToLast)
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

	public void JumpBack()
	{
		jumpToLast = true;
		blackScreenAnimator.SetBool("isOpen", value: false);
	}
}
