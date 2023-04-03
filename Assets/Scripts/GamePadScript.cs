using System;
using System.IO;
using UnityEngine;

public class GamePadScript : MonoBehaviour
{
	public bool eightwayControls;

	private static bool[,] buttonstate = new bool[14, 2];

	public static GamePadScript instance;

	private int rumblecounter;

	private int rumblestyle;

	private bool focus;

	private static bool NOSAVE = false;

	private int saveCooldown;

	private string boolPath;

	private string intPath;

	private static int chosenCharacter = 0;

	private int[] savedInts;

	private bool[] savedBools;

	private bool stickflag;

	public bool menuUp;

	public bool menuDown;

	public bool menuLeft;

	public bool menuRight;

	public void rumble(int rumblestyle = 0)
	{
		this.rumblestyle = rumblestyle;
		rumblecounter = ((rumblestyle == 0) ? 20 : 10);
	}

	private void Awake()
	{
		instance = this;
		boolPath = Application.persistentDataPath + "/bools.sav";
		intPath = Application.persistentDataPath + "/ints.sav";
	}

	private void Start()
	{
		for (int i = 0; i < 14; i++)
		{
			buttonstate[i, 0] = (buttonstate[i, 1] = false);
		}
	}

	public void SetInt(string s, int x)
	{
		if (NOSAVE)
		{
			return;
		}
		if (s == "ChosenCharacter")
		{
			chosenCharacter = x;
			return;
		}
		if (savedInts == null)
		{
			loadGame();
		}
		if (saveIndexIsBool(s))
		{
			savedBools[getSaveIndex(s)] = x != 0;
		}
		else
		{
			savedInts[getSaveIndex(s)] = x;
		}
		saveCooldown = 60;
	}

	public bool HasKey(string s)
	{
		return true;
	}

	public int GetInt(string s)
	{
		if (NOSAVE)
		{
			return 0;
		}
		if (s == "ChosenCharacter")
		{
			return chosenCharacter;
		}
		if (savedInts == null || savedInts.Length == 0)
		{
			Debug.Log("SavedInts Length = 0");
			loadGame();
		}
		if (saveIndexIsBool(s))
		{
			if (!savedBools[getSaveIndex(s)])
			{
				return 0;
			}
			return 1;
		}
		return savedInts[getSaveIndex(s)];
	}

	private void loadGame()
	{
		FileStream fileStream = new FileStream(intPath, FileMode.OpenOrCreate);
		byte[] array = new byte[fileStream.Length];
		fileStream.Read(array, 0, (int)fileStream.Length);
		fileStream.Close();
		if (NOSAVE)
		{
			savedInts = new int[24];
			savedBools = new bool[27];
			return;
		}
		if (array.Length < 1)
		{
			initSave();
		}
		else
		{
			savedInts = toIntArray(array);
		}
		fileStream = new FileStream(boolPath, FileMode.OpenOrCreate);
		array = new byte[fileStream.Length];
		fileStream.Read(array, 0, (int)fileStream.Length);
		fileStream.Close();
		if (array.Length < 1)
		{
			initSave();
		}
		else
		{
			savedBools = fromByteArray(array);
		}
	}

