using UnityEngine;
using UnityEngine.Video;

public class DemoThankYouTeaser : MonoBehaviour
{
	public GameObject TeaserPack;

	public GameObject RealCam;

	public GameObject Canvas;

	public GameObject Manager;

	public VideoPlayer vid;

	private void Start()
	{
		vid.loopPointReached += CheckOver;
	}

	private void CheckOver(VideoPlayer vp)
	{
		RealCam.SetActive(value: true);
		Canvas.SetActive(value: true);
		Manager.SetActive(value: true);
		TeaserPack.SetActive(value: false);
	}
}
