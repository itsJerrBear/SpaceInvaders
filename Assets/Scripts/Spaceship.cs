using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour {

    public float speed = 30;

    public GameObject theBullet;

    void FixedUpdate()
    {
        //get input from user in horz direction ... defined in project input settings
        float horzMove = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(horzMove, 0) * speed;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
    //Fire bullets anytime spacebar is pressed and play a sound
	void Update () {

        //should really create your own input and assign it to spacebar
        if (Input.GetButton("Jump"))
        {
            Instantiate(theBullet, transform.position,
                Quaternion.identity);

            //Make Sound
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.BulletFire);

        }
		
	}
}
