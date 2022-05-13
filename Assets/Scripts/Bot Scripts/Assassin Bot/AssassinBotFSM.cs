using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssassinBot;

public class AssassinBotFSM : MonoBehaviour
{
    FSM<string> fsm;
    FSMState<string> idleState, prepareState, resupplyState, searchState, disguiseState;

    public enum Arsenal { None, Sword, ThrowingKnife, Sniper };

    public Arsenal Weapon { get; private set; }
    public float Speed { get; private set; }
    public float RotateSpeed { get; private set; }

    public int DisguiseCount, InvisCount, DecoyCount, SniperBullets, ThrowingKnives;

    public GameObject EliminationTarget { get; set; }

    public AssassinBotFSM()
    {
        Speed = 2f;
        RotateSpeed = 250f;
        Weapon = Arsenal.None;

        DisguiseCount = 0;
        InvisCount = 0;
        DecoyCount = 0;
        SniperBullets = 0;
        ThrowingKnives = 0;
    }

    void Start()
    {
        fsm = new FSM<string>();

        idleState = new IdleState(fsm);
        prepareState = new PrepareState(fsm, this);
        resupplyState = new ResupplyState(fsm, this);
        searchState = new SearchState(fsm, this);
        disguiseState = new DisguiseState(fsm, this);

        fsm.AddState(idleState);
        fsm.AddState(prepareState);
        fsm.AddState(resupplyState);
        fsm.AddState(searchState);
        fsm.AddState(disguiseState);

        fsm.SetState("Idle");
    }

    void Update()
    {
        fsm.Update();
    }

    public void EquipSword()
    { Weapon = Arsenal.Sword; }

    public void EquipThrowingKnife()
    { Weapon = Arsenal.ThrowingKnife; }

    public void EquipSniper()
    { Weapon = Arsenal.Sniper; }
}