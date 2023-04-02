using UnityEngine;

public class PCOnly_WindowModeManager : MonoBehaviour
{
	private Resolution currentResolution;

	private void Start()
	{
		currentResolution = Screen.currentResolution;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			SwitchWindowMode();
		}
	}

	private void SwitchWindowMode()
	{
		if (Screen.fullScreen)
		{
			Screen.SetResolution(800, 480, fullscreen: false);
		}
		else if (!Screen.fullScreen)
		{
			Screen.SetResolution(currentResolution.width, currentResolution.height, fullscreen: true);
		}
	}
}
