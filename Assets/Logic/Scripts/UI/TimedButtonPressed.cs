using UnityEngine;
using UnityEngine.UI;
using System;

namespace mercenary_guild
{
    public class TimedButtonPressed : MonoBehaviour
    {
        [Header("UI Hookups")]
        [SerializeField] private Button inputButton;
        [SerializeField] private Image movingImage;
        [SerializeField] private Image targetImage;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 100f;

        [Header("Timing Thresholds")]
        [SerializeField] private float perfectTolerance = 15f;
        [SerializeField] private float earlyTolerance = 40f;

        public event Action<int> OnClick;

        public Vector2 StartPosition { get; private set; }
        public bool IsMoving { get; private set; } = false;

        private Vector3 movementDirection;
        private bool hasReachedTarget = false;

        void Start()
        {
            if (movingImage != null)
            {
                StartPosition = movingImage.rectTransform.anchoredPosition;
            }

            if (inputButton != null)
            {
                inputButton.onClick.RemoveListener(OnButtonPressed);
                inputButton.onClick.AddListener(OnButtonPressed);
            }
            else
            {
                Debug.LogWarning($"Input Button is missing on {gameObject.name}!", this);
            }

            ResetPosition();
            StartMoving();
        }

        void OnDestroy()
        {
            if (inputButton != null)
            {
                inputButton.onClick.RemoveListener(OnButtonPressed);
            }
        }

        void Update()
        {
            if (!IsMoving || movingImage == null || targetImage == null) return;

            if (!hasReachedTarget)
            {
                Vector3 currentWorldPos = movingImage.transform.position;
                Vector3 targetWorldPos = targetImage.transform.position;

                Vector3 toTarget = targetWorldPos - currentWorldPos;
                float distanceToTarget = toTarget.magnitude;

                if (distanceToTarget > 0)
                {
                    movementDirection = toTarget.normalized;
                }

                float moveStep = moveSpeed * Time.deltaTime;
                if (distanceToTarget <= moveStep)
                {
                    movingImage.transform.position = targetWorldPos;
                    hasReachedTarget = true;
                }
                else
                {
                    movingImage.transform.Translate(movementDirection * moveStep, Space.World);
                }
            }
            else
            {
                movingImage.transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }

        public void OnButtonPressed()
        {
            if (!IsMoving || movingImage == null || targetImage == null)
            {
                OnClick?.Invoke(0);
                return;
            }

            Vector2 currentAnchoredPos = movingImage.rectTransform.anchoredPosition;

            Vector2 targetLocalPos;
            RectTransform utilityRect = movingImage.rectTransform.parent as RectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                utilityRect,
                RectTransformUtility.WorldToScreenPoint(null, targetImage.transform.position),
                null,
                out targetLocalPos
            );

            float distance = Vector2.Distance(currentAnchoredPos, targetLocalPos);

            if (distance <= perfectTolerance)
            {
                OnClick?.Invoke(1);
                Debug.Log("PERFECT! " + distance);
            }
            else if (!hasReachedTarget && distance <= earlyTolerance)
            {
                OnClick?.Invoke(2);
                Debug.Log("Early! " + distance);
            }
            else
            {
                OnClick?.Invoke(0);
                Debug.Log("Miss/Fail! " + distance);
            }
        }

        public void StartMoving()
        {
            IsMoving = true;
            hasReachedTarget = false;

            if (movingImage != null && targetImage != null)
            {
                movementDirection = (targetImage.transform.position - movingImage.transform.position).normalized;
            }
        }

        public void ResetPosition()
        {
            IsMoving = false;
            hasReachedTarget = false;

            if (movingImage != null)
            {
                movingImage.rectTransform.anchoredPosition = StartPosition;
            }
        }
    }
}