using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeAoE : MonoBehaviour
{ //Prefab script for syringe AoE goop
    
    float projectilespeed;
    float maxProjectileSpeed;
    float trajectoryMaxHeight;
    

    private AnimationCurve trajectory;
    private  AnimationCurve trajectoryLinearCorrection;
    private AnimationCurve ProjectileSpeedCurve;
    private Transform target;
    private Vector3 TrajectoryStart, projectileMoveDirection, TrajectoryRange;

    private float nextXTrajectoryPosition, nextYTrajectoryPosition; //next position along curve
    private float nextXTrajectoryAbs, nextYTrajectoryAbs; //correction with absolute values

    public void InitializeProjectile(Transform target, float maxProjectileSpeed, float trajectoryMaxHeight){
        this.target = target;
        this.maxProjectileSpeed = maxProjectileSpeed;
        float xDistanceToTarget = target.position.x - transform.position.x;
        this.trajectoryMaxHeight = Mathf.Abs(xDistanceToTarget) * trajectoryMaxHeight;
   }

    
    private void Start(){
        TrajectoryStart = transform.position; 
    }

    private void Update(){
        UpdateProjectilePos();
        UpdateProjectileRotation();

        if(Vector3.Distance(transform.position,target.position) < 1f){
            Destroy(gameObject);
        }
    }

    private void UpdateProjectilePos(){ //interpolates projectile on the curve with respect to each axis
        TrajectoryRange = target.position - TrajectoryStart;
        
        if(Mathf.Abs(TrajectoryRange.normalized.x) < Mathf.Abs(TrajectoryRange.normalized.y)){ //check for each target direction on 2d plane
            if(TrajectoryRange.y < 0){
                projectilespeed = -projectilespeed;
            }
            UpdateXCurve(); 
        }else{
            if(TrajectoryRange.x < 0){
                projectilespeed = -projectilespeed;
            }
            UpdateYCurve();
        }
   }

  
   private void UpdateXCurve(){ //updates with respect to the X axis (Horizontal)
        float nextY = transform.position.y + projectilespeed * Time.deltaTime;
        float nextYnormal = (nextY - TrajectoryStart.y) / TrajectoryRange.y;

        float nextXnormal = trajectory.Evaluate(nextYnormal);
        nextXTrajectoryPosition = nextXnormal * trajectoryMaxHeight;

        float nextXTrajectoryNormal = trajectory.Evaluate(nextXnormal);
        nextXTrajectoryAbs = nextXTrajectoryNormal * TrajectoryRange.x;

        if(TrajectoryRange.x > 0 && TrajectoryRange.y > 0){
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        if(TrajectoryRange.x < 0 && TrajectoryRange.y < 0){
            nextXTrajectoryPosition = -nextXTrajectoryPosition;
        }

        float nextX = TrajectoryStart.x + nextXTrajectoryPosition + nextXTrajectoryAbs;
        Vector3 newPos = new Vector3(nextX, nextY, 0);

        CalculateNextProjectileSpeed(nextYnormal);
        projectileMoveDirection = newPos - transform.position;
        transform.position = newPos;
    
    }

   private void UpdateYCurve(){//updates with respect to the Y axis (Vertical)
        float nextX = transform.position.x + projectilespeed * Time.deltaTime;
        float nextXnormal = (nextX - TrajectoryStart.x) / TrajectoryRange.x;

        float nextYnormal = trajectory.Evaluate(nextXnormal);
        nextYTrajectoryPosition = nextYnormal * trajectoryMaxHeight;

        float nextYTrajectoryNormal = trajectory.Evaluate(nextXnormal);
        nextYTrajectoryAbs = nextYTrajectoryNormal * TrajectoryRange.y;

        float nextY = TrajectoryStart.y + nextXTrajectoryPosition + nextYTrajectoryAbs;
        Vector3 newPos = new Vector3(nextX, nextY, 0);

        CalculateNextProjectileSpeed(nextXnormal);
        projectileMoveDirection = newPos - transform.position;
        transform.position = newPos;
   }

   private void CalculateNextProjectileSpeed(float nextPositionXNormalized) {
        float nextMoveSpeedNormalized = ProjectileSpeedCurve.Evaluate(nextPositionXNormalized);

        projectilespeed = nextMoveSpeedNormalized * maxProjectileSpeed;
    }

   public void InitializeAnimationCurves(AnimationCurve trajectory, AnimationCurve trajectoryLinearCorrection, AnimationCurve ProjectileSpeedCurve){
    this.trajectory = trajectory;
    this.trajectoryLinearCorrection = trajectoryLinearCorrection;
    this.ProjectileSpeedCurve = ProjectileSpeedCurve;
   }
        

    private void UpdateProjectileRotation() {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileMoveDirection.y, projectileMoveDirection.x) * Mathf.Rad2Deg);
    }



    private void OnTriggerEnter2D (Collider2D collision) {

    if (collision.GetComponent<PlayerController>()) {
        gameObject.SetActive(false);
        if (collision.GetComponent<PlayerController>()._isFrenzied) {
            if (!collision.GetComponent<PlayerController>()._gaugeInvincible) {
                collision.GetComponent<SpriteFlash>().StartFlash((float)0.12, new Color((float)255,(float)0.0,(float)0.0), 1);
                collision.GetComponent<PlayerController>().ChangeFrenzyGauge(-5);
            }
        } else {
            if (collision.GetComponent<HealthController>()._isInvincible) {
                collision.GetComponent<SpriteFlash>().StartFlash((float)0.12, new Color((float)255,(float)0.0,(float)0.0), 1);
                collision.GetComponent<HealthController>().TakeDamage(1);
                collision.GetComponent<HealthController>().InitIFrames();
            }
        }
    }
 }
    private void OnTriggerExit2D (Collider2D collision) {
    if (collision.GetComponent<EnemyMovement>()) {
        collision.GetComponent<HealthController>().ExitIFrames();
    }
}

}
