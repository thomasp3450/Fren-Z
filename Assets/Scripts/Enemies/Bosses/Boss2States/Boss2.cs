using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public StateMachine stateMachine;
    //States from FSM 
    //public DecideState decideState;
    public B2Idle IdleState; //state for when the boss is attacking

    public B2EnergyPistol PistolState; //if player is too far away, he will take pot shots with his pistol
    public B2ScalpelShot ScalpelState; //Scalpel shot -- player must take cover or take massive damage 
    public B2Hammer HammerState; //Hammer Slam Attack
    public B2Syringe SyringeState;// syringe AoE poison

    

    public B2Spin SpinState; // spin to decide on each attack
    public B2Weakened WeakenedState; //After either type of attack, boss shield drops and they are vulnerable

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _awareOfPlayerController;
   
    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<StateMachine>();
    }

    private void Update(){

    }

     

}


