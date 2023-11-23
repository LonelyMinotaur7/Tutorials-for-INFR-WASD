using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    public Rigidbody bulletPrefab;
    public Rigidbody bulletPrefab2;
    public int bulletForce;
    public Transform lookAtPoint;
    public float spreadAngle = 15f;
    public GameObject Sound;

    public GameObject Effect;

    //public class Swap : MonoBehaviour
    //{
        public static bool Shotgun = false;
    //}

    protected override void Attack(float percent)
    {
        
        GameObject currentSound = Instantiate(Sound, transform.position, transform.rotation);
        
        GameObject currentEffect = Instantiate(Effect, transform.position, transform.rotation);
        
        
        Destroy(currentEffect.gameObject, 0.2f);
        Destroy(currentSound.gameObject, 4);
        
        if (Shotgun == false)
        {
            Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, transform.rotation);
       
            currentProjectile.AddForce(3 * bulletForce * transform.forward, ForceMode.Impulse);

            


            Destroy(currentProjectile.gameObject, 4);
        }

        if (Shotgun == true)
        {
            for (int i = 0; i < 6; i++)
            {
            
            
            
                print("attacked" + percent);
            
                Vector3 randomDirection = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * lookAtPoint.forward;
                Rigidbody currentProjectile = Instantiate(percent>0.5?bulletPrefab2:bulletPrefab, transform.position, transform.rotation);
       
                currentProjectile.AddForce(Mathf.Max(percent, 0.1f) * bulletForce * randomDirection, ForceMode.Impulse);

            


                Destroy(currentProjectile.gameObject, 4); //destroy after 4 seconds

            
            }
        }

        
       
       

        
    }
}
