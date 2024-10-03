using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public float Speed { get; private set; }

    public float RotationSpeed { get; private set; }

    public float Health { get; private set; }

    public float PlayerAwarenessDistance { get; private set; }

    public int KillScore { get; private set; }

    public float ChanceOfCollectableDrop { get; private set; }

    public float Damage { get; private set; }

    [SerializeField]
    private EnemyBaseAttributes _baseAttributes;

    private void Awake()
    {
        SetAttributeModifier(1);
    }

    public void SetAttributeModifier(float modifier)
    {
        Speed = _baseAttributes.Speed * modifier;
        RotationSpeed = _baseAttributes.RotationSpeed * modifier;
        Health = _baseAttributes.Health * modifier;
        PlayerAwarenessDistance = _baseAttributes.PlayerAwarenessDistance * modifier;
        KillScore = Mathf.RoundToInt(_baseAttributes.KillScore * modifier);
        ChanceOfCollectableDrop = _baseAttributes.ChanceOfCollectableDrop / modifier;
        Damage = _baseAttributes.Damage * modifier;
    }
}