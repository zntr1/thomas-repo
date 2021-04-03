using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;
using UnityEditor;
using System.Net;
using System.Security.Cryptography;
using System.Collections.Specialized;
using UnityEngine.UI;
using System.Globalization;

public class TuerGenerator : MonoBehaviour
{
    [Tooltip("Das genutzte Logging-Script muss in dem nutzenden Script instanziert werden.")]
    public Logging log; // immer erst initialisieren via log.Initialisiere();

    [Tooltip("true = Erhoehung der Loggingtiefe")]
    public bool debug = false;

    public Material mat1;
    public Material mat2;
    public Material IN;
    public Material OUT;

    // Objekt-Bestandteile
    public string oBT_Tuerblatt = "";
    public string oBT_Zarge = "";
    public string oBT_DrueckerFalz = "";
    public string oBT_DrueckerZier = "";
    public string oBT_Band1 = "";
    public string oBT_Band2 = "";
    public string oBT_Bandaufnahme1 = "";
    public string oBT_Bandaufnahme2 = "";
    public string oBT_Schlosskasten = "";
    public string oBT_Schliessblech = "";
    public string oBT_Schwelle = "";
    // Objekt-Rotationen
    //public string oRT_Rotation1 = "";


    public Vector3 position;
    public Vector3 rotation;

    private Text displayText;

    private bool toggleBoolInOut;


    string[] materialien = {
        "Material/weiss",
        "Material/rot",
        "Material/04_Holz/04-1_pur/M_1K_3DTE_Wood_020",
        "Material/04_Holz/04-1_pur/M_2K_CC0T_Wood003_2K-JPG",
        "Material/04_Holz/04-1_pur/M_2K_CC0T_Wood007_2K-JPG",
        "Material/04_Holz/04-1_pur/M_2K_CC0T_Wood010_2K-JPG",
        "Material/04_Holz/04-1_pur/M_2K_CC0T_Wood011_2K-JPG",
        "Material/04_Holz/04-1_pur/M_2K_CC0T_Wood014_2K-JPG",
        "Material/04_Holz/04-1_pur/M_2K_CC0T_Wood015_2K-JPG",
        "Material/04_Holz/04-1_pur/M_2K_CC0T_Wood016_2K-JPG",
        "Material/04_Holz/04-1_pur/M_2K_CC0T_Wood052_2K-JPG",
        "Material/04_Holz/04-1_pur/M_2K_TCAN_wood_0005_2k_j3Ev4S",
        "Material/04_Holz/04-1_pur/M_2K_TCAN_wood_0015_2k_U9kCUd"
    };
    private int aktuellesMaterial = -1;

    // public Mesh mesh;
    // public Material material;

    private GameObject[] gosInPrefabOrdner;
    private GameObject goNeueInstanz;
    private GameObject goNeuInHierarchie;
    private GameObject goRotationspunkt;


    // ---------------------------------------------------------------------------------------------------
    // Objekt-Konstruktion
    // ---------------------------------------------------------------------------------------------------

    // hierarchiePfadObjekt 
    string hierarchiePfadPrefabs = "TEMP_PREFABS";

    string[] objektBestandteile = { "Tuerblatt", "Zarge", "DrueckerFalz", "DrueckerZier", "Band1", "Band2", "Bandaufnahme1", "Bandaufnahme2", "Schlosskasten", "Schliessblech", "Schwelle" };
    string[] objektRotationen = { "Rotation1" };

    // ---------------------------------------------------------------------------------------------------
    //  Unity Hierarchie(-Elemente)
    // ---------------------------------------------------------------------------------------------------

    // hierarchiePfadObjekt 
    string hierarchiePfadObjekt = "INNENTUER";

