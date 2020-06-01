using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{

    public bool room_T=false, room_B = false, room_L = false, room_R = false;
    public bool room_T_1 = false, room_B_1 = false, room_L_1 = false, room_R_1 = false;

    public GameObject door_U, door_D, door_L, door_R;

    public int roomnumber;

    public int doorcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //7door_U.SetActive(room_T);
        //door_D.SetActive(room_B);
        //door_L.SetActive(room_L);
        //door_R.SetActive(room_R);
    }

    public void UpdateRoomNum(float xoffset, float yoffset)
    {
        roomnumber = (int)(Mathf.Abs(transform.position.x) / xoffset + Mathf.Abs(transform.position.y) / yoffset);

        if (room_T)
            doorcount += 1;
        if (room_B)
            doorcount += 1;
        if (room_L)
            doorcount += 1;
        if (room_R)
            doorcount += 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraControl.camerainstance.ChangeTargetPos(transform);
            door_U.SetActive(room_T);
            door_D.SetActive(room_B);
            door_L.SetActive(room_L);
            door_R.SetActive(room_R);
        }
    }

    public void SetFirst()
    {
        door_U.SetActive(room_T);
        door_D.SetActive(room_B);
        door_L.SetActive(room_L);
        door_R.SetActive(room_R);
    }

    public void SetBehind()
    {
        door_U.SetActive(room_T_1);
        door_D.SetActive(room_B_1);
        door_L.SetActive(room_L_1);
        door_R.SetActive(room_R_1);
    }
}
