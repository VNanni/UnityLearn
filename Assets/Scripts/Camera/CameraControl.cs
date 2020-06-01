using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl camerainstance;

    public float cameraspeed;
    public Transform target;

    private void Awake()
    {

        camerainstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), cameraspeed * Time.deltaTime);

        }
    }

    public void ChangeTargetPos(Transform ntransform)
    {
        target = ntransform;
    }
}