    // 2D-Array mit Zuordnung 
    // 1 Position = Bestandteil
    // 2 Position = das dazugehörige parent GameObjekt)
    string[,] objektElementeUndParents = {
        // Level 1  
        { "OUT_Zarge", "INNENTUER" },
        // Level 2
        {"3D_Zarge" , "OUT_Zarge"},
        {"IN_Bandaufnahme1" , "OUT_Zarge"},
        {"OUT_Bandaufnahme1" , "OUT_Zarge"},
        {"IN_Bandaufnahme2" , "OUT_Zarge"},
        {"OUT_Bandaufnahme2" , "OUT_Zarge"},
        {"IN_Schliessblech" , "OUT_Zarge"},
        {"OUT_Schliessblech" , "OUT_Zarge"},
        {"IN_Tuerblatt" , "OUT_Zarge"},
        {"OUT_Tuerblatt" , "OUT_Zarge"},
        {"IN_Schwelle" , "OUT_Zarge"},
        {"OUT_Schwelle" , "OUT_Zarge"},
        // Level 3        
        {"3D_Bandaufnahme1" , "OUT_Bandaufnahme1"},
        {"3D_Bandaufnahme2" , "OUT_Bandaufnahme2"},
        {"3D_Schliessblech" , "OUT_Schliessblech"},
        {"3D_Tuerblatt" , "OUT_Tuerblatt"},
        {"IN_DrueckerFalz" , "OUT_Tuerblatt"},
        {"OUT_DrueckerFalz" , "OUT_Tuerblatt"},
        {"IN_DrueckerZier" , "OUT_Tuerblatt"},
        {"OUT_DrueckerZier" , "OUT_Tuerblatt"},
        {"IN_Band1" , "OUT_Tuerblatt"},
        {"OUT_Band1" , "OUT_Tuerblatt"},
        {"IN_Band2" , "OUT_Tuerblatt"},
        {"OUT_Band2" , "OUT_Tuerblatt"},
        {"IN_Schlosskasten" , "OUT_Tuerblatt"},
        {"OUT_Schlosskasten" , "OUT_Tuerblatt"},
        {"ROTIN_Rotation1" , "OUT_Tuerblatt"},
        {"3D_Schwelle" , "OUT_Schwelle"},
        // Level 4
        {"3D_DrueckerFalz" , "OUT_DrueckerFalz"},
        {"3D_DrueckerZier" , "OUT_DrueckerZier"},
        {"3D_Band1" , "OUT_Band1"},
        {"3D_Band2" , "OUT_Band2"},
        {"3D_Schlosskasten" , "OUT_Schlosskasten"}

    };

    //string[,] objektRotationenUndParents = {
    //    // Rotationen
    //    { "ROT_Rotation1", "OUT_Zarge" },
    //};

    // Rotation
    private Transform rotierer;
    private Transform rotierachseBezug;
    public float rotationZeit = 1.0f;
    public float rotationWinkel = 90.0f;
    private bool rotating = false;
    private bool tuerAuf = false;


    // Start is called before the first frame update
    void Awake()
    {
        //Initialisiere das Logging
        log.Initialisiere();
        displayText = GameObject.Find("Canvas/displayText").GetComponent<Text>();
        displayText.text = "Taste 1-3 = Scenenaufbau\n4 = Materialwechsel\n<- = Tür auf\n-> = Tür zu" +
            "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            displayText.text = "Taste 1:\nGeneriere Hierarchie\n(Excel 6)";
            Schritt_1();
        }

        if (Input.GetKeyDown("2"))
        {
            displayText.text = "Taste 2:\nInstanziere Bestandteile\n(Excel 2)";
            Schritt_2();
        }

        if (Input.GetKeyDown("3"))
        {
            displayText.text = "Taste 3:\nZuordnung Instanzen -> Hierarchie\n(Excel 7)";
            Schritt_3();
        }

        if (Input.GetKeyDown("4"))
        {
            displayText.text = "Taste 4\nMaterialwechsel";
            toggleMaterial();
        }

