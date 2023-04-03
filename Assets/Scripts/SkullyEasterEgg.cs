using UnityEngine;

public class SkullyEasterEgg : MonoBehaviour
{
	private bool gotime;

	public AudioSource scream;

	private void Start()
	{
	}

	private void Update()
	{
		if (gotime)
		{
			base.transform.Translate(-base.transform.up * Time.deltaTime * 50f);
			if (base.transform.position.y < -100f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !gotime)
		{
			scream.Play();
			gotime = true;
		}
	}
}
