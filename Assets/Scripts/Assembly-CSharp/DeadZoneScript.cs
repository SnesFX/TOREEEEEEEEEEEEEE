using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManager.singleton.PlayerDeath();
		}
	}
}
