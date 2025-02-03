using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
            enabled = false; // Disable the script if no Animator is found
            return;
        }

        // Verify Animator parameters
        bool parameterExists = false;
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == "isWalking")
            {
                parameterExists = true;
                break;
            }
        }

        if (!parameterExists)
        {
            Debug.LogError("Animator parameter 'isWalking' not found!");
            enabled = false;
            return;
        }

        isWalkingHash = Animator.StringToHash("isWalking");
    }

    void Update()
    {
        if (animator == null) return;

        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }
}
