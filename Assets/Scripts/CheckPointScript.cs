using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
	public Animation _flaganimation;

	public Transform respawnPoint;

	private bool done;

	public AudioSource mySound;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !done)
		{
			mySound.Play();
			GameManager.singleton._respawn = respawnPoint.position;
			GameManager.singleton._rerotation = base.transform.rotation;
			GameManager.singleton.PlayerStars();
			_flaganimation.Play("FlagRise");
			done = true;
		}
	}
}
