using UnityEngine;

public class SetPositionToObject : MonoBehaviour
{
	public Transform objectPos;

	public bool SaveY;

	private void Update()
	{
		if (!SaveY)
		{
			base.transform.position = objectPos.position;
		}
		else
		{
			base.transform.position = new Vector3(objectPos.position.x, base.transform.position.y, objectPos.position.z);
		}
	}
}
