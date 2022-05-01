using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private CoroutineQueue actions;
    private PlayerController controller;

    #region Actions
    IEnumerator PreparePlayer()
    {
        controller.enabled = false;
        SceneManager.singleton.SubscribeStartGame(OnStartGame);
        SceneManager.singleton.PlayerReady = true;
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

    private void Awake()
    {
        actions = new CoroutineQueue(this);
        actions.StartLoop();

        controller = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        actions.EnqueueAction(PreparePlayer(), "PreparePlayer");
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
