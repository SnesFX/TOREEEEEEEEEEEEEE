using UnityEngine;
using UnityEngine.UI;

public class LevelFrameScript : MonoBehaviour
{
	public int myLevelID;

	public string myName = "";

	public GameObject starEmblem;

	public Image rankingImage;

	public Sprite ARankSmol;

	public Sprite BRankSmol;

	public Sprite CRankSmol;

	public Sprite SRankSmol;

	private void Start()
	{
		RankingCheck();
		StarCheck();
	}

	private void RankingCheck()
	{
		int num = 0;
		if (myLevelID == 0 && GamePadScript.instance.HasKey("Level1Act1Rank"))
		{
			num = GamePadScript.instance.GetInt("Level1Act1Rank");
		}
		else if (myLevelID == 1 && GamePadScript.instance.HasKey("Level1Act2Rank"))
		{
			num = GamePadScript.instance.GetInt("Level1Act2Rank");
		}
		else if (myLevelID == 2 && GamePadScript.instance.HasKey("Level2Act1Rank"))
		{
			num = GamePadScript.instance.GetInt("Level2Act1Rank");
		}
		else if (myLevelID == 3 && GamePadScript.instance.HasKey("Level2Act2Rank"))
		{
			num = GamePadScript.instance.GetInt("Level2Act2Rank");
		}
		else if (myLevelID == 4 && GamePadScript.instance.HasKey("Level3Act1Rank"))
		{
			num = GamePadScript.instance.GetInt("Level3Act1Rank");
		}
		else if (myLevelID == 5 && GamePadScript.instance.HasKey("Level3Act2Rank"))
		{
			num = GamePadScript.instance.GetInt("Level3Act2Rank");
		}
		else if (myLevelID == 6 && GamePadScript.instance.HasKey("Level4Act1Rank"))
		{
			num = GamePadScript.instance.GetInt("Level4Act1Rank");
		}
		else if (myLevelID == 7 && GamePadScript.instance.HasKey("Level4Act2Rank"))
		{
			num = GamePadScript.instance.GetInt("Level4Act2Rank");
		}
		else if (myLevelID == 8 && GamePadScript.instance.HasKey("Level5Act1Rank"))
		{
			num = GamePadScript.instance.GetInt("Level5Act1Rank");
		}
		if (num > 0)
		{
			rankingImage.gameObject.SetActive(value: true);
		}
		if (num == 1)
		{
			rankingImage.sprite = CRankSmol;
		}
		if (num == 2)
		{
			rankingImage.sprite = BRankSmol;
		}
		if (num == 3)
		{
			rankingImage.sprite = ARankSmol;
		}
		if (num == 4)
		{
			rankingImage.sprite = SRankSmol;
		}
	}

	private void StarCheck()
	{
		int num = 0;
		if (myLevelID == 0 && GamePadScript.instance.HasKey("1_1AllStars"))
		{
			num = GamePadScript.instance.GetInt("1_1AllStars");
		}
		else if (myLevelID == 1 && GamePadScript.instance.HasKey("1_2AllStars"))
		{
			num = GamePadScript.instance.GetInt("1_2AllStars");
		}
		else if (myLevelID == 2 && GamePadScript.instance.HasKey("2_1AllStars"))
		{
			num = GamePadScript.instance.GetInt("2_1AllStars");
		}
		else if (myLevelID == 3 && GamePadScript.instance.HasKey("2_2AllStars"))
		{
			num = GamePadScript.instance.GetInt("2_2AllStars");
		}
		else if (myLevelID == 4 && GamePadScript.instance.HasKey("3_1AllStars"))
		{
			num = GamePadScript.instance.GetInt("3_1AllStars");
		}
		else if (myLevelID == 5 && GamePadScript.instance.HasKey("3_2AllStars"))
		{
			num = GamePadScript.instance.GetInt("3_2AllStars");
		}
		else if (myLevelID == 6 && GamePadScript.instance.HasKey("4_1AllStars"))
		{
			num = GamePadScript.instance.GetInt("4_1AllStars");
		}
		else if (myLevelID == 7 && GamePadScript.instance.HasKey("4_2AllStars"))
		{
			num = GamePadScript.instance.GetInt("4_2AllStars");
		}
		else if (myLevelID == 8 && GamePadScript.instance.HasKey("5_1AllStars"))
		{
			num = GamePadScript.instance.GetInt("5_1AllStars");
		}
		if (num == 1)
		{
			starEmblem.SetActive(value: true);
		}
	}

	private void Update()
	{
	}
}
