using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private Transform mainMenu;
    private Transform optionsMenu;
    
    void Awake()
    {
        if (GameObject.Find("Main Menu"))
            mainMenu = GameObject.Find("Main Menu").GetComponent<Transform>();
        if (GameObject.Find("Options Menu"))
            optionsMenu = GameObject.Find("Options Menu").GetComponent<Transform>();
    }

    #region Main Menu Buttons
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Options()
    {
        mainMenu.localPosition = new Vector3(2400, 0f, mainMenu.localPosition.z);
        optionsMenu.localPosition = new Vector3(0f, 0f, mainMenu.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Back Button"));
    }
    public void Back()
    {
        mainMenu.localPosition = new Vector3(0f, 0f, mainMenu.localPosition.z);
        optionsMenu.localPosition = new Vector3(-2400f, 0f, mainMenu.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Options Button"));
    }
    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Pause Menu Buttons
    public void Resume()
    {
        PauseControls pauseControlsScript = GameObject.Find("Canvas").GetComponent<PauseControls>();

        PauseControls.pauseIsOn = !PauseControls.pauseIsOn;
        pauseControlsScript.pauseMenu.SetActive(PauseControls.pauseIsOn);

        PauseControls.pauseOptionsIsOn = !PauseControls.pauseOptionsIsOn;
        pauseControlsScript.pauseOptionsMenu.SetActive(PauseControls.pauseOptionsIsOn);

        if (PauseControls.pauseIsOn && PauseControls.pauseOptionsIsOn)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        PauseControls.pauseIsOn = false;
    }
    #endregion
}
