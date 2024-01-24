using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;  // Unity's physics component
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float respawnHeight = -10f;
    private Vector2 direction = Vector2.zero;
    private bool isGrounded = false;
    private Vector3 originalScale; // Store the original scale of the player

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale; // Store the original scale
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < respawnHeight)
            Respawn();

        Move(direction.x, direction.y);
    }

    void OnJump()
    {
        if (isGrounded)
            Jump();
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
    }

    void OnMove(InputValue moveVal)
    {
        direction = moveVal.Get<Vector2>();
    }

    private void Move(float x, float z)
    {
        rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Collision logic (if needed)
    }

    void OnCollisionStay(Collision collision)
    {
        if (Vector3.Angle(collision.GetContact(0).normal, Vector3.up) < 45f)
            isGrounded = true;
        else
            isGrounded = false;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to flatten the player
    void OnFlatten()
    {
        Debug.Log("Flatten Pressed");
        transform.localScale = new Vector3(originalScale.x * 2, originalScale.y * 0.5f, originalScale.z * 2);
    }

    // Method to revert the player back to original scale
    void OnReleaseFlatten()
    {
        Debug.Log("Flatten Released");
        transform.localScale = originalScale;
    }
}
