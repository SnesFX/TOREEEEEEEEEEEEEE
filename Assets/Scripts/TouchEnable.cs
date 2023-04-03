using UnityEngine;

public class TouchEnable : MonoBehaviour
{
	public bool onlyonce;

	public GameObject activationObject;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			activationObject.SetActive(value: true);
			if (onlyonce)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
