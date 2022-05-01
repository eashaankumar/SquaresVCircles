using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    private Transform target;
    private Vector3 position;
    private Coroutine update;

    // Start is called before the first frame update
    void Start()
    {
        update = StartCoroutine(UpdateCamera());
    }

    IEnumerator UpdateCamera()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!target)
            {
                Player p = GameObject.FindObjectOfType<Player>();
                if (p)
                {
                    target = p.transform;
                }
            }
            else
            {
                position = target.transform.position;
                position.z = -10f;
                transform.position = position;
            }
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(update);
    }
}
