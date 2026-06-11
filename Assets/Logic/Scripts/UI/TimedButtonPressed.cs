using UnityEngine;
using UnityEngine.UI;
using System;

namespace mercenary_guild
{
    /// <summary>
    /// Moves one UI Image towards another UI Image at a constant linear speed.
    /// Evaluates player click timing based on distance to target.
    /// Continues past the target in its last known direction upon impact.
    /// </summary>
    public class TimedButtonPressed : MonoBehaviour
    {
        [Header("Images")]
        /// <summary>The image that will move</summary>
        [SerializeField] private Image movingImage;

        /// <summary>The target image to move towards</summary>
        [SerializeField] private Image targetImage;

        [Header("Movement Settings")]
        /// <summary>Speed of movement in pixels per second</summary>
        [SerializeField] private float moveSpeed = 100f;

        [Header("Timing Thresholds")]
        [Tooltip("Distance threshold for a perfect hit. Also acts as the late-hit window after passing target.")]
        [SerializeField] private float perfectTolerance = 15f;

        [Tooltip("Distance threshold for a slightly early hit (beyond the perfect window).")]
        [SerializeField] private float earlyTolerance = 40f;

        // --- Events ---
        public event Action OnPerfectSuccess;
        public event Action OnLittleEarly;
        public event Action OnFail;

        // Properties
        public Vector2 StartPosition { get; private set; }
        public bool IsMoving { get; private set; } = false;

        // Internal state tracking
        private Vector2 movementDirection;
        private bool hasReachedTarget = false;
        private bool isPausedAtTarget = false;

        void Start()
        {
            if (movingImage != null)
            {
                StartPosition = movingImage.rectTransform.anchoredPosition;
            }
            StartMoving();
        }

        void Update()
        {
            if (!IsMoving || movingImage == null || targetImage == null) return;

            Vector2 currentPosition = movingImage.rectTransform.anchoredPosition;
            Vector2 targetPosition = targetImage.rectTransform.anchoredPosition;

            if (isPausedAtTarget)
            {
                isPausedAtTarget = false;
                hasReachedTarget = true;

                // Continue moving in the dynamically updated direction
                movingImage.rectTransform.anchoredPosition += movementDirection * moveSpeed * Time.deltaTime;
                return;
            }

            if (!hasReachedTarget)
            {
                Vector2 directionToTarget = (targetPosition - currentPosition).normalized;

                // FIX: Update the direction continuously so we know exactly 
                // which way it was facing right before hitting the target.
                if (directionToTarget != Vector2.zero)
                {
                    movementDirection = directionToTarget;
                }

                float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

                if (distanceToTarget <= moveSpeed * Time.deltaTime)
                {
                    movingImage.rectTransform.anchoredPosition = targetPosition;
                    isPausedAtTarget = true;
                }
                else
                {
                    movingImage.rectTransform.anchoredPosition += directionToTarget * moveSpeed * Time.deltaTime;
                }
            }
            else
            {
                // Continue in the direction it was last moving when it hit the target
                movingImage.rectTransform.anchoredPosition += movementDirection * moveSpeed * Time.deltaTime;
            }
        }

        /// <summary>
        /// Call this method via your UI Button component when the player presses the input button.
        /// </summary>
        public void OnButtonPressed()
        {
            if (!IsMoving || movingImage == null || targetImage == null)
            {
                OnFail?.Invoke();
                return;
            }

            Vector2 currentPosition = movingImage.rectTransform.anchoredPosition;
            Vector2 targetPosition = targetImage.rectTransform.anchoredPosition;
            float distance = Vector2.Distance(currentPosition, targetPosition);

            if (!hasReachedTarget && !isPausedAtTarget)
            {
                if (distance <= perfectTolerance)
                {
                    OnPerfectSuccess?.Invoke();
                }
                else if (distance <= earlyTolerance)
                {
                    OnLittleEarly?.Invoke();
                }
                else
                {
                    OnFail?.Invoke();
                }
            }
            else
            {
                if (isPausedAtTarget || distance <= perfectTolerance)
                {
                    OnPerfectSuccess?.Invoke();
                }
                else
                {
                    OnFail?.Invoke();
                }
            }
        }

        public void StartMoving()
        {
            IsMoving = true;
            hasReachedTarget = false;
            isPausedAtTarget = false;
            movementDirection = Vector2.zero;
        }

        public void ResetPosition()
        {
            IsMoving = false;
            hasReachedTarget = false;
            isPausedAtTarget = false;

            if (movingImage != null)
            {
                movingImage.rectTransform.anchoredPosition = StartPosition;
            }
        }
    }
}