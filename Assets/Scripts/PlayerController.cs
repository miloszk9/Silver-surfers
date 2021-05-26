using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float gravity = 20.0f;
    public float jumpHeight = 2.5f;
    public float shrinkOnRoll = 0.4f;
    public float strafeDistance = 1.5f;
    public AnimationClip rollAnimation;

    Rigidbody rigidBody;
    bool grounded = false;
    Vector3 originalScale;
    float jumpVerticalSpeed;
    BoxCollider boxCollider;
    Vector3 colliderSize;
    Vector3 colliderCenter;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        rigidBody.freezeRotation = true;
        rigidBody.useGravity = false;
        originalScale = transform.localScale;
        colliderSize = boxCollider.size;
        colliderCenter = boxCollider.center;
        jumpVerticalSpeed = CalculateJumpVerticalSpeed();
    }

    void Update()
    {
        if (!SC_GroundGenerator.instance.gameOver)
        {
            // Jump
            if (Input.GetKeyDown(KeyCode.W) && grounded)
            {
                StopCoroutine(RollCoroutine());
                ResetCollider();

                rigidBody.velocity = new Vector3(0, jumpVerticalSpeed, 0);
            }

            //Roll
            if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(RollCoroutine());
            }

            //Go left and right
            if (Input.GetKeyDown(KeyCode.A) && transform.position.z < 0.9f * strafeDistance)
            {
                transform.position += new Vector3(0, 0, strafeDistance);
            }

            if (Input.GetKeyDown(KeyCode.D) && transform.position.z > 0.9f * -strafeDistance)
            {
                transform.position += new Vector3(0, 0, -strafeDistance);
            }
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
    IEnumerator RollCoroutine()
    {
        float scale = 0.4f;
        boxCollider.size = new Vector3(colliderSize.x, colliderSize.y * scale, colliderSize.z);
        boxCollider.center = new Vector3(colliderCenter.x, colliderCenter.y * scale, colliderCenter.z);
        yield return new WaitForSecondsRealtime(rollAnimation.length);
        ResetCollider();
    }

    void ResetCollider()
    {
        boxCollider.size = colliderSize;
        boxCollider.center = colliderCenter;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            SC_GroundGenerator.instance.gameOver = true;
        }
    }
}
