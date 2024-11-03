using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public StateMachine stateMachine;
    //States from FSM 
    //public DecideState decideState;
    public B1Idle IdleState; //state for when the boss is attacking
    public B1Slam SlamState; //if player is close, slam attack 
    public B1Volley VolleyState; //if player is far, choose between two volleys
    public B1Weakened WeakenedState; //After either type of attack, boss shield drops and they are vulnerable

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _awareOfPlayerController;
   
    private void Start(){
        _rigidbody = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<StateMachine>();
    }

    private void Update(){

    }

     

}
