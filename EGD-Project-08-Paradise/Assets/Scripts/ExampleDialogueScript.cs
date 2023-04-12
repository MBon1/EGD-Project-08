using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogueScript : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        dialogueList.Add(new Dialogue { speakerName = "NPC", text = "Hello there!" });
        dialogueList.Add(new Dialogue { speakerName = "Player", text = "Hi!" });

        dialogueManager.StartDialogue(dialogueList);
    }
}
