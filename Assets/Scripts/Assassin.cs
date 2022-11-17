using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ASSASSIN_STATE
{ 
    Walk,Ready,Attack,Die
}

public class AssaWalkState : BaseState<Assassin>
{ 
    public override void Enter(Assassin assassin)
    {
        assassin.animator.Play("Walk");
    }

    public override void Update(Assassin assassin)
    {

    }

    public override void Exit(Assassin assassin)
    {

    }
}

public class ReadyState : BaseState<Assassin>
{
    int spaceIndex;
    public override void Enter(Assassin assassin)
    {
        assassin.animator.Play("Stab");
        Time.timeScale = 0.1f;
        assassin.isAttack = true;
    }

    public override void Update(Assassin assassin)
    {
        if(assassin.isAttack)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                spaceIndex++;
            }
        }
        
        assassin.slider.value = spaceIndex;

        if(spaceIndex >= 15)
        {
            assassin.ChangeState(ASSASSIN_STATE.Attack);
        }
    }

    public override void Exit(Assassin assassin)
    {
        assassin.isAttack = false;
        spaceIndex = 0;
        assassin.slider.value = 0;
    }
}

public class AttackState : BaseState<Assassin>
{
    public override void Enter(Assassin assassin)
    {

    }

    public override void Update(Assassin assassin)
    {

    }

    public override void Exit(Assassin assassin)
    {

    }
}

public class DieState : BaseState<Assassin>
{
    public override void Enter(Assassin assassin)
    {

    }

    public override void Update(Assassin assassin)
    {

    }

    public override void Exit(Assassin assassin)
    {

    }
}



public class Assassin : MonoBehaviour
{
    public bool isAttack;
    public ASSASSIN_STATE assassinState;
    private Animator _animator;
    public Animator animator => _animator;
    [SerializeField] private Slider _slider;
    public Slider slider => _slider;

    public StateMachine<ASSASSIN_STATE, Assassin> assassinmachine = new StateMachine<ASSASSIN_STATE, Assassin>();



    private void Awake()
    {
        isAttack = false;
        _animator = GetComponent<Animator>();
        assassinmachine.Reset(this);
        assassinmachine.AddState(ASSASSIN_STATE.Walk, new AssaWalkState());
        assassinmachine.AddState(ASSASSIN_STATE.Ready, new ReadyState());
        assassinmachine.AddState(ASSASSIN_STATE.Attack, new AttackState());
        assassinmachine.AddState(ASSASSIN_STATE.Die, new DieState());
    }

    public void ChangeState(ASSASSIN_STATE nextState)
    {
        assassinState = nextState;
        assassinmachine.ChangeState(nextState);
    }

    public void StartStab()
    {
        ChangeState(ASSASSIN_STATE.Ready);
    }

    private void Update()
    {
        assassinmachine.Update();
    }
}
