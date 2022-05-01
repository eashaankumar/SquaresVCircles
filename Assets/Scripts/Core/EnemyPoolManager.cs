using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField]
    private EnemyPool basicEnemyPoolPrefab;

    private EnemyPool pool;

    private bool isReady = false;
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    public static EnemyPoolManager singleton;
    private CoroutineQueue actions;

    #region Actions
    IEnumerator SetupEnemyPools()
    {
        isReady = false;
        pool = Instantiate(basicEnemyPoolPrefab);
        yield return new WaitUntil(() => pool.IsReady);
        SceneManager.singleton.SubscribeStartGame(OnStartGame);
        isReady = true;
        yield return CoroutineQueueResult.PASS;
        yield break;
    }
    #endregion

    #region Delegates
    private void OnStartGame()
    {
        if (!pool) throw new System.Exception("No enemy pool found!");
        Debug.Log("EnemyPoolManager start game");
        
    }
    #endregion
    
    private void Awake()
    {
        singleton = this;
        actions = new CoroutineQueue(this);
        actions.StartLoop();
    }

    private void Start()
    {
        actions.EnqueueAction(SetupEnemyPools(), "SetupEnemyPools");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        actions.StopLoop();
    }

}
