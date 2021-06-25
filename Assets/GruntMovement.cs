using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntMovement : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject john;
    private float lastShoot;
    private int health = 3;

    private void Update() 
    {
        if(john == null) return;
        
        Vector3 direction = john.transform.position - transform.position;
        if(direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(john.transform.position.x - transform.position.x);
        if(distance < 1.0f && Time.time > lastShoot + 0.25f) 
        {
            shoot();
            lastShoot = Time.time;
        }
    }

    private void shoot()
    {
        //Debug.Log("Shoot");
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        // metodo que permite instanciar el prefab, en una posicion definidia y rotacion
        // definidas
        GameObject bullet = Instantiate(bulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletMovement>().setDirection(direction);
    }

    public void hit() 
    {
        health--;
        if(health == 0) Destroy(gameObject);
    }
}
