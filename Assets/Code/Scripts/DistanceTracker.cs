using UnityEngine;
using TMPro;

public class DistanceTracker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private BackgroundScroller backgroundScroller;

    private float _distanceTraveled = 0f;
    private int _metersTraveled = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundScroller == null)
        {
            backgroundScroller = FindAnyObjectByType<BackgroundScroller>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (backgroundScroller == null) return;

        _distanceTraveled += backgroundScroller.ScrollSpeed * Time.deltaTime;
        _metersTraveled = Mathf.FloorToInt(_distanceTraveled); // Convert to meters (assuming 1 unit = 1 meter)

        // Update the UI text
        if (distanceText != null)
        {
            distanceText.text = $"{_metersTraveled} m";
        }
    }
}
