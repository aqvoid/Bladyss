using System.Collections;
using UnityEngine;

public class ToggleTorches : MonoBehaviour
{
    public Sprite leftExtinguished, rightExtinguished;
    public Sprite[] leftFlameSprites, rightFlameSprites;
    public float frameRate = 0.1f;

    private SpriteRenderer[] torchRenderers;
    private bool isLit = false;

    private void Awake()
    {
        torchRenderers = GetComponentsInChildren<SpriteRenderer>();
        SetExtinguished();
        Debug.Log($"[ToggleTorches] Найдено {torchRenderers.Length} факелов в {gameObject.name}");
    }

    private void SetExtinguished()
    {
        for (int i = 0; i < torchRenderers.Length; i++)
        {
            if (i % 2 == 0)
                torchRenderers[i].sprite = leftExtinguished;
            else
                torchRenderers[i].sprite = rightExtinguished;
        }
        Debug.Log("[ToggleTorches] Установлены потухшие спрайты");
    }

    public void TryLightTorches()
    {
        if (isLit) return;
        isLit = true;
        Debug.Log($"[ToggleTorches] Факела начали зажигаться в {gameObject.name}");
        StartCoroutine(LightUpWithDelay());
    }

    private IEnumerator LightUpWithDelay()
    {
        for (int i = 0; i < torchRenderers.Length; i++)
        {
            //i = every torch sprite slot
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            StartCoroutine(AnimateTorch(torchRenderers[i], i % 2 == 0 ? leftFlameSprites : rightFlameSprites));
        }
        Debug.Log("[ToggleTorches] Все факела зажглись.");
    }

    private IEnumerator AnimateTorch(SpriteRenderer renderer, Sprite[] frames)
    {
        while (true)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                renderer.sprite = frames[i];
                yield return new WaitForSeconds(frameRate);
            }
        }
    }
}
