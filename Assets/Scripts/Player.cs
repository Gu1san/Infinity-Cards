using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    public float speed = 6.0f;
    Rigidbody rb;

    //Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 100f;
    private float dashingTime = 1f;
    private float dashingCooldown = 1f;
    TrailRenderer tr;

    void Start()
    {
    rb = GetComponent<Rigidbody>();
    tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
    float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime ;
    float v = Input.GetAxis("Vertical") * speed * Time.deltaTime ;
    Vector3 dir = new Vector3(h, rb.velocity.y, v);
    rb.velocity = dir * speed;
    if(Input.GetKeyDown(KeyCode.Space) && canDash)
    {
        StartCoroutine(Dash());
    }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.useGravity = false;
        rb.velocity = new Vector3(transform.localScale.x * dashingPower,0f,0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
