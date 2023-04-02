using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{
	private bool firstperson;

	public Transform target;

	public Transform playermodel;

	public float distance = 5f;

	public float xSpeed = 120f;

	public float ySpeed = 120f;

	public float yMinLimit = -20f;

	public float yMaxLimit = 80f;

	public float distanceMin = 0.5f;

	public float distanceMax = 15f;

	private Rigidbody rigidbody;

	public float x;

	public float y;

	private bool centerCameraMove;

	private float goalYRot;

	public float xAxisClamp;

	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		x = eulerAngles.y;
		y = eulerAngles.x;
		rigidbody = GetComponent<Rigidbody>();
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
		if (GameManager.singleton.GameRunning)
		{
			PlatformerCamera();
		}
	}

	private void PlatformerCamera()
	{
		if (!target)
		{
			return;
		}
		float axis = Input.GetAxis("Mouse X");
		float axis2 = Input.GetAxis("Mouse Y");
		if (!centerCameraMove)
		{
			if (GamePadScript.instance.GetInt("CameraXInverted") == 0)
			{
				x += axis * xSpeed * distance * 0.01f;
			}
			else
			{
				x -= axis * xSpeed * distance * 0.01f;
			}
			if (GamePadScript.instance.GetInt("CameraXInverted") == 0)
			{
				y -= axis2 * ySpeed * 0.01f;
			}
			else
			{
				y += axis2 * ySpeed * 0.01f;
			}
		}
		if (GamePadScript.instance.focusButtonPressed())
		{
			goalYRot = playermodel.eulerAngles.y;
			centerCameraMove = true;
		}
		if (centerCameraMove)
		{
			x = Mathf.LerpAngle(x, goalYRot, Time.deltaTime * 7.5f);
			y = Mathf.LerpAngle(y, 30f, Time.deltaTime * 7.5f);
			if (base.transform.localEulerAngles.y < goalYRot + 1f && base.transform.localEulerAngles.y > goalYRot - 1f)
			{
				centerCameraMove = false;
			}
		}
		y = ClampAngle(y, yMinLimit, yMaxLimit);
		Quaternion quaternion = Quaternion.Euler(y, x, 0f);
		distance = Mathf.SmoothStep(distance, distanceMax, 0.1f);
		Vector3 vector = new Vector3(0f, -0.5f, 0f - distance);
		Vector3 b = quaternion * vector + target.position;
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, quaternion, 15f * Time.deltaTime);
		base.transform.position = Vector3.Lerp(base.transform.position, b, 15f * Time.deltaTime);
	}

	public void ForceCameraCenter()
	{
		goalYRot = playermodel.eulerAngles.y;
		centerCameraMove = true;
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
