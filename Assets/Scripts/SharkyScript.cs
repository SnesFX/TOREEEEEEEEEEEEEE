using UnityEngine;

public class SharkyScript : MonoBehaviour
{
	public Animation _sharkyanimation;

	private void Start()
	{
		_sharkyanimation.Play("Idle");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_sharkyanimation.CrossFade("Nom", 0.1f);
			GameManager.singleton.PlayerDeath();
		}
	}
}
