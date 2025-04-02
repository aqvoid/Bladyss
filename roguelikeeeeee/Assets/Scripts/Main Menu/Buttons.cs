using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private Transform mainMenu;
    private Transform optionsMenu;
    private Transform howToPlayMenu;

    private static AudioSource audioSource;

    private GameObject continueGameButton;

    void Awake()
    {
        if (audioSource == null) // Ищем SoundManager только один раз
        {
            GameObject soundManager = GameObject.Find("SoundManager");
            if (soundManager != null)
                audioSource = soundManager.GetComponent<AudioSource>();
        }

        if (GameObject.Find("Main Menu"))
            mainMenu = GameObject.Find("Main Menu").GetComponent<Transform>();
        if (GameObject.Find("Options Menu"))
            optionsMenu = GameObject.Find("Options Menu").GetComponent<Transform>();
        if (GameObject.Find("How To Play Menu"))
            howToPlayMenu = GameObject.Find("How To Play Menu").GetComponent<Transform>();
    }

    private void Start()
    {
        if (GameObject.Find("Continue Game Button"))
        {
            continueGameButton = GameObject.Find("Continue Game Button");

            if (GameSaveManager.isSaveExist()) continueGameButton.SetActive(true);
            else continueGameButton.SetActive(false);
        }
    }

    private void PlayClickSound()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(audioSource.clip); // Используем звук, установленный в AudioSource
    }

    #region Main Menu Buttons
    public void NewGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("Game");
        if (GameSaveManager.isSaveExist()) 
            File.Delete(GameSaveManager.saveFilePath);
    }
    public void SaveGame() //not exactly button
    {
        //GameSaveManager.SaveGame();
    }

    public void ContinueGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("Game");
        
        GameSaveData saveData = GameSaveManager.LoadGame();
        if (saveData != null)
        {
            SceneManager.LoadScene(saveData.sceneName);
            StartCoroutine(WaitForSceneLoad(saveData));
        }

    }

    public void Options()
    {
        PlayClickSound();
        mainMenu.localPosition = new Vector3(2400f, 0, mainMenu.localPosition.z);
        optionsMenu.localPosition = new Vector3(0, 0, mainMenu.localPosition.z);
        howToPlayMenu.localPosition = new Vector3(4800f, 0, mainMenu.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Options Back Button"));
    }
    public void HowToPlay()
    {
        PlayClickSound();
        mainMenu.localPosition = new Vector3(-2400f, 0, mainMenu.localPosition.z);
        optionsMenu.localPosition = new Vector3(-4800f, 0, mainMenu.localPosition.z);
        howToPlayMenu.localPosition = new Vector3(0, 0, mainMenu.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("HowToPlay Back Button"));
    }
    public void Back()
    {
        PlayClickSound();
        mainMenu.localPosition = new Vector3(0, 0, mainMenu.localPosition.z);
        optionsMenu.localPosition = new Vector3(-2400f, 0, mainMenu.localPosition.z);
        howToPlayMenu.localPosition = new Vector3(2400f, 0, mainMenu.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("New Game Button"));
    }
    public void Quit()
    {
        PlayClickSound();
        SaveGame();
        Application.Quit();
    }
    #endregion

    #region Pause Menu Buttons
    public void Resume()
    {
        PlayClickSound();
        PauseControls pauseControlsScript = GameObject.Find("Canvas").GetComponent<PauseControls>();

        PauseControls.pauseIsOn = !PauseControls.pauseIsOn;
        pauseControlsScript.pauseMenu.SetActive(PauseControls.pauseIsOn);

        PauseControls.pauseOptionsIsOn = !PauseControls.pauseOptionsIsOn;
        pauseControlsScript.pauseOptionsMenu.SetActive(PauseControls.pauseOptionsIsOn);

        Time.timeScale = PauseControls.pauseIsOn && PauseControls.pauseOptionsIsOn ? 0 : 1;
    }
    public void BackToMainMenu()
    {
        PlayClickSound();

        SaveGame();
        SceneManager.LoadScene("Menu");

        Time.timeScale = 1;
        PauseControls.pauseIsOn = false;
        PauseControls.pauseOptionsIsOn = false;
    }
    #endregion


    private IEnumerator WaitForSceneLoad(GameSaveData saveData)
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == saveData.sceneName);

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.transform.position = saveData.playerPos;
            player.health = saveData.health;
        }

        Debug.Log("Game loaded!");
    }

}