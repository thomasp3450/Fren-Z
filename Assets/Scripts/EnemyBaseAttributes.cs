using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Attributes", menuName = "ScriptableObjects/Enemy Base Attributes", order = 1)]
public class EnemyBaseAttributes : ScriptableObject
{
    [field: SerializeField]
    public float Speed { get; private set; }

    [field: SerializeField]
    public float RotationSpeed { get; private set; }

    [field: SerializeField]
    public float Health { get; private set; }

    [field: SerializeField]
    public float PlayerAwarenessDistance { get; private set; }

    [field: SerializeField]
    public int KillScore { get; private set; }

    [field: SerializeField]
    [field: Range(0, 1)]
    public float ChanceOfCollectableDrop { get; private set; }

    [field: SerializeField]
    public int Damage { get; private set; }
}
