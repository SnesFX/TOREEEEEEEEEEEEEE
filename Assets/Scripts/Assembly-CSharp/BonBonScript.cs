using UnityEngine;

public class BonBonScript : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			GameManager.singleton.CollectStar();
			Object.Destroy(base.gameObject);
		}
	}
}
