using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleDialogueScript : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public TileMapPainter[] mapPainters;

    SpriteRenderer[] people;

    void Start()
    {
        TestText();
        //GrassPlanted();
        //BuildingsAdded(0.8f);
    }

    void EnableTileMapPainters()
    {
        foreach (TileMapPainter t in mapPainters)
        {
            t.enablePainting = true;
        }
    }
    void DisableTileMapPainters()
    {
        foreach (TileMapPainter t in mapPainters)
        {
            t.enablePainting = false;
        }
    }


    public void TestText()
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "1" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You wake up to the warm touch of the sunlight and the sound of birds singing. Clear skies again." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Good morning, world! Another day in Paradise!" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "Eager, you eat breakfast and get cleaned up. Today is a very special day." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "2" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "When you arrive to the lab, the instructor stands in the front of the room." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "Today, we will begin an important project. Each of you will be simulating an island and will eventually populate it with people." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "For today, why don't you paint some plant life onto the island. Left-click to draw." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "I expect great things from you all." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "0" });

        dialogueManager.StartDialogue(dialogueList, new DialogueManager.StartDel(DisableTileMapPainters), new DialogueManager.EndDel(EnablePlantCanvas));
    }

    private void EnablePlantCanvas()
    {
        mapPainters[0].gameObject.SetActive(true);
        mapPainters[0].enablePainting = true;
        mapPainters[0].completionCondition = 0;
    }

    public void GrassPlanted()
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Aaaand... DONE!" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You plant the last of the vegetation and stretch. Done with plenty of time to spare. And so, you saved your work and went to bed." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "4" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "1" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You wake up the next morning once again to sunshine and clear, blue skies." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Good morning, world! Another day in Paradise!" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You cannot wait to show the instructor your work. You eat breakfast and get cleaned up. You just know that today will be another wonderful day." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "2" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "One by one, the instructor checks each student's progress. Finally, they get to you. You proudly show off your plant-covered island." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "Wonderful work. I expected nothing short of perfection from you." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "For tomorrow, why don't you make some buildings? I provided you with a stencil to follow. " });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "Again, left-click to draw. Right-click to erase. Shift + Left-click to select a color from the palette. Enter to generate your work." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "Unfortunately, a blank canvas is a dangerous thing. As such, I provided you with a stencil to follow." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "Follow the stencil and don't let me down." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "0" });

        dialogueManager.StartDialogue(dialogueList, new DialogueManager.StartDel(DisableTileMapPainters), new DialogueManager.EndDel(EnableBuildingCanvas));
    }

    private void EnableBuildingCanvas()
    {
        mapPainters[1].gameObject.transform.parent.gameObject.SetActive(true);
        mapPainters[1].enablePainting = true;
        mapPainters[1].completionCondition = 1;
    }

    public void BuildingsAdded(float score)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Phew. That was a little more involved. But, at least it's done." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "As you pack up, you notice that it's well past when you typically go to bed." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Oh, well. At least I'll get some sleep." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "4" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "1" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You wake up the next morning to the sun shining in your eyes and to a slight headache. You pull yourself out of bed only to feel a little dizzy. " });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Ugh. Shouldn't have stayed up so late." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You shuffle on over to the medicine cabinate and shake a pain killer out of the bottle. After taking it, you feel a little better." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Morning, world.... Another day in Paradise." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You notice it's almost time for class. Oh, well. You can skip breakfast this once." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You get changed and head out the door." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "4" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "2" });

        string evaluation = ScoreToGrade(score);
        char letterGrade = ScoreToLetter(score);
        if (letterGrade == 'A')
        {
            evaluation += ". Decent work. Though, I'm not too keen on how blurry your buildings are.";
        }
        else if (letterGrade == 'B')
        {
            evaluation += ". I know you can do better. Follow my instructions closer next time and do what you're supposed to.";
        }
        else if (letterGrade == 'B')
        {
            evaluation += ". I'm disappointed. You showed such promise. You have a few more tasks left. Don't mess up them up.";
        }
        else
        {
            evaluation = "This work is unsalvagable. If you're not going to take the work I give you seriously, get out!";
            dialogueList.Add(new Dialogue { speakerName = "", text = "You're shocked. Did I really not try hard enough?" });
            dialogueList.Add(new Dialogue { speakerName = "", text = "You feel faint. You leave the room. Was best not good enough?" });
            dialogueManager.StartDialogue(dialogueList, new DialogueManager.StartDel(DisableTileMapPainters), new DialogueManager.EndDel(GameOver));
            return;
        }

        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = evaluation });
        dialogueList.Add(new Dialogue { speakerName = "", text = "Was it really not good enough?" });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "Now that they have a place to live, let's add some people. And while you're at it, why don't you make some food for them." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "This is the most important part of the assignment." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "I'll provide you a new canvas. Get it done faster this time." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You think to yourself, \"I must do even better this time.\" You rub your eyes to try to stay awake as the headache returns." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "4" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "1" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "Hopefully, you can get to bed early tonight. Hopefully..." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You take two pain killers, sit down, and yawn." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Alright. Let's get this over with." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "0" });
        dialogueManager.StartDialogue(dialogueList, new DialogueManager.StartDel(DisableTileMapPainters), new DialogueManager.EndDel(EnablePeopleCanvas));
    }

    private void EnablePeopleCanvas()
    {
        mapPainters[2].gameObject.transform.parent.gameObject.SetActive(true);
        mapPainters[2].enablePainting = true;
        mapPainters[2].completionCondition = 2;
        StartCoroutine(((CanvasPainter)mapPainters[2]).TimedPaint(30));
    }


    public void PeopleAdded(SpriteRenderer[] peoples)
    {
        people = peoples;
        List<Dialogue> dialogueList = new List<Dialogue>();
        dialogueList.Add(new Dialogue { speakerName = "", text = "You feel dizzy." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You don't even know what time it is." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "I must... do better. Let me just refocus, and..." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "As you start to stand up, your body feels heavy...." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "And with a thud, your body collapses onto the floor." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "4" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "And everything... goes dark." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "3" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You hear rain tapping on the window." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "Everything's blurry, and you feel unimaginable pain." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You crawled your way to the medicine cabinate and reached for something to ease the pain." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "The bottle knocks over. Oh, well." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You grab a hand full of pills." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Another day..." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You're already late for class! With what little energy you have you stumble your way to class." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "4" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "0" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "You realize something's wrong with your people, but you don't have enough time to do anything." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "This is far from perfection." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "The instructor looks crossed at you." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "But I..." });
        dialogueList.Add(new Dialogue { speakerName = "Instructor", text = "I don't want to hear it. This is pathetic! Get your act together or else you'll never amount to anything!" });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "But..." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "But it was too late. What's done is done. The instructor moved onto to the next victim." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "I did my best...." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "What else could I have done?" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "Am I never going to amount to anything?" });
        dialogueList.Add(new Dialogue { speakerName = "Background", text = "4" });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "You", text = "Please.... I just need more time...." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "But nobody heard your pleas." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "... ... ..." });
        dialogueList.Add(new Dialogue { speakerName = "", text = "Paraside is lost." });
        dialogueManager.StartDialogue(dialogueList, new DialogueManager.StartDel(EnablePeopleStart), new DialogueManager.EndDel(GameOver2));
    }

    private void EnablePeopleStart()
    {
        DisableTileMapPainters();
        StartCoroutine(KillOff());
    }

    IEnumerator KillOff()
    {
        int count = 0;
        yield return new WaitForSeconds(10f);
        foreach (SpriteRenderer renderer in people)
        {
            if (count == 8)
            {
                continue;
            }
            renderer.sprite = null;
            yield return new WaitForSeconds(Random.Range(1.25f, 2f));
            count++;
        }
    }


    public string ScoreToGrade(float score)
    {
        string scoreString = (score * 100).ToString("0.##\\%");
        char letterGrade = ScoreToLetter(score);

        return letterGrade + " (" + scoreString + ")";
    }

    char ScoreToLetter(float score)
    {
        if (score >= 0.9f)
            return 'A';
        else if (score >= 0.8)
            return 'B';
        else if (score >= 0.7)
            return 'C';
        else
            return 'F';
    }

    private void GameOver()
    {
        // Set player pref
        PlayerPrefs.SetInt("Ending", 1);
        // Go to Title Screen
        SceneManager.LoadScene("Title");
    }

    private void GameOver2()
    {
        // Set player pref
        PlayerPrefs.SetInt("Ending", 2);
        // Go to Title Screen
        SceneManager.LoadScene("Title");
    }
}
