using UnityEngine;

public class GlitchigamiRunning : MonoBehaviour
{
	private bool done;

	public Transform Glitchigami;

	public Transform Pos1;

	public Transform Pos2;

	public AudioSource laughter;

	private void Start()
	{
	}

	private void Update()
	{
		if (!done)
		{
			Glitchigami.position = Vector3.Lerp(Glitchigami.position, Pos1.position, 2f * Time.deltaTime);
		}
		else
		{
			Glitchigami.position = Vector3.Lerp(Glitchigami.position, Pos2.position, 0.5f * Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!done && other.tag == "Player")
		{
			laughter.Play();
			done = true;
		}
	}
}
