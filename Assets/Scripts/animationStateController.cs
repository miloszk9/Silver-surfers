using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKey("w")){
            animator.SetBool("isJumping", true);
        }else{
            animator.SetBool("isJumping", false);
        }

        if(Input.GetKey("s")){
            animator.SetBool("isRolling", true);
        }else{
            animator.SetBool("isRolling", false);
        }
    }
}
