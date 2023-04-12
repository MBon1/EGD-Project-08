using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public float textSpeed = 0.05f; // Speed at which characters appear on screen

    private Queue<Dialogue> dialogueQueue;

    private Coroutine typingCoroutine;

    void Awake()
    {
        dialogueQueue = new Queue<Dialogue>();
    }

    public void StartDialogue(List<Dialogue> dialogueList)
    {
        dialogueQueue.Clear();

        foreach (Dialogue dialogue in dialogueList)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue dialogue = dialogueQueue.Dequeue();
        nameText.text = dialogue.speakerName;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(dialogue.text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    public string text;
}
