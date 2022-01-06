using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectTime : MonoBehaviour
{
    // this script allows you to manage the single navemsh
    public float slowMotionSpeed = 0.1f;    //slow motion speed
    public float fastMotionSpeed = 3f;      //fast motion speed
    private NavMeshAgent agent;                 
    private Animator anim;
    private bool doSlowMotion = false;
    private GameObject canvas;

    private bool isCar = false;
    private int carIndexPos;

    private List <GameObject> paths = new List<GameObject>();
    private void Start()
    {                                                       //pick all the references
        if(transform.tag == "Car")
        isCar = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        if(!isCar)
        agent.SetDestination(GetRandomLocation());
        else
        {
            for (int i = 0; i < GameObject.Find("Path").transform.childCount; i++)
            {
                paths.Add( GameObject.Find("Path").transform.GetChild(i).gameObject);
            }
        }
        canvas = transform.GetChild(2).gameObject;
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;


        CloseTimeControl();         //close the child canvas at the start
    }



    public void DoSlowMotion()          //fuction to do slow motion
    {
        agent.speed =slowMotionSpeed;
        anim.speed =slowMotionSpeed;
        CloseTimeControl();
    }

        public void BackMotion()        //fuction to back the motion normal
    {
        agent.speed =1;
        anim.speed = 1;
        CloseTimeControl();
    }

    public void DoFastMotion()      //function to speed up the motion
    {
        agent.speed = fastMotionSpeed;
        anim.speed = fastMotionSpeed;
        CloseTimeControl();
    }

    public void StopMotion()            //function to pause the motion
    {
        agent.speed = 0;
        anim.speed = 0;
       
    }

   
    void Update()
    {   
        
         if (  agent.remainingDistance < 0.5f)          //check if reach the current position
         {
             if(!isCar)
              agent.SetDestination(GetRandomLocation());
               else
               agent.SetDestination(GetNextLocation());
             
         } 
         canvas.transform.LookAt(transform.position + Camera.main.transform.rotation * -Vector3.back, Camera.main.transform.rotation * Vector3.up);

    }
    private Vector3 GetRandomLocation()             //get random position in the area for the characters
     {
         NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
         int t = Random.Range(0, navMeshData.indices.Length-3);
         Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t+1]], Random.value);
         Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t+2]], Random.value);
         return point;
     }

       private Vector3 GetNextLocation()            //get next position of the point in the scene for the car navmesh
       {    
           carIndexPos++;
           if(carIndexPos == paths.Count)
           carIndexPos = 0;
              return  paths[carIndexPos].transform.position;
       }

    public void OpenTimeControl()           //open the child canvas
    {
        canvas.SetActive(true);
    }

       public void CloseTimeControl()           //close the child canvas
    {
        canvas.SetActive(false);
    }

}
