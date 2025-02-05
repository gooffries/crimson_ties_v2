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
        new DialogueLine { speaker = "King Ergoth: ", dialogue = "Well, well…look " +
            "who finally decided to show up! Took you long enough!" },

        new DialogueLine { speaker = "Arya: ", dialogue = "You have taken everything " +
            "from me. My father. My peace. My life. And now, I have come to take my revenge!" },

        new DialogueLine { speaker = "King Ergoth: ", dialogue = "Your father? That pathetic " +
            "fool thought he could keep secrets from me. Did he really think I wouldn’t find " +
            "out about his little mistake?" },

        new DialogueLine { speaker = "Arya: ", dialogue = "You don’t deserve this throne. " +
            "You don’t deserve to sit there like a coward, surrounded by your soldiers!" },

        new DialogueLine { speaker = "King Ergoth: ", dialogue = "I am King Ergoth! " +
            "Everything in this kingdom bends to my will. Your father? Dead. Because I wanted it." },

        new DialogueLine { speaker = "King Ergoth: ", dialogue = "And now…there is only one last matter to attend to." },

        new DialogueLine { speaker = "King Ergoth: ", dialogue = "Kill her! And make it slow…" },
    };

    void Update()
    {
        // Ensure cursor is visible when dialogue is active
        if (isDialogueActive)
        {
            Cursor.visible = true; // Keep the cursor visible
        }

        // Dialogue progression with keyboard input (E) or button press (downward arrow)
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextLine(); // Advance dialogue when E is pressed
        }
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }

    public void StartDialogue()
    {
        if (dialogueLines.Count == 0) return; // Prevents errors

        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        currentLine = -1; // Start before first line
        ShowNextLine(); // Show first line immediately
    }

    // Button click handler for advancing dialogue
    public void OnNextDialogueButtonClick()
    {
        ShowNextLine(); // Advance dialogue when button is clicked
    }

    private void ShowNextLine()
    {
        currentLine++; // Move to the next line

        if (currentLine < dialogueLines.Count) // If more lines exist, display them
        {
            nameText.text = dialogueLines[currentLine].speaker;
            dialogueText.text = dialogueLines[currentLine].dialogue;

            // Change text color based on speaker (Optional)
            if (dialogueLines[currentLine].speaker == "Arya: ")
            {
                nameText.color = new Color32(215, 38, 56, 255); // Arya text
            }
            else
            {
                nameText.color = Color.white; // King Ergoth text
            }

            dialogueText.ForceMeshUpdate();
            nameText.ForceMeshUpdate();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;

        // Loop through all guards and activate them
        foreach (GameObject guard in guards)
        {
            if (guard != null)
            {
                GuardAI guardAI = guard.GetComponent<GuardAI>();
                if (guardAI != null)
                {
                    guardAI.StartAttacking(); // Make the guard attack after dialogue ends
                }
            }
        }
    }
}
