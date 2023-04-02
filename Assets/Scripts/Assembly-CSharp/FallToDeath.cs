using UnityEngine;

public class FallToDeath : MonoBehaviour
{
	public float fallBoundary = -14f;

	private Transform Player;

	private void Start()
	{
		Player = GameObject.Find("Player").transform;
	}

	private void FixedUpdate()
	{
		if (Player.position.y < fallBoundary)
		{
			GameManager.singleton.PlayerDeath();
		}
	}
}
