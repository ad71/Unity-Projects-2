using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA {

    /* Parameters:
     * 1. maxSteerAngle/
     * 2. topSpeed/
     * 3. maxMotorTorque/
     * 4. maxBrakingTorque/
     * 5. centerOfMass.y/
     * 6. Mass/
     * 7. sensorLength/
     * 8. sensorSkewAngle/
     * 9. 4WheelDrive?/
     * 10. 4WheelBrake?/
     * 11. 4WheelTurn?/
     * 12. switchToNextWaypointDistance/
     * 13. sense() ?
     * 14. brakingConditions1/
     * 15. brakingConditions2/
     * 16. avoidMultiplier parameter/
     * 17. lerpToSteerAngle ?/
     * 18.     turningSpeed/
     */

    public List<float> genes;

    public DNA()
    {
        this.genes = new List<float>();
        for(int i = 0; i < 18; ++i)
        {
            this.genes.Add(Random.Range(0f, 1f));
        }
    }

    public DNA(List<float> _genes)
    {
        this.genes = new List<float>();
        this.genes = _genes;
    }

    public DNA Crossover (DNA partner)
    {
        List<float> newGenes = new List<float>();
        int mid = Random.Range(0, genes.Count - 1);
        for(int i = 0; i < genes.Count; ++i)
        {
            if (i > mid)
            {
                newGenes[i] = genes[i];
            }
            else
            {
                newGenes[i] = partner.genes[i];
            }
        }
        return new DNA(newGenes);
    }
}