        // Rotation
        if (Input.GetKeyDown(KeyCode.RightArrow) && !rotating && tuerAuf)
        {

            displayText.text = "Taste ->\nTür zu";
            rotierer = GameObject.Find("OUT_Tuerblatt").GetComponent<Transform>();
            rotierachseBezug = GameObject.Find("ROTIN_Rotation1").GetComponent<Transform>();
            Debug.Log(" S32 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
            //Debug.Log(" S32 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));
            StartCoroutine(Rotate(rotierer, rotierachseBezug, Vector3.up, -rotationWinkel, rotationZeit));
            tuerAuf = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !rotating && !tuerAuf)
        {
            displayText.text = "Taste <-\nTür auf";
            rotierer = GameObject.Find("OUT_Tuerblatt").GetComponent<Transform>();
            rotierachseBezug = GameObject.Find("ROTIN_Rotation1").GetComponent<Transform>();
            Debug.Log(" S32 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
            //Debug.Log(" S32 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));
            StartCoroutine(Rotate(rotierer, rotierachseBezug, Vector3.up, rotationWinkel, rotationZeit));
            tuerAuf = true;
        }



        if (Input.GetKeyDown("5"))
        {
            displayText.text = "Taste 5\nToggle IN_/OUT_";
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (!toggleBoolInOut && ((go.name.Contains("IN_")) || (go.name.Contains("OUT_"))))
                {
                    //Debug.Log(go.name);
                    toggleBoolInOut = true;
                    go.SetActive(false);
                }
                else
                {
                    toggleBoolInOut = false;
                    go.SetActive(true);
                }
            }

        }

    }

    void Schritt_1()
    {

        int anzahlobjektElementeUndParents = objektElementeUndParents.Length / 2;
        Debug.Log("Schritt 1: Generiere Objekt-Hierarchie aus insgesamt " + anzahlobjektElementeUndParents.ToString() + " Elementen gemaess Excel Datei Punkt 6. Unity Hierarchie(-Elemente)");

        // generiere Hirarchie

        // generiere als hierarchiePfadObjekt ein leeres GameObjekt
        goNeuInHierarchie = new GameObject(hierarchiePfadObjekt);

        // generiere für jedes Bestandteil des Objeketes ein GameObjekt mit leerem MeshFilter und leerem MeshRenderer
        for (int i = 0; i < anzahlobjektElementeUndParents; i++)
        {
            //log.wl(objektElementeUndParents[i, 0] + " --- " + objektElementeUndParents[i, 1]);
            goNeuInHierarchie = new GameObject(objektElementeUndParents[i, 0]);
            MeshFilter meshFilter = goNeuInHierarchie.AddComponent<MeshFilter>();
            //meshFilter.sharedMesh = mesh;
            MeshRenderer meshRenderer = goNeuInHierarchie.AddComponent<MeshRenderer>();
            //meshRenderer.material = material;

            //string child = objektElementeUndParents[i, 0];
            //string parent = objektElementeUndParents[i, 1];

            // ordne jedem Bestandteil (child) dem jeweiligen parent GameObjekt zu
            GameObject.Find(objektElementeUndParents[i, 0]).transform.parent = GameObject.Find(objektElementeUndParents[i, 1]).transform;

        };


        //int anzahlobjektRotationenUndParents = objektRotationenUndParents.Length / 2;

        //// generiere für jede Rotation des Objeketes ein GameObjekt mit leerem MeshFilter und leerem MeshRenderer
        //for (int i = 0; i < anzahlobjektRotationenUndParents; i++)
        //{

        //    goNeuInHierarchie = new GameObject(objektRotationenUndParents[i, 0]);
        //    MeshFilter meshFilter = goNeuInHierarchie.AddComponent<MeshFilter>();
        //    MeshRenderer meshRenderer = goNeuInHierarchie.AddComponent<MeshRenderer>();
        //    GameObject.Find(objektRotationenUndParents[i, 0]).transform.parent = GameObject.Find(objektRotationenUndParents[i, 1]).transform;
        //}

    }

    void Schritt_2()
    {
        // generiere Hirarchie
        // generiere als hierarchiePfadPrefabs ein leeres GameObjekt
        goNeuInHierarchie = new GameObject(hierarchiePfadPrefabs);

        string[] gesuchtePrefabNamen = { oBT_Tuerblatt, oBT_Zarge };
        Debug.Log("Schritt 2: Instanziere Prefabs anhand der Namen");

        // ermittele alle Prefabs im Prefab Order "Assets/Resourcen/Prefabs"
        gosInPrefabOrdner = Resources.LoadAll<GameObject>("Prefabs");


        if (oBT_Tuerblatt != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Tuerblatt, "Tuerblatt");

        }

