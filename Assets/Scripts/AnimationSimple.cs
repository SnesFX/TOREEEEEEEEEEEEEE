using UnityEngine;

public class AnimationSimple : MonoBehaviour
{
	public Animation _myanimation;

	public float animationSpeed = 0.05f;

	private void Start()
	{
		_myanimation["Animation"].speed = animationSpeed;
		_myanimation.Play("Animation");
	}
}
