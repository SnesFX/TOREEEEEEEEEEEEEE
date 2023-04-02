using UnityEngine;

public class SubmarineScript : MonoBehaviour
{
	public Transform canonjoint;

	public Transform normpos;

	public Transform shotpos;

	public bool justShot;

	private void Start()
	{
	}

	private void Update()
	{
		AdjustCanon();
	}

	private void AdjustCanon()
	{
		if (justShot)
		{
			canonjoint.position = Vector3.Lerp(canonjoint.position, shotpos.position, Time.deltaTime * 50f);
			if (Vector3.Distance(shotpos.position, canonjoint.position) < 0.01f)
			{
				justShot = false;
			}
		}
		else
		{
			canonjoint.position = Vector3.Lerp(canonjoint.position, normpos.position, Time.deltaTime * 2f);
			if (Vector3.Distance(normpos.position, canonjoint.position) < 0.01f)
			{
				justShot = true;
			}
		}
	}
}
