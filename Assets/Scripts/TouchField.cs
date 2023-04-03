using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	[HideInInspector]
	public Vector2 TouchDist;

	[HideInInspector]
	protected Vector2 PointerOld;

	[HideInInspector]
	protected int PointerId;

	public bool Pressed;

	public bool UseFixedUpdate;

	private void Update()
	{
		if (!UseFixedUpdate)
		{
			DoUpdate();
		}
	}

	private void FixedUpdate()
	{
		if (UseFixedUpdate)
		{
			DoUpdate();
		}
	}

	private void DoUpdate()
	{
		if (Pressed)
		{
			Debug.Log("yup");
			if (PointerId >= 0 && PointerId < Input.touches.Length)
			{
				TouchDist = Input.touches[PointerId].position - PointerOld;
				PointerOld = Input.touches[PointerId].position;
			}
			else
			{
				TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
				PointerOld = Input.mousePosition;
			}
		}
		else
		{
			TouchDist = default(Vector2);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("yes");
		Pressed = true;
		PointerId = eventData.pointerId;
		PointerOld = eventData.position;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Pressed = false;
	}
}
