using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6.0f;
    Rigidbody rb;
    void Start() {
    rb = GetComponent<Rigidbody>();
    }
    void Update() {
    float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime ; ;
    float v = Input.GetAxis("Vertical") * speed * Time.deltaTime ; ;
    Vector3 dir = new Vector3(h, rb.velocity.y, v);
    rb.velocity = dir * speed;
    }
}
