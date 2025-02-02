using UnityEngine;
using TMPro;
using System.Collections.Generic; // ✅ Required for Lists
using System.Collections;


public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // ✅ Assign Dialogue Panel in Inspector
    public TextMeshProUGUI dialogueText; // ✅ Assign TMP Text in Inspector
    public TextMeshProUGUI nameText; // ✅ Assign TMP Text for character name
    public Animator characterAnimator; // ✅ Assign Animator (for standing up)

    public GameObject[] guards;
    private int currentLine = -1; // ✅ Start before first line to show correctly
    private bool isDialogueActive = false;

    [System.Serializable]
    public struct DialogueLine
    {
        public string speaker;
        public string dialogue;
    }

    public List<DialogueLine> dialogueLines = new List<DialogueLine>
    {
        new DialogueLine { speaker = "King Blabla: ", dialogue = "I heard you killed many of my man" },
        new DialogueLine { speaker = "King Blabla: ", dialogue = "What makes you think you can kill me?" },
        new DialogueLine { speaker = "You: ", dialogue = "You destroyed my whole family. I want you dead." },
        new DialogueLine { speaker = "King Blabla: ", dialogue = "HAHAHAHHA  Try me." },
    };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🎮 'E' Pressed! Checking dialogue state...");
        }

        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🟢 Dialogue is active. Calling ShowNextLine()");
            ShowNextLine();
        }
        else if (!isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("🔴 Dialogue is NOT active. Ignoring input.");
        }
    }
    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }


    public void StartDialogue()
    {
        if (dialogueLines.Count == 0) return; // ✅ Prevents errors

        Debug.Log("✅ Dialogue started!");

        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        currentLine = -1; // ✅ Start before first line

        RefreshDialogue(); // ✅ Force update

        Debug.Log("🔄 Forced Dialogue Refresh - NEW LINES SHOULD NOW SHOW");
    }


    public void RefreshDialogue()
    {
        Debug.Log("🔄 Refreshing Dialogue Data...");

        // ✅ Force clear the dialogue list (IMPORTANT)
        dialogueLines = new List<DialogueLine>
    {
        new DialogueLine { speaker = "King Blabla: ", dialogue = "I heard you killed many of my men" },
        new DialogueLine { speaker = "King Blabla: ", dialogue = "What makes you think you can kill me?" },
        new DialogueLine { speaker = "You: ", dialogue = "You destroyed my whole family. I want you dead." },
        new DialogueLine { speaker = "King Blabla: ", dialogue = "HAHAHAHA Try me." },
    };

        Debug.Log("✅ DialogueLines list has been RESET with new data!");

        currentLine = -1; // ✅ Reset dialogue index
        ShowNextLine(); // ✅ Force UI update

        Debug.Log("✅ Dialogue has been refreshed.");
    }

    private void ShowNextLine()
    {
        Debug.Log($"🔹 ShowNextLine() called. Current Line BEFORE increment: {currentLine}");

        currentLine++; // ✅ Move to the next line

        if (currentLine < dialogueLines.Count) // ✅ If more lines exist, display them
        {
            Debug.Log($"📝 Showing Line {currentLine}: {dialogueLines[currentLine].dialogue}");

            nameText.text = dialogueLines[currentLine].speaker; // ✅ Update UI with speaker name
            dialogueText.text = dialogueLines[currentLine].dialogue; // ✅ Update UI with dialogue

            // ✅ Change text color based on speaker (Optional)
            if (dialogueLines[currentLine].speaker == "Player")
            {
                nameText.color = Color.green; // ✅ Make Player text green
            }
            else
            {
                nameText.color = Color.white; // ✅ Keep NPC text white
            }

            // ✅ Force Unity to refresh UI
            dialogueText.ForceMeshUpdate();
            nameText.ForceMeshUpdate();
        }
        else
        {
            Debug.Log("✅ All lines finished. Calling EndDialogue() now...");
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        Debug.Log("✅ Dialogue ended. All guards will start attacking!");

        // ✅ Loop through all guards and activate them
        foreach (GameObject guard in guards)
        {
            if (guard != null)
            {
                GuardAI guardAI = guard.GetComponent<GuardAI>();
                if (guardAI != null)
                {
                    guardAI.StartAttacking(); // ✅ Make the guard attack after dialogue ends
                }
                else
                {
                    Debug.LogError($"❌ ERROR: Guard AI script not found on {guard.name}!");
                }
            }
            else
            {
                Debug.LogError("❌ ERROR: One of the Guard GameObjects is NULL! Assign all guards in Inspector.");
            }
        }
    }


    private IEnumerator AdjustPositionAfterAnimation()
    {
        yield return new WaitForSeconds(1.5f); // ✅ Adjust based on animation length

        Vector3 correctPosition = characterAnimator.transform.position;
        correctPosition.y = 127; // ✅ Move character back to ground level
        characterAnimator.transform.position = correctPosition;

        Debug.Log("✅ Character repositioned to the ground.");
    }



}
