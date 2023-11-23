using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chickenandegg : MonoBehaviour
{

    [SerializeField] private Rigidbody prefabSpawn;
    [SerializeField] private Transform wheretospawn;
    [SerializeField] private float spawnrate;

    private float timeleft = 5f;
    void Start()
    {
        prefabSpawn.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        if (timeleft <= 0)
        {
            Rigidbody CurrentProj = Instantiate(prefabSpawn, wheretospawn.position, Quaternion.identity);
            
            Destroy(CurrentProj.gameObject, 4);

            timeleft = spawnrate;
        }




    }
}
