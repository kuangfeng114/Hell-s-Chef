using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    private Rigidbody rb;
    private Camera MainCamera;
    
    private float detectR = 0.4f;
    private float foodturnspeed = 100f;

    [Header("食物箱")]
    public LayerMask DetectFoodBoxMask;
    private bool FromFoodBox = false;

    [Header("人物和视角移动")]
    public float movespeed = 2f;
    public float runspeed = 5f;
    public float turnspeed = 1f;
    Vector3 moveset;

    [Header("捡拾食物")]
    public LayerMask DetectFoodMask;
    public LayerMask DetectWallMask;
    public float pickupdis = 3f;
    public float holddis = 4f;

    [Header("扔出食物")]
    public float throwstrength = 250f;
    bool isholding = false;
    private Rigidbody itemrb;
    private Transform itemtf;

    [Header("食物靠近墙壁")]
    public float detectr = 0.3f;

    private float throwspeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MainCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        move();
        applymove();
        visionturn();
    }

    private void LateUpdate()
    {
        DetectFood();
    }

    void visionturn()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up , x * turnspeed);

        float TargetAngle = MainCamera.transform.localEulerAngles.x - y;
        if (TargetAngle > 60 && TargetAngle < 330)
        {
            return;
        }

        MainCamera.transform.Rotate(Vector3.right , -y * turnspeed);
    }

    void move()
    {
        float hor = Input.GetAxis("Horizontal");
        float vct = Input.GetAxis("Vertical");

        moveset = Vector3.zero;
        if(hor != 0)
        {
            moveset += transform.right * hor * movespeed;
        }
        if(vct != 0)
        {
            moveset += transform.forward * vct * movespeed;
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? runspeed : movespeed;
        moveset *= speed;

        Changethrowspeed(vct,hor);

    }

    void Changethrowspeed(float vct, float hor)
    {
        if (hor == 0 && vct == 0)
        {
            throwspeed = 1;
        }
        else
        {
            throwspeed = Input.GetKey(KeyCode.LeftShift) ? runspeed / 2.5f : movespeed / 1.5f;
        }
    }

    void applymove()
    {
        rb.MovePosition(transform.position + moveset * Time.fixedDeltaTime);
    }

    void DetectFood()
    {
        if (isholding)
        {
            DealWithHolding();
        }
        else
        {
            DetectingObject();
        }
    }

    void DetectingObject()
    {

        RaycastHit hitInfo;

        if (Physics.SphereCast(MainCamera.transform.position, detectR, MainCamera.transform.forward, out hitInfo, pickupdis, DetectFoodMask.value))
        {
            itemrb = hitInfo.rigidbody;
            itemtf = hitInfo.transform;
            DealWithItem(); 
        }
        
        if (Physics.SphereCast(MainCamera.transform.position, detectR, MainCamera.transform.forward, out hitInfo, pickupdis, DetectFoodBoxMask.value))
        {
            DealWithItemFromFoodBox();
        }

    }

    void DealWithItemFromFoodBox()
    {
        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0))
        {
            FromFoodBox = true;
            if (Physics.SphereCast(MainCamera.transform.position, detectR, MainCamera.transform.forward, out hitInfo, pickupdis, DetectFoodMask.value))
            {
                itemrb = hitInfo.rigidbody;
                itemtf = hitInfo.transform;
                DealWithItem();
            }
        }
    }

    void DealWithItem()
    {
        if (Input.GetMouseButtonDown(0) || FromFoodBox)
        {
            isholding = true;
            FromFoodBox = false;

            itemtf.SetParent(MainCamera.transform,true);

            itemtf.rotation = Quaternion.identity;
            itemrb.isKinematic = true;

            Vector3 targetPosition = MainCamera.transform.position + MainCamera.transform.forward * holddis;
            itemtf.position = targetPosition;

        }
    }

    float DetectingWall()
    {
        float dis = holddis;


        if (Physics.SphereCast(MainCamera.transform.position, detectr, MainCamera.transform.forward,out RaycastHit wallHit, holddis, DetectWallMask.value))
        {
            dis = wallHit.distance;
        }

        return dis;
    }

    void DealWithHolding()
    {

        itemtf.Rotate(Vector3.up, foodturnspeed*Time.deltaTime,Space.World);
        Vector3 targetPosition = MainCamera.transform.position + MainCamera.transform.forward * DetectingWall();
        itemtf.position = targetPosition;

        if (Input.GetMouseButtonDown(1))
        {
            isholding = false;
            itemrb.isKinematic = false;
            itemtf.SetParent(null);

            Vector3 ThrowDireaction = MainCamera.transform.forward;
            ThrowDireaction.y += 0.75f;
            ThrowDireaction.z *= throwspeed;
            ThrowDireaction.x *= throwspeed;
            itemrb.AddForce(ThrowDireaction * throwstrength);
        }
        else if(!Input.GetMouseButton(0)) 
        {
            isholding = false;
            itemrb.isKinematic = false;
            itemtf.SetParent(null);
        }
    }
}
