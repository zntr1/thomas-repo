using System;
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
using System.Threading.Tasks;
using Assets.Scripts;
using Newtonsoft.Json;

public class GENERATOR_Innentuer : MonoBehaviour
{

    public Material IN;
    public Material OUT;
    public Material ROT;

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

    public AktuelleGetoggelteInnentuer innentuer;

    public Vector3 position;
    public Vector3 rotation;


    private GameObject goNeuInHierarchie;

    // ---------------------------------------------------------------------------------------------------
    // Objekt-Konstruktion
    // ---------------------------------------------------------------------------------------------------

    // hierarchiePfadObjekt 
    private string hierarchiePfadPrefabs = "TEMP_PREFABS";
    private string[] objektBestandteile = { "Tuerblatt", "Zarge", "DrueckerFalz", "DrueckerZier", "Band1", "Band2", "Bandaufnahme1", "Bandaufnahme2", "Schlosskasten", "Schliessblech", "Schwelle" };


    // ---------------------------------------------------------------------------------------------------
    //  Unity Hierarchie(-Elemente)
    // ---------------------------------------------------------------------------------------------------

    // hierarchiePfadObjekt 
    string hierarchiePfadObjekt = "INNENTUER";


    // Rotation
    private Transform rotierer;
    private Transform rotierachseBezug;
    public float rotationZeit = 1.0f;
    public float rotationWinkel = -90.0f;
    public bool rotating = false;
    public bool tuerAuf = false;

    // ---------------------------------------------------------------------------------------------------
    // GUI
    // ---------------------------------------------------------------------------------------------------



    // Start is called before the first frame update
    void Awake()
    {
        // leite Variablen ab
        //objektBestandteileAnzahl = objektBestandteile.Length;
        //objektBestandteileAktuell = new string[objektBestandteileAnzahl];
    }

    // Start is called before the first frame update
    void Start()
    {
        //Schritt_2_und_3();
        //toggleMaterial3();
    }

    // Update is called once per frame
    public void Rotation(string richtung)
    {
        // Rotation
        if (richtung == "zu" && !rotating && tuerAuf)
        {
            rotierer = GameObject.Find("OUT_Tuerblatt").GetComponent<Transform>();
            rotierachseBezug = GameObject.Find("ROTIN_Rotation1").GetComponent<Transform>();
            //Debug.Log(" S32 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
            //Debug.Log(" S32 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));
            StartCoroutine(Rotate(rotierer, rotierachseBezug, Vector3.up, -rotationWinkel, rotationZeit));
            tuerAuf = false;
        }

        if (richtung == "auf" && !rotating && !tuerAuf)
        {
            rotierer = GameObject.Find("OUT_Tuerblatt").GetComponent<Transform>();
            rotierachseBezug = GameObject.Find("ROTIN_Rotation1").GetComponent<Transform>();
            //Debug.Log(" S32 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
            //Debug.Log(" S32 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));
            StartCoroutine(Rotate(rotierer, rotierachseBezug, Vector3.up, rotationWinkel, rotationZeit));
            tuerAuf = true;
        }

    }



    // Lädt die Prefabs in den Variablen, aus local und in das entsprechende Verzeichnis.

    public void ErsetzeAssetsUndTransformiere()
    {
        //if (innentuer.Id == null)
        //{
        //    return;
        //}
        // generiere Hirarchie
        // generiere als hierarchiePfadPrefabs ein leeres GameObjekt
        goNeuInHierarchie = new GameObject(hierarchiePfadPrefabs);

        //// ermittele alle Prefabs im Prefab Order "Assets/Resourcen/Prefabs"
        //gosInPrefabOrdner = Resources.LoadAll<GameObject>("Prefabs");

        //InstanzierePrefabWennNameDesPrefabsExistiert(oBT_Tuerblatt, "Tuerblatt");
        Debug.Log(JsonConvert.SerializeObject(innentuer));
        SetPrefabFromServer(innentuer.Tuerblatt.Bezeichnung, "Tuerblatt");
        SetPrefabFromServer(innentuer.Zarge.Bezeichnung, "Zarge");
        SetPrefabFromServer(innentuer.DrueckerFalz.Bezeichnung, "DrueckerFalz");
        SetPrefabFromServer(innentuer.DrueckerZier.Bezeichnung, "DrueckerZier");
        SetPrefabFromServer(innentuer.Band1.Bezeichnung, "Band1");
        SetPrefabFromServer(innentuer.Band2.Bezeichnung, "Band2");
        SetPrefabFromServer(innentuer.Bandaufnahme1.Bezeichnung, "Bandaufnahme1");
        SetPrefabFromServer(innentuer.Bandaufnahme2.Bezeichnung, "Bandaufnahme2");
        SetPrefabFromServer(innentuer.Schlosskasten.Bezeichnung, "Schlosskasten");
        SetPrefabFromServer(innentuer.Schliessblech.Bezeichnung, "Schliessblech");
        //SetPrefabFromServer(oBT_Schwelle, "Schwelle");

        TransformiereKomponentenPositionen();
    }

   

