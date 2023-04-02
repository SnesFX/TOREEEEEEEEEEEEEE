using UnityEngine;

public class FootStepSounds : MonoBehaviour
{
	public PlayerSystem _myPlayerSystem;

	public CharacterController controller;

	public float wait;

	private int soundID;

	public GameObject parentObject;

	public AudioClip[] DirtSounds;

	public AudioClip[] WoodSounds;

	public AudioSource mySoundSource;

	private void Update()
	{
		float f = GamePadScript.instance.xAxis();
		float f2 = 0f - GamePadScript.instance.yAxis();
		float num = Mathf.Abs(f);
		float num2 = Mathf.Abs(f2);
		float num3 = 0f;
		num3 = ((!(num > num2)) ? num2 : num);
		if (num3 > 1.17f)
		{
			num3 = 1.17f;
		}
		if (controller.isGrounded && _myPlayerSystem.IamActive)
		{
			wait += num3 * Time.deltaTime;
			if (wait > 1.5f / _myPlayerSystem.speed)
			{
				mySoundSource.Stop();
				mySoundSource.clip = DirtSounds[soundID];
				mySoundSource.Play();
				wait = 0f;
				if (soundID == 0)
				{
					soundID = 1;
				}
				else
				{
					soundID = 0;
				}
			}
		}
		else
		{
			wait = 0f;
		}
	}
}
