using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
	public Renderer feathers1;

	public Renderer feathers2;

	public Renderer feathers3;

	public Renderer feathers4;

	public Material golden_Material;

	public int characterID;

	public bool IamActive = true;

	[Header("Movement Variabls")]
	public float LevelMomentum = 7f;

	private float horizontalInput;

	private float verticalInput;

	private float horizontal;

	private float vertical;

	private float horizontalRAW;

	private float verticalRAW;

	public float inputDeadZone;

	private float LasthorizontalRAW;

	private float LastverticalRAW;

	public float speed = 10f;

	private float bigspeed = 10f;

	private float smallspeed = 6f;

	public float jumpSpeed = 11f;

	private bool doublejump;

	private int donejumps;

	public float gravity = 20f;

	private float AccelerationUpwards;

	private float maxJumpHeight;

	private bool lastFrameGrounded;

	public Vector3 LastPos = Vector3.zero;

	[Header("Movement Tools")]
	public Transform Camera;

	private CharacterController controller;

	private Vector3 inputDirection = Vector3.zero;

	private Vector3 moveDirection = Vector3.zero;

	public Vector3 cameraDirection = Vector3.zero;

	private Vector3 accelerationDirection = Vector3.zero;

	public Vector3 pushDirection = Vector3.zero;

	public bool weDriftUp;

	public Transform currentUpdriftObject;

	public Vector3 upDriftDirection = Vector3.zero;

	private Quaternion targetCamQuat;

	private float fGroundedRemember;

	private float fGroundedRememberTime = 0.25f;

	[Header("Animations")]
	public Animation _playeranimation;

	public Animation _macbatanimation;

	public Animation _tastyanimation;

	public Transform _playermodel;

	public ScaleManagerScript myScaleManager;

	public GameObject FootSounds;

	public ParticleSystem FootStepSmoke;

	public ParticleSystem FootStepSmokeExtra;

	public GameObject FootAura;

	public ParticleSystem StarEffect;

	public ParticleSystem DeathEffect;

	private Vector3 lastFrameForward = Vector3.zero;

	private bool lastFrameGrounded_Animation;

	private float horizontalLeaning;

	[Header("Sounds")]
	public AudioSource _squishSound;

	public AudioSource _jumpSound;

	public AudioSource _deathSound;

	public VoiceManager _myVoiceManager;

	[Header("Mobile Inputs")]
	public Joystick myJoystick;

	public TouchField myTouchField;

	private void Awake()
	{
		Camera = GameObject.Find("Main Camera").transform;
		Camera.gameObject.GetComponent<MouseOrbitImproved>().target = base.transform;
	}

	private void Start()
	{
		if (GamePadScript.instance.HasKey("ChosenCharacter"))
		{
			characterID = GamePadScript.instance.GetInt("ChosenCharacter");
			if (characterID == 1)
			{
				_playeranimation.gameObject.SetActive(value: false);
				_macbatanimation.gameObject.SetActive(value: true);
				_playeranimation = _macbatanimation;
			}
			else if (characterID == 2)
			{
				_playeranimation.gameObject.SetActive(value: false);
				_tastyanimation.gameObject.SetActive(value: true);
				_playeranimation = _tastyanimation;
				bigspeed = 14f;
			}
		}
		CheckIfWeGolden();
		pushDirection = Vector3.zero;
		controller = GetComponent<CharacterController>();
		_playeranimation["Idle"].speed = speed / 32f;
		_playeranimation["Run"].speed = speed / 12f;
		_playeranimation["JumpNEW"].speed = speed / 12f;
		_playeranimation["FallNEW"].speed = speed / 18f;
		_playeranimation.Play("Idle");
		controller = GetComponent<CharacterController>();
		ImportantSquish();
	}

	private void CheckIfWeGolden()
	{
		int num = 0;
		if (GamePadScript.instance.HasKey("Level1Act1Rank") && GamePadScript.instance.GetInt("Level1Act1Rank") == 4)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level1Act2Rank") && GamePadScript.instance.GetInt("Level1Act2Rank") == 4)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level2Act1Rank") && GamePadScript.instance.GetInt("Level2Act1Rank") == 4)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level2Act2Rank") && GamePadScript.instance.GetInt("Level2Act2Rank") == 4)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level3Act1Rank") && GamePadScript.instance.GetInt("Level3Act1Rank") == 4)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level3Act2Rank") && GamePadScript.instance.GetInt("Level3Act2Rank") == 4)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level4Act1Rank") && GamePadScript.instance.GetInt("Level4Act1Rank") == 4)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level4Act2Rank") && GamePadScript.instance.GetInt("Level4Act2Rank") == 4)
		{
			num++;
		}
		if (GamePadScript.instance.HasKey("Level5Act1Rank") && GamePadScript.instance.GetInt("Level5Act1Rank") == 4)
		{
			num++;
		}
		if (num >= 9)
		{
			feathers1.material = golden_Material;
			feathers2.material = golden_Material;
			feathers3.material = golden_Material;
			feathers4.material = golden_Material;
		}
	}

	private void Update()
	{
		if (IamActive)
		{
			CheckForInput();
		}
	}

	private void CheckForInput()
	{
		if (GameManager.singleton.PlayOnMobile)
		{
			horizontal = myJoystick.Horizontal;
			vertical = myJoystick.Vertical;
		}
		else
		{
			horizontal = GamePadScript.instance.xAxis();
			vertical = 0f - GamePadScript.instance.yAxis();
		}
		horizontalRAW = GamePadScript.instance.xAxis();
		verticalRAW = 0f - GamePadScript.instance.yAxis();
		DelayCheck();
		RotatePlayer();
		WalkAround();
		GroundCheck();
		if (weDriftUp)
		{
			UpDrifting();
		}
		MoveController();
		AnimationManager();
		LasthorizontalRAW = horizontalRAW;
		LastverticalRAW = verticalRAW;
	}

	private void DelayCheck()
	{
		fGroundedRemember -= Time.deltaTime;
		if (controller.isGrounded)
		{
			fGroundedRemember = fGroundedRememberTime;
		}
	}

	private void RotatePlayer()
	{
		base.transform.rotation = new Quaternion(base.transform.rotation.x, 0f, base.transform.rotation.z, 1f);
		Vector3 value = moveDirection;
		value.y = 0f;
		float num = Vector3.Angle(_playermodel.forward, Vector3.Normalize(value));
		if (num < 120f)
		{
			_playermodel.forward = Vector3.Lerp(_playermodel.forward, Vector3.Normalize(value), 3.5f * Time.deltaTime);
		}
		else
		{
			_playermodel.forward = Vector3.Normalize(value);
		}
		_playermodel.forward = Vector3.Lerp(_playermodel.forward, Vector3.Normalize(value), 3.5f * Time.deltaTime);
		float num2 = Vector3.Angle(lastFrameForward, _playermodel.forward);
		if (Vector3.Cross(lastFrameForward, _playermodel.forward).y < 0f)
		{
			num2 = 0f - num2;
		}
		if (num2 < -1.5f)
		{
			num2 = -1.5f;
		}
		else if (num2 > 1.5f)
		{
			num2 = 1.5f;
		}
		if ((num2 > 0.5f && controller.isGrounded && num > 20f) || (num2 < -0.5f && controller.isGrounded && num > 20f))
		{
			horizontalLeaning = Mathf.Lerp(horizontalLeaning, num2 * 8f, 15f * Time.deltaTime);
		}
		else
		{
			horizontalLeaning = Mathf.Lerp(horizontalLeaning, 0f, 10f * Time.deltaTime);
		}
		Vector3 eulerAngles = _playermodel.rotation.eulerAngles;
		eulerAngles.z -= horizontalLeaning;
		if (!controller.isGrounded && characterID == 0 && controller.velocity.magnitude > 0.2f)
		{
			eulerAngles.x = Mathf.Lerp(eulerAngles.x, 50f, 10f * Time.deltaTime);
		}
		else if ((GamePadScript.instance.runButtonPressed() && LastPos != base.transform.position) || (GamePadScript.instance.sprintButtonPressed() && LastPos != base.transform.position))
		{
			eulerAngles.x = Mathf.Lerp(eulerAngles.x, 15f, 10f * Time.deltaTime);
		}
		else
		{
			eulerAngles.x = Mathf.Lerp(eulerAngles.x, 0f, 10f * Time.deltaTime);
		}
		_playermodel.rotation = Quaternion.Euler(eulerAngles);
		lastFrameForward = _playermodel.forward;
		if (GameManager.singleton.PlayOnMobile && moveDirection != Vector3.zero)
		{
			moveDirection = new Vector3(_playermodel.forward.x * speed, moveDirection.y, _playermodel.forward.z * speed);
		}
	}

	private void WalkAround()
	{
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		if (GamePadScript.instance.runButtonPressed() || GamePadScript.instance.sprintButtonPressed())
		{
			speed = Mathf.Lerp(speed, bigspeed, 5f * Time.deltaTime);
		}
		else
		{
			speed = Mathf.Lerp(speed, smallspeed, 5f * Time.deltaTime);
		}
		zero = base.transform.forward * vertical;
		zero2 = base.transform.right * horizontal;
		if (new Vector2(horizontal, vertical).magnitude < inputDeadZone)
		{
			zero = Vector3.zero;
			zero2 = Vector3.zero;
		}
		moveDirection = Vector3.ClampMagnitude(zero + zero2, 1f);
		Vector3 euler = new Vector3(Camera.rotation.eulerAngles.x, Camera.rotation.eulerAngles.y, Camera.rotation.eulerAngles.z);
		Vector3 euler2 = new Vector3(0f, Camera.rotation.eulerAngles.y, 0f);
		Camera.rotation = Quaternion.Euler(euler2);
		moveDirection = Camera.TransformDirection(moveDirection);
		moveDirection.y = 0f;
		moveDirection *= speed;
		Camera.rotation = Quaternion.Euler(euler);
		if (Input.GetButtonDown("Fire2"))
		{
			myScaleManager.newScale = new Vector3(myScaleManager.newScale.x, _playermodel.localScale.y - 0.3f, myScaleManager.newScale.z);
		}
		if ((fGroundedRemember > 0f && GamePadScript.instance.jumpButton() && accelerationDirection.y < 0f && donejumps < 2) || (doublejump && GamePadScript.instance.jumpButton() && donejumps < 2) || (fGroundedRemember > 0f && myTouchField.Pressed && accelerationDirection.y < 0f && donejumps < 2))
		{
			_playermodel.localScale = new Vector3(1.5f, 0.5f, 1.5f);
			myScaleManager.newScale = new Vector3(0.5f, 1.25f, 0.5f);
			moveDirection.y = jumpSpeed;
			StarEffect.Play();
			_jumpSound.Play();
			accelerationDirection = moveDirection;
			AccelerationUpwards = moveDirection.y;
			if (characterID != 1)
			{
				donejumps++;
			}
			if (fGroundedRemember < 0f && characterID != 1)
			{
				doublejump = false;
			}
			if (characterID == 0 && !doublejump)
			{
				_myVoiceManager.GetALine(0);
			}
		}
	}

	public void ExternJump(float externPower)
	{
		_playermodel.localScale = new Vector3(1.75f, 0.25f, 1.75f);
		myScaleManager.newScale = new Vector3(0.5f, 1.5f, 0.5f);
		moveDirection.y = externPower;
		accelerationDirection = moveDirection;
		MoveController();
	}

	private void GroundCheck()
	{
		int num = 256;
		num = ~num;
		if (Physics.Raycast(base.transform.position, Vector3.down, out var hitInfo, 1.5f, num))
		{
			if (hitInfo.transform.tag == "Ice" || hitInfo.transform.tag == "MovingIceObject")
			{
				LevelMomentum = Mathf.Lerp(LevelMomentum, 1f, Time.deltaTime * 15.5f);
			}
			else
			{
				LevelMomentum = Mathf.Lerp(LevelMomentum, 7f, Time.deltaTime * 15.5f);
			}
			if (hitInfo.transform.tag == "Movingobject" || hitInfo.transform.tag == "MovingIceObject")
			{
				base.transform.parent = hitInfo.transform;
			}
			else if (!(hitInfo.transform.tag == "Enemy"))
			{
				if (hitInfo.transform.tag == "Bouncer")
				{
					ExternJump(25f);
				}
				else if (hitInfo.transform.tag == "Pusher")
				{
					pushDirection = hitInfo.transform.forward * 10f;
				}
				else
				{
					base.transform.parent = null;
				}
			}
		}
		else
		{
			base.transform.parent = null;
		}
	}

	public void UpDrifting()
	{
		upDriftDirection.y += 50f * Time.deltaTime;
		if (currentUpdriftObject != null)
		{
			pushDirection = currentUpdriftObject.transform.up * 10f;
		}
		doublejump = true;
		donejumps = 0;
	}

	public void StopUpDrifting()
	{
		upDriftDirection = Vector3.zero;
		weDriftUp = false;
	}

	private void MoveController()
	{
		LastPos = base.transform.position;
		if (controller.isGrounded)
		{
			accelerationDirection.x = Mathf.Lerp(accelerationDirection.x, moveDirection.x + pushDirection.x, LevelMomentum * Time.deltaTime);
			accelerationDirection.z = Mathf.Lerp(accelerationDirection.z, moveDirection.z + pushDirection.z, LevelMomentum * Time.deltaTime);
			AccelerationUpwards = moveDirection.y;
		}
		else
		{
			accelerationDirection.x = Mathf.Lerp(accelerationDirection.x, moveDirection.x + pushDirection.x, LevelMomentum * Time.deltaTime);
			accelerationDirection.z = Mathf.Lerp(accelerationDirection.z, moveDirection.z + pushDirection.z, LevelMomentum * Time.deltaTime);
		}
		if ((Input.GetButton("Jump") && accelerationDirection.y > 0f && upDriftDirection == Vector3.zero) || (myTouchField.Pressed && accelerationDirection.y > 0f && upDriftDirection == Vector3.zero))
		{
			AccelerationUpwards -= gravity / 2f * Time.deltaTime;
		}
		else if (upDriftDirection == Vector3.zero)
		{
			AccelerationUpwards -= gravity * Time.deltaTime;
		}
		if (upDriftDirection != Vector3.zero)
		{
			AccelerationUpwards = upDriftDirection.y * 20f * Time.deltaTime;
		}
		accelerationDirection.y = AccelerationUpwards;
		controller.Move(accelerationDirection * Time.deltaTime);
		if (!controller.isGrounded && lastFrameGrounded && accelerationDirection.y < 0f && upDriftDirection == Vector3.zero)
		{
			AvoidSlopeBounce();
		}
		if (controller.isGrounded && !lastFrameGrounded)
		{
			myScaleManager.newScale = new Vector3(1.5f, 0.25f, 1.5f);
			StarEffect.Play();
			_squishSound.Play();
			doublejump = true;
			donejumps = 0;
		}
		lastFrameGrounded = controller.isGrounded;
		if (pushDirection != Vector3.zero)
		{
			pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, Time.deltaTime);
			if (Vector3.Distance(pushDirection, Vector3.zero) < 3f)
			{
				pushDirection = Vector3.zero;
			}
		}
	}

	private void AvoidSlopeBounce()
	{
		if (Physics.Raycast(base.transform.position, new Vector3(0f, -1f, 0f), out var hitInfo, 1.25f) && hitInfo.transform.tag != "Bouncer")
		{
			controller.Move(new Vector3(0f, 0f - hitInfo.distance, 0f));
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Breaker")
		{
			moveDirection = Vector3.zero;
			accelerationDirection = Vector3.zero;
			pushDirection = Vector3.zero;
			Object.Destroy(other.gameObject);
		}
	}

	public void EndOfLevel()
	{
		myScaleManager.newScale = new Vector3(1.5f, 0.25f, 1.5f);
		StarEffect.Play();
		_squishSound.Play();
		Vector3 target = Camera.position - base.transform.position;
		target.y = 0f;
		Vector3 forward = Vector3.RotateTowards(_playermodel.forward, target, 90000f, 100f);
		_playermodel.rotation = Quaternion.LookRotation(forward);
		if (characterID == 0)
		{
			_myVoiceManager.GetALine(2);
		}
		_playeranimation["JumpNEW"].speed = 0.3f;
		_playeranimation.CrossFade("JumpNEW", 0.25f);
		FootStepSmoke.enableEmission = false;
		FootStepSmokeExtra.enableEmission = false;
		FootAura.SetActive(value: false);
		FootSounds.SetActive(value: false);
	}

	public void DeathAnimation()
	{
		Vector3 target = Camera.position - base.transform.position;
		target.y = 0f;
		Vector3.RotateTowards(_playermodel.forward, target, 90000f, 100f);
		if (characterID == 0)
		{
			_myVoiceManager.GetALine(1);
		}
		_deathSound.Play();
		DeathEffect.Play();
		FootStepSmoke.enableEmission = false;
		FootStepSmokeExtra.enableEmission = false;
		FootAura.SetActive(value: false);
		FootSounds.SetActive(value: false);
	}

	public void ImportantSquish()
	{
		myScaleManager.newScale = new Vector3(0.25f, 0.25f, 0.25f);
		StarEffect.Play();
		_squishSound.Play();
	}

	public void CollectStar()
	{
		GameManager.singleton.stars++;
		GameManager.singleton.CollectPoint();
		GameManager.singleton.score += 100;
		GameManager.singleton.UpdateScore();
	}

	private void AnimationManager()
	{
		float num = 0f;
		float num2 = Mathf.Abs(horizontal);
		float num3 = Mathf.Abs(vertical);
		if (num2 > num3)
		{
			num = num2;
		}
		else
		{
			num = num3;
		}
		if (GameManager.singleton.PlayOnMobile)
		{
			num = 1f;
		}
		num = Mathf.Abs(moveDirection.magnitude / 10f);
		int num4 = 0;
		if (controller.isGrounded)
		{
			if (LastPos == base.transform.position)
			{
				num4 = 1;
				FootStepSmoke.enableEmission = false;
				FootStepSmokeExtra.enableEmission = false;
				FootAura.SetActive(value: false);
			}
			else
			{
				num4 = 2;
				FootStepSmoke.enableEmission = true;
				if (GamePadScript.instance.runButtonPressed() || GamePadScript.instance.sprintButtonPressed())
				{
					FootStepSmokeExtra.enableEmission = true;
					FootAura.SetActive(value: true);
				}
				else
				{
					FootStepSmokeExtra.enableEmission = false;
					FootAura.SetActive(value: false);
				}
			}
		}
		else
		{
			FootStepSmoke.enableEmission = false;
			FootStepSmokeExtra.enableEmission = false;
			FootAura.SetActive(value: false);
			num4 = ((!(LastPos.y < base.transform.position.y)) ? 4 : 3);
		}
		if (num4 != 2)
		{
			FootSounds.SetActive(value: false);
		}
		switch (num4)
		{
		case 1:
			_playeranimation.CrossFade("Idle", 0.1f);
			if (!lastFrameGrounded_Animation)
			{
				_playeranimation.Play("Idle");
			}
			break;
		case 2:
			if (moveDirection == Vector3.zero)
			{
				FootStepSmoke.enableEmission = false;
				FootSounds.SetActive(value: false);
				break;
			}
			_playeranimation["Run"].speed = speed * num / 10f;
			_playeranimation.CrossFade("Run", 0.15f);
			FootSounds.SetActive(value: true);
			if (!lastFrameGrounded_Animation)
			{
				_playeranimation.Play("Run");
			}
			break;
		case 3:
			_playeranimation["JumpNEW"].speed = speed / 20f;
			_playeranimation.CrossFade("JumpNEW", 0.25f);
			break;
		case 4:
			_playeranimation["FallNEW"].speed = speed / 20f;
			_playeranimation.CrossFade("FallNEW", 0.25f);
			break;
		case 5:
			_playeranimation["Slide"].speed = speed / 20f;
			_playeranimation.CrossFade("Slide", 0.05f);
			break;
		case 6:
			_playeranimation["Attack"].speed = speed / 11f;
			_playeranimation.CrossFade("Attack", 0.25f);
			break;
		case 7:
			_playeranimation["Fly"].speed = speed / 20f;
			_playeranimation.CrossFade("Fly", 0.1f);
			break;
		}
		lastFrameGrounded_Animation = controller.isGrounded;
	}
}
