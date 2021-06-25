using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody2D;
    private Vector2 direction;
    public AudioClip sound;

    // Start is called before the first frame update
    // Se ejecuta una vez se instancia un componente de este tipo
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        // de esta manera obtenemos una instancia a la camara principal de la escena 'Camera.main'
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody2D.velocity = direction * speed;
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction;
    }    

    public void destroyBullet() 
    {
        // gameObject es una referencia al objeto que usa este script
        Destroy(gameObject);
    }

    // Usado como un OnCollisionEnter2D pero en modo 'isTrigger'. 
    // la unica diferencia es que no aplica las fisicas del componente. En este caso
    // la bala no 'empuja' al componente con el que colisiona
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        JohnMovement jhon = collider.GetComponent<JohnMovement>();        
        GruntMovement grunt = collider.GetComponent<GruntMovement>();  
        if(jhon != null) 
        {
            jhon.hit();      
            Debug.Log("jhon hit");
        }
        if(grunt != null) 
        {
            grunt.hit();      
            Debug.Log("grunt hit");
        }
        destroyBullet();
    }
}
