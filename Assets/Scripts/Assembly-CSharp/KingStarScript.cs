using UnityEngine;

public class KingStarScript : MonoBehaviour
{
	private bool Spoops;

	public Vector3 newPos;

	public Transform player;

	public Color newAmbientLight;

	public GameObject mySounds;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (Spoops)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, newPos, Time.deltaTime * 8f);
		}
		else if (Vector3.Distance(player.position, base.transform.position) < 180f)
		{
			Spoops = true;
			RenderSettings.ambientLight = newAmbientLight;
			mySounds.SetActive(value: true);
			GameManager.singleton.DistortMusic(1);
		}
	}
}
