using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodBox : MonoBehaviour
{

    private Camera maincamera;
    public GameObject RedMeat;
    public float ProduceDis;

    [Header("获取isholding参数")]

    public SimpleMove MC;

    [Header("获取InThePlace参数")]

    public CookingPlate CP;

    void Start()
    {
        maincamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    private void OnMouseDown()
    {
        GameObject newfood = Instantiate(RedMeat, maincamera.transform.position + maincamera.transform.forward * ProduceDis, Quaternion.identity);
        Food food = newfood.GetComponent<Food>();
            //调用food脚本中的InitializeFood方法
        food.InitializeFood(MC, CP);  
    }
}
