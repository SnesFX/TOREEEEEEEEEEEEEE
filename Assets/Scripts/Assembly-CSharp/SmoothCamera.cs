using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
	public float height = 2.5f;

	public float distance = 5f;

	public Transform target;

	public PlayerSystem myPlayerSystem;

	private void Start()
	{
	}

	private void Update()
	{
		float y = Mathf.Lerp(base.transform.position.y, target.position.y + height, Time.deltaTime * 6f);
		if (target.position.y < base.transform.position.y - height)
		{
			y = Mathf.Lerp(base.transform.position.y, target.position.y + height, Time.deltaTime * 24f);
		}
		Vector3 b = new Vector3(target.position.x - distance, y, target.position.z);
		base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * 12f);
	}
}
