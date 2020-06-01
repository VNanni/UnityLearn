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

    public GameObject mybag;
    bool isopenbag = false;

    public GameObject MiniMap;
    bool isopenMiniMap = false;

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
        Openbag();
        OpenMiniMap();
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

    void Openbag()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isopenbag = !isopenbag;
            mybag.SetActive(isopenbag);
        }
        isopenbag = mybag.activeSelf;
    }

    void OpenMiniMap()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isopenMiniMap = !isopenMiniMap;
            MiniMap.SetActive(isopenMiniMap);
        }
    }

}
