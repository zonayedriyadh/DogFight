using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControll : MonoBehaviour
{
    #region Variable
    private Rigidbody rb;
    public Camera GameCamera;
    private int speed = 30;
    public VariableJoystick variableJoystick;

    private float currentAngle;
    private float nextAngleToMove;
    private int disAccpt;
    private bool isMovingChanged = false;
    private int life = 0;
    private float TurnSpeed = 0.05f;
    private int angleSpeed = 150;
    #endregion

    public static PlaneControll sharedInstance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sharedInstance = this;
        variableJoystick = JoystickPanel.sharedInstance.joystick;
    }

    public void setCamera(Camera gameCamera)
    {
        GameCamera = gameCamera;
        InitializeVariable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        SetAngle();
        if (variableJoystick.Vertical != 0 && variableJoystick.Horizontal != 0)
        {
            var angle = GetAngle();
            SetNextAngleToMove(angle);
            //transform.eulerAngles = new Vector3(0, angle, 0);

            /*Vector3 desireAngle = new Vector3(0, angle, 0);
            Vector3 smoothAngle = Vector3.Lerp(transform.eulerAngles, desireAngle, TurnSpeed);
            transform.eulerAngles = smoothAngle;*/

            //Debug.Log("angle-> "+angle);
        }

    }

    public void InitializeVariable()
    {
        currentAngle = 180;
        nextAngleToMove = 0;
        disAccpt = 5;
        isMovingChanged = false;
        life = 0;
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
                    if(Mathf.Abs(currentAngle - nextAngleToMove) >180)
                        direction = 1;
                }
                else
                {
                    direction = 1;
                    if (Mathf.Abs(currentAngle - nextAngleToMove) > 180)
                        direction = -1;
                }

               /* if (currentAngle <=45 && nextAngleToMove >= 180 + 45)
                    direction = -1;
                
                else if (currentAngle > 180 && nextAngleToMove < 0)
                    direction = 1;

                else if (currentAngle > 180+45 && nextAngleToMove < 45)
                    direction = 1;

                else if ((currentAngle > 45 && currentAngle < 90) && nextAngleToMove < 180+45)
                    direction = 1;*/

                currentAngle += direction * Time.deltaTime * angleSpeed;

                if (currentAngle > 270)
                    currentAngle = -90;
                else if (currentAngle < -90)
                    currentAngle = 270;

                //Debug.Log("current angle--> "+currentAngle);
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
        return -Mathf.Atan2(variableJoystick.Vertical - 0, variableJoystick.Horizontal - 0) * 180 / Mathf.PI + 90;
    }
}
