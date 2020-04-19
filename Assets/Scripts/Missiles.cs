using System.Collections;
using System;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    #region Variable
    private Rigidbody rb;
    public GameObject Plane;
    public VariableJoystick variableJoystick;
    public int missileNo;


    float speed;
    float angleSpeed;
    int rocketNo;
    bool isCollide;
    float currentAngle;
    float nextAngleToMove;
    bool isMovingChanged;
    float disAccpt;
    float lifeTime;
    float deathTime;
    bool isActive;
    Vector3 lastPoint;
    float traceLength;

    bool isIndicationOn;
    bool isIndicationAdded;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // Plane = PlaneControll.sharedInstance.gameObject;
        InitializeVariable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        SetAngle();
        //if (variableJoystick.Vertical != 0 && variableJoystick.Horizontal != 0)
        {
            var angle = GetAngle();
            SetNextAngleToMove(angle);
            //transform.eulerAngles = new Vector3(0, angle, 0);

            /*Vector3 desireAngle = new Vector3(0, angle, 0);
            Vector3 smoothAngle = Vector3.Lerp(transform.eulerAngles, desireAngle, TurnSpeed);
            transform.eulerAngles = smoothAngle;*/

           // Debug.Log("angle-> " + angle);
        }

    }

    public void InitializeVariable()
    {
        System.Random rand = new System.Random();
        speed = 34;
        //Debug.Log("Random speed --> "+ speed); 
        disAccpt = 5;
        angleSpeed = 140 ;
        isMovingChanged = false;
        currentAngle = 180;
        nextAngleToMove = 0;
        lifeTime = 0;
        deathTime = 2000 / speed;
        lastPoint = gameObject.transform.position;
        isCollide = false;
        isIndicationOn = false;
        isIndicationAdded = false;
        isActive = true;
        transform.eulerAngles = new Vector3(0, currentAngle, 0);
    
    }

    public void SetAngle()
    {
        if (isMovingChanged)
        {
            float dtAngle = Mathf.Abs(currentAngle - nextAngleToMove);
            if (dtAngle < disAccpt)
            {
                currentAngle = nextAngleToMove;
                SetRotation(nextAngleToMove);
                isMovingChanged = false;
            }
            else
            {
                int direction = 1;
                if (currentAngle > nextAngleToMove)
                {
                    direction = -1;
                    if (Mathf.Abs(currentAngle - nextAngleToMove) > 180)
                        direction = 1;
                }
                else
                {
                    direction = 1;
                    if (Mathf.Abs(currentAngle - nextAngleToMove) > 180)
                        direction = -1;
                }


                currentAngle += direction * Time.deltaTime * angleSpeed;

                if (currentAngle > 270)
                    currentAngle = -90;
                else if (currentAngle < -90)
                    currentAngle = 270;

                Debug.Log("current angle--> " + currentAngle);
                SetRotation(currentAngle);
            }
        }
    }

    public void SetRotation(float angle)
    {
        transform.eulerAngles = new Vector3(0, angle, 0);
    }

    public void SetNextAngleToMove(float angle)
    {
        float dtAngle = Mathf.Abs(currentAngle - angle);
        if (dtAngle < disAccpt)
        {
            isMovingChanged = false;
        }
        else
        {
            nextAngleToMove = angle;
            isMovingChanged = true;
        }
    }

    public float GetAngle()
    {
        return -Mathf.Atan2(Plane.gameObject.transform.position.z - this.gameObject.transform.position.z, Plane.gameObject.transform.position.x - this.gameObject.transform.position.x) * 180 / Mathf.PI +90;
    }
}
