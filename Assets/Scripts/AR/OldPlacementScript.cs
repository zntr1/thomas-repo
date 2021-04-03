using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Exception = System.Exception;
using MeshRenderer = UnityEngine.MeshRenderer;

namespace Assets.Scripts.AR
{
    public class OldPlacementScript : MonoBehaviour
    {

        private GameObject spawnedGameObject;
        private ARRaycastManager aRRaycastManager;
        private Vector2 touchPos;
        private bool isObjectPlaced = false;
        
        private int currentDoorNr = 1;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private GENERATOR_Innentuer generator;
        private Konfigurator Konfigurator;
        // Start is called before the first frame update
        void Awake()
        {
            aRRaycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
            generator = GameObject.Find("GeneratorManager").GetComponent<GENERATOR_Innentuer>();
            Konfigurator = GameObject.Find("KonfiguratorManager").GetComponent<Konfigurator>();

            //swapDoorButton.onClick.AddListener(() =>
            //{
            //    SetPrefab($"Innentuer_{currentDoorNr}");
            //    if (currentDoorNr < 4)
            //    {
            //        currentDoorNr++;
            //    }
            //    else
            //    {
            //        currentDoorNr = 1;
            //    }
            //});

            //swapMaterialButton.onClick.AddListener(ChangeMaterial);

        }

        bool TryGetTouchPosition()
        {
            if (Input.touchCount > 0)
            {
                touchPos = Input.GetTouch(0).position;
                return true;

            }

            touchPos = default;
            return false;
        }

        // Update is called once per frame
        void Update()
        {
            try
            {
                if (!TryGetTouchPosition())
                {
                    return;
                }
                Debug.Log("Schritt 2");


                // Platziere objekt
                if (aRRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
                {
                    Debug.Log("Schritt 3");

                    var hitPose = hits[0].pose;

                   
                    // Get current Innentuer model!
                    var innentuerDummy = GameObject.Find("INNENTUER");
                    innentuerDummy.transform.position = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);
                    innentuerDummy.transform.rotation = hitPose.rotation;
                    //spawnedGameObject = Instantiate(PrefabToSpawn, hitPose.position, hitPose.rotation);
                    //RotateManager.GetInstance().SetDoor(spawnedGameObject);
                    //PositionManager.GetInstance().SetDoor(spawnedGameObject);
                    ////SwapManager.GetInstance().SetDoor(spawnedGameObject);

                    isObjectPlaced = true;
                    //RotationCanvas.gameObject.SetActive(true);
                    //PositionCanvas.gameObject.SetActive(true);


                    // Neu
                    Konfigurator.MachF3();
                    spawnedGameObject = innentuerDummy;
                    innentuerDummy.transform.position = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);
                    innentuerDummy.transform.rotation = hitPose.rotation;


                }

            }
            catch (Exception ex)
            {
                Debug.LogError("Fehler:");
                Debug.LogError(ex.Message);
            }
          
        
        }

        public void NextDoor()
        {
            Konfigurator.MachTaste0();
        }

        //public void SetPrefab(String prefabName)
        //{
        //    Debug.Log("Changing Prefab to " + prefabName);
        //    PrefabToSpawn = Resources.Load<GameObject>($"Prefabs/{prefabName}");

        //    if (spawnedGameObject == null)
        //    {
        //        Debug.LogError("Cant change Prefab of not existing GameObject..");
        //        return;
        //    }

        //    var pos = spawnedGameObject.transform.position;
        //    var rot = spawnedGameObject.transform.rotation;

        //    // Destroy existing Door
        //    Destroy(spawnedGameObject);
        //    spawnedGameObject = default;

        //    // Respawn new door with other Prefab
        //    spawnedGameObject = Instantiate(PrefabToSpawn, pos, rot);
        //    Debug.Log("setPrefab finished!");
        //}

        //public void ChangeMaterial()
        //{
        //    Debug.Log("Changing Material!");

        //    if (spawnedGameObject == null)
        //    {
        //        Debug.LogError("Cant change Material of Component if it does not exist.");
        //        return;

        //    }

        //    var transform = RecursiveFindChild(spawnedGameObject.transform, "Mesh8");

        //    transform.gameObject.GetComponent<MeshRenderer>().material = MaterialToBeSet; 
        
     
        //    // PrefabToSpawn = Resources.Load<GameObject>($"Prefabs/Innentuer_1");
        //    // var Tuerblatt = spawnedGameObject.transform.Find("Tuerblatt").gameObject;
        //    // var Tuerblatt = spawnedGameObject.transform.Find("G_1985x860").gameObject;


        //    Debug.Log("Changed the material!" + transform);



     
        //    // var materials = spawnedGameObject.GetComponent<Renderer>().materials;
        //    //
        //    // foreach (var material in materials)
        //    // {
        //    //     Debug.Log("Next Material:");
        //    //     Debug.Log(material.name);
        //    //     Debug.Log(material.color);
        //    //     Debug.Log(material.mainTexture);
        //    // }
        //}

        Transform RecursiveFindChild(Transform parent, string childName)
        {
            Debug.Log("Recursive..");
            foreach (Transform child in parent)
            {
                if (child.name == childName)
                {
                    return child;
                }
                else
                {
                    Transform found = RecursiveFindChild(child, childName);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }


    }
}
