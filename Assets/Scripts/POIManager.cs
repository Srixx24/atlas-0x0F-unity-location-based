using Niantic.Lightship.Maps.Core.Coordinates;
using Niantic.Lightship.Maps.MapLayers.Components;
using UnityEngine;

public class POIManager : MonoBehaviour
{
    [SerializeField] private LayerGameObjectPlacement POIMarker;


    void Start()
    {
        PlaceTestMultiMarker();
    }
    
    public void PlaceObject(LatLng pos)
    {
        // Check if the POIMarker is assigned
        if (POIMarker == null)
        {
            Debug.LogError("POIMarker is not assigned!");
            return;
        }

        // Place the instance at the given position
        POIMarker.PlaceInstance(pos);
    }

    public void PlaceTestMultiMarker()
    {
        // Testing with array may switch to list depending on how
        // performance and mem management goes. Fill with actual
        // locations after testing complete.
        LatLng[] positions = new LatLng[]
        {
            new LatLng(36.13147f, 95.89166f),
            new LatLng(36.13025f, 95.89097f)
        };

        foreach (var position in positions)
        {
            PlaceObject(position);
        }
    }
}
