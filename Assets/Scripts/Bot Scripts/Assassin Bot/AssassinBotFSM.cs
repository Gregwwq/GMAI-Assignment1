using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssassinBot;

public class AssassinBotFSM : MonoBehaviour
{
    FSM<string> fsm;
    FSMState<string> idleState, prepareState, resupplyState, searchState, disguiseState, selectState, pursueState, highGroundState, aimState, teleportState, eliminateState, escapeState, decoyState, invisibleState, suicideState;

    public enum Arsenal { None, Sword, ThrowingKnife, Sniper };

    public Arsenal Weapon { get; private set; }
    public float Speed { get; private set; }
    public float RotateSpeed { get; private set; }

    public int DisguiseCount { get; set; }
    public int InvisCount { get; set; }
    public int DecoyCount { get; set; }
    public int ThrowingKnives { get; set; }
    public int SniperBullets { get; set; }

    public bool DecoyActive { get; set; }
    public bool InvisActive { get; set; }

    public GameObject EliminationTarget { get; set; }

    GameObject original, disguise, invisible, dead;

    Coroutine changeCor;

    public AssassinBotFSM()
    {
        Speed = 3f;
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
        // creating the fsm
        fsm = new FSM<string>();

        // creating the states for the fsm
        idleState = new IdleState(fsm, this);
        prepareState = new PrepareState(fsm, this);
        resupplyState = new ResupplyState(fsm, this);
        searchState = new SearchState(fsm, this);
        disguiseState = new DisguiseState(fsm, this);
        selectState = new SelectState(fsm, this);
        pursueState = new PursueState(fsm, this);
        highGroundState = new HighGroundState(fsm, this);
        aimState = new AimState(fsm, this);
        teleportState = new TeleportState(fsm, this);
        eliminateState = new EliminateState(fsm, this);
        escapeState = new EscapeState(fsm, this);
        decoyState = new DecoyState(fsm, this);
        invisibleState = new InvisibleState(fsm, this);
        suicideState = new SuicideState(fsm, this);

        // adding the states to the fsm
        fsm.AddState(idleState);
        fsm.AddState(prepareState);
        fsm.AddState(resupplyState);
        fsm.AddState(searchState);
        fsm.AddState(disguiseState);
        fsm.AddState(selectState);
        fsm.AddState(pursueState);
        fsm.AddState(highGroundState);
        fsm.AddState(aimState);
        fsm.AddState(teleportState);
        fsm.AddState(eliminateState);
        fsm.AddState(escapeState);
        fsm.AddState(decoyState);
        fsm.AddState(invisibleState);
        fsm.AddState(suicideState);

        // setting the starting state to IDLE
        fsm.SetState("Idle");

        // assigning the respective parts of the assassin bot
        original = transform.Find("Original").gameObject;
        disguise = transform.Find("Disguise").gameObject;
        invisible = transform.Find("Invisible").gameObject;
        dead = transform.Find("Dead").gameObject;
    }

    void Update()
    {
        fsm.Update();
    }

    // functions for equipping the various weapons
    public void EquipSword()
    { Weapon = Arsenal.Sword; }

    public void EquipThrowingKnife()
    { Weapon = Arsenal.ThrowingKnife; }

    public void EquipSniper()
    { Weapon = Arsenal.Sniper; }

    // function for unequipping all weapons
    public void UnequipWeapon()
    { Weapon = Arsenal.None; }

    // function to consume a decoy charge
    public void UseDecoy()
    {
        DecoyCount--;
        DecoyActive = true;
    }

    // function to change into disguise and use a disguise charge
    public void ChangeToDisguise()
    {
        original.SetActive(false);
        disguise.SetActive(true);
        invisible.SetActive(false);

        DisguiseCount--;
    }

    // function to activate invisibility and use a invisibility charge
    public void ChangeToInvisible()
    {
        original.SetActive(false);
        disguise.SetActive(false);
        invisible.SetActive(true);

        InvisCount--;
        InvisActive = true;
    }

    // function to change back to the original look after a short delay
    public void ChangeToOriginal()
    { changeCor = StartCoroutine(ChangeToOriginal_Cor()); }
    IEnumerator ChangeToOriginal_Cor()
    {
        yield return new WaitForSeconds(0.5f);

        original.SetActive(true);
        disguise.SetActive(false);
        invisible.SetActive(false);
    }

    // function to change state to suicide
    public void TriggerSuicide()
    {
        fsm.SetState("Suicide");
    }

    // function for the assassin bot to commit suicide
    public void ChangeToDead()
    {
        StopCoroutine(changeCor);

        dead.SetActive(true);
        original.SetActive(false);
        disguise.SetActive(false);
        invisible.SetActive(false);
    }
}