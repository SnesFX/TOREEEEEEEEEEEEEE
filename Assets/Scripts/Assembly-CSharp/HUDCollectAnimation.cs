using UnityEngine;

public class HUDCollectAnimation : MonoBehaviour
{
	private RectTransform rectTransform;

	public Vector2 Goal;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		Vector2 anchoredPosition = rectTransform.anchoredPosition;
		anchoredPosition.x = Mathf.Lerp(anchoredPosition.x, Goal.x, Time.deltaTime * 20f);
		anchoredPosition.y = Mathf.Lerp(anchoredPosition.y, Goal.y, Time.deltaTime * 20f);
		rectTransform.anchoredPosition = anchoredPosition;
		if (Vector2.Distance(anchoredPosition, Goal) < 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
