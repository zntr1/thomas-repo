using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.AR
{
    public class PlacementScript : MonoBehaviour
    {
        public GameObject prefabToSpawn;
        public GameObject prefabToSpawn2;
        public GameObject positionIndicator;
        private GameObject SpawnedGameObject;
        private ARRaycastManager aRRaycastManager;
        public Camera aRCamera;
        private Vector2 touchPos;
        private bool isObjectPlaced = false;
        private bool isPlacingLocked = false;
        private bool isOnTouchHold = false;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private List<GameObject> objectListe;
        private int currentIndex = 0;

        void Awake()
        {
            objectListe = new List<GameObject>();
            objectListe.Add(prefabToSpawn);
            objectListe.Add(prefabToSpawn2);
            aRRaycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
            
        }

        bool TryGetTouchPosition()
        {
            if (Input.touchCount > 0)
            {
                Debug.Log("Touchcounter gt 0");
                var touch= Input.GetTouch(0);
                Debug.Log(touch.type);

                touchPos = touch.position;
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("TouchPhase Began:");

                    //Ray ray = aRCamera.ScreenPointToRay(touchPos);
                    Ray ray = aRCamera.ScreenPointToRay(touchPos);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                    
                        Debug.Log("hitObject hit:");
                        Debug.Log(hitObject.transform.name);
                        Debug.Log("SpawnedGameObject:");
                        Debug.Log(SpawnedGameObject.name);
                        if (hitObject.transform.name.Contains(SpawnedGameObject.name))
                        {
                            Debug.Log(hitObject.transform.name);
                            isOnTouchHold = true;
                        }
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("TouchPhase Ended:");

                    isOnTouchHold = false;
                }

                return true;
            }

            //touchPos = default;
            return false;
        }

        // Update is called once per frame
        public void Update()
        {
            try
            {
                //UpdatePlacementPose();

                TryGetTouchPosition();
                
                // erstelle
                if (!isObjectPlaced)
                {
                  

                    // Platziere objekt
                    if (aRRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
                    {
                        if (EventSystem.current.IsPointerOverGameObject())
                        {
                            Debug.Log("Wont Spawn gameobject because click was over a gameobject!");
                            return;
                        }
                        Debug.Log("Objekt noch nicht vorhanden, spawne es!");

                 
                        var hitPose = hits[0].pose;
                        var positionX = hitPose.position.x;
                        var positionY = hitPose.position.y;
                        var positionZ = hitPose.position.z;

                        Debug.Log($"Hitpose: X:{positionX} Y:{positionY} Z:{positionZ}");

                        prefabToSpawn = Instantiate(prefabToSpawn2, new Vector3(positionX, positionY, positionZ), hitPose.rotation);

                  
                        SpawnedGameObject = prefabToSpawn;
                        //objectToSpawn.transform.position = new Vector3(positionX, positionY, positionZ);
                        //objectToSpawn.transform.rotation = hitPose.rotation;

                        UpdateDebugInfo(SpawnedGameObject.transform.position, SpawnedGameObject.transform.localScale);

                        //    // Get current Innentuer model!
                        //    var innentuerDummy = GameObject.Find("INNENTUER");
                        //innentuerDummy.transform.position = new Vector3(positionX, positionY, positionZ);
                        //innentuerDummy.transform.rotation = hitPose.rotation;

                        //if (SpawnedGameObject.transform.hasChanged)
                        //{

                        //}

                        isObjectPlaced = true;
                        positionIndicator.SetActive(false);

                        //spawnedGameObject = innentuerDummy;
                        //innentuerDummy.transform.position = new Vector3(positionX, positionY, positionZ);
                        //innentuerDummy.transform.rotation = hitPose.rotation;

                    }
                }
                // verschiebe
                else
                {
                    // Platziere objekt
                    if (!isPlacingLocked && isOnTouchHold && aRRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
                    {

                        Debug.Log("Objekt schon vorhanden, verschiebe es!");

                        var hitPose = hits[0].pose;
                        var positionX = hitPose.position.x;
                        var positionY = hitPose.position.y;
                        var positionZ = hitPose.position.z;

                                
                        Debug.Log($"Hitpose: X:{positionX} Y:{positionY} Z:{positionZ}");
                        SpawnedGameObject.transform.position = new Vector3(positionX, positionY, positionZ);
                        SpawnedGameObject.transform.rotation = hitPose.rotation;

                        UpdateDebugInfo(SpawnedGameObject.transform.position, SpawnedGameObject.transform.localScale);


                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }

        }

        public void DisablePlanes()
        {
         

            var x = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
            x.enabled = !x.enabled;
            //session.Reset();
            foreach (ARPlane plane in x.trackables)
            {
                plane.gameObject.SetActive(x.enabled);
            }

            if (!x.enabled)
            {
                isPlacingLocked = true;
            }
            else
            {
                isPlacingLocked = false;
            }

        }

        public void UpdateDebugInfo(Vector3 pos, Vector3 scale)
        {
            GameObject debugPosX = GameObject.Find("debugPosX");
            Debug.Log(debugPosX.ToString());
            if (debugPosX == null) return;
            var posX = debugPosX.GetComponent<TextMeshProUGUI>();
            Debug.Log(posX.ToString());
            if (posX == null) return;

            var posY = GameObject.Find("debugPosY").GetComponent<TextMeshProUGUI>();
            var posZ = GameObject.Find("debugPosZ").GetComponent<TextMeshProUGUI>();
            var scaleX = GameObject.Find("debugScaleX").GetComponent<TextMeshProUGUI>();
            var scaleY = GameObject.Find("debugScaleY").GetComponent<TextMeshProUGUI>();
            var scaleZ = GameObject.Find("debugScaleZ").GetComponent<TextMeshProUGUI>();
            if (posX == null)
            {
                Debug.Log("posX ist wohl null :/");
                return;
            }
            posX.text = "X: " + pos.x;
            posY.text = "Y: " + pos.y;
            posZ.text = "Z: " + pos.z;
            scaleX.text = "X: " + scale.x;
            scaleY.text = "Y: " + scale.y;
            scaleZ.text = "Z: " + scale.z;

        }

        public void ChangeObject()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("IsPointerOverGameObject true, returning");
                return;
            }

            if (!isObjectPlaced)
            {
                return;
            }

            if (currentIndex == (objectListe.Count-1))
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            var prefab = objectListe[currentIndex];

            GameObject newObject;

            
            newObject = Instantiate(prefab);
            //newObject.name = prefab.name;
            
            newObject.transform.parent = SpawnedGameObject.transform.parent;
            newObject.transform.localPosition = SpawnedGameObject.transform.localPosition;
            newObject.transform.localRotation = SpawnedGameObject.transform.localRotation;
            //newObject.transform.localScale = SpawnedGameObject.transform.localScale;
            newObject.transform.SetSiblingIndex(SpawnedGameObject.transform.GetSiblingIndex());
            Debug.Log("ChangeObject.name");
            Debug.Log(newObject.name);
            Destroy(SpawnedGameObject);
            SpawnedGameObject = newObject;

            //var theObject = GameObject.Find(SpawnedGameObject.name);
            //theObject = objectListe[currentIndex];
            //SpawnedGameObject = theObject;

            Debug.Log("Changed Object!");

        }

        private void UpdatePlacementPose()
        {
            var screenCenter = aRCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

            // Platziere objekt
            if (aRRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
            {

                Debug.Log("Trying to place Squad?");
                var hits = new List<ARRaycastHit>();

                var isPlacementPoseValid = hits.Count > 0;
                if (isPlacementPoseValid && !isObjectPlaced)
                {
                    Debug.Log("Valid position!");

                    var placementPose = hits[0].pose;
                    var cameraForward = aRCamera.transform.forward;
                    var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                    placementPose.rotation = Quaternion.LookRotation(cameraBearing);
                    positionIndicator.SetActive(true);
                    positionIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

                }
            }
        }
        
    }
}
