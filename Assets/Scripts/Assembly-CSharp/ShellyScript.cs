using UnityEngine;

public class ShellyScript : MonoBehaviour
{
	public Transform _player;

	private bool open;

	private bool done;

	public GameObject _Teeth;

	public ScaleManagerScript _myScaleManager;

	public Transform Joint;

	public Transform newrot;

	public Transform oldrot;

	private void Start()
	{
		_player = GameObject.Find("Player").transform;
	}

	private void Update()
	{
		if (!done)
		{
			float num = Vector3.Distance(base.transform.position, _player.position);
			if (num < 6f && !open)
			{
				open = true;
				_myScaleManager.myOriginScale = 1.3f;
			}
			else if (num > 6f && open)
			{
				open = false;
				_myScaleManager.myOriginScale = 1f;
			}
		}
		if (open)
		{
			LookAtPlayer();
		}
		AnimatedShelly();
	}

	private void AnimatedShelly()
	{
		if (open)
		{
			if (!_Teeth.activeSelf)
			{
				_Teeth.SetActive(value: true);
				GameManager.singleton.FaceChange(1);
			}
			Joint.rotation = Quaternion.Lerp(Joint.rotation, newrot.rotation, Time.deltaTime * 20f);
		}
		else if (!open)
		{
			if (_Teeth.activeSelf)
			{
				_Teeth.SetActive(value: false);
				GameManager.singleton.FaceChange(0);
			}
			Joint.rotation = Quaternion.Lerp(Joint.rotation, oldrot.rotation, Time.deltaTime * 20f);
		}
	}

	private void LookAtPlayer()
	{
		Quaternion rotation = Quaternion.LookRotation(_player.position - base.transform.position);
		rotation.z = 0f;
		rotation.x = 0f;
		base.transform.rotation = rotation;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			open = false;
			GameManager.singleton.PlayerDeath();
		}
	}
}
