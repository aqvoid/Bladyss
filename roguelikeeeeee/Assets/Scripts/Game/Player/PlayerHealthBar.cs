using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    ParticleSystem healthBarParticleSystem1;
    ParticleSystem healthBarParticleSystem2;

    private void Awake()
    {
        healthBarParticleSystem1 = GameObject.Find("HealthBar ParticleSystem 1").GetComponent<ParticleSystem>();
        healthBarParticleSystem2 = GameObject.Find("HealthBar ParticleSystem 2").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!PauseControls.pauseIsOn)
        {
            PlayerController.player.imageHealthBar.fillAmount = PlayerController.player.health; //health bar = hp
            if (PlayerController.player.health <= 1)
                PlayerController.player.health += Mathf.LerpUnclamped(0.000075f, 0.0001f / 3, Mathf.Clamp01(PlayerController.player.currentSpeed)); //regeneration
                                                                                                                                                    //player.health += 0.0001f; //???????????

            //healthPercent = emissionRate of healthbar particle
            var healthParticleManager1 = healthBarParticleSystem1;
            var healthParticleManager2 = healthBarParticleSystem2;

            var healthParticleEmission1 = healthParticleManager1.emission;
            var healthParticleEmission2 = healthParticleManager2.emission;
            var healthParticleSize1 = healthParticleManager1.sizeBySpeed;
            var healthParticleSize2 = healthParticleManager2.sizeBySpeed;

            healthParticleEmission1.rateOverTime = Mathf.LerpUnclamped(0f, 300f, Mathf.Clamp01(PlayerController.player.imageHealthBar.fillAmount));
            healthParticleEmission2.rateOverTime = Mathf.LerpUnclamped(0f, 600f, Mathf.Clamp01(PlayerController.player.imageHealthBar.fillAmount));
            healthParticleSize1.size = new ParticleSystem.MinMaxCurve(Mathf.Min(PlayerController.player.imageHealthBar.fillAmount), Mathf.Max(PlayerController.player.imageHealthBar.fillAmount + 1f));
            healthParticleSize2.size = new ParticleSystem.MinMaxCurve(Mathf.Min(PlayerController.player.imageHealthBar.fillAmount), Mathf.Max(PlayerController.player.imageHealthBar.fillAmount + 1f));
        }
    }
}
