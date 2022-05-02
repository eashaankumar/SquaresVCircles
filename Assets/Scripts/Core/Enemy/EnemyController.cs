using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D rigidBody;

    Player target;
    Vector2 input;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            target = FindObjectOfType<Player>();
            return;
        }
        input = (target.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = input * speed;
    }
}