	private bool[] fromByteArray(byte[] source)
	{
		bool[] array = new bool[27];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = getBool(source, i);
		}
		return array;
	}

	private bool getBool(byte[] source, int index)
	{
		return getBool(source[index / 8], index % 8);
	}

	private bool getBool(byte source, int index)
	{
		if (index < 7)
		{
			return (int)source % (int)twopow(index + 1) / (int)twopow(index) == 1;
		}
		return (int)source / (int)twopow(index) == 1;
	}

	private int[] toIntArray(byte[] data)
	{
		int[] array = new int[data.Length / 4];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = BitConverter.ToInt32(data, i * 4);
		}
		return array;
	}

	private void initSave()
	{
		savedInts = new int[24];
		savedBools = new bool[27];
		SetInt("MusicVolume", 3);
		SetInt("CameraSensitivity", 3);
		SetInt("GotMacbat", 0);
		SetInt("GotTasty", 0);
		SetInt("ChosenCharacter", 0);
		SetInt("BonusOST", 1);
		SavingRoutine();
	}

	private byte[] toByteArray(int[] data)
	{
		byte[] array = new byte[4 * data.Length];
		for (int i = 0; i < data.Length; i++)
		{
			byte[] bytes = BitConverter.GetBytes(data[i]);
			for (int j = 0; j < bytes.Length; j++)
			{
				array[i * 4 + j] = bytes[j];
			}
		}
		return array;
	}

	private byte[] toByteArray(bool[] data)
	{
		byte[] array = new byte[data.Length / 8 + 1];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 0;
			for (int j = 0; j < 8 && 8 * i + j < data.Length; j++)
			{
				array[i] += (byte)(data[8 * i + j] ? twopow(j) : 0);
			}
		}
		return array;
	}

	private byte twopow(int j)
	{
		byte b = 1;
		for (int num = j; num > 0; num--)
		{
			b = (byte)(b * 2);
		}
		if (b == 0)
		{
			Debug.Log("Result 0 for input: " + j);
		}
		return b;
	}

	public void SavingRoutine()
	{
		Debug.Log("SAVING");
		byte[] array = toByteArray(savedBools);
		FileStream fileStream = new FileStream(boolPath, FileMode.Create);
		fileStream.Write(array, 0, array.Length);
		fileStream.Close();
		array = toByteArray(savedInts);
		FileStream fileStream2 = new FileStream(intPath, FileMode.Create);
		fileStream2.Write(array, 0, array.Length);
		fileStream2.Close();
	}

	private int getSaveIndex(string s)
	{
		switch (s)
		{
		case "MusicVolume":
			return 0;
		case "CameraSensitivity":
			return 1;
		case "Level1Act1Rank":
			return 2;
		case "Level1Act2Rank":
			return 3;
		case "Level2Act1Rank":
			return 4;
		case "Level2Act2Rank":
			return 5;
		case "Level3Act1Rank":
			return 6;
		case "Level3Act2Rank":
			return 7;
		case "Level4Act1Rank":
			return 8;
		case "Level4Act2Rank":
			return 9;
		case "Level5Act1Rank":
			return 10;
		case "Level5Act2Rank":
			return 11;
		case "LevelFinalRank":
			return 12;
		case "ChosenCharacter":
			return 13;
		case "ChosenLevel":
			return 14;
		case "1_1AllStars":
			return 15;
		case "1_2AllStars":
			return 16;
		case "2_1AllStars":
			return 17;
		case "2_2AllStars":
			return 18;
		case "3_1AllStars":
			return 19;
		case "3_2AllStars":
			return 20;
		case "4_1AllStars":
			return 21;
		case "4_2AllStars":
			return 22;
		case "5_1AllStars":
			return 23;
		case "GotMacbat":
			return 0;
		case "GotTasty":
			return 1;
		case "CameraXInverted":
			return 2;
		case "CameraYInverted":
			return 3;
		case "CRTEffectActive":
			return 4;
		case "Tutorial":
			return 5;
		case "WaitedToLong":
			return 6;
		case "BonusOST":
			return 7;
		default:
			return int.Parse(s.Substring(10)) + 7;
		}
	}

	private bool saveIndexIsBool(string s)
	{
		if (!s.Equals("GotMacbat") && !s.Equals("GotTasty") && !s.Equals("CameraXInverted") && !s.Equals("CameraYInverted") && !s.Equals("CRTEffectActive") && !s.Equals("Tutorial") && !s.Equals("WaitedToLong"))
		{
			return s.Equals("BonusOST");
		}
		return true;
	}

	public float xAxis()
	{
		return right() - left();
	}

	public float yAxis()
	{
		return up() - down();
	}

	public float left()
	{
		if (!eightwayControls)
		{
			return leftIntern();
		}
		return (leftIntern() > 0.3f) ? 1 : 0;
	}

	public float leftIntern()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
			{
				return 0.7f;
			}
			return 1f;
		}
		return 0f - Input.GetAxisRaw("Horizontal");
	}

	public float right()
	{
		if (!eightwayControls)
		{
			return rightIntern();
		}
		return (rightIntern() > 0.3f) ? 1 : 0;
	}

	public float rightIntern()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
			{
				return 0.7f;
			}
			return 1f;
		}
		return Input.GetAxisRaw("Horizontal");
	}

	public float up()
	{
		if (!eightwayControls)
		{
			return upIntern();
		}
		return (upIntern() > 0.3f) ? 1 : 0;
	}

	public float upIntern()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
			{
				return 0.7f;
			}
			return 1f;
		}
		return 0f - Input.GetAxisRaw("Vertical");
	}

	public float down()
	{
		if (!eightwayControls)
		{
			return downIntern();
		}
		return (downIntern() > 0.3f) ? 1 : 0;
	}

	public float downIntern()
	{
		if (Input.GetKey(KeyCode.DownArrow))
		{
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
			{
				return 0.7f;
			}
			return 1f;
		}
		return Input.GetAxisRaw("Vertical");
	}

	public float XInvert()
	{
		return GetInt("CameraXInverted");
	}

	public float YInvert()
	{
		return GetInt("CameraYInverted");
	}

	private void LateUpdate()
	{
		if (saveCooldown > 0 && --saveCooldown == 0)
		{
			SavingRoutine();
		}
		updateMenuDirections();
	}

	private void updateMenuDirections()
	{
		menuUp = (menuDown = (menuLeft = (menuRight = false)));
		if (left() < 0.6f && right() < 0.6f && up() < 0.6f && down() < 0.6f)
		{
			stickflag = false;
		}
		else if (!stickflag)
		{
			if (right() >= 0.6f)
			{
				menuRight = true;
			}
			if (left() >= 0.6f)
			{
				menuLeft = true;
			}
			if (up() >= 0.6f)
			{
				menuUp = true;
			}
			if (down() >= 0.6f)
			{
				menuDown = true;
			}
			stickflag = true;
		}
	}

	public bool anyInput()
	{
		if (!(left() > 0f) && !(right() > 0f) && !(up() > 0f) && !(down() > 0f) && !jumpButton() && !runButton())
		{
			return startButton();
		}
		return true;
	}

	public bool anyButton()
	{
		if (!jumpButton() && !runButton())
		{
			return startButton();
		}
		return true;
	}

	public bool jumpButton()
	{
		return Input.GetButtonDown("Jump");
	}

	public bool jumpButtonPressed()
	{
		return Input.GetButton("Jump");
	}

	public bool runButton()
	{
		return Input.GetButtonDown("Fire1");
	}

	public bool runButtonPressed()
	{
		if (Input.GetKey(KeyCode.R))
		{
			return true;
		}
		return Input.GetButton("Fire3");
	}

	public bool sprintButtonPressed()
	{
		if (Input.GetAxis("RightTrigger") > 0.35f)
		{
			return true;
		}
		return false;
	}

	public bool focusButtonPressed()
	{
		if (Input.GetAxis("LeftTrigger") > 0.35f)
		{
			if (!focus)
			{
				focus = true;
				return true;
			}
			return false;
		}
		if (Input.GetMouseButtonDown(1))
		{
			focus = true;
			return true;
		}
		focus = false;
		return false;
	}

	public bool startButton()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			return true;
		}
		return Input.GetButtonDown("Start");
	}

	public bool cancelButton()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			return true;
		}
		return Input.GetButtonDown("Cancel");
	}
}
