using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2FSM : MonoBehaviour
{
  
  public State CurrentState => _currentState; 
  public State _currentState; //stores current state
  protected bool transition; //checking if changing states
  public int currentPosition;


  private void Start(){
    ChangeState<Boss2>(); //starts on Decide state to initialize the boss
    currentPosition = 1;
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

 public State GetState(){
  return CurrentState;
 }

  public int GetPosition(){
     return currentPosition;
  }

  public void SetPosition(int pos){
     currentPosition = pos;
  }

}


