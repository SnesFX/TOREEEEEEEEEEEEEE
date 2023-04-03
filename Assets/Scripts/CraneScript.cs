using UnityEngine;

public class CraneScript : MonoBehaviour
{
	public bool _paused;

	public Transform from;

	public Transform to;

	public float speed = 1f;

	private float cooldown;

	public Transform myPlatform;

	private void Start()
	{
	}

	private void Update()
	{
		if (!_paused)
		{
			MoveCrane();
		}
	}

	private void MoveCrane()
	{
		if (myPlatform.childCount > 0)
		{
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, to.rotation, Time.deltaTime * speed);
			cooldown = 2f;
		}
		else if (cooldown < 0f)
		{
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, from.rotation, Time.deltaTime * speed);
		}
		if (cooldown > 0f)
		{
			cooldown -= 1f * Time.deltaTime;
		}
	}
}
