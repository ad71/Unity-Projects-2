using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

    /* Parameters:
     * 1. maxSteerAngle
     * 2. topSpeed
     * 3. maxMotorTorque
     * 4. maxBrakingTorque
     * 5. centerOfMass.y
     * 6. sensorLength
     * 7. sensorSkewAngle
     * 8. 4WheelDrive?
     * 9. 4WheelBrake?
     * 10. 4WheelTurn?
     * 11. switchToNextWaypointDistance
     * 12. sense() ?
     * 13. brakingConditions
     * 14. avoidMultiplier parameter
     * 15. lerpToSteerAngle ?
     * 16.     turningSpeed
     */

    List<float> genes;

    public DNA()
    {
        this.genes = new List<float>();
        for(int i = 0; i < 16; ++i)
        {
            this.genes.Add(Random.Range(0f, 1f));
        }
    }
}
