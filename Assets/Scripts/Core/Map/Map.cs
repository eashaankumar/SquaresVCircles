using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map singleton;

    private bool isReady = false;
    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }
    private CoroutineQueue actions;


    private void Awake()
    {
        singleton = this;
        actions = new CoroutineQueue(this);
        actions.StartLoop();
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
        Player player = FindObjectOfType<Player>();
        player.PreparePlayer();
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            enemy.PrepareEnemy();
        }
        yield return new WaitUntil(() => player.IsReady && enemies.All((Enemy e) => e.IsReady));
        this.isReady = true;
        Debug.Log("Map is ready");
        yield return CoroutineQueueResult.PASS;
        yield break;
    }
    #endregion

    #region public methods
    public void PrepareMap()
    {
        isReady = false;
        actions.EnqueueAction(Prepare(), "Prepare");
    }
    #endregion

}
