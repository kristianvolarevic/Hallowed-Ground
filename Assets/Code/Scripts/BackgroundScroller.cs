using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Transform _bg1, _bg2;
    [SerializeField] private float _scrollSpeed = 5f, _overlapPadding = 0.05f;

    private Camera _cam;
    private float _width;

    public float ScrollSpeed => _scrollSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cam = Camera.main;

        if (_bg1.TryGetComponent<SpriteRenderer>(out var sr))
        {
            _width = sr.bounds.size.x;
        }
        else
        {
            Debug.LogError("Background 1 does not have a SpriteRenderer component.");
        }

        Vector3 bg2Position = _bg1.position;
        bg2Position.x += _width - _overlapPadding;
        _bg2.position = bg2Position;
    }



    // Update is called once per frame
    void Update()
    {
        // Move both tiles left
        float distance = _scrollSpeed * Time.deltaTime;
        _bg1.position += Vector3.left * distance;
        _bg2.position += Vector3.left * distance;

        // Calculate the left edge of the camera's view
        float cameraLeftEdge = _cam.transform.position.x - (_cam.orthographicSize * _cam.aspect);

        // Check if bg1 has moved completely off-screen to the left
        if (_bg1.position.x + (_width / 2f) < cameraLeftEdge)
        {
            RepositionTile(_bg1, _bg2);
        }

        // Check if bg2 has moved completely off-screen to the left
        if (_bg2.position.x + (_width / 2f) < cameraLeftEdge)
        {
            RepositionTile(_bg2, _bg1);
        }
    }

    private void RepositionTile(Transform tileToReposition, Transform referenceTile)
    {
        Vector3 newPosition = tileToReposition.position;
        newPosition.x = referenceTile.position.x + _width - _overlapPadding;
        tileToReposition.position = newPosition;
    }
}
