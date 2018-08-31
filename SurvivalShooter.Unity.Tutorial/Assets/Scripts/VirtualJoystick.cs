using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
    private enum AnchorPosition
    {
        LeftDown,
        RightDown
    }
    private Image imageBackground;
    [SerializeField] private Image imageJoystick;
    [SerializeField] private float joystickLerpTime;
    [SerializeField] private AnchorPosition anchorPosition;
    private Vector3 newPosition;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = touchPosition.x / imageBackground.rectTransform.sizeDelta.x;
            touchPosition.y = touchPosition.y / imageBackground.rectTransform.sizeDelta.y;
            newPosition = anchorPosition == AnchorPosition.LeftDown? new Vector3(touchPosition.x * 2 - 1, 0, touchPosition.y * 2 - 1) : new Vector3(touchPosition.x * 2 + 1, 0, touchPosition.y * 2 - 1);
            newPosition = newPosition.sqrMagnitude > 1 ? newPosition.normalized : newPosition;
            Debug.Log(newPosition);
            imageJoystick.rectTransform.anchoredPosition = new Vector2(newPosition.x * (imageBackground.rectTransform.sizeDelta.x / 2), newPosition.z * (imageBackground.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(JoystickReturnToCenter());
    }

    // Use this for initialization
    void Start () {
        imageBackground = GetComponent<Image>();
	}

    IEnumerator JoystickReturnToCenter()
    {
        Debug.Log("Nani?");
        float timer = 0;
        while(timer < joystickLerpTime)
        {
            timer += Time.deltaTime;
            imageJoystick.rectTransform.anchoredPosition = Vector2.Lerp(imageJoystick.rectTransform.anchoredPosition, Vector2.zero, timer / joystickLerpTime);
            yield return null;
        }

        imageJoystick.rectTransform.anchoredPosition = Vector2.zero;
    }
}
