using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookingPlate : MonoBehaviour
{
    public bool InThePlace = false;
    private bool iscutting = false;
    private bool iscooking = false;

    private int cnt1 = 0;
    private int cnt2 = 0;

    private int cutnum = 0;

    private GameObject Newfood;
    private GameObject CookedFood;

    private float RecTime = 0f;
    public float TimeForCook = 0.5f; 


    public float FlyForce = 10f;

    [Header("MainCharacter引入")]
    public SimpleMove MC;

    [Header("食物（pieces）引入")]
    public GameObject Redmeat;

    [Header("食物生成位置引入")]
    public GameObject RedmeatPlace;

    [Header("食物入锅位置")]
    public GameObject IntoPotPlace;
    public float DT = 0.5f;



    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "RedMeat" && iscutting == false)
        {
            CookedFood = Redmeat;
            DealWithFood(3, Redmeat, RedmeatPlace);
            ClearEnteredFood(other.gameObject);
        }


    }

    private void OnMouseDown()
    {
        if (iscutting)
        {
            CutFood(Newfood);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (iscooking)
        {
            IntoPot(CookedFood);
        }
    }

    void CutFood(GameObject foodpieces)
    {
        Transform pieces = foodpieces.transform.GetChild(cnt1);
 //       pieces.SetParent(null);

        Rigidbody rb = pieces.GetComponent<Rigidbody>();
        LetFoodJump(rb);

        cnt1++;
        if(cnt1 == cutnum+1)
        {
            for(int i  = 0; i <= cutnum; i++)
            {
                Destroy(foodpieces.transform.GetChild(i).gameObject);
            }
            cnt1 = 0;
            iscutting = false;
            iscooking = true;
        }
    }

    void DealWithFood(int num, GameObject food, GameObject place)
    {
        InThePlace = true;
        iscutting = true;

        Newfood = Instantiate(food, place.transform.position, Quaternion.identity);
        cutnum = num;

    }

    void LetFoodJump(Rigidbody piece)
    {
        piece.isKinematic = false;
        Vector3 randomForce = new Vector3(
            UnityEngine.Random.Range(-3f, 3f),
            UnityEngine.Random.Range(5f, 8f),
            UnityEngine.Random.Range(-2f, 2f)
        );
        piece.AddForce(randomForce * FlyForce);
    }

    void ClearEnteredFood(GameObject food)
    {
        if (!MC.isholding)
        {
            Destroy(food);
        }
    }

    void IntoPot(GameObject Foodpieces)
    {
        /*
        if (Time.time - RecTime > TimeForCook)
        {
            Newfood = Instantiate(Foodpieces.transform.GetChild(cnt2).gameObject, IntoPotPlace.transform.position, Quaternion.identity);
           
            Rigidbody rb = Newfood.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            RecTime = Time.time;
            print(cnt2);
        }
        

        if (cnt2 == cutnum)
        {
            iscooking = false;
        }

        cnt2++;
        */
        Newfood = Instantiate(Foodpieces, IntoPotPlace.transform.position, Quaternion.identity);
        for(int i = 0;i < cutnum+1; i++)
        {
            Transform pieces = Newfood.transform.GetChild(i);
            Rigidbody rb = pieces.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            //施加一个初始的力不然可能会造成物理学混乱
            Vector3 Force = new Vector3(0, 10f, 0);
            rb.AddForce(Force);
            //重置速度角速度
            rb.velocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero;


        }
        iscooking = false;
    }
}
