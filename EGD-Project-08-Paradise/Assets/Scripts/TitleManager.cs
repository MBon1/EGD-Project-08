using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Text title;
    public Text endingText;
    public GameObject ending2Text;
    public GameObject button;
    public Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Ending"))
        {
            int ending = PlayerPrefs.GetInt("Ending");
            if (ending == 1)
            {
                title.text = "Another Day in Paradise";
                endingText.text = "1/3 (not good enough)";
                buttonText.text = "Again!";
            }
            if (ending == 2)
            {
                title.text = "Paradise is Lost";
                ending2Text.SetActive(true);
                button.SetActive(false);
                endingText.text = "2/3 (regret)";

            }
        }
    }

    public void BeginGame()
    {
        PlayerPrefs.SetInt("Ending", 0);
        SceneManager.LoadScene("SampleScene");
    }
}
