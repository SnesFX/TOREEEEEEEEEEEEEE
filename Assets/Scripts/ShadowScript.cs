using UnityEngine;

public class ShadowScript : MonoBehaviour
{
	public Transform ShadowObject;

	public LayerMask IgnoreMe;

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (Physics.Raycast(base.transform.position, Vector3.down, out var hitInfo, 150f, ~(int)IgnoreMe))
		{
			ShadowObject.position = new Vector3(hitInfo.point.x, hitInfo.point.y + 0.02f, hitInfo.point.z);
		}
	}
}
