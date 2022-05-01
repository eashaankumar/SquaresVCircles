using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private Player playerPrefab;
    [SerializeField]
    private TextMeshProUGUI countdown;

    private CoroutineQueue actions;
    public delegate void SceneEvent();
    private SceneEvent startGame;

    public static SceneManager singleton;

    private bool playerReady = false;
    public bool PlayerReady
    {
        set
        {
            playerReady = value;
        }
    }


    #region Actions
    IEnumerator PrepareSceneManager()
    {
        // clear delegates
        startGame = null;
        actions.EnqueueAction(PrepareSceneAction(), "PrepareSceneAction");
        yield return CoroutineQueueResult.PASS;
        yield break;
    }
    IEnumerator PrepareSceneAction()
    {
        Instantiate(playerPrefab);
        yield return new WaitUntil(() => playerReady && EnemyPoolManager.singleton.IsReady);
        yield return new WaitForEndOfFrame();
        actions.EnqueueAction(StartGame(), "StartGame");
        yield return CoroutineQueueResult.PASS;
        yield break;
    }


    IEnumerator StartGame()
    {
        string[] subsriberCount = { "Player", "Enemy Pool Manager" };
        yield return new WaitUntil(() => startGame.GetInvocationList().Length == subsriberCount.Length);
        for(int i = 0; i < 3; i++)
        {
            countdown.text = (3 - i) + "";
            yield return new WaitForSecondsRealtime(1);
        }
        countdown.text = "";
        Debug.Log("Start game");
        startGame();
        yield return CoroutineQueueResult.PASS;
    }

    IEnumerator TrackGame()
    {
        yield return null;
    }

    IEnumerator FinishGame()
    {
        yield return null;
    }
    #endregion

    #region Subscribers
    public void SubscribeStartGame(SceneEvent callback)
    {
        startGame += callback;
    }
    #endregion

    private void Awake()
    {
        singleton = this;
        actions = new CoroutineQueue(this);
        actions.StartLoop();
    }

    // Start is called before the first frame update
    void Start()
    {
        actions.EnqueueAction(PrepareSceneManager(), "PrepareSceneManager");
    }

    

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        startGame = null;
        actions.StopLoop();
    }
}
