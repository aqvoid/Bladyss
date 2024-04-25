using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] public Text speedText;

    private void Update()
    {
        if (!PauseControls.pauseIsOn)
        {
            speedText.text = PlayerController.player.currentSpeed.ToString("Speed: 0.0");
        }
    }
}
