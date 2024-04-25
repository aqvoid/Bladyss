using UnityEngine;
using UnityEngine.EventSystems;

public class PauseControls : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseOptionsMenu;
    [HideInInspector] static public bool pauseIsOn;
    [HideInInspector] static public bool pauseOptionsIsOn;

    void Start()
    {
        pauseIsOn = false;
        pauseOptionsIsOn = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseIsOn = !pauseIsOn;
            pauseMenu.SetActive(pauseIsOn);
            pauseOptionsIsOn = !pauseOptionsIsOn;
            pauseOptionsMenu.SetActive(pauseOptionsIsOn);

            pauseMenu.transform.localPosition = new Vector3(0, 0f, pauseMenu.transform.localPosition.z);
            pauseOptionsMenu.transform.localPosition = new Vector3(-1920f, 0f, pauseOptionsMenu.transform.localPosition.z);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Pause Options Menu Button"));

            if (pauseIsOn && pauseOptionsIsOn) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }

    public void BackToPause()
    {
        pauseMenu.transform.localPosition = new Vector3(0, 0f, pauseMenu.transform.localPosition.z);
        pauseOptionsMenu.transform.localPosition = new Vector3(-1920f, 0f, pauseOptionsMenu.transform.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Pause Options Menu Button"));
    }

    public void PauseOptions()
    {
        pauseMenu.transform.localPosition = new Vector3(1920, 0f, pauseMenu.transform.localPosition.z);
        pauseOptionsMenu.transform.localPosition = new Vector3(0f, 0f, pauseOptionsMenu.transform.localPosition.z);

        EventSystem.current.SetSelectedGameObject(GameObject.Find("Back To Pause Menu Button"));
        // animation: moving from center to left if pauseoptions closed, if pauseoptions opened move from right to center
    }
}
