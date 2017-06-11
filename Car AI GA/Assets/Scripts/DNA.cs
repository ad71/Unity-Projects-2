using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    /* Parameters:
     * maxSteerAngle
     * topSpeed
     * maxMotorTorque
     * maxBrakingTorque
     * centerOfMass.y
     * sensorLength
     * sensorSkewAngle
     * 4WheelDrive?
     * 4WheelBrake?
     * 4WheelTurn?
     * switchToNextWaypointDistance
     * sense() ?
     * brakingConditions
     * avoidMultiplier parameter
     */

    List<float> genes;

    private void Start()
    {
        genes = new List<float>();
        for(int i = 0; i < 14; ++i)
        {
            genes.Add(Random.Range(0f, 1f));
        }
    }
}
