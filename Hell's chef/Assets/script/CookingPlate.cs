using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPlate : MonoBehaviour
{
    public bool InThePlace = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RedMeat")
        {
            InThePlace = true;
            print("Play the Animation");
        }
        

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
