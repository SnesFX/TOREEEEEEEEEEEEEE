using UnityEngine;

public class TextureScroll : MonoBehaviour
{
	public float scrollSpeedX = 0.5f;

	public float scrollSpeedY = 0.5f;

	public Renderer rend;

	private void Start()
	{
		rend = GetComponent<Renderer>();
	}

	private void Update()
	{
		float x = Time.time * scrollSpeedX;
		float y = Time.time * scrollSpeedY;
		rend.material.SetTextureOffset("_MainTex", new Vector2(x, y));
	}
}
