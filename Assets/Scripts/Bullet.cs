using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    public float speed = 30;

    private Rigidbody2D rigidBody;

    public Sprite explodedAlienImage;

	// Use this for initialization
	void Start () {

        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.velocity = Vector2.up * speed;


	}

    //handle triggered collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if(collision.tag == "Alien")
        {
            //play alien dies sound
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.AlienDies);

            //increase score
            IncreaseTextUIScore();

            collision.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;

            Destroy(gameObject);

            DestroyObject(collision.gameObject, 0.5f);
        }

        if(collision.tag == "Shield")
        {
            Destroy(gameObject);
            DestroyObject(collision.gameObject);

        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void IncreaseTextUIScore()
    {
        //find score UI component
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        //get string stored inside text window

        int score = int.Parse(textUIComp.text);

        score += 10;

        textUIComp.text = score.ToString();
    }
}
