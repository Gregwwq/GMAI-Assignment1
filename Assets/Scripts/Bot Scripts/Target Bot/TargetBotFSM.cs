using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TargetBot;

public class TargetBotFSM : MonoBehaviour
{
    FSM<string> fsm;
    FSMState<string> wanderState, fleeState, chaseState;

    public bool Targeted { get; private set; }
    public float Speed { get; private set; }
    public float RotateSpeed { get; private set; }

    GameObject head, body;

    public TargetBotFSM()
    {
        Targeted = false;
        
        Speed = 1f;
        RotateSpeed = 200f;
    }

    void Start()
    {
        head = transform.Find("Head").gameObject;
        body = transform.Find("Body").gameObject;

        fsm = new FSM<string>();

        wanderState = new WanderState(fsm, this);
        fleeState = new FleeState(fsm, this);

        fsm.AddState(wanderState);
        fsm.AddState(fleeState);

        fsm.SetState("Wander");
    }

    void Update()
    {
        fsm.Update();

        if (Targeted) fsm.SetState("Flee");
    }

    public void TriggerTargeted()
    {
        Targeted = true;
        Speed = 1.5f;
    }

    public void ChangeColor(Color color)
    {
        head.GetComponent<Renderer>().material.color = color;
        body.GetComponent<Renderer>().material.color = color;
        
        Renderer[] renderers = body.GetComponentsInChildren<Renderer>();

        foreach(Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
}