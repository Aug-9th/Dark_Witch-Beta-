using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerposition;

    private void Update()
    {
        playerposition = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = new Vector3(playerposition.position.x, playerposition.position.y, transform.position.z);
    }
}
