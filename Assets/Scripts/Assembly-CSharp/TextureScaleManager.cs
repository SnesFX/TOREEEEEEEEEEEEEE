using UnityEngine;

public class TextureScaleManager : MonoBehaviour
{
	public Renderer zaunRend1;

	public Renderer zaunRend2;

	private void Start()
	{
		zaunRend1.material.SetTextureScale("_MainTex", new Vector2(base.transform.localScale.x * 2f, base.transform.localScale.z));
		zaunRend2.material.SetTextureScale("_MainTex", new Vector2(base.transform.localScale.x * 2f, base.transform.localScale.z));
	}
}
