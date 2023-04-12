using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject textbox;

    public Text nameText;
    public Text dialogueText;

    public float textSpeed = 0.05f; // Speed at which characters appear on screen

    private Queue<Dialogue> dialogueQueue;
    private bool isTyping = false; // Whether the dialogue is currently being typed out
    private string currentSentence = ""; // The current sentence being typed out
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

        textbox.SetActive(true);

        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (isTyping)
        {
            // If dialogue is still being typed out, finish typing it out before displaying next dialogue
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue dialogue = dialogueQueue.Dequeue();
        nameText.text = dialogue.speakerName;
        currentSentence = dialogue.text;
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        textbox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextDialogue();
        }
    }
}

[System.Serializable]
public class Dialogue
{
    public string speakerName;
    public string text;
}
