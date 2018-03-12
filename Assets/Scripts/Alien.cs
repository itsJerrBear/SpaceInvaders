using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Alien : MonoBehaviour {

    public float speed = 10;

    public Rigidbody2D rigidBody;

    public Sprite startingImage;

    public Sprite altImage;

    private SpriteRenderer spriteRenderer;

    public float secBeforeSpriteChange = 0.5f;

    public GameObject alienBullet;

    public float minFireRateTime = 1.0f;

    public float maxFireRateTime = 3.0f;

    public float baseFireWaitTime = 3.0f;

    public Sprite explodedShipImage;

    public IEnumerator ChangeAlienSprite()
    {
        while (true)
        {
            if (spriteRenderer.sprite == startingImage)
            {
                spriteRenderer.sprite = altImage;

                if (SoundManager.Instance)
                {
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienBuzz1);

                }
            }

            else
            {
                spriteRenderer.sprite = startingImage;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienBuzz2);
            }

            yield return new WaitForSeconds(secBeforeSpriteChange);
        }
    }

    // Use this for initialization
    void Start () {
        //get object's rigidBody
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = new Vector2(1, 0) * speed;

        spriteRenderer = GetComponent<SpriteRenderer>();

        //cycle alien sprites
        StartCoroutine(ChangeAlienSprite());

        //randomize initial firing wait time
        baseFireWaitTime = baseFireWaitTime + Random.Range(minFireRateTime, maxFireRateTime);
	}

    // Turn in opposite direction
    void Turn(int direction)
    {
        Vector2 newVelocity = rigidBody.velocity;
        newVelocity.x = speed * direction;
        rigidBody.velocity = newVelocity;
    }

    // Move ddown after hitting wall 
	void MoveDown()
    {
        Vector2 position = transform.position;
        position.y -= 1;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "LeftWall")
        {
            Turn(1);
            MoveDown();
        }

        if (collision.gameObject.name == "RightWall")
        {
            Turn(-1);
            MoveDown();
        }

        if(collision.gameObject.name == "Bullet")
        {
            //play sound
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienDies);

            //Alien dies
            Destroy(gameObject);
        }
    }

    

    void FixedUpdate()
    {
        if(Time.time > baseFireWaitTime)
        {
            //reset fire rate
            baseFireWaitTime = baseFireWaitTime + Random.Range(minFireRateTime, maxFireRateTime);

            //create bullet
            Instantiate(alienBullet, transform.position,
                Quaternion.identity);
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //play sound of ship dieing if alien hits it
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.ShipExplosion);

            collision.GetComponent<SpriteRenderer>().sprite = explodedShipImage;

            Destroy(gameObject);

            //alien handles destroying itself
        }
    }
}
