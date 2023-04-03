using UnityEngine;

public class UpDriftObject : MonoBehaviour
{
	public bool AlsoPushesSidways;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerSystem>().weDriftUp = true;
			if (AlsoPushesSidways)
			{
				other.GetComponent<PlayerSystem>().currentUpdriftObject = base.transform;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PlayerSystem>().StopUpDrifting();
		}
		if (AlsoPushesSidways)
		{
			other.GetComponent<PlayerSystem>().currentUpdriftObject = null;
		}
	}
}
