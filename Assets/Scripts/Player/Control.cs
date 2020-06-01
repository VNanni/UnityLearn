using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;

    public float speed;

    Vector2 movement;

    int xdirectid;
    int ydirectid;
    int speedid;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        xdirectid = Animator.StringToHash("xdirect");
        ydirectid = Animator.StringToHash("ydirect");
        speedid = Animator.StringToHash("speed");
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Switchanim();
    }

    void Switchanim()
    {
        if (movement != Vector2.zero)
        {
            anim.SetFloat(xdirectid, movement.x);
            anim.SetFloat(ydirectid, movement.y);
        }

        anim.SetFloat(speedid, movement.magnitude);
    }
}
