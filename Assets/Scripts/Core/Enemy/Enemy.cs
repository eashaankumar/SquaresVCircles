using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    public abstract void Pool();
    public abstract void UnPool();

}

public class Enemy : AbstractEnemy
{

    [SerializeField]
    private GameObject graphics;
    [SerializeField]
    private new GameObject collider;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
