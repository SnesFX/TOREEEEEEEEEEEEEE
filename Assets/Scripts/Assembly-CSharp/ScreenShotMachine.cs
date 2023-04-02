using UnityEngine;

public class ScreenShotMachine : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ScreenCapture.CaptureScreenshot("filename", 8);
		}
	}
}
