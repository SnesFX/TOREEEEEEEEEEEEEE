using UnityEngine;

public class ScaleManagerScript : MonoBehaviour
{
	public Vector3 newScale;

	public float myOriginScale = 1f;

	public float speed = 14f;

	private void Start()
	{
		newScale = new Vector3(myOriginScale, myOriginScale, myOriginScale);
	}

	private void Update()
	{
		newScaleManager();
	}

	private void ScaleManager()
	{
		if (base.transform.localScale.y < myOriginScale)
		{
			base.transform.localScale += new Vector3(0f, myOriginScale * 1.5f, 0f) * Time.deltaTime * speed;
			if (base.transform.localScale.y > myOriginScale - 0.01f)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x, myOriginScale, base.transform.localScale.z);
			}
		}
		else if (base.transform.localScale.y > myOriginScale)
		{
			base.transform.localScale -= new Vector3(0f, myOriginScale * 1.5f, 0f) * Time.deltaTime * speed;
			if (base.transform.localScale.y < myOriginScale - 0.01f)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x, myOriginScale, base.transform.localScale.z);
			}
		}
		if (base.transform.localScale.z < myOriginScale)
		{
			base.transform.localScale += new Vector3(0f, 0f, myOriginScale * 1.5f) * Time.deltaTime * speed;
			if (base.transform.localScale.z > myOriginScale - 0.01f)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x, base.transform.localScale.y, myOriginScale);
			}
		}
		else if (base.transform.localScale.z > myOriginScale)
		{
			base.transform.localScale -= new Vector3(0f, 0f, myOriginScale * 1.5f) * Time.deltaTime * speed;
			if (base.transform.localScale.z < myOriginScale - 0.01f)
			{
				base.transform.localScale = new Vector3(base.transform.localScale.x, base.transform.localScale.y, myOriginScale);
			}
		}
		if (base.transform.localScale.x < myOriginScale)
		{
			base.transform.localScale += new Vector3(myOriginScale * 1.5f, 0f, 0f) * Time.deltaTime * speed;
			if (base.transform.localScale.x > myOriginScale - 0.01f)
			{
				base.transform.localScale = new Vector3(myOriginScale, base.transform.localScale.y, base.transform.localScale.z);
			}
		}
		else if (base.transform.localScale.x > myOriginScale)
		{
			base.transform.localScale -= new Vector3(myOriginScale * 1.5f, 0f, 0f) * Time.deltaTime * speed;
			if (base.transform.localScale.x < myOriginScale - 0.01f)
			{
				base.transform.localScale = new Vector3(myOriginScale, base.transform.localScale.y, base.transform.localScale.z);
			}
		}
	}

	private void newScaleManager()
	{
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, newScale, Time.deltaTime * speed);
		if (Vector3.Distance(base.transform.localScale, newScale) < 0.08f)
		{
			newScale = new Vector3(myOriginScale, myOriginScale, myOriginScale);
		}
	}
}
