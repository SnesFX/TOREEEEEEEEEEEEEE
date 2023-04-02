using UnityEngine;

public class HausReihenScript : MonoBehaviour
{
	private Vector3 ogPos;

	private Vector3 newPos;

	private void Start()
	{
		ogPos = base.transform.position;
		newPos = new Vector3(base.transform.position.x - 120f, base.transform.position.y, base.transform.position.z);
	}

	private void FixedUpdate()
	{
		base.transform.Translate(-base.transform.right * 6f * Time.deltaTime);
		if (base.transform.position.x < newPos.x)
		{
			base.transform.position = ogPos;
		}
	}
}
