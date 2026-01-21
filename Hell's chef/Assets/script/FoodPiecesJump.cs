using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPiecesJump : MonoBehaviour
{
    private Rigidbody rb;
    private bool IsGrounded;
    private float TimeCut;

    public float IsGDis = 0.5f;
    public LayerMask GroundMask;
    public float JumpCoolTime = 1.0f;
    public float JumoForce = 400f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectGrounded();
        if (Time.time - TimeCut < JumpCoolTime || !IsGrounded)
        {
            return;
        }
        else
        {
            Jump();
        }
    }

    void DetectGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, IsGDis, GroundMask.value))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }

    void Jump()
    {
        float x, y, z;

 //       x = Random.Range(-0.2f, 0.2f);
        y = Random.Range(0.25f, 0.5f);
 //       z = Random.Range(-0.2f, 0.2f);

        rb.AddForce(new Vector3(0, y, 0) * JumoForce);


        TimeCut = Time.time;
    }
}
