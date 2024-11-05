using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    [SerializeField] public bool isActive;
    [SerializeField] protected GameObject enemyType;
    [SerializeField] protected GameObject BossAttached;
    [SerializeField] protected Transform transform;
    private HealthController healthController;
    private PlayerAwarenessController playerAwarenessController;
    private Vector3 TransformPosition;
    
    void Start()
    {
        healthController = BossAttached.GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        StartCoroutine(SpawnEnemy());
        TransformPosition = transform.position;
    }

    void Update()
    {
        float bossHealth = getHealth();
        if(bossHealth >= 0 && playerAwarenessController.AwareOfPlayer){
            isActive = true;
        }else{
            isActive = false;
        }
    }

    public float getHealth(){
        return healthController.getHealth();
    }
   

    IEnumerator SpawnEnemy(){
        
        while(isActive){
            yield return new WaitForSeconds(6f); //wait 6 seconds before spawning new enemy
            Instantiate(enemyType, TransformPosition, Quaternion.identity);
        }

    }
}
