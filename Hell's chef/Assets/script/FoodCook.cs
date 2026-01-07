using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Food : MonoBehaviour

{

    private CookingPlate CP;
    private SimpleMove MC;

    public void InitializeFood(SimpleMove playerRef, CookingPlate plateRef)
    {
        MC = playerRef;
        CP = plateRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
