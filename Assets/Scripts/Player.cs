using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    public float        speed = 6.0f;
    private Rigidbody2D rb;
    private Vector2     movmentDirection;

    //Dash
    private bool  canDash = true;
    private bool  isDashing;
    private float dashingPower = 100f;
    private float dashingTime = 2f;
    private float dashingCooldown = 1f;
    TrailRenderer tr;

    void Start()
    {
    rb = GetComponent<Rigidbody2D>();
    tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
    /*float h = Input.GetAxis("Horizontal") * speed * Time.deltaTime ;
    float v = Input.GetAxis("Vertical") * speed * Time.deltaTime ;
    Vector3 dir = new Vector3(h, rb.velocity.y, v);
    rb.velocity = dir * speed;*/
    movmentDirection = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

    if(Input.GetKeyDown(KeyCode.Space) && canDash)
    {
        StartCoroutine(Dash());
    }
    }
    void FixedUpdate(){
        rb.MovePosition(rb.position + movmentDirection * speed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower,0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
