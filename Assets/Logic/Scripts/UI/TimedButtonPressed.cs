using UnityEngine;
using UnityEngine.UI;

namespace Logic.Scripts
{
    /// <summary>
    /// Moves one UI Image towards another UI Image at a constant linear speed.
    /// When reaching the target, pauses for 1 frame then continues past it in the same direction.
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

        /// <summary>The starting position of the moving image</summary>
        public Vector2 StartPosition { get; private set; }
        
        /// <summary>Whether the image is currently moving</summary>
        public bool IsMoving { get; private set; } = false;

        // Internal state tracking
        private Vector2 movementDirection;
        private bool hasReachedTarget = false;
        private bool isPausedAtTarget = false;

        void Start()
        {
            // Store starting position if moving image is assigned
            if (movingImage != null)
            {
                StartPosition = movingImage.rectTransform.anchoredPosition;
            }
        }

        void Update()
        {
            if (!IsMoving || movingImage == null || targetImage == null) return;

            // Get current and target positions
            Vector2 currentPosition = movingImage.rectTransform.anchoredPosition;
            Vector2 targetPosition = targetImage.rectTransform.anchoredPosition;

            // If we're currently paused at the target, resume movement after 1 frame
            if (isPausedAtTarget)
            {
                isPausedAtTarget = false;
                hasReachedTarget = true;
                
                // Continue in the original direction past the target
                movingImage.rectTransform.anchoredPosition += movementDirection * moveSpeed * Time.deltaTime;
                return;
            }

            if (!hasReachedTarget)
            {
                // Calculate direction to target
                Vector2 directionToTarget = (targetPosition - currentPosition).normalized;
                
                // Store the initial movement direction on first frame
                if (movementDirection == Vector2.zero)
                {
                    movementDirection = directionToTarget;
                }
                
                // Check if we're close enough to snap to target
                float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);
                
                if (distanceToTarget <= moveSpeed * Time.deltaTime)
                {
                    // Snap to exact position and pause for 1 frame
                    movingImage.rectTransform.anchoredPosition = targetPosition;
                    isPausedAtTarget = true;
                }
                else
                {
                    // Move at constant speed towards target
                    movingImage.rectTransform.anchoredPosition += directionToTarget * moveSpeed * Time.deltaTime;
                }
            }
            else
            {
                // Continue in the original direction past the target
                movingImage.rectTransform.anchoredPosition += movementDirection * moveSpeed * Time.deltaTime;
            }
        }

        /// <summary>
        /// Starts the movement animation from current position to target
        /// </summary>
        public void StartMoving()
        {
            IsMoving = true;
            hasReachedTarget = false;
            isPausedAtTarget = false;
            movementDirection = Vector2.zero;
        }

        /// <summary>
        /// Stops the movement and resets to starting position
        /// </summary>
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
