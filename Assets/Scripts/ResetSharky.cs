using UnityEngine;

public class ResetSharky : MonoBehaviour
{
	public Animation _sharkyanimation;

	private void Start()
	{
		_sharkyanimation.Play("Idle");
		base.gameObject.SetActive(value: false);
	}
}
