using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
	public bool iamActive = true;

	private Vector3 pos1 = new Vector3(0f, 0f, 0f);

	private Vector3 pos2 = new Vector3(0f, 0f, 0f);

	public float speed = 0.5f;

	public float driveDistance = 6f;

	public float driveDistanceY;

	public float driveDistanceZ;

	public float driveSnailing = 2f;

	private float origZpos;

	private float currentSnailing;

	private float timeaccumulator;

	private void Start()
	{
		pos1 = new Vector3(base.transform.localPosition.x - driveDistance, base.transform.localPosition.y - driveDistanceY, base.transform.localPosition.z - driveDistanceZ);
		pos2 = new Vector3(base.transform.localPosition.x + driveDistance, base.transform.localPosition.y + driveDistanceY, base.transform.localPosition.z + driveDistanceZ);
		origZpos = base.transform.localPosition.z;
	}

	private void Update()
	{
		if (iamActive && GameManager.singleton.myTimeManager.timerIsRunning)
		{
			timeaccumulator += Time.deltaTime;
			currentSnailing = ((driveSnailing == 0f) ? 0f : Mathf.PingPong(timeaccumulator * speed, driveSnailing));
			base.transform.localPosition = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * timeaccumulator) + 1f) / 2f);
			Vector3 b = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z + currentSnailing);
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, b, Time.deltaTime * 4000f);
		}
	}
}
