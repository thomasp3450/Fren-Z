using System.Collections;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    public void StartFlash(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        StartCoroutine(FlashCoroutine(flashDuration, flashColor, numberOfFlashes));
    }

    public IEnumerator FlashCoroutine(float flashDuration, Color flashColor, int numberOfFlashes)
    {
        float elapsedFlashTime = 0;
        float elapsedFlashPercentage = 0;

        while (elapsedFlashPercentage < 1)
        {
            elapsedFlashTime += Time.deltaTime;
            elapsedFlashPercentage = elapsedFlashTime / flashDuration;

            if (elapsedFlashPercentage < 1)
            {
                _spriteRenderer.color = Color.Lerp(_originalColor, flashColor, Mathf.PingPong(elapsedFlashPercentage * 2 * numberOfFlashes, 1));
            }
            else
            {
                _spriteRenderer.color = _originalColor;
            }

            yield return null;
        }
    }


    public void SetOriginalColor(Color color)
    {
        _originalColor = color;
        _spriteRenderer.color = color;
    }
}
