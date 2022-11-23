using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PLAYER_STATE
{
    Idle, Avoid, Ready, Attack, Die
}

public class PlayerIdleState : BaseState<PlayerState>
{
    public override void Enter(PlayerState playerState)
    {
        playerState.animator.Play("Idle");
    }
    public override void Update(PlayerState playerState)
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            playerState.animator.SetBool("Walk", true);
        }
        else
        {
            playerState.animator.SetBool("Walk", false);
        }
    }

    public override void Exit(PlayerState playerState)
    {
        playerState.animator.SetBool("Walk", false);
    }

}

public class PlayerAvoidState : BaseState<PlayerState>
{
    public override void Enter(PlayerState playerState)
    {
        playerState.animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        playerState.animator.Play("Avoid");
    }
    public override void Update(PlayerState playerState)
    {
        if (playerState.animator.GetCurrentAnimatorStateInfo(0).IsName("IdleM"))
        {
            playerState.ChangeState(PLAYER_STATE.Ready);
        }
    }

    public override void Exit(PlayerState playerState)
    {
    }
}
public class PlayerReadyState : BaseState<PlayerState>
{
    KeyCode[] keys = new KeyCode[] { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };

    int rand;
    int success;
    int[] rands = new int[8];
    public override void Enter(PlayerState playerState)
    {
        Time.timeScale = 0.1f;
        for (int i = 0; i < 8; i++)
        {
            rand = Random.Range(0, keys.Length);
            rands[i] = rand;
            playerState.keyText[i].text = playerState.keyTextDic[keys[rand]];
        }
        playerState.keyUI.gameObject.SetActive(true);
        success = 0;
        for (int i = 0; i < playerState.keyText.Count; i++)
        {
            playerState.keyText[i].gameObject.SetActive(true);
        }
    }
    public override void Update(PlayerState playerState)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (!Input.GetKeyDown(keys[i]))
                continue;

            Debug.Log(playerState.keyTextDic[keys[i]] + " " + playerState.keyText[success].text);

            if (playerState.keyTextDic[keys[i]] != playerState.keyText[success].text)
            {
                playerState.ChangeState(PLAYER_STATE.Ready);
                return;
            }
            else
            {
                playerState.keyText[success].gameObject.SetActive(false);
                success++;
                Debug.Log(success);
            }
        }

        if (success >= 8)
        {
            playerState.ChangeState(PLAYER_STATE.Attack);
            
        }
    }

    public override void Exit(PlayerState playerState)
    {
        playerState.keyUI.gameObject.SetActive(false);
        success = 0;
    }

}

public class PlayerAtkState : BaseState<PlayerState>
{
    public override void Enter(PlayerState playerState)
    {
        playerState.mainCam.gameObject.SetActive(false);
        playerState.serveCam.gameObject.SetActive(true);
        playerState.animator.Play("Kick");
    }
    public override void Update(PlayerState playerState)
    {
        if (playerState.animator.GetCurrentAnimatorStateInfo(0).IsName("IdleM"))
        {
            playerState.mainCam.gameObject.SetActive(true);
            playerState.serveCam.gameObject.SetActive(false);
            playerState.ChangeState(PLAYER_STATE.Idle);
            playerState.StartCoroutine(Return(playerState));
        }
    }

    public override void Exit(PlayerState playerState)
    {
    }

    IEnumerator Return(PlayerState playerState)
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Game");
        GoldManager.Instance.Bronze += 50000;
    }
}

public class PlayerDieState : BaseState<PlayerState>
{
    public override void Enter(PlayerState playerState)
    {
    }
    public override void Update(PlayerState playerState)
    {
    }

    public override void Exit(PlayerState playerState)
    {
    }

}



public class PlayerState : MonoBehaviour
{
    [Header("적")]
    [SerializeField] private Assassin _assassin;
    public Assassin assassin => _assassin;
    [Header("카메라")]
    public Camera mainCam;
    public Camera serveCam;

    [Header("리스트들")]
    public List<TextMeshProUGUI> keyText = new List<TextMeshProUGUI>();
    public List<StartPoint> startPointsarr = new List<StartPoint>();

    [Header("기타")]
    public Image keyUI;

    [Header("현재상태")]
    public PLAYER_STATE playerState;

    public StateMachine<PLAYER_STATE, PlayerState> playerMachine = new StateMachine<PLAYER_STATE, PlayerState>();
    Dictionary<string, Vector3> startPositionDict = new Dictionary<string, Vector3>();
    public Dictionary<KeyCode, string> keyTextDic = new Dictionary<KeyCode, string>();

    private Animator _animator;
    public Animator animator => _animator;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        keyUI.gameObject.SetActive(false);
        serveCam.gameObject.SetActive(false);
        _animator = GetComponent<Animator>();
        playerMachine.Reset(this);
        playerMachine.AddState(PLAYER_STATE.Idle, new PlayerIdleState());
        playerMachine.AddState(PLAYER_STATE.Avoid, new PlayerAvoidState());
        playerMachine.AddState(PLAYER_STATE.Ready, new PlayerReadyState());
        playerMachine.AddState(PLAYER_STATE.Attack, new PlayerAtkState());
        playerMachine.AddState(PLAYER_STATE.Die, new PlayerDieState());
        ChangeState(PLAYER_STATE.Idle);

        keyTextDic.Add(KeyCode.Q, "Q");
        keyTextDic.Add(KeyCode.W, "W");
        keyTextDic.Add(KeyCode.E, "E");
        keyTextDic.Add(KeyCode.R, "R");
        keyTextDic.Add(KeyCode.A, "A");
        keyTextDic.Add(KeyCode.S, "S");
        keyTextDic.Add(KeyCode.D, "D");
        keyTextDic.Add(KeyCode.F, "F");
        SceneManager.sceneLoaded += SetPlayerPosition;
        StartPoints startPoints = GameObject.Find("StartPointsObj").GetComponent<StartPoints>();
        startPointsarr = startPoints.Points;

        // 정보가 가져와서 배열이나 Dic
        foreach (var point in startPointsarr)
        {
            startPositionDict.Add(point.posName, point.startPos);
        }
    }
    public void SetPlayerPosition(Scene scen, LoadSceneMode mode)
    {
        if (startPositionDict[scen.name] == null) {return; }
        transform.localPosition = startPositionDict[scen.name];
    }
    public void HitAnimation()
    {
        Time.timeScale = 1f;
        assassin.animator.Play("Blow");
    }

    private void Update()
    {
        if(assassin == null)
        {
            _assassin = FindObjectOfType<Assassin>();
        }
        playerMachine.Update();
    }

    public void ChangeState(PLAYER_STATE nextState)
    {
        playerState = nextState;
        playerMachine.ChangeState(nextState);
    }
    public void TimeScA()
    {
        Time.timeScale = 0.5f;
    }
    public void TimeScB()
    {
        Time.timeScale = 1f;
    }



}
