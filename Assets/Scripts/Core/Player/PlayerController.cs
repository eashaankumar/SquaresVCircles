using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D rigidBody;

    private Vector2 input;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized ;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = input * speed;
    }
}
