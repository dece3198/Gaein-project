using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public enum GUEST_STATE
{
    Walk,Sit,Order,Eat,DrinkOrder ,Return
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
        guest.foodImage.gameObject.SetActive(true);
        guest.foodImage.sprite = guest.foodList[rand].foodImage;
    }

    public override void Exit(Guest guest)
    {
        guest.canvas.gameObject.SetActive(false);
        guest.foodImage.gameObject.SetActive(false);
        guest.foodImage.sprite = null;
    }

    public override void Update(Guest guest)
    {
    }
}

public class EatState : BaseState
{
    int rand;
    public override void Enter(Guest guest)
    {
        guest.animator.Play("Eat");
        guest.StartCoroutine(EatCo(guest));
    }

    public override void Exit(Guest guest)
    {
    }

    public override void Update(Guest guest)
    {
    }

    IEnumerator EatCo(Guest guest)
    {
        yield return new WaitForSeconds(15f);
        rand = Random.Range(0, 10);

        if(rand < 5)
        {
            guest.ChangeState(new DrinkOrderState());
        }
        else if(rand >= 5)
        {
            guest.ChangeState(new ReturnState());
        }
    }
}

public class DrinkOrderState : BaseState
{
    public override void Enter(Guest guest)
    {
        int rand = Random.Range(0, guest.alcoholList.Count);
        guest.canvas.gameObject.SetActive(true);
        guest.foodImage.gameObject.SetActive(true);
        guest.foodImage.sprite = guest.alcoholList[rand].foodImage;
        FoodManager.instance.Drink(guest.alcoholList[rand]);
    }

    public override void Exit(Guest guest)
    {
        guest.canvas.gameObject.SetActive(false);
        guest.foodImage.gameObject.SetActive(false);
        guest.foodImage.sprite = null;
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
    [SerializeField] private Image _foodImage;
    public Image foodImage => _foodImage;

    public List<Food> menuList = new List<Food>();
    public List<Food> foodList = new List<Food>();
    public List<Food> alcoholList = new List<Food>();

    private NavMeshAgent _nav;
    public NavMeshAgent nav => _nav;
    private Animator _animator;
    public Animator animator => _animator;

    BaseState curState = null;
    [SerializeField] private GUEST_STATE _guestState;
    public GUEST_STATE guestState => _guestState;
    

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _canvas.gameObject.SetActive(false);
        _foodImage.gameObject.SetActive(false);
        _guestState = GUEST_STATE.Walk;
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
