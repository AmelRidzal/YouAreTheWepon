using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    public float rotationSpeed = 3;
    public float moveSpeed = 1;
    public bool isMoving=false;
    public bool moveDirection=false;
    public GameObject pointA;
    public GameObject pointB;


    private void Start()
    {
        if(isMoving==true)
        {
            this.transform.position=pointA.transform.position;
        }
    }
    void FixedUpdate()
    {
        if (moveDirection)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, pointA.transform.position, moveSpeed);
            if(this.transform.position==pointA.transform.position) { moveDirection = false; }
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, pointB.transform.position, moveSpeed);
            if (this.transform.position == pointB.transform.position) { moveDirection = true; }
        }
        
        this.transform.localEulerAngles = new Vector3(0f, 0f , this.transform.localEulerAngles.z+rotationSpeed);
    }
}
