using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustJump : MonoBehaviour
{
    public float jumpSpeed;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);
        player.GetComponent<Rigidbody>().velocity = Vector3.up * jumpSpeed;
    }
}
