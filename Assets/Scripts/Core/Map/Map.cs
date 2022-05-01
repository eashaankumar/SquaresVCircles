using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private SpawnData spawnData;

    public static Map singleton;

    private void Awake()
    {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(Player prefab)
    {
        CameraFollow cam = FindObjectOfType<CameraFollow>();
        cam.transform.position = new Vector3(spawnData.playerSpawnPosition.position.x, spawnData.playerSpawnPosition.position.y, cam.transform.position.z);
        Instantiate(prefab, spawnData.playerSpawnPosition.position, Quaternion.identity);
    }

}

[Serializable]
 public struct SpawnData
{
    public Transform playerSpawnPosition;
}