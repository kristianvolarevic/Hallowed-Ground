using UnityEngine;

public class Obstacle : ScrollableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            collision.GetComponent<CharacterController>().Die();
        }
    }
}
