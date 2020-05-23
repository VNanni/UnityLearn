using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerate : MonoBehaviour
{

    public enum Direction {UP, DOWN, LEFT, RIGHT };
    public Direction roomdirection; 

    [Header("房间信息")]
    public GameObject roombase;
    public int roomcount;
    public Color startcolor, endcolor;
    public LayerMask roomlayer;
    public Rooms endroom;

    [Header("位置信息")]
    public Transform roomcenter;
    public float xoffset, yoffset;


    public List<Rooms> rooms = new List<Rooms>();
    private int maxroomnum = 0;
    public List<Rooms> LastRooms = new List<Rooms>();

    public WallType walltype;

    void Start()
    {
        for (int i = 0; i < roomcount; i++)
        {
            rooms.Add(Instantiate(roombase, roomcenter.position, Quaternion.identity).GetComponent<Rooms>());

            // Change Point
            ChangePoint();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startcolor;
        foreach (var room in rooms)
        {
            // Set Door whether Visualable
            SetDoorState(room, room.transform.position);

            // Set Room Number and Door Count
            room.UpdateRoomNum(xoffset, yoffset);
        }

        // Generate Last Room
        GetFarRoom();

        rooms[rooms.Count-1].GetComponent<SpriteRenderer>().color = endcolor;

        // Set Wall
        SetWall();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangePoint()
    {
        do
        {
            roomdirection = (Direction)Random.Range(0, 4);
            switch (roomdirection)
            {
                case Direction.UP:
                    roomcenter.position += new Vector3(0, yoffset, 0);
                    break;
                case Direction.DOWN:
                    roomcenter.position += new Vector3(0, -yoffset, 0);
                    break;
                case Direction.LEFT:
                    roomcenter.position += new Vector3(-xoffset, 0, 0);
                    break;
                case Direction.RIGHT:
                    roomcenter.position += new Vector3(xoffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(roomcenter.position, 0.2f, roomlayer));

    }

    public void SetDoorState(Rooms room, Vector3 position)
    {
        room.room_T = Physics2D.OverlapCircle(position + new Vector3(0, yoffset, 0), 0.2f, roomlayer);
        room.room_B = Physics2D.OverlapCircle(position + new Vector3(0, -yoffset, 0), 0.2f, roomlayer);
        room.room_L = Physics2D.OverlapCircle(position + new Vector3(-xoffset, 0, 0), 0.2f, roomlayer);
        room.room_R = Physics2D.OverlapCircle(position + new Vector3(xoffset, 0, 0), 0.2f, roomlayer);
    }

    public void GetFarRoom()
    {
        foreach (var room in rooms) 
        {
            if(maxroomnum < room.roomnumber)
            {
                maxroomnum = room.roomnumber;
            }
        }
        foreach (var room in rooms)
        {
            if (room.roomnumber == maxroomnum)
            {
                LastRooms.Add(room);
            }
        }

        endroom = LastRooms[Random.Range(0, LastRooms.Count)];

        // Get the direction of no door and Generate the last room
        if (!endroom.room_T)
        {
            endroom.room_T = true;
            endroom = Instantiate(roombase, endroom.transform.position + new Vector3(0, yoffset, 0), Quaternion.identity).GetComponent<Rooms>();
            endroom.room_B = true;
        }
        else if (!endroom.room_B)
        {
            endroom.room_B = true;
            endroom = Instantiate(roombase, endroom.transform.position + new Vector3(0, -yoffset, 0), Quaternion.identity).GetComponent<Rooms>();
            endroom.room_T = true;
        }
        else if (!endroom.room_L)
        {
            endroom.room_L = true;
            endroom = Instantiate(roombase, endroom.transform.position + new Vector3(-xoffset, 0, 0), Quaternion.identity).GetComponent<Rooms>();
            endroom.room_R = true;
        }
        else if (!endroom.room_R)
        {
            endroom.room_R = true;
            endroom = Instantiate(roombase, endroom.transform.position + new Vector3(xoffset, 0, 0), Quaternion.identity).GetComponent<Rooms>();
            endroom.room_L = true;
        }

        // Update the room info and Add the last room to rooms List
        endroom.doorcount = 1;
        endroom.roomnumber = maxroomnum + 1;
        rooms.Add(endroom);
    }

    public void SetWall()
    {
        foreach (var room in rooms)
        {
            switch (room.doorcount)
            {
                case 1:
                    if (room.room_T)
                        Instantiate(walltype.Wall_U, room.transform.position, Quaternion.identity);
                    else if (room.room_B)
                        Instantiate(walltype.Wall_D, room.transform.position, Quaternion.identity);
                    else if (room.room_L)
                        Instantiate(walltype.Wall_L, room.transform.position, Quaternion.identity);
                    else if (room.room_R)
                        Instantiate(walltype.Wall_R, room.transform.position, Quaternion.identity);
                    break;
                case 2:
                    if (room.room_T&&room.room_L)
                        Instantiate(walltype.Wall_UL, room.transform.position, Quaternion.identity);
                    else if (room.room_T && room.room_R)
                        Instantiate(walltype.Wall_UR, room.transform.position, Quaternion.identity);
                    else if (room.room_T && room.room_B)
                        Instantiate(walltype.Wall_UD, room.transform.position, Quaternion.identity);
                    else if (room.room_L && room.room_R)
                        Instantiate(walltype.Wall_LR, room.transform.position, Quaternion.identity);
                    else if (room.room_L && room.room_B)
                        Instantiate(walltype.Wall_LD, room.transform.position, Quaternion.identity);
                    else if (room.room_R && room.room_B)
                        Instantiate(walltype.Wall_RD, room.transform.position, Quaternion.identity);
                    break;
                case 3:
                    if (room.room_T&&room.room_L&&room.room_R)
                        Instantiate(walltype.Wall_ULR, room.transform.position, Quaternion.identity);
                    else if (room.room_T && room.room_L && room.room_B)
                        Instantiate(walltype.Wall_ULD, room.transform.position, Quaternion.identity);
                    else if (room.room_T && room.room_R && room.room_B)
                        Instantiate(walltype.Wall_URD, room.transform.position, Quaternion.identity);
                    else if (room.room_L && room.room_R && room.room_B)
                        Instantiate(walltype.Wall_LRD, room.transform.position, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(walltype.Wall_ULRD, room.transform.position, Quaternion.identity);
                    break;
            }
        }
    }

}


[System.Serializable]
public class WallType
{
    public GameObject Wall_U, Wall_D, Wall_L, Wall_R,
                      Wall_UL, Wall_UR, Wall_UD, Wall_LR, Wall_LD, Wall_RD,
                      Wall_ULD, Wall_URD, Wall_ULR, Wall_LRD,
                      Wall_ULRD;
}
