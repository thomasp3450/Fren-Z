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
    float bossHealth;
    
    void Start()
    {
        healthController = BossAttached.GetComponent<HealthController>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        TransformPosition = transform.position;
        InvokeRepeating("SpawnEnemy", 10f, 10f); 
        bossHealth = getHealth();
    }

    void Update()
    {
        bossHealth = getHealth();
        if(bossHealth >= 0){
            isActive = true;
        }else{
            isActive = false;
            //gameObject.SetActive(false);
        }
    }

    public float getHealth(){
        return healthController.getHealth();
    }
   

    void SpawnEnemy(){
       if(isActive){
            Instantiate(enemyType, TransformPosition, Quaternion.identity);
        }

    }
}
