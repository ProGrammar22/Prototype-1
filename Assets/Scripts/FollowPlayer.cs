using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -9);

    // Update is called once per frame
    void LateUpdate()
    {
        //Offset the camera behind the player by adding to player's position
        transform.position = player.transform.position + offset;
    }
}