using UnityEngine;

public class animationStateController : MonoBehaviour
{
    bool gameOver = false;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameOver != SC_GroundGenerator.instance.gameOver)
        {
            animator.SetBool("isDying", true);
            gameOver = SC_GroundGenerator.instance.gameOver;
        }
        else
        {
            animator.SetBool("isDying", false);
        }

        if (!gameOver)
        {
            if (Input.GetKey("w"))
            {
                animator.SetBool("isJumping", true);
            }
            else
            {
                animator.SetBool("isJumping", false);
            }

            if (Input.GetKey("s"))
            {
                animator.SetBool("isRolling", true);
            }
            else
            {
                animator.SetBool("isRolling", false);
            }
        }

    }
}
