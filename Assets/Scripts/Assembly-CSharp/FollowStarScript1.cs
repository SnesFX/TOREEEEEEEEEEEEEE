using UnityEngine;

public class FollowStarScript1 : MonoBehaviour
{
	public TimeManager myTimeManager;

	public bool goHunt;

	public GameObject niceStar;

	public GameObject evilStar;

	private Vector3 StartPos;

	private Quaternion StartRotation;

	public Color oldAmbientLight;

	public Color newAmbientLight;

	public Transform target;

	private void Start()
	{
		myTimeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
		StartPos = base.transform.position;
		StartRotation = base.transform.rotation;
	}

	private void Update()
	{
		if (goHunt)
		{
			if (myTimeManager.timerIsRunning)
			{
				FollowPlayer();
			}
			ChangeAmbient();
			if (Vector3.Distance(target.position, base.transform.position) < 20f)
			{
				GameManager.singleton.PlayerDeath();
			}
		}
		else if (Vector3.Distance(target.position, base.transform.position) < 100f)
		{
			goHunt = true;
			ChangeStar(1);
		}
	}

	private void FollowPlayer()
	{
		Quaternion b = Quaternion.LookRotation(target.position - base.transform.position, Vector3.up);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.time * 0.001f);
		base.transform.Translate(Vector3.forward * Time.deltaTime * 3f);
	}

	private void ChangeStar(int starform)
	{
		switch (starform)
		{
		case 0:
			niceStar.SetActive(value: true);
			evilStar.SetActive(value: false);
			break;
		case 1:
			niceStar.SetActive(value: false);
			evilStar.SetActive(value: true);
			break;
		}
	}

	private void ChangeAmbient()
	{
		float num = Vector3.Distance(target.position, base.transform.position);
		if (num < 100f)
		{
			RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, newAmbientLight, Time.deltaTime * 0.5f);
			GameManager.singleton.DistortMusic(1);
		}
		else
		{
			RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, oldAmbientLight, Time.deltaTime * 2.5f);
			GameManager.singleton.DistortMusic(0);
		}
		if (num > 150f)
		{
			goHunt = false;
			ChangeStar(0);
		}
	}

	public void StarReset()
	{
		goHunt = false;
		RenderSettings.ambientLight = oldAmbientLight;
		niceStar.SetActive(value: true);
		evilStar.SetActive(value: false);
		base.transform.position = StartPos;
		base.transform.rotation = StartRotation;
		GameManager.singleton.DistortMusic(0);
	}

	public void UnAtmoStarReset()
	{
		goHunt = false;
		niceStar.SetActive(value: true);
		evilStar.SetActive(value: false);
		base.transform.position = StartPos;
		base.transform.rotation = StartRotation;
	}
}
