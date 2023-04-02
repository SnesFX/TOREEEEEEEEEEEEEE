using UnityEngine;

public class LevelGoalScript : MonoBehaviour
{
	private void Start()
	{
	}

	public void LevelGoal()
	{
		GameManager.singleton.EndGame(Victory: true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			LevelGoal();
		}
	}
}
