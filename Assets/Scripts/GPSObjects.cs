using UnityEngine;
using Niantic.Lightship.AR.WorldPositioning;
using System;
using UnityEngine.SceneManagement;

public class AddGPSObjects : MonoBehaviour
{
    [SerializeField] ARWorldPositioningObjectHelper positioningHelper;
    [SerializeField] Camera trackingCamera;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject petPrefab;
    double latitude = 36.1540;
    double longitude = 95.9928;
    double altitude = 0.0;
    private GameObject mainObject;
    private GameObject arObject;


    void Start()
    {
        // Initialize location service
        Input.location.Start();

        // Instantiate the main object at the initial GPS location, may need to adjust later
        mainObject = Instantiate(playerPrefab);
        //mainObject.transform.localScale *= 2.0f; // Scale the main object
        //positioningHelper.AddOrUpdateObject(mainObject, latitude, longitude, altitude, Quaternion.identity); // Add to AR system
        arObject = Instantiate(petPrefab);
        arObject.SetActive(false);
    }

    void Update()
    {
        ManageObjectVisibility();
    }

    private void ManageObjectVisibility()
    {
        // Manage object visibility based on the active scene
        if (SceneManager.GetActiveScene().name == "AR View")
        {
            arObject.SetActive(true);
            mainObject.SetActive(false);
        }
        else
        {
            arObject.SetActive(false);
            mainObject.SetActive(true);
        }
    }

    public Vector2 EastNorthOffset(double latitudeDegreesA, double longitudeDegreesA, double latitudeDegreesB, double longitudeDegreesB)
    {
        const double DEGREES_TO_METRES = 111139.0; // Conversion factor
        float lonDifferenceMetres = (float)(Math.Cos((latitudeDegreesA + latitudeDegreesB) * 0.5 * Math.PI / 180.0) * (longitudeDegreesA - longitudeDegreesB) * DEGREES_TO_METRES);
        float latDifferenceMetres = (float)((latitudeDegreesA - latitudeDegreesB) * DEGREES_TO_METRES);
        return new Vector2(lonDifferenceMetres, latDifferenceMetres); // Return offset as Vector2
    }
}