    public void SetPrefabFromServer(string nameDesGesuchtenPrefabs, string bestandteil)
    {
        Debug.Log("Versuche SetPrefabFromServer für " + nameDesGesuchtenPrefabs + " - " + bestandteil);
        AssetManager manager = new AssetManager();
        nameDesGesuchtenPrefabs = nameDesGesuchtenPrefabs.ToLower();
        var serverAsset = manager.DownloadAsset(nameDesGesuchtenPrefabs);
        if (serverAsset == null)
        {
            Debug.LogError("Fehler! Server Asset ist null!!");
            return;
        }
        GameObject tuerObjekt = Instantiate(serverAsset);
        tuerObjekt.transform.parent = GameObject.Find(hierarchiePfadPrefabs).transform;
        tuerObjekt.name = bestandteil;
    }



    public string ToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
   

    void TransformiereKomponentenPositionen()
    {
        //Debug.Log(" S3 ROTIN--- " + GameObject.Find("ROTIN_Rotation1").transform.position.ToString("f6"));
        //Debug.Log(" S3 ROT -- " + GameObject.Find("ROT_Rotation1").transform.position.ToString("f6"));

        //Debug.Log("Schritt 3: Ordne die MeshFilter der Prefab-GameObjekte den dazugehoerigen GameObjekten des Unity-Hierarchie zu ");

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
            }
        }
        Destroy(GameObject.Find(hierarchiePfadPrefabs));

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

        GameObject.Find("OUT_Zarge").transform.position = position;
        GameObject.Find("OUT_Zarge").transform.rotation = Quaternion.Euler(rotation);

    }

    public void RefreshMaterialsForAllComponents()
    {
        Debug.Log("RefreshMaterialsForAllComponents()");

        ///local
        SetLocalMaterial("3D_Zarge");
        SetLocalMaterial("3D_Tuerblatt");
        SetLocalMaterial("3D_DrueckerFalz");
        SetLocalMaterial("3D_DrueckerZier");
        SetLocalMaterial("3D_Band1");
        SetLocalMaterial("3D_Band2");
        SetLocalMaterial("3D_Bandaufnahme1");
        SetLocalMaterial("3D_Bandaufnahme2");
        SetLocalMaterial("3D_Schliessblech");
        SetLocalMaterial("3D_Schlosskasten");
        SetLocalMaterial("3D_Schwelle");
        

        // web
        //SetMaterialsForZarge();
        //SetMaterialsForTuerblatt();
        //SetMaterialsForDrueckerFalz();
        //SetMaterialsForDrueckerZier();
        //SetMaterialsForBand1();
        //SetMaterialsForBand2();
        //SetMaterialsForBandaufnahme1();
        //SetMaterialsForBandaufnahme2();
        //SetMaterialsForSchliessblech();
        //SetMaterialsForSchlosskasten();
        //SetMaterialsForSchwelle();
    }

    private void SetLocalMaterial(string obj)
    {
        Renderer renderer;
        Material[] materialArray;
        renderer = GameObject.Find(obj).GetComponent<Renderer>();
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        Debug.Log("Settings standard mat for "+ obj);
        var mat1 = Resources.Load("Material/RAL/RAL9001_Cremeweis", typeof(Material)) as Material;
        Debug.Log(mat1);

        AssetManager manager = new AssetManager();


        //Debug.Log("alle nicht null");
        materialArray = new Material[3];
        materialArray[0] = mat1;
        materialArray[1] = mat1;
        materialArray[2] = mat1;


        renderer.materials = materialArray;
    }

    private void SetMaterialsForZarge()
    {
        Renderer renderer;
        Material[] materialArray;
        renderer = GameObject.Find("3D_Zarge").GetComponent<Renderer>();
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        Debug.Log("SetMaterialsForZarge");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Zarge.MaterialKombination));
        var mat1 = innentuer.Zarge.MaterialKombination.Material1;
        var mat2 = innentuer.Zarge.MaterialKombination.Material2;
        var mat3 = innentuer.Zarge.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();

        if (mat2 == null)
        {

            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1); 
        }
        else if (mat3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }
        renderer.materials = materialArray;
    }

    private void SetMaterialsForTuerblatt()
    {
        Renderer renderer;
        Material[] materialArray;
        // Tuerblatt ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_Tuerblatt").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForTuerblatt");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Tuerblatt.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.Tuerblatt.MaterialKombination.Material1;
        var mat2 = innentuer.Tuerblatt.MaterialKombination.Material2;
        var mat3 = innentuer.Tuerblatt.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.Tuerblatt.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }

        else if (innentuer.Tuerblatt.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }

        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForDrueckerFalz()
    {
        Renderer renderer;
        Material[] materialArray;
        // DrueckerFalz ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_DrueckerFalz").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForDrueckerFalz");
        Debug.Log(JsonConvert.SerializeObject(innentuer.DrueckerFalz.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.DrueckerFalz.MaterialKombination.Material1;
        var mat2 = innentuer.DrueckerFalz.MaterialKombination.Material2;
        var mat3 = innentuer.DrueckerFalz.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.DrueckerFalz.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.DrueckerFalz.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForDrueckerZier()
    {
        Renderer renderer;
        Material[] materialArray;
        // DrueckerZier ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_DrueckerZier").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForDrueckerZier");
        Debug.Log(JsonConvert.SerializeObject(innentuer.DrueckerZier.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.DrueckerZier.MaterialKombination.Material1;
        var mat2 = innentuer.DrueckerZier.MaterialKombination.Material2;
        var mat3 = innentuer.DrueckerZier.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.DrueckerZier.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.DrueckerZier.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForBand1()
    {
        Renderer renderer;
        Material[] materialArray;
        // Band1 ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_Band1").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForBand1");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Band1.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.Band1.MaterialKombination.Material1;
        var mat2 = innentuer.Band1.MaterialKombination.Material2;
        var mat3 = innentuer.Band1.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.Band1.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.Band1.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForSchwelle()
    {
        Renderer renderer;
        Material[] materialArray;
        // Schwelle ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_Schwelle").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForSchwelle");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Schwelle.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.Schwelle.MaterialKombination.Material1;
        var mat2 = innentuer.Schwelle.MaterialKombination.Material2;
        var mat3 = innentuer.Schwelle.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.Schwelle.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.Schwelle.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForSchlosskasten()
    {
        Renderer renderer;
        Material[] materialArray;
        // Schlosskasten ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_Schlosskasten").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForSchlosskasten");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Schlosskasten.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.Schlosskasten.MaterialKombination.Material1;
        var mat2 = innentuer.Schlosskasten.MaterialKombination.Material2;
        var mat3 = innentuer.Schlosskasten.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.Schlosskasten.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.Schlosskasten.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForSchliessblech()
    {
        Renderer renderer;
        Material[] materialArray;
        // Schliessblech ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_Schliessblech").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForSchliessblech");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Schliessblech.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.Schliessblech.MaterialKombination.Material1;
        var mat2 = innentuer.Schliessblech.MaterialKombination.Material2;
        var mat3 = innentuer.Schliessblech.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.Schliessblech.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.Schliessblech.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForBandaufnahme2()
    {
        Renderer renderer;
        Material[] materialArray;
        // Bandaufnahme2 ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_Bandaufnahme2").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForBandaufnahme2");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Bandaufnahme2.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.Bandaufnahme2.MaterialKombination.Material1;
        var mat2 = innentuer.Bandaufnahme2.MaterialKombination.Material2;
        var mat3 = innentuer.Bandaufnahme2.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.Bandaufnahme2.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.Bandaufnahme2.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForBandaufnahme1()
    {
        Renderer renderer;
        Material[] materialArray;
        // Bandaufnahme1 ------------------------------------------------------------------------------

        renderer = GameObject.Find("3D_Bandaufnahme1").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForBandaufnahme1");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Bandaufnahme1.MaterialKombination));
        // setze Size von Array genau auf die Anzahl der Materialien in Json
        var mat1 = innentuer.Bandaufnahme1.MaterialKombination.Material1;
        var mat2 = innentuer.Bandaufnahme1.MaterialKombination.Material2;
        var mat3 = innentuer.Bandaufnahme1.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.Bandaufnahme1.MaterialKombination.Material2 == null)
        {
            //Debug.Log("Material2 = null");
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.Bandaufnahme1.MaterialKombination.Material3 == null)
        {
            //Debug.Log("Material3 = null");
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            //Debug.Log("alle nicht null");
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        // überint Material-Array an Renderer (damit wird auch der Wert Size angepasst)
        renderer.materials = materialArray;
    }

    private void SetMaterialsForBand2()
    {
        Material[] materialArray;
        Renderer renderer;
        renderer = GameObject.Find("3D_Band2").GetComponent<Renderer>();
        Debug.Log("SetMaterialsForBand2");
        Debug.Log(JsonConvert.SerializeObject(innentuer.Band2.MaterialKombination));
        var mat1 = innentuer.Band2.MaterialKombination.Material1;
        var mat2 = innentuer.Band2.MaterialKombination.Material2;
        var mat3 = innentuer.Band2.MaterialKombination.Material3;
        AssetManager manager = new AssetManager();
        if (innentuer.Band2.MaterialKombination.Material2 == null)
        {
            materialArray = new Material[1];
            materialArray[0] = manager.DownloadMaterial(mat1);
        }
        else if (innentuer.Band2.MaterialKombination.Material3 == null)
        {
            materialArray = new Material[2];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
        }
        else
        {
            materialArray = new Material[3];
            materialArray[0] = manager.DownloadMaterial(mat1);
            materialArray[1] = manager.DownloadMaterial(mat2);
            materialArray[2] = manager.DownloadMaterial(mat3);
        }

        renderer.materials = materialArray;
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
