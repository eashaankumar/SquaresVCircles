using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    private AbstractEnemy basicEnemyPrefab;
    [SerializeField]
    private int poolCount = 100;

    private CoroutineQueue actions;

    private bool isReady;
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    private Stack<AbstractEnemy> pool;

    #region Actions
    IEnumerator CreatePool()
    {
        isReady = false;
        for(int i = 0; i < poolCount; i++)
        {
            AbstractEnemy enemy = Instantiate(basicEnemyPrefab);
            enemy.Pool();
            enemy.transform.SetParent(transform);
            pool.Push(enemy);
            yield return new WaitForEndOfFrame();
        }
        isReady = true;
        yield return CoroutineQueueResult.PASS;
        yield break;
    }
    #endregion

    #region External state changers
    public void StartGame()
    {

    }
    #endregion


    private void Awake()
    {
        pool = new Stack<AbstractEnemy>();
        actions = new CoroutineQueue(this);
        actions.StartLoop();
    }

    // Start is called before the first frame update
    void Start()
    {
        actions.EnqueueAction(CreatePool(), "CreatePool");
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
