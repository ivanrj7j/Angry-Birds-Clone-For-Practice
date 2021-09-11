using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    // This script is for red bird game object 
    // Start is called before the first frame update
    Vector3 initPosition;
    // initailising a variable that stores the position of the bird at the start 
    // stores the position of the object before the last move 


    // constatnts 
    Rigidbody2D rigid_body;
    SpriteRenderer sprite_renderer;
    GameObject circle_one;
    GameObject circle_two;
    GameObject circle_three;

    
    // constants end

    //Serialize fields
    [SerializeField] float launchForce = 100;
    [SerializeField] float max_drag = 3;
    // serialize field end
    void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        circle_one = GameObject.Find("circle one");
        circle_two = GameObject.Find("circle_2");
        circle_three = GameObject.Find("circle_3");
        circle_one.SetActive(false);
        circle_two.SetActive(false);
        circle_three.SetActive(false);
        // assiging the values to the components 
    }
    void Start()
    {


        initPosition = rigid_body.position;
        rigid_body.isKinematic = true;
        // turns off the kinematic simulation 

    }

    private void OnMouseDown()
    {
        sprite_renderer.color = Color.red;
        //    sets the color of the bird when clicked 
    }

    private void OnMouseUp()
    {
        sprite_renderer.color = Color.white;
        // removes the color of the bird when the player takes hands off the mouse 


        rigid_body.isKinematic = false;
        Vector3 force = (initPosition - transform.position) * launchForce;
        circle_one.SetActive(false);
        circle_two.SetActive(false);
        circle_three.SetActive(false);
        rigid_body.AddForce(force);
        
        
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // getting the position of the mouse, Here the camera is looking for the CameraObject named "main"
        //Getting the Point of mouse after that
        var new_position = new Vector3(mousePosition.x, mousePosition.y, initPosition.z);
        // stores the position of the object     

        float distance = Vector2.Distance(initPosition, new_position);
        // get the distance between the point and the start point

        if(distance > max_drag){
            var direction = new_position - initPosition;
            direction.Normalize();
            // gets the direction and value of the force 
            new_position = initPosition + (direction * max_drag);
        }

         if (new_position.x > initPosition.x)
        {
             new_position = initPosition;
        }
        transform.position = new_position;
        // setting up the position of the bird according to mouse 
        getProjectileDirection();
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision_body) {
 
        StartCoroutine(ResetAfterDelay());
        // calls everytime when the bird collides with something.  
    }

    private IEnumerator ResetAfterDelay(){
        yield return new WaitForSeconds(1);
        // Wait for 2 seconds 
        transform.position = initPosition;
        // resets the positon 
        rigid_body.velocity = Vector2.zero;
        // sets back the velocity to zero
        rigid_body.isKinematic = true;
        // turns the bird back to a kinematic body 
    }

    Vector3 get_displacement(Vector3 force, float mass, float factor){
        float earth_gravitational_pull = 9.81f;
        var init_velocity = force / (mass*earth_gravitational_pull);
        var displacement = (init_velocity * factor) + 0.5f *(force * (factor * factor));
        Debug.Log("iniital velocity: "+ init_velocity);
        return displacement;
    }

    void getProjectileDirection(){
        circle_one.SetActive(true);
        circle_one.transform.position = transform.position - get_displacement((initPosition - transform.position) * launchForce, circle_one.GetComponent<Rigidbody2D>().mass, -0.01f);
        circle_two.SetActive(true);
        circle_two.transform.position = transform.position - get_displacement((initPosition - transform.position) * launchForce, circle_one.GetComponent<Rigidbody2D>().mass, -0.03f);
        circle_three.SetActive(true);
        circle_three.transform.position = transform.position - get_displacement((initPosition - transform.position) * launchForce, circle_one.GetComponent<Rigidbody2D>().mass, -0.05f);
    }
}
