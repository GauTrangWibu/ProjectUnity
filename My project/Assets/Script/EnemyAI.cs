using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    //reference to waypoints
    public List<Transform> points;
    //the int value for next point index
    public int nextID;
    //the value of that applies to id for the changing
    public int idChangeValue = 1;

    [SerializeField] private float speed = 2;

    void Start()
    {
        
    }

    // Update is called once per frame
    

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        //make a box collider trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
        //create root obj
        GameObject root = new GameObject(name + "_Root");
        //reset position of root to enemy obj
        root.transform.position = transform.position;
        //set enemy obj as child of root
        transform.SetParent(root.transform);
        //create waypoints obj
        GameObject waypoints = new GameObject("Waypoints");
        //reset waypoints position to root

        //make a waypoint obj child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = Vector3.zero;
        //create two pint (gameobj) and reset their position to waypoints obj
        //make the point children of waypoint obj
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = Vector3.zero;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = Vector3.zero;

        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
        
    }

    void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //get the next point transform
        Transform goalPoint = points[nextID];
        //flip the enemy transfrom to look into the point's direction
        if(goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-7,7);
        }
        else
            transform.localScale = new Vector2(7,7);
        //move the enemy toward the goal point
        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position,speed*Time.deltaTime);
        //check the distance between enemy  and goal point to trigger the next point
        if (Vector2.Distance(transform.position, goalPoint.position) <1f)
        {
            //check if we are at the end of the line (make change - 1)
            //2 point (0,1) nextID == points.count(2) -1
            if(nextID == points.Count - 1)
            {
                idChangeValue = -1;
            }
            //check if we are at the start of the line (make change + 1)
            if (nextID==0)
                idChangeValue = 1;
            //apply the change on the nextID
                nextID += idChangeValue;
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Trigger on");
        }
    }
}
