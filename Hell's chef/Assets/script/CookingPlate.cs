using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPlate : MonoBehaviour
{
    public bool InThePlace = false;
    private bool iscooking = false;
    private int cnt = 0;
    private int cutnum = 0;
    private GameObject Newfood;
    public float FlyForce = 10f;

    [Header("MainCharacter引入")]
    public SimpleMove MC;

    [Header("食物（pieces）引入")]
    public GameObject Redmeat;

    [Header("食物生成位置引入")]
    public GameObject RedmeatPlace;

    [Header("食物入锅位置")]
    public GameObject IntoPotPlace;



    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "RedMeat" && iscooking == false)
        {
            DealWithFood(3, Redmeat, RedmeatPlace);
            ClearEnteredFood(other.gameObject);
        }


    }

    private void OnMouseDown()
    {
        if (iscooking == true)
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
        
    }

    void CutFood(GameObject foodpieces)
    {
        Transform pieces = foodpieces.transform.GetChild(cnt);
 //       pieces.SetParent(null);

        Rigidbody rb = pieces.GetComponent<Rigidbody>();
        LetFoodJump(rb);

        cnt++;
        if(cnt == cutnum+1)
        {
            for(int i  = 0; i <= cutnum; i++)
            {
                Destroy(foodpieces.transform.GetChild(i).gameObject);
            }
            print("intopot");
            cnt = 0;
            iscooking = false;
        }
    }

    void DealWithFood(int num, GameObject food, GameObject place)
    {
        InThePlace = true;
        iscooking = true;

        Newfood = Instantiate(food, place.transform.position, Quaternion.identity);
        cutnum = num;

    }

    void LetFoodJump(Rigidbody piece)
    {
        piece.isKinematic = false;
        Vector3 randomForce = new Vector3(
            Random.Range(-3f, 3f),
            Random.Range(5f, 8f),
            Random.Range(-2f, 2f)
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
}
