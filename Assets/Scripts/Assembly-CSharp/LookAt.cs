using UnityEngine;

public class LookAt : MonoBehaviour
{
	public Transform target;

	public bool LookAtCam = true;

	public bool OnlyY;

	private void Start()
	{
		target = Camera.main.transform;
	}

	private void Update()
	{
		Quaternion rotation = Quaternion.LookRotation(target.position - base.transform.position);
		if (OnlyY)
		{
			rotation.z = 0f;
			rotation.x = 0f;
		}
		base.transform.rotation = rotation;
	}
}
