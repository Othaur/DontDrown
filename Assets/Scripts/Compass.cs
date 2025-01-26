using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private GameObject compassHand;

    //A point for the compass to point towards.
    public Transform northPole;
    //Compass hand rotation speed;
    private float rotationSpeed = 10;

    private void Start()
    {
        GoNorth();
    }

    private void Update()
    {
        RotateCompass();
    }

    private void GoNorth()
    {
        //Move northPole north to act as a target for the compass.
        northPole.transform.position = new Vector3(1000, 0, 0);
    }

    private void RotateCompass()
    {
        //Direction to rotate towards.
        Vector3 targetDirection = northPole.position - compassHand.transform.position;

        // Rotate the forward vector towards the target direction.
        Vector3 newDirection = Vector3.RotateTowards(compassHand.transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        compassHand.transform.rotation = Quaternion.LookRotation(newDirection);
    }

}
