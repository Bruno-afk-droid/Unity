using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    public GameObject SetupMenu;
    public GameObject GameManager;

    public GameObject InputWidth;
    public GameObject InputHeight;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    public void PlayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)

        int.TryParse(InputWidth.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text, out int r);
        int.TryParse(InputHeight.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text, out int c);

        if ((r >= 10 && r <= 50)&&(r >= 10 && r <= 50))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game Scene");
            GameObject g = Instantiate(GameManager);

            GameManager.GetComponent<MazeLoader>().mazeRows = r;
            GameManager.GetComponent<MazeLoader>().mazeColumns = c;
        }

    }

    public void CreditsButton()
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        SetupMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        SetupMenu.SetActive(false);
    }

    public void SetupButton()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        SetupMenu.SetActive(true);

        print(SetupMenu.transform.Find("InputWidth").Find("Text").GetComponent<UnityEngine.UI.Text>().text);
        print(SetupMenu.transform.Find("InputHeight").Find("Text").GetComponent<UnityEngine.UI.Text>().text);
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}