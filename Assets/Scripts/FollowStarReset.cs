using UnityEngine;

public class FollowStarReset : MonoBehaviour
{
	public FollowStarScript1[] myStars;

	private void OnEnable()
	{
		FollowStarScript1[] array = myStars;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].StarReset();
		}
		base.gameObject.SetActive(value: false);
	}
}
