using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    private SliderJoint2D slider;
    private JointMotor2D aux;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<SliderJoint2D>();
        aux = slider.motor;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = Random.Range(-1, -5);
            slider.motor = aux;
        }

        if (slider.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = Random.Range(1, 5);
            slider.motor = aux;
        }
    }
}
