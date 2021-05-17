using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity = 20.0f;
    public float jumpHeight = 2.5f;
    public float crouchSpeed = 7.0f;
    public float strafeDistance = 1.5f;

    Rigidbody rigidBody;
    bool grounded = false;
    Vector3 originalScale;
    float jumpVerticalSpeed;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        rigidBody.useGravity = false;
        originalScale = transform.localScale;
        jumpVerticalSpeed = CalculateJumpVerticalSpeed();
    }

    void Update()
    {
        //TODO: Replace with model + animation

        // Jump
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            
            rigidBody.velocity = new Vector3(0, jumpVerticalSpeed, 0);
        }

        //Crouch
        if(Input.GetKey(KeyCode.S))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(originalScale.x, originalScale.y * 0.4f, originalScale.z), Time.deltaTime * crouchSpeed);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * crouchSpeed);
        }

        //Go left and right
        if(Input.GetKeyDown(KeyCode.A) && transform.position.z < 0.9f * strafeDistance)
        {
            transform.position += new Vector3(0, 0, strafeDistance);
        }

        if(Input.GetKeyDown(KeyCode.D) && transform.position.z > 0.9f * -strafeDistance)
        {
            transform.position += new Vector3(0, 0, -strafeDistance);
        }
    }

    void FixedUpdate()
    {
        // We apply gravity manually for more tuning control
        rigidBody.AddForce(new Vector3(0, -gravity * rigidBody.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            SC_GroundGenerator.instance.gameOver = true;
        }
    }
}
