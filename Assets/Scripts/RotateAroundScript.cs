using UnityEngine;

public class RotateAroundScript : MonoBehaviour
{
	public float RotateX;

	public float RotateY;

	public float RotateZ;

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.Rotate(RotateX, RotateY, RotateZ);
	}
}
