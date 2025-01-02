using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class NewZombieCtrl : MonoBehaviour
{
    [SerializeField]
    private Transform charcterBody;
    [SerializeField]
    private Transform cameraArm;

    private Rigidbody rigid;

    public int JumpPower;
    private bool IsJumping;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = charcterBody.GetComponent<Animator>();
        rigid = charcterBody.GetComponent<Rigidbody>();
        IsJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        Attack();
        Jump();
    }

    private void Move()
    {
        cameraArm.position = charcterBody.position;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        animator.SetBool("isMove", isMove);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            charcterBody.forward = moveDir; // 바라보는 방향
            transform.position += moveDir * Time.deltaTime * 5f; // 속도           
        }
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }
    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x , camAngle.z);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");           
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            if (!IsJumping)
            {
                rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                IsJumping = true;
            }
            else
            {
                return;
            }
        }
        Debug.Log("ang");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping = false;
        }
    }
}
