using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private bool isGrounded;
    private Vector3 _moveDir;



    [SerializeField, Range(1, 20)] private float mouseSensX;
    [SerializeField, Range(1, 20)] private float mouseSensY;
    
    [SerializeField, Range(-90, 0)] private float minViewAngle;
    [SerializeField, Range(0, 90)] private float maxViewAngle;

    [SerializeField] private Transform lookAtPoint;


    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float bulletForce;

    private Vector2 currentRotation;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        
        InputMang.Init(this);
        
        InputMang.GameMode();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (_moveDir * speed * Time.deltaTime);
        CheckGround();

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot");
        }
    }


    public void SetMovementDirection (Vector3 newDirection)
    {
        _moveDir = newDirection;
    }



    public void Jump()
    {
        Debug.Log("Jumping");
        if (isGrounded)
        {
            Debug.Log("Am grounded");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.size.y);
        Debug.DrawRay(transform.position, Vector3.down * GetComponent<Collider>().bounds.size.y, Color.magenta, duration:0, depthTest:false);
    }


    public void SetLookDirection(Vector2 readValue)
    {
        //controls rotation angles
        currentRotation.x += readValue.x * Time.deltaTime * mouseSensX;
        currentRotation.y += readValue.y * Time.deltaTime * mouseSensY;
        
        //rotates left and right
        transform.rotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
        
        //clamp rotation angle so you cant roll your head
        currentRotation.y = Mathf.Clamp(currentRotation.y, minViewAngle, maxViewAngle);
        
        //rotate up and down
        lookAtPoint.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.right);

        
    }

    public void Shoot()
    {
        Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        currentProjectile.AddForce(lookAtPoint.forward * bulletForce, ForceMode.Impulse);
        
        Destroy(currentProjectile.gameObject, 4); //destroy after 4 seconds
    }
}
