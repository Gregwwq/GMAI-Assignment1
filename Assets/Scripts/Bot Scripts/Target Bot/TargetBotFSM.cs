using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TargetBot;

public class TargetBotFSM : MonoBehaviour
{
    FSM<string> fsm;
    FSMState<string> wanderState, fleeState, chaseState, dieState;

    public float Speed { get; private set; }
    public float RotateSpeed { get; private set; }

    GameObject head, body;

    public TargetBotFSM()
    {
        Speed = 1f;
        RotateSpeed = 200f;
    }

    void Start()
    {
        // assigning the respective parts of the target bot
        head = transform.Find("Head").gameObject;
        body = transform.Find("Body").gameObject;

        //  creating the fsm
        fsm = new FSM<string>();

        // creating the states
        wanderState = new WanderState(fsm, this);
        fleeState = new FleeState(fsm, this);
        chaseState = new ChaseState(fsm, this);
        dieState = new DieState(fsm, this);

        // adding the states to the fsm
        fsm.AddState(wanderState);
        fsm.AddState(fleeState);
        fsm.AddState(chaseState);
        fsm.AddState(dieState);

        // setting the default state to WANDER
        fsm.SetState("Wander");
    }

    void Update()
    {
        fsm.Update();
    }

    // function to make the target bot flee
    public void TriggerTargeted()
    {
        Speed = 1.5f;
        fsm.SetState("Flee");
    }

    // function to kill the target bot
    public void TriggerDie()
    {
        fsm.SetState("Die");
    }

    // function to make the target bot start to chase
    public void TriggerChase()
    {
        fsm.SetState("Chase");
    }

    // function to change the color of the target bot except for its wheels
    public void ChangeColor(Color color)
    {
        head.GetComponent<Renderer>().material.color = color;
        body.GetComponent<Renderer>().material.color = color;

        Renderer[] renderers = body.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }

    // function to reset the target bot back to WANDER
    public void ReturnToWander()
    {
        fsm.SetState("Wander");
    }
}