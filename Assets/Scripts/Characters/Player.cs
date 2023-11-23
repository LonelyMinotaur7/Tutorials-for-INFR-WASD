using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour

{
    private bool _weaponShootToggle;

    public GameObject Model;

    public GameObject ShotGunText;
    public GameObject SniperText;

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

    [Header("Player UI")] 
    [SerializeField] private Image healthbar;

    [SerializeField] private TextMeshProUGUI shotsfired;
    [SerializeField] private TextMeshProUGUI shotsleft;
    private int ShotsLeft = 12;

    [SerializeField] private float maxhealth;

    [SerializeField] private WeaponBase myWeapon;
    
    private int shotsfiredcounter;
    private float _health;

    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            healthbar.fillAmount = _health / maxhealth;
        }
        
    }

    private Vector2 currentRotation;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        
        InputMang.Init(this);
        
        InputMang.GameMode();

        rb = GetComponent<Rigidbody>();

        Health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (_moveDir * speed * Time.deltaTime);
        CheckGround();
        
        shotsleft.text = ShotsLeft.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot");
        }

        Health -= Time.deltaTime * 5;
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

    private bool firestate;

    public void Shoot()
    {
        if (ShotsLeft > 0)
        {
            _weaponShootToggle = !_weaponShootToggle;
            if (_weaponShootToggle)
            {
                myWeapon.StartShooting();
                
            }
            else
            {
                myWeapon.StopShooting();
                ShotsLeft = ShotsLeft - 1;
            }
            
            

             shotsfiredcounter++;

             shotsfired.text = shotsfiredcounter.ToString();


             

             
        }
        
    }

    public void Reload()
    {
        ShotsLeft = 12;
    }

    public void Swap()
    {
        ProjectileWeapon.Shotgun = true;
        Model.SetActive(false);
        ShotGunText.SetActive(true);
        SniperText.SetActive(false);
    }
    
    public void Swap2()
    {
        ProjectileWeapon.Shotgun = false;
        Model.SetActive(true);
        ShotGunText.SetActive(false);
        SniperText.SetActive(true);
    }
}
