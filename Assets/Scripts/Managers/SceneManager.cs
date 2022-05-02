using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countdown;
   

    private CoroutineQueue actions;
    public delegate void SceneEvent();
    private SceneEvent startGame;

    public static SceneManager singleton;



    #region Actions
    IEnumerator PrepareSceneAction()
    {
        startGame = null;
        Map.singleton.PrepareMap();
        yield return new WaitUntil(() => Map.singleton.IsReady);
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 3; i++)
        {
            countdown.text = (3 - i) + "";
            yield return new WaitForSecondsRealtime(1);
        }
        countdown.text = "";
        Debug.Log("Start game");
        startGame();
        yield return CoroutineQueueResult.PASS;
        yield break;
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
        actions.EnqueueAction(PrepareSceneAction(), "PrepareSceneAction");
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

