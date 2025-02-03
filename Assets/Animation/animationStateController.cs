using UnityEngine;

public class animationStateController : MonoBehaviour
{
    private Animator animator;
    private int isWalkingHash;
    private int isRunningHash;

    void Start()
    {
        animator = GetComponent<Animator>(); // Correctly assigning the Animator
        isWalkingHash = Animator.StringToHash("isWalking"); // Store hash for efficiency
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // If player presses W key
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (isWalking && !forwardPressed) // If player stops pressing W key
        {
            animator.SetBool(isWalkingHash, false);
        }

        // If player is walking and presses Left Shift -> Start running
        if (!isRunning && forwardPressed && runPressed)
        {
            animator.SetBool(isRunningHash, true);
        }
        // If player stops running or stops walking
        else if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
