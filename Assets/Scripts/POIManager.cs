using Niantic.Lightship.Maps.Core.Coordinates;
using Niantic.Lightship.Maps.MapLayers.Components;
using UnityEngine;

public class POIManager : MonoBehaviour
{
    [SerializeField] private LayerGameObjectPlacement followPOILayer;
    [SerializeField] private LayerGameObjectPlacement mapPOILayer;
    //[SerializeField] private GameObject POIMarker;
    //[SerializeField] private GameObject POIMarkerMaps;


    private LatLng[] positions = new LatLng[]
    {
        new LatLng(36.13147f, -95.89166f),
        new LatLng(36.13025f, -95.89097f)
    };

    void Start()
    {
        PlaceTestMultiMarker();
    }
    
    public void LayerList(LatLng pos)
    {
        followPOILayer.PlaceInstance(pos);
        mapPOILayer.PlaceInstance(pos);
        //followPOILayer.PlaceInstance(pos, Quaternion.identity, POIMarker.name);
        //mapPOILayer.PlaceInstance(pos, Quaternion.identity, POIMarkerMaps.name);
    }

    public void PlaceTestMultiMarker()
    {
        foreach (var position in positions)
        {
            LayerList(position);
        }
    }
}
