using UnityEngine;

public class ControlTitleMusic : MonoBehaviour
{
	public bool PlayMusic;

	public TitleMusic titleMusic;

	private void Start()
	{
		titleMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<TitleMusic>();
		UseSavedSettings();
		if (PlayMusic)
		{
			GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<TitleMusic>().PlayMusic();
		}
		else if (!PlayMusic)
		{
			GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<TitleMusic>().StopMusic();
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
		GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<TitleMusic>()._audioSource.volume = num2;
	}

	private void Update()
	{
		float num = GamePadScript.instance.GetInt("MusicVolume");
		titleMusic._audioSource.volume = num / 10f;
	}
}