        if (oBT_Zarge != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Zarge, "Zarge");
        }

        if (oBT_DrueckerFalz != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_DrueckerFalz, "DrueckerFalz");
        }

        if (oBT_DrueckerZier != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_DrueckerZier, "DrueckerZier");
        }

        if (oBT_Band1 != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Band1, "Band1");
        }

        if (oBT_Band2 != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Band2, "Band2");
        }

        if (oBT_Bandaufnahme1 != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Bandaufnahme1, "Bandaufnahme1");
        }

        if (oBT_Bandaufnahme2 != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Bandaufnahme2, "Bandaufnahme2");
        }

        if (oBT_Schlosskasten != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Schlosskasten, "Schlosskasten");
        }

        if (oBT_Schliessblech != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Schliessblech, "Schliessblech");
        }

        if (oBT_Schwelle != "")
        {
            InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Schwelle, "Schwelle");
        }

        //// Rotation
        //if (oRT_Rotation1 != "")
        //{
        //    InstanzierePrefabWennNameDesPrefabsExistiert(oRT_Rotation1, "Rotation1");
        //}

    }

    void InstanzierePrefabWennNameDesPrefabsExistiert(string nameDesGesuchtenPrefabs, string bestandteil)
    {
        bool esGibtEinPrefabDesGesuchtenNamens = false;

        // checke für jedes Prefab im Prefab-Ordner, ... 
        foreach (GameObject goInPrefabOrdner in gosInPrefabOrdner)
        {

            // ob das Prefab (ohne den seitens Unity automatisch hinzugefühten Suffix " (UnityEngine.GameObject)" so heißt, wie der Name des gesuchten Prefabs
            // Wenn ja:
            if (KappeStringSuffix(goInPrefabOrdner.ToString(), " (UnityEngine.GameObject)") == nameDesGesuchtenPrefabs)
            {
                esGibtEinPrefabDesGesuchtenNamens = true;

                // generiere ein neues GameObject in der Scene aus einer Intanz des Prefabs
                goNeueInstanz = goInPrefabOrdner;
                Instantiate(goNeueInstanz);

                // ordne dem neuen GameObject das parent GameObjekt zu
                GameObject.Find(nameDesGesuchtenPrefabs + "(Clone)").transform.parent = GameObject.Find(hierarchiePfadPrefabs).transform;

                // benenne das neue GameObjekt in den jeweiligen Namen des Bestandteils 
                GameObject.Find(nameDesGesuchtenPrefabs + "(Clone)").name = bestandteil;

            }
        }

        if (!esGibtEinPrefabDesGesuchtenNamens)
        {
            Debug.Log("ACHTUNG: Dem Objektbestandteil OBT_" + bestandteil + " wurde ein Name zugewiesen, zu dem es kein dazugehöriges Prefab im Prefab-Ordner gibt.");
        }
    }

    string KappeStringSuffix(string original, string suffix)
    {
        int positionAnDerSuffixImOriginalStringBeginnt = original.IndexOf(suffix);
        string originalOhneSuffix = original.Substring(0, positionAnDerSuffixImOriginalStringBeginnt);
        return originalOhneSuffix;
    }

    void Schritt_3()
    {
        Debug.Log(" S3 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
        //Debug.Log(" S3 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));

        Debug.Log("Schritt 3: Ordne die MeshFilter der Prefab-GameObjekte den dazugehoerigen GameObjekten des Unity-Hierarchie zu ");

        // TEMP_PREFABS --> INNENTUER
        string[,] tempZuObjekt = {
            // Zarge
            { "/Zarge/OUT_Zarge", "/OUT_Zarge" },
            { "/Zarge/3D_Zarge", "/OUT_Zarge/3D_Zarge" },
            { "/Zarge/IN_Bandaufnahme1", "/OUT_Zarge/IN_Bandaufnahme1" },
            { "/Zarge/IN_Bandaufnahme2", "/OUT_Zarge/IN_Bandaufnahme2" },
            { "/Zarge/IN_Schliessblech", "/OUT_Zarge/IN_Schliessblech" },
            { "/Zarge/IN_Tuerblatt", "/OUT_Zarge/IN_Tuerblatt" },
            { "/Zarge/IN_Tuerblatt", "/OUT_Zarge/IN_Tuerblatt" },
            //{ "/Zarge/IN_Schwelle", "/OUT_Zarge/IN_Schwelle" },
            //{ "/Zarge/ROTIN_Rotation1", "/OUT_Zarge/ROT_Rotation1" },
            // Bandaufnahme1
            { "/Bandaufnahme1/OUT_Bandaufnahme", "/OUT_Zarge/OUT_Bandaufnahme1" },
            { "/Bandaufnahme1/3D_Bandaufnahme", "/OUT_Zarge/OUT_Bandaufnahme1/3D_Bandaufnahme1" },
            // Bandaufnahme2
            { "/Bandaufnahme2/OUT_Bandaufnahme", "/OUT_Zarge/OUT_Bandaufnahme2" },
            { "/Bandaufnahme2/3D_Bandaufnahme", "/OUT_Zarge/OUT_Bandaufnahme2/3D_Bandaufnahme2" },
            // Schliessblech
            { "/Schliessblech/OUT_Schliessblech", "/OUT_Zarge/OUT_Schliessblech" },
            { "/Schliessblech/3D_Schliessblech", "/OUT_Zarge/OUT_Schliessblech/3D_Schliessblech" },
            // Tuerblatt
            { "/Tuerblatt/OUT_Tuerblatt", "/OUT_Zarge/OUT_Tuerblatt" },
            { "/Tuerblatt/3D_Tuerblatt", "/OUT_Zarge/OUT_Tuerblatt/3D_Tuerblatt" },
            { "/Tuerblatt/IN_DrueckerFalz", "/OUT_Zarge/OUT_Tuerblatt/IN_DrueckerFalz" },
            { "/Tuerblatt/IN_DrueckerZier", "/OUT_Zarge/OUT_Tuerblatt/IN_DrueckerZier" },
            { "/Tuerblatt/IN_Band1", "/OUT_Zarge/OUT_Tuerblatt/IN_Band1" },
            { "/Tuerblatt/IN_Band2", "/OUT_Zarge/OUT_Tuerblatt/IN_Band2" },
            { "/Tuerblatt/IN_Schlosskasten", "/OUT_Zarge/OUT_Tuerblatt/IN_Schlosskasten" },
            // Schwelle
            //{ "/Schwelle/OUT_Schwelle", "/OUT_Zarge/OUT_Schwelle" },
            //{ "/Schwelle/3D_Schwelle", "/OUT_Zarge/OUT_Schwelle/3D_Schwelle" },
            // DrueckerFalz
            { "/DrueckerFalz/OUT_Druecker", "/OUT_Zarge/OUT_Tuerblatt/OUT_DrueckerFalz" },
            { "/DrueckerFalz/3D_Druecker", "/OUT_Zarge/OUT_Tuerblatt/OUT_DrueckerFalz/3D_DrueckerFalz" },
            // DrueckerZier
            { "/DrueckerZier/OUT_Druecker", "/OUT_Zarge/OUT_Tuerblatt/OUT_DrueckerZier" },
            { "/DrueckerZier/3D_Druecker", "/OUT_Zarge/OUT_Tuerblatt/OUT_DrueckerZier/3D_DrueckerZier" },
            // Band1
            { "/Band1/OUT_Band", "/OUT_Zarge/OUT_Tuerblatt/OUT_Band1" },
            { "/Band1/3D_Band", "/OUT_Zarge/OUT_Tuerblatt/OUT_Band1/3D_Band1" },
            // Band 2
            { "/Band2/OUT_Band", "/OUT_Zarge/OUT_Tuerblatt/OUT_Band2" },
            { "/Band2/3D_Band", "/OUT_Zarge/OUT_Tuerblatt/OUT_Band2/3D_Band2" },
            // Level 4
            { "/Schlosskasten/OUT_Schlosskasten", "/OUT_Zarge/OUT_Tuerblatt/OUT_Schlosskasten" },
            { "/Schlosskasten/3D_Schlosskasten", "/OUT_Zarge/OUT_Tuerblatt/OUT_Schlosskasten/3D_Schlosskasten" },
            // Rotationen
            { "/Tuerblatt/ROTIN_Rotation1", "/OUT_Zarge/OUT_Tuerblatt/ROTIN_Rotation1" },
        };

        int anzahlTempZuObjekt = tempZuObjekt.Length / 2;

        for (int i = 0; i < anzahlTempZuObjekt; i++)
        {
            // weise dem Objekt in der Hierarchie das mesh der dazugehörigen Prefab-GameObjekt-Instanz zu 
            if (GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]))
            {
                GameObject.Find(hierarchiePfadObjekt + tempZuObjekt[i, 1]).GetComponent<MeshFilter>().mesh = GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]).GetComponent<MeshFilter>().mesh;
                GameObject.Find(hierarchiePfadObjekt + tempZuObjekt[i, 1]).transform.position = GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]).transform.position;
                GameObject.Find(hierarchiePfadObjekt + tempZuObjekt[i, 1]).transform.rotation = GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]).transform.rotation;
                GameObject.Find(hierarchiePfadPrefabs + tempZuObjekt[i, 0]).SetActive(false);
            }
        }
        Debug.Log(" S32 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
        //Debug.Log(" S32 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));

        ////GameObject.Find(hierarchiePfadObjekt + "/ROT_Rotation1").GetComponent<MeshFilter>().mesh = GameObject.Find(hierarchiePfadPrefabs + "/OUT_Zarge/OUT_Tuerblatt/OUT_Band1").GetComponent<MeshFilter>().mesh;
        //Debug.Log((GameObject.Find("/INNENTUER/ROT_Rotation1").transform.position).ToString());
        //Debug.Log((GameObject.Find("/INNENTUER/OUT_Zarge/OUT_Bandaufnahme1").transform.position).ToString());
        //Debug.Log((GameObject.Find("/INNENTUER/ROT_Rotation1").transform.rotation).ToString());
        //Debug.Log((GameObject.Find("/INNENTUER/OUT_Zarge/OUT_Bandaufnahme1").transform.rotation).ToString());
        ////GameObject.Find("/INNENTUER/ROT_Rotation1").transform.position = GameObject.Find("/INNENTUER/OUT_Zarge/OUT_Bandaufnahme1").transform.position;
        ////GameObject.Find("/INNENTUER/ROT_Rotation1").transform.rotation = GameObject.Find("/INNENTUER/OUT_Zarge/OUT_Bandaufnahme1").transform.rotation;


        // Gib allen childreen Material
        Renderer[] renderers = GameObject.Find("INNENTUER").GetComponentsInChildren<Renderer>();
        foreach (var ren in renderers)
        {
            ren.material = mat1;
        }

        Renderer r;

        r = GameObject.Find("3D_Band1").GetComponent<Renderer>();
        r.material = mat2;

        r = GameObject.Find("3D_Band2").GetComponent<Renderer>();
        r.material = mat2;

        r = GameObject.Find("3D_Bandaufnahme1").GetComponent<Renderer>();
        r.material = mat2;

        r = GameObject.Find("3D_Bandaufnahme2").GetComponent<Renderer>();
        r.material = mat2;

        r = GameObject.Find("IN_Tuerblatt").GetComponent<Renderer>();
        r.material = IN;

        r = GameObject.Find("3D_DrueckerFalz").GetComponent<Renderer>();
        r.material = mat2;

        r = GameObject.Find("3D_DrueckerZier").GetComponent<Renderer>();
        r.material = mat2;

        r = GameObject.Find("3D_Schlosskasten").GetComponent<Renderer>();
        r.material = mat2;

        r = GameObject.Find("3D_Schliessblech").GetComponent<Renderer>();
        r.material = mat2;

        r = GameObject.Find("ROTIN_Rotation1").GetComponent<Renderer>();
        r.material = OUT;

        foreach (string objektBestandteil in objektBestandteile)
        {
            if (objektBestandteil != "Zarge")
            {
                GameObject.Find("OUT_" + objektBestandteil).transform.position = GameObject.Find("IN_" + objektBestandteil).transform.position;
                GameObject.Find("OUT_" + objektBestandteil).transform.rotation = GameObject.Find("IN_" + objektBestandteil).transform.rotation;
            }
        }

        Vector3 ls = GameObject.Find("3D_DrueckerZier").transform.localScale;
        //ls += new Vector3(ls.x, ls.y, -ls.z);
        ls = new Vector3(1, 1, -1);
        GameObject.Find("3D_DrueckerZier").transform.localScale = ls;

        //Vector3 position2 = new Vector3(-0.04385f, 0.0f, 0.02393684f); ;
        //GameObject.Find("ROT_Rotation1").GetComponent<MeshFilter>().mesh = GameObject.Find("ROTIN_Rotation1").GetComponent<MeshFilter>().mesh;
        //GameObject.Find("ROT_Rotation1").transform.position = position2;
        //GameObject.Find("ROT_Rotation1").transform.rotation = GameObject.Find("ROTIN_Rotation1").transform.rotation;


        //Vector3 newPosition = GameObject.Find("ROT_Rotation1").transform.position + GameObject.Find("ROTIN_Rotation1").transform.position;
        //GameObject.Find("ROT_Rotation1").transform.position = newPosition;
        //Vector3 newRotation = GameObject.Find("ROT_Rotation1").transform.rotation + GameObject.Find("ROTIN_Rotation1").transform.rotation;
        //GameObject.Find("ROT_Rotation1").transform.rotation = newRotation;

        GameObject.Find("OUT_Zarge").transform.position = position;
        GameObject.Find("OUT_Zarge").transform.rotation = Quaternion.Euler(rotation);

        toggleMaterial();
    }

    void toggleMaterial()
    {
        Material yourMaterial;

        Renderer r;

        //Material yourMaterial = (Material)Resources.Load("04_Holz/04-1_pur/M_2K_CC0T_Wood003_2K", typeof(Material));
        //Material yourMaterial = Resources.Load<Material>("Material/03-4_Struktur/M_2K_CC0T_MetalPlates006_2K-JPG");
        // Load a text file (Assets/Resources/Material/04_Holz/04-1_pur/M_2K_CC0T_Wood003_2K)
        if (aktuellesMaterial < materialien.Length-1)
        {
            aktuellesMaterial += 1;
        }
        else
        {
            aktuellesMaterial = 0;
        }

        //Debug.Log("Array-Laenge: " + materialien.Length.ToString());
        //Debug.Log("Array-Index:  " + aktuellesMaterial.ToString());
        //Debug.Log("Material:     " + materialien[aktuellesMaterial]);
        //Debug.Log("--------------------------------------------------------------------------------------");

        yourMaterial = Resources.Load<Material>(materialien[aktuellesMaterial]);
        r = GameObject.Find("3D_Zarge").GetComponent<Renderer>();
        r.material = yourMaterial;
        r = GameObject.Find("3D_Tuerblatt").GetComponent<Renderer>();
        r.material = yourMaterial;

        //Material yourMaterial = Resources.Load<Material>(mm);
        //Material yourMaterial = Resources.Load<Material>("Material/04_Holz/04-1_pur/M_2K_CC0T_Wood003_2K");

    }

    private IEnumerator Rotate(Transform rotierer, Transform rotierachseBezug, Vector3 rotateAxis, float degrees, float totalTime)
    {
        Debug.Log(rotierachseBezug.ToString());

        if (rotating)
        {
            yield return null;
        }
        else
        {
            rotating = true;
        }


        float rate = degrees / totalTime;

        //Start Rotate
        for (float i = 0.0f; Mathf.Abs(i) < Mathf.Abs(degrees); i += Time.deltaTime * rate)
        {
            rotierer.RotateAround(rotierachseBezug.position, rotateAxis, Time.deltaTime * rate);
            yield return null;
        }

        rotating = false;
    }

}
