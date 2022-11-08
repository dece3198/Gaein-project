using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public enum GUEST_STATE
{
    Walk,Sit,Order,Eat,Return
}


public abstract class BaseState : IGuestState
{
    public abstract void Enter(Guest guest);
    public abstract void Exit(Guest guest);
    public abstract void Update(Guest guest);
}

public class WalkState : BaseState
{
    public override void Enter(Guest guest)
    {
        guest.nav.SetDestination(guest.startPos.position);
    }

    public override void Exit(Guest guest)
    {
    }

    public override void Update(Guest guest)
    {
    }
}

public class SitState : BaseState
{
    public override void Enter(Guest guest)
    {
        guest.nav.ResetPath();
        guest.animator.Play("Sit");
        guest.ChangeState(new OrderState());
    }

    public override void Exit(Guest guest)
    {
    }

    public override void Update(Guest guest)
    {
    }
}

public class OrderState : BaseState
{
    public override void Enter(Guest guest)
    {
        int rand = Random.Range(0, guest.foodList.Count);
        FoodManager.instance.Cooking(guest.foodList[rand]);
        guest.canvas.gameObject.SetActive(true);
    }

    public override void Exit(Guest guest)
    {
    }

    public override void Update(Guest guest)
    {
        guest.canvas.gameObject.SetActive(false);
    }
}

public class EatState : BaseState
{
    public override void Enter(Guest guest)
    {
    }

    public override void Exit(Guest guest)
    {
    }

    public override void Update(Guest guest)
    {
    }
}

public class ReturnState : BaseState
{
    public override void Enter(Guest guest)
    {
    }

    public override void Exit(Guest guest)
    {
    }

    public override void Update(Guest guest)
    {
    }
}


public class Guest : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    public Transform startPos => _startPos;
    [SerializeField] private Canvas _canvas;
    public Canvas canvas => _canvas;

    public List<Food> menuList = new List<Food>();
    public List<Food> foodList = new List<Food>();

    private NavMeshAgent _nav;
    public NavMeshAgent nav => _nav;
    private Animator _animator;
    public Animator animator => _animator;

    BaseState curState = null;

    GUEST_STATE guestState;
    

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _canvas.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ChangeState(new WalkState());
    }

    private void Update()
    {
        curState.Update(this);
    }

    public void ChangeState(BaseState baseState)
    {
        if (curState != null)
            curState.Exit(this);
        curState = baseState;
        curState.Enter(this);
    }
}
