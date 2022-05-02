using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private CoroutineQueue actions;
    private PlayerController controller;

    private bool isReady = false;
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    #region Actions
    IEnumerator Prepare()
    {
        isReady = false;
        controller.enabled = false;
        SceneManager.singleton.SubscribeStartGame(OnStartGame);
        isReady = true;
        Debug.Log("Player is Ready");
        yield return CoroutineQueueResult.PASS;
        yield break;
    }
    #endregion

    #region delegates
    private void OnStartGame()
    {
        Debug.Log("Player starting game");
        controller.enabled = true;
    }
    #endregion

    #region public methods
    public void PreparePlayer()
    {
        actions.EnqueueAction(Prepare(), "PreparePlayer");
    }
    #endregion

    private void Awake()
    {
        actions = new CoroutineQueue(this);
        actions.StartLoop();

        controller = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        actions.StopLoop();
    }

}
