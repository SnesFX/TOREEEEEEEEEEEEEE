using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
	public bool mute;

	public AudioSource myMusic;

	public Slider MusicVolumeSlider;

	private int secondOST;

	public AudioClip OST1;

	public AudioClip OST2;

	public AudioClip VictoryJingle;

	public AudioClip VictoryJingle2;

	private void Start()
	{
		UseSavedSettings();
		if (mute)
		{
			myMusic.Stop();
		}
	}

	private void UseSavedSettings()
	{
		int num = 3;
		if (GamePadScript.instance.HasKey("MusicVolume"))
		{
			num = GamePadScript.instance.GetInt("MusicVolume");
		}
		float num2 = num;
		num2 /= 10f;
		myMusic.volume = num2;
		MusicVolumeSlider.value = num;
		ChangeTheSoundtrack();
	}

	public void ChangeSetting()
	{
		float num = MusicVolumeSlider.value;
		myMusic.volume = num / 10f;
	}

	public void ChangeTheSoundtrack()
	{
		secondOST = 0;
		if (GamePadScript.instance.HasKey("BonusOST"))
		{
			secondOST = GamePadScript.instance.GetInt("BonusOST");
		}
		if (secondOST == 0)
		{
			myMusic.clip = OST1;
		}
		else if (secondOST == 1)
		{
			myMusic.clip = OST2;
		}
		myMusic.Play();
	}

	private void Update()
	{
	}

	public void StopMusic()
	{
		myMusic.Stop();
	}

	public void PlayJingle()
	{
		secondOST = 0;
		if (GamePadScript.instance.HasKey("BonusOST"))
		{
			secondOST = GamePadScript.instance.GetInt("BonusOST");
		}
		if (secondOST == 0)
		{
			myMusic.clip = VictoryJingle;
		}
		else if (secondOST == 1)
		{
			myMusic.clip = VictoryJingle2;
		}
		myMusic.loop = false;
		myMusic.Play();
	}
}
