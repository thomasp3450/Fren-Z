using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    private bool _isPermanantlyAware;

    public Vector2 DirectionToPlayer { get; private set; }
    private Transform _player;
    public float _playerAwarenessDistance;
    //private EnemyAttributes _enemyAttributes;

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable() {
        _isPermanantlyAware = false;
    }

    private void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance || _isPermanantlyAware) {
            AwareOfPlayer = true;
        } else {
            AwareOfPlayer = false;
        }
    }

    public Vector2 getPlayerDistance(){
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;
        return DirectionToPlayer;
    }

    public void MakePermanantlyAware() {
        _isPermanantlyAware = true;
        AwareOfPlayer = true;
    }
}
