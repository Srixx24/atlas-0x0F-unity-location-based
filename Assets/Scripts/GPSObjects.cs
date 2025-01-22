using UnityEngine;
using Niantic.Lightship.AR.WorldPositioning;
using System;

public class AddGPSObjects : MonoBehaviour
{
    [SerializeField] ARWorldPositioningObjectHelper positioningHelper;
    [SerializeField] Camera trackingCamera;
    [SerializeField] GameObject objectPrefab;
    [SerializeField] GameObject manipulationPrefab;

    double latitude = 36.13069;
    double longitude = -95.89185;
    double altitude = 0.0;

    private GameObject mainObject;
    private GameObject manipulationObject;

    void Start()
    {
        // Initialize location service
        Input.location.Start();

        // Instantiate the main object at the initial GPS location
        mainObject = Instantiate(objectPrefab);
        mainObject.transform.localScale *= 2.0f; // Scale the main object
        positioningHelper.AddOrUpdateObject(mainObject, latitude, longitude, altitude, Quaternion.identity); // Add to AR system

        InstantiateManipulationObject();
    }

    void Update()
    { 
        if (mainObject == null)
        {
            Debug.LogWarning("mainObject is null!");
            return;
        }

        // Update the position of the main object continuously based on current location
        if (Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running)
        {
            double deviceLatitude = Input.location.lastData.latitude;
            double deviceLongitude = Input.location.lastData.longitude;

            Vector2 eastNorthOffsetMetres = EastNorthOffset(latitude, longitude, deviceLatitude, deviceLongitude);
            Vector3 trackingOffsetMetres = Quaternion.Euler(0, 0, Input.compass.trueHeading) * new Vector3(eastNorthOffsetMetres.x, (float)altitude, eastNorthOffsetMetres.y);
            Vector3 trackingMetres = trackingCamera.transform.localPosition + trackingOffsetMetres;
            mainObject.transform.localPosition = trackingMetres; // Update main object's position
        }
    }

    private void InstantiateManipulationObject()
    {
        // Instantiate the object for later manipulation
        manipulationObject = Instantiate(manipulationPrefab);
        manipulationObject.transform.localPosition = mainObject.transform.localPosition;
    }

    public Vector2 EastNorthOffset(double latitudeDegreesA, double longitudeDegreesA, double latitudeDegreesB, double longitudeDegreesB)
    {
        const double DEGREES_TO_METRES = 111139.0; // Conversion factor
        float lonDifferenceMetres = (float)(Math.Cos((latitudeDegreesA + latitudeDegreesB) * 0.5 * Math.PI / 180.0) * (longitudeDegreesA - longitudeDegreesB) * DEGREES_TO_METRES);
        float latDifferenceMetres = (float)((latitudeDegreesA - latitudeDegreesB) * DEGREES_TO_METRES);
        return new Vector2(lonDifferenceMetres, latDifferenceMetres); // Return offset as Vector2
    }
}