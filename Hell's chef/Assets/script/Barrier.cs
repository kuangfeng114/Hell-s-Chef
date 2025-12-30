using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    private Camera cam;
    public float Dis = 2.0f;
    public int ClickNumber = 3;
    private Animator anim;
    public string clickTrigger = "Onclick";
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        if (Vector3.Distance(cam.transform.position,transform.position) <= Dis)
        {

            ClickNumber--;
            anim.ResetTrigger(clickTrigger);
            anim.SetTrigger(clickTrigger);
            if (ClickNumber == 0)
            {
                anim.SetTrigger("Ondestory");
                Destroy(gameObject);
            }
        }
    }
}
