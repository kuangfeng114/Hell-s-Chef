using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodBox : MonoBehaviour
{
    private Camera maincamera;
    public GameObject RedMeat;
    public float ProduceDis;

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
        Instantiate(RedMeat, maincamera.transform.position + maincamera.transform.forward * ProduceDis, Quaternion.identity);
    }
}
