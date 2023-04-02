using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartScript : MonoBehaviour
{
	public VideoPlayer vid;

	private void Start()
	{
		vid.loopPointReached += CheckOver;
	}

	private void CheckOver(VideoPlayer vp)
	{
		MonoBehaviour.print("Video Is Over");
		SceneManager.LoadScene(1);
	}
}
