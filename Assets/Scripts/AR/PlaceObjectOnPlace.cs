using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Assets.Scripts.AR
{
    public class PlaceObjectOnPlace : MonoBehaviour
    {
        private ARRaycastManager raycastManager;
        private Pose placementPose;
        private bool placementPoseIsValid;
    
        public GameObject positionIndicator;
        public GameObject prefabToPlace;
        public Camera arCamera;

        private void Awake()
        {
            Debug.Log("Awake called!");
            raycastManager = GetComponent<ARRaycastManager>();


        
        }

        // Update is called once per frame
        void Update()
        {
            UpdatePlacementPose();
            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
            }
        }

        private void UpdatePlacementPose()
        {
            Debug.Log("UpdatePlacementPose called!");

            var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();

            raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

            placementPoseIsValid = hits.Count > 0;

            if (placementPoseIsValid)
            {
                Debug.Log("placementPoseIsValid is Valid!");

                placementPose = hits[0].pose;
                var cameraForward = arCamera.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
                positionIndicator.SetActive(true);
                positionIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
            else
            {
                Debug.Log("placementPoseIsValid is not Valid!");
                positionIndicator.SetActive(false);
            }

        }

        private void PlaceObject()
        {
            Instantiate(prefabToPlace, placementPose.position, placementPose.rotation);

        }
    }
}
