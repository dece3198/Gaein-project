using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ASSASSIN_STATE
{ 
    Idle,Walk,Ready,Attack, Die
}

public class IdleState : BaseState<Assassin>
{
    public override void Enter(Assassin assassin)
    {
        assassin.animator.Play("IdleM");
    }

    public override void Update(Assassin assassin)
    {
        assassin.eyesight.FindTarget();
        if(assassin.eyesight.target != null)
        {
            assassin.ChangeState(ASSASSIN_STATE.Walk);
        }
    }

    public override void Exit(Assassin assassin)
    {

    }

}


public class AssaWalkState : BaseState<Assassin>
{ 
    public override void Enter(Assassin assassin)
    {
        assassin.animator.Play("Run");
    }

    public override void Update(Assassin assassin)
    {
        assassin.eyesight.FindTarget();
        assassin.eyesight.FindAtkTarget();
        if (assassin.eyesight.target != null)
        {
            assassin.navMeshAgent.SetDestination(assassin.player.gameObject.transform.position);
        }

        if (assassin.eyesight.atkTarget != null)
        {
            assassin.ChangeState(ASSASSIN_STATE.Ready);
        }
    }

    public override void Exit(Assassin assassin)
    {

    }
}

public class ReadyState : BaseState<Assassin>
{
    int spaceIndex;
    float time;
    public override void Enter(Assassin assassin)
    {
        assassin.navMeshAgent.ResetPath();
        assassin.animator.SetTrigger("Attack");
        Time.timeScale = 0.1f;
        assassin.isAttack = true;
        assassin.slider.gameObject.SetActive(true);
        assassin.timeSlider.gameObject.SetActive(true);
        assassin.timeSlider.value = assassin.timeSlider.maxValue;
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
        time += Time.deltaTime * 0.1f;
        assassin.timeSlider.value -= time;

        if(assassin.timeSlider.value <= 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Game");
        }
        if (spaceIndex >= 15)
        {
            assassin.player.ChangeState(PLAYER_STATE.Avoid);
            assassin.ChangeState(ASSASSIN_STATE.Attack);
            assassin.timeSlider.gameObject.SetActive(false);
        }
    }

    public override void Exit(Assassin assassin)
    {
        assassin.isAttack = false;
        assassin.slider.value = 0;
        time = 0;
        assassin.slider.gameObject.SetActive(false);
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
    [SerializeField] private PlayerState _player;
    public PlayerState player => _player;
    [SerializeField]
    private Eyesight _eyesight;
    public Eyesight eyesight => _eyesight;
    public bool isAttack;
    public ASSASSIN_STATE assassinState;
    private Animator _animator;
    public Animator animator => _animator;
    private NavMeshAgent _navMeshAgent;
    public NavMeshAgent navMeshAgent => _navMeshAgent;

    [SerializeField] private Slider _slider;
    public Slider slider => _slider;

    [SerializeField] private Slider _timeSlider;
    public Slider timeSlider => _timeSlider;

    public StateMachine<ASSASSIN_STATE, Assassin> assassinmachine = new StateMachine<ASSASSIN_STATE, Assassin>();



    private void Awake()
    {
        isAttack = false;
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _eyesight = GetComponent<Eyesight>();
        _slider.gameObject.SetActive(false);
        timeSlider.gameObject.SetActive(false);
        assassinmachine.Reset(this);
        assassinmachine.AddState(ASSASSIN_STATE.Idle, new IdleState());
        assassinmachine.AddState(ASSASSIN_STATE.Walk, new AssaWalkState());
        assassinmachine.AddState(ASSASSIN_STATE.Ready, new ReadyState());
        assassinmachine.AddState(ASSASSIN_STATE.Attack, new AttackState());
        assassinmachine.AddState(ASSASSIN_STATE.Die, new DieState());
        assassinmachine.ChangeState(ASSASSIN_STATE.Idle);
    }

    public void ChangeState(ASSASSIN_STATE nextState)
    {
        assassinState = nextState;
        assassinmachine.ChangeState(nextState);
    }
    private void Update()
    {
        if(player == null)
        {
            _player = FindObjectOfType<PlayerState>();
        }
        assassinmachine.Update();
    }
}
