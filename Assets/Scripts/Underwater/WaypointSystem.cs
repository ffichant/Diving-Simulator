using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Example of a Waypoint system taken from https://forum.unity3d.com/threads/a-waypoint-script-explained-in-super-detail.54678/, courtesy of user "cherub"
//Adapted for personal use
public class WaypointSystem : MonoBehaviour
{
    // This is a very simple waypoint system.
    // Each bit is explained in as much detail as possible for people (like me!) who need every single line explained.
    // As a side note to the inexperienced (like me at the moment!), you can delete the word "private" on any variable to see it in the inspector for debugging.
    // I am sure there are issues with this as is, but it seems to work pretty well as a demonstration.

    //STEPS:
    //1. Attach this script to a GameObject with a RidgidBody and a Collider.
    //2. Change the "Size" variable in "Waypoints" to the number of waypoints you want to use.
    //3. Drop your waypoint objects on to the empty variable slots.
    //4. Make sure all your waypoint objects have colliders. (Sphere Collider is best IMO).
    //5. Click the checkbox for "is Trigger" to "On" on the waypoint objects to make them triggers.
    //6. Set the Size (radius for sphere collider) or just Scale for your waypoints.
    //7. Have fun! Try changing variables to get different speeds and such.

    // Disclaimer:
    // Extreme values will start to mess things up.
    // Maybe someone more experienced than me knows how to improve it.
    // Please correct me if any of my comments are incorrect.

    // This is the rate of accelleration after the function "Accell()" is called.
    // Higher values will cause the object to reach the "speedLimit" in less time.
    public float accel = 0.8f;

    // This is the the amount of velocity retained after the function "Slow()" is called.
    // Lower values cause quicker stops. A value of "1.0" will never stop. Values above "1.0" will speed up.
    public float inertia = 0.9f;

    // This is as fast the object is allowed to go.
    public float speedLimit = 10.0f;

    // This is the speed that tells the functon "Slow()" when to stop moving the object.
    public float minSpeed = 1.0f;

    // This is how long to pause inside "Slow()" before activating the function
    // "Accell()" to start the script again.
    public float[] stopTime;


    public int horizontalMovementDirection;

    // This variable "currentSpeed" is the major player for dealing with velocity.
    // The "currentSpeed" is mutiplied by the variable "accel" to speed up inside the function "accell()".
    // Again, The "currentSpeed" is multiplied by the variable "inertia" to slow
    // things down inside the function "Slow()".
    private float currentSpeed = 0.0f;

    // The variable "functionState" controlls which function, "Accell()" or "Slow()",
    // is active. "0" is function "Accell()" and "1" is function "Slow()".
    private float functionState = 0;

    // The next two variables are used to make sure that while the function "Accell()" is running,
    // the function "Slow()" can not run (as well as the reverse).
    private bool accelState;
    private bool slowState;

    // This variable will store the "active" target object (the waypoint to move to).
    private Transform waypoint;

    // This is the speed the object will rotate to face the active Waypoint.
    public float rotationDamping = 6.0f;

    // If this is false, the object will rotate instantly toward the Waypoint.
    // If true, you get smoooooth rotation baby!
    public bool smoothRotation = true;

    // This variable is an array. []< that is an array container if you didnt know.
    // It holds all the Waypoint Objects that you assign in the inspector.
    public Transform[] waypoints;

    // This variable keeps track of which Waypoint Object,
    // in the previously mentioned array variable "waypoints", is currently active.
    //On initialise le pointeur initial à -1 car il sera incrémenté dès la collision
    private int WPindexPointer=-1;

    private bool answeredToQuery;
    // Functions! They do all the work.
    // You can use the built in functions found here: http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.html
    // Or you can declare your own! The function "Accell()" is one I declared.
    // You will want to declare your own functions because theres just certain things that wont work in "Update()". Things like Coroutines: http://unity3d.com/support/documentation/ScriptReference/index.Coroutines_26_Yield.html

    //The function "Start()" is called just before anything else but only one time.
    void Start()
    {
        // When the script starts set "0" or function Accell() to be active.
        functionState = 0;
        WPindexPointer = 0;
        waypoint = null;
    }

