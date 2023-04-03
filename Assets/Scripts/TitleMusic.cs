using UnityEngine;

public class TitleMusic : MonoBehaviour
{
	private int secondOST;

	public AudioClip OST1;

	public AudioClip OST2;

	public AudioSource _audioSource;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		secondOST = 0;
		if (GamePadScript.instance.HasKey("BonusOST"))
		{
			secondOST = GamePadScript.instance.GetInt("BonusOST");
		}
		if (secondOST == 0)
		{
			_audioSource.clip = OST1;
		}
		else if (secondOST == 1)
		{
			_audioSource.clip = OST2;
		}
		Object.DontDestroyOnLoad(base.transform.gameObject);
	}

	public void PlayMusic()
	{
		secondOST = 0;
		if (GamePadScript.instance.HasKey("BonusOST"))
		{
			secondOST = GamePadScript.instance.GetInt("BonusOST");
		}
		if (secondOST == 0)
		{
			_audioSource.clip = OST1;
		}
		else if (secondOST == 1)
		{
			_audioSource.clip = OST2;
		}
		if (!_audioSource.isPlaying)
		{
			_audioSource.Play();
		}
	}

	public void StopMusic()
	{
		_audioSource.Stop();
	}
}
