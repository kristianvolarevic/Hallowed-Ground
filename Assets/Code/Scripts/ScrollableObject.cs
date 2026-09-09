using UnityEngine;

public class ScrollableObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BackgroundScroller _backgroundScroller;

    [Header("Settings")]
    [SerializeField] private float _destroyXPosition = -20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckIfOutOfBounds();
    }

    private void Move()
    {
        float speed = _backgroundScroller != null ? _backgroundScroller.ScrollSpeed : 0f;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void CheckIfOutOfBounds()
    {
        if (transform.position.x < _destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}
