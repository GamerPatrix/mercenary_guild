using UnityEngine;
using mercenary_guild.input; 

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Movement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float roadSpeedMultiplier = 1.5f;
    [SerializeField] private LayerMask roadLayer;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private float currentSpeedMultiplier = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Subscribe to the centralized GameInput movement event
        if (GameInput.Instance != null)
        {
            GameInput.Instance.MoveChanged += OnMoveChanged;
        }
        else
        {
            Debug.LogError("GameInput Instance missing from the scene!");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe when the player is destroyed
        if (GameInput.Instance != null)
        {
            GameInput.Instance.MoveChanged -= OnMoveChanged;
        }
    }

    // Handles updating local move vector when GameInput fires event
    private void OnMoveChanged(Vector2 newMoveVector)
    {
        moveInput = newMoveVector;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed * currentSpeedMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & roadLayer) != 0)
        {
            currentSpeedMultiplier = roadSpeedMultiplier;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & roadLayer) != 0)
        {
            currentSpeedMultiplier = 1f;
        }
    }
}