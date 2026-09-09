using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MobileInputController : MonoBehaviour
{
    [Header("Touch Settings")]
    [SerializeField] private float _swipeThreshold = 50f; // Minimum distance for a swipe to be recognized

    [Header("Events")]
    [SerializeField] private UnityEvent OnSwipeUp;

    private Vector2 _touchStartPosition;
    private Vector2 _touchEndPosition;
    private bool _isSwiping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check for touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (touch.press.wasPressedThisFrame)
            {
                _touchStartPosition = touch.position.ReadValue();
                _isSwiping = true;
            }
            else if (touch.press.wasReleasedThisFrame && _isSwiping)
            {
                _touchEndPosition = touch.position.ReadValue();
                HandleSwipe();
                _isSwiping = false;
            }
        }
        // Check for mouse input (for testing in the editor)
        else if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _touchStartPosition = Mouse.current.position.ReadValue();
                _isSwiping = true;
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame && _isSwiping)
            {
                _touchEndPosition = Mouse.current.position.ReadValue();
                HandleSwipe();
                _isSwiping = false;
            }
        }
    }

    private void HandleSwipe()
    {
        Vector2 swipeVector = _touchEndPosition - _touchStartPosition;

        // Check if the swipe distance exceeds the threshold
        if (swipeVector.magnitude >= _swipeThreshold)
        {
            Vector2 direction = swipeVector.normalized;

            if (Vector2.Dot(direction, Vector2.up) > 0.5f)
            {
                OnSwipeUp?.Invoke();
            }
        }
    }
}
