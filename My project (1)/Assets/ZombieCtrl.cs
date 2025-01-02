using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieCtrl : MonoBehaviour
{
    private float h = 0f;
    private float v = 0f;
    private Transform tr;
    public float speed = 10f;
    public float rotSpeed = 100f;
    private Animator animator;
    private Rigidbody rigid;
    public int JumpPower;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        //Camera.main.GetComponent<FollowCam>().targetTr = tr.Find("Cube").gameObject.transform;

        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * Time.deltaTime * speed);
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        if (moveDir.magnitude > 0)
        {
            animator.SetFloat("Speed", 1.0f);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
            Debug.Log("ButtonTest");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);                      
        }
    }
}