    //The function "Update()" is called every frame. It can get slow if overused.
    void Update()
    {
        // If functionState variable is currently "0" then run "Accell()".
        // Withouth the "if", "Accell()" would run every frame.
        if (functionState == 0)
        {
            Accell();
        }

        // If functionState variable is currently "1" then run "Slow()".
        // Withouth the "if", "Slow()" would run every frame.
        if (functionState == 1)
        {
            StartCoroutine(Slow());
        }

        waypoint = waypoints[WPindexPointer]; //Keep the object pointed toward the current Waypoint object.
    }

    // I declared "Accell()".
    void Accell()
    {
        if (accelState == false)
        {
            // Make sure that if Accell() is running, Slow() can not run.
            accelState = true;
            slowState = false;
        }


        // Now do the accelleration toward the active waypoint untill the "speedLimit" is reached
        currentSpeed = currentSpeed + accel * accel;
        // I grabbed this next part from the unity "SmoothLookAt" script but try to explain more.
        if (waypoint) //If there is a waypoint do the next "if".
        {
            //Debug.Log("Waypoint trouvé, déplacement vers lui");
            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, currentSpeed * Time.deltaTime);
            if (waypoint.position.x - transform.position.x > 0)
                horizontalMovementDirection = 1;
            else if (waypoint.position.x - transform.position.x < 0)
                horizontalMovementDirection = -1;
            else
                horizontalMovementDirection = 0;

        }

        //transform.Translate(Time.deltaTime * currentSpeed, 0, 0);


        // When the "speedlimit" is reached or exceeded ...
        if (currentSpeed >= speedLimit)
        {
            // ... turn off accelleration and set "currentSpeed" to be
            // exactly the "speedLimit". Without this, the "currentSpeed
            // will be slightly above "speedLimit"
            currentSpeed = speedLimit;
        }
    }

    //The function "OnTriggerEnter" is called when a collision happens.
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ontrigger : colliding with : " + other);
        // When the GameObject collides with the waypoint's collider,
        if (waypoint.gameObject == other.gameObject)
        {

            // activate "Slow()" by setting "functionState" to "1".
            functionState = 1;

            // When the GameObject collides with the waypoint's collider,
            // change the active waypoint to the next one in the array variable "waypoints".
            WPindexPointer++;

            // When the array variable reaches the end of the list ...
            if (WPindexPointer >= waypoints.Length)
            {
                // ... reset the active waypoint to the first object in the array variable
                // "waypoints" and start from the beginning.
                WPindexPointer = 0;
            }
        }
    }

    // I declared "Slow()".
    IEnumerator Slow()
    {
        if (slowState == false) //
        {
            // Make sure that if Slow() is running, Accell() can not run.
            accelState = false;
            slowState = true;
        }

        // Begin to do the slow down (or speed up if inertia is set above "1.0" in the inspector).
        currentSpeed = currentSpeed * inertia;
        if (waypoint) //If there is a waypoint do the next "if".
        {
            /*if (smoothRotation)
            {
                var rotation = Quaternion.LookRotation(waypoint.position - transform.position, -Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            }*/
            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, currentSpeed * Time.deltaTime);
            if (waypoint.position.x - transform.position.x > 0)
                horizontalMovementDirection = 1;
            else if (waypoint.position.x - transform.position.x < 0)
                horizontalMovementDirection = -1;
            else
                horizontalMovementDirection = 0;

        }

        // When the "minSpeed" is reached or exceeded ...
        if (currentSpeed <= minSpeed)
        {
            // ... Stop the movement by setting "currentSpeed to Zero.
            currentSpeed = 0.0f;
            // Wait for the amount of time set in "stopTime" before moving to next waypoint.
            //Debug.Log("Stopping for " + stopTime[WPindexPointer] + " seconds at waypoint "+waypoint);
            //On attend que le joueur réponde au moniteur
            /*if(waypoint == waypoints[3])
                yield return new WaitUntil(() => answeredToQuery);
            if (waypoint == waypoints[4])
                yield return new WaitUntil(() => answeredToQuery);
            if (waypoint == waypoints[6])
                yield return new WaitUntil(() => answeredToQuery );
            */
            yield return new WaitForSeconds(stopTime[WPindexPointer]);
            // Activate the function "Accell()" to move to next waypoint.
            functionState = 0;
            answeredToQuery = false;
            //Une fois que le moniteur est arrivé au dernier waypoint, on passe à la scène des scores
            if(waypoint == waypoints[9])
                SceneManager.LoadScene("Score");
        }
    }

}
