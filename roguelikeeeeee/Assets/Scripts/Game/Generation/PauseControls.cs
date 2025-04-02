using UnityEngine;
using UnityEngine.EventSystems;

public class PauseControls : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseOptionsMenu;
    [HideInInspector] static public bool pauseIsOn = false;
    [HideInInspector] static public bool pauseOptionsIsOn = false;

    private static AudioSource audioSource;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu();
    }

    private void PlayClickSound()
    {
        if (soundManager != null)
            soundManager.PlayClickSound();
    }

    private void TogglePauseMenu()
    {
        pauseIsOn = !pauseIsOn;
        pauseMenu.SetActive(pauseIsOn);
        pauseOptionsIsOn = !pauseOptionsIsOn;
        pauseOptionsMenu.SetActive(pauseOptionsIsOn);

        pauseMenu.transform.localPosition = new Vector3(0, 0f, pauseMenu.transform.localPosition.z);
        pauseOptionsMenu.transform.localPosition = new Vector3(-1920f, 0f, pauseOptionsMenu.transform.localPosition.z);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Pause Options Menu Button"));

        Time.timeScale = pauseIsOn && pauseOptionsIsOn ? 0 : 1;
    }

    public void BackToPause()
    {
        PlayClickSound();

        pauseMenu.transform.localPosition = new Vector3(0, 0f, pauseMenu.transform.localPosition.z);
        pauseOptionsMenu.transform.localPosition = new Vector3(-1920f, 0f, pauseOptionsMenu.transform.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Pause Options Menu Button"));
    }

    public void PauseOptions()
    {
        PlayClickSound();

        pauseMenu.transform.localPosition = new Vector3(1920, 0f, pauseMenu.transform.localPosition.z);
        pauseOptionsMenu.transform.localPosition = new Vector3(0f, 0f, pauseOptionsMenu.transform.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Back To Pause Menu Button"));
        // animation: moving from center to left if pauseoptions closed, if pauseoptions opened move from right to center
    }
}
