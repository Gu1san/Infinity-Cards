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

    //Shoot
    public Transform mira;
    public Transform refArma;


    void Start()
    {
    rb = GetComponent<Rigidbody2D>();
    tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
    //detectar o mause e mira
    mira.position = Camera.main.ScreenToWorldPoint(new Vector3( 
        Input.mousePosition.x,
        Input.mousePosition.y,
        -Camera.main.transform.position.z
    ));
    

    movmentDirection = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

    if(Input.GetKeyDown(KeyCode.Space) && canDash)
    {
        StartCoroutine(Dash());
    }

    if(Input.GetKeyDown(KeyCode.Mouse0)){
       Shoot();
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
    
    void Shoot(){
      RaycastHit2D hit = Physics2D.Raycast(refArma.position, (mira.position - refArma.position).normalized,1000f, ~(1<<6));
      if(hit.collider != null){
        if (hit.collider.gameObject.CompareTag("Enemy")){
        Debug.Log("hit");
        //Destroy(hit.collider.gameObject);
       }
      }
    }
}
