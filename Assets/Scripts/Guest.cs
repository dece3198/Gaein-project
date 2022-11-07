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
    }

    public override void Exit(Guest guest)
    {
    }

    public override void Update(Guest guest)
    {
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
    public Transform startPos { get { return _startPos; } }
    [SerializeField] NavMeshAgent _nav;
    public NavMeshAgent nav { get { return _nav; } }
    BaseState curState = null;
    GUEST_STATE guestState;
    

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
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
