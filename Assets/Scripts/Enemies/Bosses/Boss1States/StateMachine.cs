using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour //Finite State Machine for boss decisionmaking in response to player position and current status
{
  public State CurrentState => _currentState; 
  State _currentState; //stores current state
  
  protected bool transition; //checking if changing states

  private void Start(){
    ChangeState<DecideState>(); //starts on Decide state to initialize the boss
  }

  public void ChangeState<T>() where T : State{ //initiate change state to targetted state, called by states to transition
    T targetState = GetComponent<T>();
    if (targetState == null){
        return;
    }
    initiateNewState(targetState);
  }

  void initiateNewState(State targetState){
    if(_currentState != targetState && !transition){ // checks that we aren't in the state we are targetting and if it is transitioning before calling new state
        callNewState(targetState);
    }

  }

  void callNewState(State newState){ //leave current state and enter target state
       transition = true;
       _currentState?.Exit();
       _currentState = newState;
       _currentState?.Enter();
        transition = false;
  }

  private void Update(){
    if(CurrentState != null && !transition){ //run current state tick function
        CurrentState.Tick();
    }
  }
}
