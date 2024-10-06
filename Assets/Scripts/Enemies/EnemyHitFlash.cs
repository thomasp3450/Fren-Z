using UnityEngine;

public class EnemyHitFlash : MonoBehaviour
{
    [SerializeField]
    private float _flashLength;

    [SerializeField]
    private Color _flashColor;

    [SerializeField]
    private int _numberOfFlashes;

    private SpriteFlash _spriteFlash;

    private void Awake()
    {
        _spriteFlash = GetComponent<SpriteFlash>();
    }

    public void StartFlash()
    {
        _spriteFlash.StartFlash(_flashLength, _flashColor, _numberOfFlashes);
    }
}
