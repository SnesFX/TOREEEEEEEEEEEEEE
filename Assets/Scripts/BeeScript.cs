using UnityEngine;

public class BeeScript : MonoBehaviour
{
	public bool activeBee = true;

	public AudioSource mySound;

	private float waitForSound;

	public float speed = 1f;

	public int distance = 1;

	private int direction;

	private Vector3 _startPos;

	private Vector3 _goalPos;

	private void Start()
	{
		_startPos = base.transform.position;
		_goalPos = base.transform.position + base.transform.forward * distance;
		Quaternion rotation = Quaternion.LookRotation(_goalPos - base.transform.position, Vector3.up);
		base.transform.rotation = rotation;
	}

	private void Update()
	{
		if (activeBee)
		{
			MoveBee();
		}
	}

	private void MoveBee()
	{
		if (direction == 0)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, _goalPos, Time.deltaTime * speed);
			if (Vector3.Distance(base.transform.position, _goalPos) < 0.5f)
			{
				direction = 1;
				Quaternion rotation = Quaternion.LookRotation(_startPos - base.transform.position, Vector3.up);
				base.transform.rotation = rotation;
			}
		}
		else
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, _startPos, Time.deltaTime * speed);
			if (Vector3.Distance(base.transform.position, _startPos) < 0.5f)
			{
				direction = 0;
				Quaternion rotation2 = Quaternion.LookRotation(_goalPos - base.transform.position, Vector3.up);
				base.transform.rotation = rotation2;
			}
		}
		waitForSound += 1f * Time.deltaTime;
		if (waitForSound > 3f)
		{
			if (mySound != null)
			{
				mySound.Play();
			}
			waitForSound = 0f;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && activeBee)
		{
			GameManager.singleton.PlayerDeath();
		}
	}
}
