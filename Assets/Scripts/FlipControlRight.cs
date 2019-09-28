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

public class FlipControlRight : MonoBehaviour
{

    private SoundController sound;
    public bool isKeyPress = false;
    public bool isTouched = false;
    private float speed = SliderValuesScript.FlipperSpeed;
    private HingeJoint2D myHingeJoint;
    private JointMotor2D motor2D;

    void Start()
    {
        sound = GameObject.Find("SoundObjects").GetComponent<SoundController>();
        myHingeJoint = GetComponent<HingeJoint2D>();
        motor2D = myHingeJoint.motor;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isKeyPress = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {       
            isKeyPress = false;
        }
    }


    void FixedUpdate()
    {
        // On press keyboard or touch Screen
        if (isKeyPress == true && isTouched == false || isKeyPress == false && isTouched == true)
        {
            sound.flipRgt.Play();

            // Set motor speed to max
            motor2D.motorSpeed = -speed;
            myHingeJoint.motor = motor2D;
        }
        else
        {
            // Reduce motor speed
            motor2D.motorSpeed = speed;
            myHingeJoint.motor = motor2D;
        }
    }
}