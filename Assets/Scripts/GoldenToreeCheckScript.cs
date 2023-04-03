using UnityEngine;

public class GoldenToreeCheckScript : MonoBehaviour
{
	public Renderer feathers1;

	public Renderer feathers2;

	public Renderer feathers3;

	public Renderer feathers4;

	public Material golden_Material;

	private void Start()
	{
		CheckIfWeGolden();
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
	}
}
