﻿/*
     * Copyright (c) 2017 Razeware LLC
     * 
     * Permission is hereby granted, free of charge, to any person obtaining a copy
     * of this software and associated documentation files (the "Software"), to deal
     * in the Software without restriction, including without limitation the rights
     * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
     * copies of the Software, and to permit persons to whom the Software is
     * furnished to do so, subject to the following conditions:
     * 
     * The above copyright notice and this permission notice shall be included in
     * all copies or substantial portions of the Software.
     *
     * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
     * distribute, sublicense, create a derivative work, and/or sell copies of the 
     * Software in any work that is designed, intended, or marketed for pedagogical or 
     * instructional purposes related to programming, coding, application development, 
     * or information technology.  Permission for such use, copying, modification,
     * merger, publication, distribution, sublicensing, creation of derivative works, 
     * or sale is expressly withheld.
     *    
     * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
     * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
     * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
     * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
     * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
     * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
     * THE SOFTWARE.
*/

using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour
{
    // EfxZoom
    public GameObject efxZoomObj;
    private SpriteRenderer efxZoomRenderer;
    private AnimateController efxZoomAniController;

    // EfxZoom light
    public GameObject efxLightObj;
    private SpriteRenderer efxLightRenderer;
    private AnimateController efxLightAniController;

    // Sound
    private SoundController sounds;
    private AudioSource pullSound;
    private AudioSource shootSound;

    // Touch Listener
    public bool isTouched = false;
    public bool isKeyPress = false;

    // Ready for Launch
    public bool isActive = true;

    // Timers
    private float pressTime = 0f;
    private float startTime = 0f;
    private int powerIndex;

    private SpringJoint2D springJoint;
    private float force = 0f;          // Current force generated
    public float maxforce = 90f;


    void Start()
    {
        // Zoom Animation Object
        efxZoomAniController = efxZoomObj.GetComponent<AnimateController>();
        efxLightAniController = efxLightObj.GetComponent<AnimateController>();

        // Zoom Light Object
        efxZoomRenderer = efxZoomObj.GetComponent<Renderer>() as SpriteRenderer;
        efxLightRenderer = efxLightObj.GetComponent<Renderer>() as SpriteRenderer;

        // Sounds
        sounds = GameObject.Find("SoundObjects").GetComponent<SoundController>();
        pullSound = sounds.pulldown;
        shootSound = sounds.zonar;

        springJoint = GetComponent<SpringJoint2D>();
        springJoint.distance = 1f;

    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown("space"))
            {
                isKeyPress = true;
            }

            if (Input.GetKeyUp("space"))
            {
                isKeyPress = false;
            }

            // On keyboard press or touch hotspot
            if (isKeyPress == true && isTouched == false || isKeyPress == false && isTouched == true)
            {   
                if (startTime == 0f)
                {   
                    startTime = Time.time;
                    pullSound.Play();
                }
            }

            // On keyboard release 
            if (isKeyPress == false && isTouched == false && startTime != 0f)
            {
                // Calculates current force of exertion
                force = powerIndex * maxforce;

                shootSound.Play();

                // Reset values & animation
                pressTime = 0f;
                startTime = 0f;
                efxLightRenderer.sprite = efxLightAniController.spriteSet[1];
                while (powerIndex >= 0)
                {
                    efxZoomRenderer.sprite = efxZoomAniController.spriteSet[powerIndex];
                    powerIndex--;
                }
            }
        }

        // Start Press
        if (startTime != 0f)
        {
            pressTime = Time.time - startTime;

            // Plays zoom animation on loop
            powerIndex = (int)(Mathf.PingPong(pressTime * efxZoomAniController.fps, efxZoomAniController.spriteSet.Length));
            efxZoomRenderer.sprite = efxZoomAniController.spriteSet[powerIndex];

            // Turns on/ off zoom light based on powerIndex
            if (powerIndex == efxZoomAniController.spriteSet.Length - 1)
            {
                efxLightRenderer.sprite = efxLightAniController.spriteSet[0];
            }
            else
            {
                efxLightRenderer.sprite = efxLightAniController.spriteSet[1];
            }
        }   

    }

    void FixedUpdate()
    {
        // When force is not 0
        if (force != 0)
        {
            // Release springJoint and power up
            springJoint.distance = 1f;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * force);
            force = 0;
        }

        // When plunger is engaged	
        if (pressTime != 0)
        {
            // Retract springJoint distance and reduce power
            springJoint.distance = .8f;
            GetComponent<Rigidbody2D>().AddForce(Vector3.down * 400);
        }
    }

}
