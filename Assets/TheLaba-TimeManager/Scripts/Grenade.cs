using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float force = 1200;                 //force of the grenade
    public int delay = 3;                   //time to execute
    private float countDown;
    private Collider[] collidiers;

    private void Start()                //start the delay
    {
        countDown = delay;
    }

    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f )           //when it's time to execute call the funcion
        {   
            explosion();
        }
    }
 
    //grenate function
    private void explosion()
    {
          collidiers = Physics.OverlapSphere(transform.position, 6);
        foreach (Collider nearbyObject in collidiers)           //find all the nearest rigidbodyes (in the radius)
        {
            
            if (nearbyObject.GetComponent<Rigidbody>())
            {

                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();              //disable kinematic and add force
                rb.isKinematic = false;
                rb.AddExplosionForce(force * Time.deltaTime * 100, transform.position, 6);
            }
        }
    }       

 

  

}
