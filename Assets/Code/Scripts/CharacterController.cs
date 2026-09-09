using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 5f, _jumpSpeed = 1f;

    [Header("References")]
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidbody;
    private bool _isJumpRequested = false;
    private bool _canDoubleJump = false;
    private bool _isGrounded;
    private bool _isDead = false;

    public static event Action<bool> OnJumpStateChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void FixedUpdate()
    {
        // Update the animator with the current vertical velocity
        _animator.SetFloat("yVelocity", _rigidbody.linearVelocityY);

        if (_isJumpRequested && _isGrounded)
        {
            // Execute the jump
            ExecuteJump();
            _isGrounded = false;
            _canDoubleJump = true;
        }
        else if (_isJumpRequested && _canDoubleJump)
        {
            // Execute the double jump
            ExecuteJump();
            _canDoubleJump = false;
        }

        // Update the animator with the jump state
        _animator.SetBool("isJumping", !_isGrounded);


    }

    private void ExecuteJump()
    {
        _rigidbody.linearVelocityY = _jumpForce;
        _isJumpRequested = false;

        OnJumpStateChanged?.Invoke(true);
    }

    public void Jump()
    {
        if (_isGrounded || _canDoubleJump)
        {
            _isJumpRequested = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the character has landed on the ground
        _isGrounded = true;
        _animator.SetBool("isJumping", !_isGrounded);
        _canDoubleJump = false;
        OnJumpStateChanged?.Invoke(false);
    }

    public void Die()
    {
        if (_isDead) return; // Prevent multiple death triggers
        _isDead = true;

        Debug.Log("Character has died.");
        gameObject.SetActive(false);
        Invoke(nameof(RestartLevel), 1f); // Respawn after 1 second
    }

    private void RestartLevel()
    {
        // Reload the current scene to restart the level
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
