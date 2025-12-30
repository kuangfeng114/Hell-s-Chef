using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartMachine : MonoBehaviour
{
    [Header("摇把转动设置")]
    public float TurningTimeGo = 3.0f;
    public float TurningTimeBack = 1.0f;
    public float TurnAngle = 90.0f;
    public float NumOfHeart = 3f;
    public GameObject Heart;

    [Header("生成位置微调")]
    public float TouchDis = 5.0f;
    public GameObject ProducePlace;

    private float lasttime;
    private float lasttimeForHeart;
    private float cnt = 0f;
    private float CameraDis;
    private Camera maincamera;


    bool IsTurning = false;

    void Start()
    {
   //     Ptf = GetComponentInChildren<Transform>();
        
        maincamera = Camera.main;
    }

    void Update()
    {
        if (IsTurning)
        {
            StickTurn();
            ProduceHeart();
        }
         
        if(Time.time - (TurningTimeGo+ TurningTimeBack) > lasttime)
        {
            cnt = 0f;
            IsTurning = false;
        }

    }

    private void OnMouseDown()
    {
        CameraDis = Vector3.Distance(transform.position,maincamera.transform.position);
        if (CameraDis < TouchDis)
        {
            if (Time.time - (TurningTimeGo + TurningTimeBack)< lasttime)
            {
                 return;
            }
            else
            {
                 IsTurning = true;
                 lasttime = Time.time;
            }
        }
    }

    void StickTurn()
    {
        if (Time.time - TurningTimeGo < lasttime)
        {
            transform.Rotate(0, 0, Time.deltaTime / TurningTimeGo * TurnAngle);
        }
        else
        {
            transform.Rotate(0, 0, Time.deltaTime / TurningTimeBack * -TurnAngle);
        }
    }

    void ProduceHeart()
    {
        if (Time.time - (TurningTimeGo+TurningTimeBack)/NumOfHeart/2.0f < lasttimeForHeart || cnt == NumOfHeart)
        {
            return;
        }
        else
        {
            cnt++;
            Instantiate(Heart, ProducePlace.transform.position, Quaternion.identity);
            lasttimeForHeart = Time.time;
        }
    }
}
