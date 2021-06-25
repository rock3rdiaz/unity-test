using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    // los 'prefabs' son objectos que quedan pre frabicados y pueden ser usados en donde se desee
    // las veces que se desee.
    public GameObject bulletPrefab;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private float horizontal;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;
    public float speed;
    private bool grounded;
    private float lastShoot;
    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        /**
        Todos los componentes en Unity tienen ua referencia implicita
        a su componente Transform, por eso siempre esta disponible mediante 
        la propiedad 'transform'
        */

        // se obtienen las referencias de los componentes dado que estan al mismo nivel
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // -1 => izquierda, 0 => sin movimiento, 1 => derecha 
        horizontal = Input.GetAxisRaw("Horizontal");

        // al pasar la corrdenada en x de localScale a -1, se gira el sprite. Es algo asi como un flap
        if(horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // definimos el valor el parametro 'running' definido en el animator
        animator.SetBool("running", horizontal != 0.0f);

        // Raycast es algo asi como una linea dirigida que funciona como un sensor.
        // en este ejemplo se usa para saber si estamos tocando el suelo o no.
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if(Input.GetKeyDown(KeyCode.W) && grounded) 
        {
            jump();
        }
        if(Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.25f)
        {
            shoot();
            lastShoot = Time.time;
        }
    }

    private void shoot() 
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        // metodo que permite instanciar el prefab, en una posicion definidia y rotacion
        // definidas
        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletMovement>().setDirection(direction);
    }

    private void jump() 
    {
        rigidBody2D.AddForce(Vector2.up * jumpForce);
    }

    private void FixedUpdate() 
    {
        rigidBody2D.velocity = new Vector2(horizontal * speed, rigidBody2D.velocity.y);
    }

    public void hit() 
    {
        health--;
        if(health == 0) Destroy(gameObject);
    }
}
