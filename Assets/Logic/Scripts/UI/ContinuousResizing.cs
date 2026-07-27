using UnityEngine;

public class ContinuousResizing : MonoBehaviour
{
    [Header("Pulse Settings")]
    [Tooltip("How fast the object pulses.")]
    [SerializeField] private float pulseSpeed = 2f;

    [Tooltip("How much larger/smaller the object gets from its base size.")]
    [SerializeField] private float pulseAmount = 0.2f;

    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
    }

    private void Update()
    {
        float sineWave = Mathf.Sin(Time.time * pulseSpeed);
        float scaleFactor = 1f + (sineWave * pulseAmount);
        transform.localScale = baseScale * scaleFactor;
    }
}