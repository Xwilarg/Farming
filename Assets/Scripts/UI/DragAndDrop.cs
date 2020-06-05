using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler
{

	private Vector2 pointerOffset;
	private RectTransform canvasRectTransform;
	private RectTransform panelRectTransform;

	void Start()
	{
		Canvas canvas = GetComponentInParent<Canvas>();
		canvasRectTransform = (RectTransform)canvas.transform;
		panelRectTransform = (RectTransform)transform;
	}

	public void OnPointerDown(PointerEventData data)
	{
		panelRectTransform.SetAsLastSibling();
		RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out pointerOffset);
	}

	public void OnDrag(PointerEventData data)
	{
		if (panelRectTransform == null)
			return;

		Vector2 pointerPosition = data.position;
		Vector2 localPointerPosition;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPosition, data.pressEventCamera, out localPointerPosition))
		{
			panelRectTransform.localPosition = localPointerPosition - pointerOffset;
		}
	}
}