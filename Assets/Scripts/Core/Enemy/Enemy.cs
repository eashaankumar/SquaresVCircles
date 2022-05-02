using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    public abstract void Pool();
    public abstract void UnPool();

}

[RequireComponent(typeof(EnemyController))]
public class Enemy : AbstractEnemy
{

    [SerializeField]
    private GameObject graphics;
    [SerializeField]
    private new GameObject collider;

    private CoroutineQueue actions;
    private EnemyController controller;

    private bool isReady = false;
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    public override void Pool()
    {
        this.graphics.SetActive(false);
        this.collider.SetActive(false);
        this.enabled = false;
    }

    public override void UnPool()
    {
        this.enabled = true;
        this.graphics.SetActive(true);
        this.collider.SetActive(true);
    }

    private void Awake()
    {
        actions = new CoroutineQueue(this);
        actions.StartLoop();
        controller = GetComponent<EnemyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Actions
    IEnumerator Prepare()
    {
        this.isReady = false;
        controller.enabled = false;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, 5));
        SceneManager.singleton.SubscribeStartGame(OnStartGame);
        this.isReady = true;
        Debug.Log("Enemy is Ready!");
        yield return CoroutineQueueResult.PASS;
        yield break;
    }
    #endregion

    #region delegates
    private void OnStartGame()
    {
        Debug.Log(gameObject.name + " starting game");
        controller.enabled = true;
    }

    #endregion

    #region public methods
    public void PrepareEnemy()
    {
        actions.EnqueueAction(Prepare(), "Prepare");
    }
    #endregion
}
