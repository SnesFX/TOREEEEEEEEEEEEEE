using UnityEngine;

public class VoiceManager : MonoBehaviour
{
	public AudioSource mySource;

	public AudioClip[] myClips;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void GetALine(int category)
	{
		int num = 0;
		switch (category)
		{
		case 0:
			num = Random.Range(0, 3);
			SayLine(num);
			break;
		case 1:
			num = Random.Range(3, 5);
			SayLine(num);
			break;
		case 2:
			num = Random.Range(5, 8);
			SayLine(num);
			break;
		}
	}

	private void SayLine(int newLine)
	{
		mySource.clip = myClips[newLine];
		mySource.Play();
	}
}
