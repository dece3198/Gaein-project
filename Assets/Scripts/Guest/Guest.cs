using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Food;


public enum GUEST_STATE
{
    Walk,Sit,Order,Eat,DrinkOrder,DrinkEat,Return
}


public abstract class BaseState<T>
{
    public abstract void Enter(T guest);
    public abstract void Exit(T guest);
    public abstract void Update(T guest);
}

public class WalkState : BaseState<Guest>
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

public class SitState : BaseState<Guest>
{
    public override void Enter(Guest guest)
    {
        guest.nav.ResetPath();
        guest.animator.Play("Sit");
        guest.ChangeState(GUEST_STATE.Order);
    }

    public override void Exit(Guest guest)
    {
    }

    public override void Update(Guest guest)
    {
    }
}

public class OrderState : BaseState<Guest>
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

public class EatState : BaseState<Guest>
{
    int rand;
    public override void Enter(Guest guest)
    {
        guest.foodPos.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
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
            guest.ChangeState(GUEST_STATE.DrinkOrder);
        }
        else if(rand >= 5)
        {
            guest.ChangeState(GUEST_STATE.Return);
        }
    }
}

public class DrinkOrderState : BaseState<Guest>
{
    public override void Enter(Guest guest)
    {
        FoodManager.instance.EnterPool(guest.foodPos.transform.GetChild(0).GetComponent<FoodPickUp>().food.foodType, guest.foodPos.transform.GetChild(0).gameObject);
        guest.animator.Play("Order");
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

public class DrinkEatState : BaseState<Guest>
{
    int rand;
    public override void Enter(Guest guest)
    {
        guest.foodPos.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
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
        guest.ChangeState(GUEST_STATE.Return);
    }    
}


public class ReturnState : BaseState<Guest>
{
    public override void Enter(Guest guest)
    {
        guest.transform.parent = guest.parent;
        guest.animator.Play("Walk");
        FoodManager.instance.EnterPool(guest.foodPos.transform.GetChild(0).GetComponent<FoodPickUp>().food.foodType, guest.foodPos.transform.GetChild(0).gameObject);
        guest.nav.SetDestination(guest.returnPos.position);
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

    [SerializeField] private Transform _parent;
    public Transform parent => _parent;
    [SerializeField] private Transform _returnPos;
    public Transform returnPos => _returnPos;
    public GameObject foodPos;


    public List<Food> menuList = new List<Food>();
    public List<Food> foodList = new List<Food>();
    public List<Food> alcoholList = new List<Food>();

    private NavMeshAgent _nav;
    public NavMeshAgent nav => _nav;
    private Animator _animator;
    public Animator animator => _animator;



    [SerializeField] private GUEST_STATE _guestState;
    public GUEST_STATE guestState => _guestState;
    
    public StateMachine<GUEST_STATE, Guest> stateMachine;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _canvas.gameObject.SetActive(false);
        _foodImage.gameObject.SetActive(false);

        stateMachine = new StateMachine<GUEST_STATE, Guest>();
        stateMachine.Reset(this);

        stateMachine.AddState(GUEST_STATE.Walk, new WalkState());
        stateMachine.AddState(GUEST_STATE.Sit, new SitState());
        stateMachine.AddState(GUEST_STATE.Order, new OrderState());
        stateMachine.AddState(GUEST_STATE.Eat, new EatState());
        stateMachine.AddState(GUEST_STATE.DrinkOrder, new DrinkOrderState());
        stateMachine.AddState(GUEST_STATE.DrinkEat, new DrinkEatState());
        stateMachine.AddState(GUEST_STATE.Return, new ReturnState());
    }

    private void OnEnable()
    {
        stateMachine.ChangeState(GUEST_STATE.Walk);
    }

    public void ChangeState(GUEST_STATE nextState)
    {
        _guestState = nextState;
        stateMachine.ChangeState(nextState);
    }
}
