using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipIntroAnimation : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (GamePadScript.instance.jumpButton() || Input.GetButtonDown("KeyboardUse"))
		{
			Skip();
		}
		if (GamePadScript.instance.cancelButton())
		{
			Skip();
		}
		if (GamePadScript.instance.startButton())
		{
			Skip();
		}
	}

	private void Skip()
	{
		SceneManager.LoadScene(1);
	}
}
