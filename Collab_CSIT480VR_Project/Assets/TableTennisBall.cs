using UnityEngine;

public class PingPongBall : MonoBehaviour
{
    // Rigidbody component of the ball
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the ball
        rb = GetComponent<Rigidbody>();

        // Set mass and drag to simulate a ping pong ball
        rb.mass = 0.5f; // Mass of a standard ping pong ball in kilograms
        rb.drag = 0.41f; // Air resistance
        rb.angularDrag = 0.1f; // Angular air resistance

        // Set the ball's bounciness to simulate the elasticity of a ping pong ball
        GetComponent<Collider>().material.bounciness = 1f;

        // Serve the ball when the game starts
        ServeBall();
    }

    // Method to serve the ball
    public void ServeBall()
    {
        // Reset the ball's position
        transform.position = Vector3.zero;

        // Reset the ball's velocity
        rb.velocity = Vector3.zero;

        // Apply a force to serve the ball
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * 2f, ForceMode.Impulse);
    }

    // OnCollisionEnter is called when the ball collides with another collider
    private void OnCollisionEnter(Collision collision)
    {
        // Play a sound or apply any other effects here when the ball collides with something
        // For example, you could play a bounce sound or spawn particle effects

        // If you want the ball to bounce off other colliders, you can add a bounce force
        // Calculate the reflection direction
        Vector3 reflectionDirection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);

        // Apply the reflection direction as the new velocity
        rb.velocity = reflectionDirection * rb.velocity.magnitude;
    }
}
