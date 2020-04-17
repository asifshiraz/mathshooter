using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector3 scaleChange, positionChange;
    private double frame = 0;
    private float multiplier = 1;
    private double smallerIncrement = 1.01;

    void Start()
    {
        scaleChange = new Vector3(0.007f, 0.007f, 0.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        this.transform.localScale += scaleChange;
        
        multiplier = multiplier + 0.04f;
        positionChange = new Vector3(0.00f, -0.01f*multiplier, 0.0f);
        this.transform.position += positionChange;
        var currentYPosition = this.transform.position.y;
        if (currentYPosition < 2.2)
        {
            EventManager.RaiseOnPlaneReachedCity();
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }
}
