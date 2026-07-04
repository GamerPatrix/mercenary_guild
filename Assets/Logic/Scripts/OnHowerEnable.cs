using UnityEngine;
using UnityEngine.EventSystems;

namespace mercenary_guild
{
    public class OnHoverEnable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject targetObject;

        void Start()
        {
            if (targetObject != null)
            {
                targetObject.SetActive(false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetTargetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetTargetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetTargetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.fullyExited && RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
            {
                return;
            }
            SetTargetActive(false);
        }

        private void SetTargetActive(bool state)
        {
            if (targetObject != null)
            {
                targetObject.SetActive(state);
            }
        }
    }
}