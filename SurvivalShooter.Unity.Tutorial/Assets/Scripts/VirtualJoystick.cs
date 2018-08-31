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

    /// <summary>
    /// value from -1 to 1
    /// </summary>
    public Vector3 value;

    private void Start()
    {
        imageBackground = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = touchPosition.x / imageBackground.rectTransform.sizeDelta.x;
            touchPosition.y = touchPosition.y / imageBackground.rectTransform.sizeDelta.y;
            value = anchorPosition == AnchorPosition.LeftDown? new Vector3(touchPosition.x * 2 - 1, 0, touchPosition.y * 2 - 1) : new Vector3(touchPosition.x * 2 + 1, 0, touchPosition.y * 2 - 1);
            value = value.sqrMagnitude > 1 ? value.normalized : value;
            
            imageJoystick.rectTransform.anchoredPosition = new Vector2(value.x * (imageBackground.rectTransform.sizeDelta.x / 2), value.z * (imageBackground.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(JoystickReturnToCenter());
        value = Vector3.zero;
    }

    IEnumerator JoystickReturnToCenter()
    {
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
