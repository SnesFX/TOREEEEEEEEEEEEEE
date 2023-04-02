using UnityEngine;

public class PingPongUpDown : MonoBehaviour
{
	private Transform target;

	public float hight = 0.5f;

	private int dir;

	private float delay;

	private void Start()
	{
		target = base.transform;
		dir = Random.Range(1, 3);
	}

	private void Update()
	{
		Vector3 zero = Vector3.zero;
		zero = ((dir != 1) ? new Vector3(base.transform.position.x, base.transform.position.y - Mathf.PingPong(Time.time * 2f, hight) + hight / 2f, base.transform.position.z) : new Vector3(base.transform.position.x, base.transform.position.y + Mathf.PingPong(Time.time * 2f, hight) - hight / 2f, base.transform.position.z));
		base.transform.position = Vector3.Lerp(base.transform.position, zero, Time.deltaTime);
	}
}
