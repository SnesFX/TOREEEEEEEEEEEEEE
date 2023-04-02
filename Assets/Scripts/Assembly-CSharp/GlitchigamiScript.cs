using UnityEngine;

public class GlitchigamiScript : MonoBehaviour
{
	public GameObject Crane;

	public GameObject GlitchWorld;

	public GameObject NormalWorld;

	public ScaleManagerScript _streckiGamie;

	public MusicManager myMusicManager;

	private float cooldown;

	private void Start()
	{
		NormalWorld.SetActive(value: false);
		Crane.SetActive(value: false);
		myMusicManager.StopMusic();
	}

	private void Update()
	{
		base.transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
		cooldown += 1f * Time.deltaTime;
		if (cooldown > 2f)
		{
			float y = Random.Range(10f, 24f);
			float x = Random.Range(10f, 14f);
			float z = Random.Range(10f, 14f);
			_streckiGamie.newScale = new Vector3(x, y, z);
			cooldown = 0f;
		}
		if (base.transform.position.x > -507f)
		{
			NormalWorld.SetActive(value: true);
			Crane.SetActive(value: true);
			GlitchWorld.SetActive(value: false);
		}
	}
}
