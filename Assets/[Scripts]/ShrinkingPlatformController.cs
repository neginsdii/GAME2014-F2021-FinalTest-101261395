/*
 * Full Name        : Negin Saeidi
 * Student ID       : 101261395
 * Date Modified    : December 18, 2021
 * File             : ShrinkingPlatformController.cs
 * Description      : This is the ShrinkingPlatformController script - Moves the platform up and down, shrinks and grow the platform
 * Version          : V01
 * Revision History : added timer 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatformController : MonoBehaviour
{
    [Header("Movement")]
    [Range(0.1f, 10.0f)]
    public float speed;
    [Range(0.1f, 20)]
    public float distance;
    [Range(0.05f, 0.1f)]
    public float distanceOffset;
    [Header("Impulse Sounds")]
    public AudioSource[] sounds;

    private Vector2 startingPosition;
    private Vector3 startingScale;
    [Header("Shrinking Properties")]
    public bool isShrinking;
    public bool isGrowing;
    public float ScalingFactor;
    public float timer = 0.0f;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingScale = transform.localScale;
        sounds = GetComponents<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        ShrinkAndGrow();
    }
    // Moving platform
    private void _Move()
    {
        float pingPongValue =Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector2(transform.position.x, startingPosition.y + pingPongValue);

    }
    // if player lands on this platform
    // plays shrink sound.
    // set isShrinking to true.
    // Set isGrowing to false.
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag=="Player")
		{
            if (!isShrinking)
            {
              
              //  if(!sounds[0].isPlaying)
                sounds[0].Play();
                isShrinking = true;
                isGrowing = false;
                timer = 0;
            }
		}
	}

    // if player exits  this platform
    // plays reset sound.
    // set isShrinking to false.
    // Set isGrowing to true.
    private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            if (!isGrowing)
            {
           
                //if (!sounds[1].isPlaying)
                    sounds[1].Play();
                isGrowing = true;
                isShrinking = false;
                timer = 0;
            }
        }
    }
    // scale the platform based on isShrinking or isGrowing 
    private void ShrinkAndGrow()
	{
         if (isShrinking)
        {
           

                transform.localScale -= Vector3.one * ScalingFactor;
                if (transform.localScale.x <= 0.2)
                {
                    transform.localScale = Vector3.zero;
                    isShrinking = false;
                    isGrowing = true;
                }
            
		}
        else if(isGrowing)
		{
            if (timer > waitTime)
            {
                transform.localScale += Vector3.one * ScalingFactor;
                if (transform.localScale.x >= startingScale.x)
                {
                    transform.localScale = startingScale;
                    isGrowing = false;
                }
            }
        }
	
        timer += Time.deltaTime;
    
    }

}
