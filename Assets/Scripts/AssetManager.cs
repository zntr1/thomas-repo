using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class AssetManager
{
    private string baseUrl = "http://62.171.164.125:3000/api/v1/";
    private static List<GameObject> DownloadedAssets = new List<GameObject>();
    private static List<Material> DownloadedMaterials= new List<Material>();

    public void Start()
    {
    }

    public void Update()
    {

    }


    public GameObject DownloadAsset(string assetId)
    {
        Debug.Log("Versuche Asset zu downloaden!!");
        string assetsDownloadUrl = baseUrl + "assets/download/" + assetId;
        Debug.Log(assetsDownloadUrl);
        var firstOrDefault = DownloadedAssets.FirstOrDefault((asset =>  asset.name == assetId));
        if (firstOrDefault != null)
        {
            Debug.Log("Asset bereits in Liste geladen!");
            return firstOrDefault;
        }
        else
        {
            Debug.Log("Asset noch nicht im Memory!");
        }

        using (WWW web = new WWW(assetsDownloadUrl))
        {
            //yield return web;
            while (!web.isDone) ;
            AssetBundle remoteAssetBundle = web.assetBundle;
            if (remoteAssetBundle == null)
            {
                Debug.LogError($"Failed to download AssetBundle {assetId}"); return null;
            }
            Debug.Log("Bundle scheint gedownloaded zu sein!");

            var obj = remoteAssetBundle.LoadAsset(assetId) as GameObject;

            Debug.Log("Folgende Assets sind im Bundle enthalten:");

            foreach (var VARIABLE in remoteAssetBundle.GetAllAssetNames())
            {
                Debug.Log(VARIABLE);
            }
            DownloadedAssets.Add(obj);
            //Instantiate(remoteAssetBundle.LoadAsset(assetName));
            //remoteAssetBundle.Unload(false);
            return obj;

        }


        //UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(assetsDownloadUrl);

        //request.SendWebRequest();
        //AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);
        //if (myLoadedAssetBundle == null)
        //{
        //    Debug.Log("Failed to load AssetBundle!");
        //    return null;
        //}
        //Debug.Log("fertig!");
        //Debug.Log(myLoadedAssetBundle.GetAllAssetNames()[0]);

        //GameObject asset = myLoadedAssetBundle.LoadAsset<GameObject>(myLoadedAssetBundle.GetAllAssetNames()[0]);


        ////konfigurator.UpdateTuerblatt("ASSET", asset);
        //return asset;


    }


    public Material DownloadMaterial(string matId)
    {

        matId = matId.ToLower();
        Debug.Log("Versuche material zu downloaden!!");
        string assetsDownloadUrl = baseUrl + "materials/download/" + matId;
        Debug.Log(assetsDownloadUrl);
        var firstOrDefault = DownloadedMaterials.FirstOrDefault((asset => asset.name == matId));
        if (firstOrDefault != null)
        {
            Debug.Log("material bereits in Liste geladen!");
            return firstOrDefault;
        }
        else
        {
            Debug.Log("material noch nicht im Memory!");
        }

        using (WWW web = new WWW(assetsDownloadUrl))
        {
            //yield return web;
            while (!web.isDone) ;
            AssetBundle remoteAssetBundle = web.assetBundle;
            if (remoteAssetBundle == null)
            {
                Debug.LogError($"Failed to download materialBundle {matId}"); return null;
            }
            Debug.Log("Bundle scheint gedownloaded zu sein!");

            var obj = remoteAssetBundle.LoadAsset(matId) as Material;

            Debug.Log("Folgende Assets sind im Bundle enthalten:");

            foreach (var VARIABLE in remoteAssetBundle.GetAllAssetNames())
            {
                Debug.Log(VARIABLE);
            }
            DownloadedMaterials.Add(obj);
            //Instantiate(remoteAssetBundle.LoadAsset(assetName));
            //remoteAssetBundle.Unload(false);
            if (obj == null)
            {
                Debug.LogError($"Es wurde null vom Server geladen '{matId}'");
            }
            return obj;
        }


        //UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(assetsDownloadUrl);

        //request.SendWebRequest();
        //AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);
        //if (myLoadedAssetBundle == null)
        //{
        //    Debug.Log("Failed to load AssetBundle!");
        //    return null;
        //}
        //Debug.Log("fertig!");
        //Debug.Log(myLoadedAssetBundle.GetAllAssetNames()[0]);

        //GameObject asset = myLoadedAssetBundle.LoadAsset<GameObject>(myLoadedAssetBundle.GetAllAssetNames()[0]);


        ////konfigurator.UpdateTuerblatt("ASSET", asset);
        //return asset;


    }










    //public void SetMaterialFromServer(string materialId)
    //{
    //    Debug.Log("Started Coroutine to download Material!");
    //    StartCoroutine(DownloadAndSetMaterialFromApi(materialId));
    //    Debug.Log("Finished GetMaterialFromServer!");

    //}


    //private IEnumerator DownloadAndSetMaterialFromApi(string materialId)
    //{
    //    Debug.Log("ImportFromApi 1 vom Download. Init!");
    //    string url = baseUrl + "materials/download/" + materialId;

    //    Debug.Log("Not downloaded yet, so continuing!");

    //    UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
    //    Debug.Log("ImportFromApi 2 vom Download. Sende Request!");
    //    yield return request.SendWebRequest();

    //    Debug.Log("ImportFromApi 3 vom Download. Get Content!");

    //    AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);

    //    if (myLoadedAssetBundle == null)
    //    {
    //        Debug.Log("Failed to load AssetBundle!");
    //        yield return null;
    //    }

    //    Debug.Log("ImportFromApi 4 vom Download. Finalising!");
    //    Debug.Log("Folgende Assets im Bundle gefunden:");

    //    foreach (var name in myLoadedAssetBundle.GetAllAssetNames())
    //    {
    //        Debug.Log(name);
    //    }

    //    var material = myLoadedAssetBundle.LoadAsset<Material>(myLoadedAssetBundle.GetAllAssetNames()[0]);
    //    Debug.Log("ImportFromApi 5: Loaded following Asset:");
    //    Debug.Log(material);


    //    Debug.Log("Trying to set new Material now!");

    //    TuerGenerator tuerGenerator = GameObject.Find("TuerManager").GetComponent<TuerGenerator>();
    //    //tuerGenerator.toggleMaterial(material);
    //    Debug.Log("was it succesful?");
    //    myLoadedAssetBundle.Unload(false);

    //    Debug.Log("Finished Coroutine to download Material!");

    //    //toggleMaterial(material);
    //    //return material;
    //}

    //private void ImportFromLocal(string assetBundleName, string assetName)
    //{
    //    Debug.Log("Trying to load Material from  local Asset-Bundle..");

    //    var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine("D:\\temp\\unityassets", assetBundleName));
    //    if (myLoadedAssetBundle == null)
    //    {
    //        Debug.Log("Failed to load AssetBundle!");
    //        return;
    //    }

    //    var material = myLoadedAssetBundle.LoadAsset<Material>(assetName);
    //    Debug.Log("Loaded following Asset:");
    //    Debug.Log(material);
    //}

    //public void StartDownload(string auswahlId, string executionType)
    //{
    //    switch (executionType)
    //    {
    //        case "UPDATE_ZARGE_MATERIAL":
    //            Debug.Log("Started Coroutine: UPDATE_ZARGE_MATERIAL!");
    //            StartCoroutine(DownloadAndSetZargenMaterial(auswahlId));
    //            Debug.Log("Finished Coroutine: UPDATE_ZARGE_MATERIAL!");
    //            break;
    //        case "UPDATE_ZARGE_ASSET":
    //            Debug.Log("Started Coroutine: UPDATE_ZARGE_ASSET!");
    //            StartCoroutine(DownloadAndSetZargenAsset(auswahlId));
    //            Debug.Log("Finished Coroutine: UPDATE_ZARGE_ASSET!");
    //            break;
    //        case "UPDATE_TUERBLATT_MATERIAL":
    //            Debug.Log("Started Coroutine: UPDATE_TUERBLATT_MATERIAL!");
    //            StartCoroutine(DownloadAndSetTuerblattMaterial(auswahlId));
    //            Debug.Log("Finished Coroutine: UPDATE_TUERBLATT_MATERIAL!");
    //            break;
    //        case "UPDATE_TUERBLATT_ASSET":
    //            Debug.Log("Started Coroutine: UPDATE_TUERBLATT_ASSET!");
    //            StartCoroutine(DownloadAndSetTuerblattAsset(auswahlId));
    //            Debug.Log("Finished Coroutine: UPDATE_TUERBLATT_ASSET!");
    //            break;
    //        default:
    //            throw new NotImplementedException();
    //            break;
    //    }


    //}

    //private IEnumerator DownloadAndSetZargenAsset(string assetId)
    //{
    //    Debug.Log("DownloadAndSetZargenAsset 1 vom Download. Init!");
    //    string url = baseUrl + "assets/download/" + assetId;

    //    Debug.Log("Not downloaded yet, so continuing!");

    //    UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
    //    Debug.Log("DownloadAndSetZargenAsset 2 vom Download. Sende Request!");
    //    yield return request.SendWebRequest();

    //    Debug.Log("DownloadAndSetZargenAsset 3 vom Download. Get Content!");

    //    AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);

    //    if (myLoadedAssetBundle == null)
    //    {
    //        Debug.Log("Failed to load AssetBundle!");
    //        yield return null;
    //    }

    //    Debug.Log("DownloadAndSetZargenAsset 4 vom Download. Finalising!");
    //    Debug.Log("Folgende Assets im Bundle gefunden:");

    //    foreach (var name in myLoadedAssetBundle.GetAllAssetNames())
    //    {
    //        Debug.Log(name);
    //    }

    //    var asset = myLoadedAssetBundle.LoadAsset<GameObject>(myLoadedAssetBundle.GetAllAssetNames()[0]);
    //    Debug.Log("DownloadAndSetZargenAsset 5: Loaded following Asset:");
    //    Debug.Log(asset);


    //    Debug.Log("Trying to set new Material now!");

    //    konfigurator.UpdateZarge("ASSET", asset);

    //    //TuerGenerator tuerGenerator = GameObject.Find("TuerManager").GetComponent<TuerGenerator>();
    //    //tuerGenerator.toggleMaterial(material);

    //    Debug.Log("was it succesful?");
    //    myLoadedAssetBundle.Unload(false);

    //    Debug.Log("Finished Coroutine to download Asset!");

    //    //toggleMaterial(material);    }
    //}


    //public IEnumerator GetLocalAndSetTuerblatt(string assetId)
    //{
    //    Debug.Log("STARTTTTTTTT");

    //    string url = "file://D:\\temp\\unityassets\\";
    //    string urlM = "D:\\temp\\unityassets\\tuerblatt_001";


    //    //AssetBundle.GetAllLoadedAssetBundles();

    //    //UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
    //    //yield return request.SendWebRequest();

    //    //AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);

    //    //AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
    //    //GameObject innentuer = bundle.LoadAsset<GameObject>("Innentuer");

    //    //AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath + "//StreamingAssets"));
    //    AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile("C:\\Users\\secto\\Downloads\\New Unity Project\\Doar3DProject\\Assets\\StreamingAssets\\StreamingAssets");


    //    AssetBundleManifest manifest = myLoadedAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

    //    //Debug.Log("manifest");
    //    //foreach (var allAssetBundle in manifest.GetAllAssetBundles())
    //    //{
    //    //    Debug.Log(allAssetBundle);
    //    //}



    //    Debug.Log("end manifest");

    //    var hash = manifest.GetAssetBundleHash("m_1k_3dte_wood_020");
    //    Debug.Log("hash:");
    //    Debug.Log(hash.ToString());

    //    Hash128 myHashCode = manifest.GetAssetBundleHash("quadbundle");
    //    Debug.Log("HashCode");
    //    Debug.Log(myHashCode);

    //    if (myLoadedAssetBundle == null)
    //    {
    //        Debug.Log("GetLocalAndSetTuerblatt Failed to load AssetBundle!");
    //        yield return null;
    //    }


    //    //AssetBundleManifest manifest = myLoadedAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

    //    //Debug.Log(manifest);
    //    Debug.Log("GetLocalAndSetTuerblatt vom Download. Finalising!");
    //    Debug.Log("GetLocalAndSetTuerblatt Folgende Assets im Bundle gefunden:");

    //    foreach (var name in myLoadedAssetBundle.GetAllAssetNames())
    //    {
    //        Debug.Log(name);
    //    }

    //    var asset = myLoadedAssetBundle.LoadAsset<GameObject>(myLoadedAssetBundle.GetAllAssetNames()[0]);
    //    Debug.Log("GetLocalAndSetTuerblatt 5: Loaded following Asset:");
    //    Debug.Log(asset);


    //    Debug.Log("Trying to set new Material now!");

    //    konfigurator.UpdateTuerblatt("ASSET", asset);

    //    //TuerGenerator tuerGenerator = GameObject.Find("TuerManager").GetComponent<TuerGenerator>();
    //    //tuerGenerator.toggleMaterial(material);

    //    Debug.Log("was it succesful?");
    //    //myLoadedAssetBundle.Unload(false);

    //    Debug.Log("GetLocalAndSetTuerblatt ended");

    //    //toggleMaterial(material);    }
    //}

    //private IEnumerator DownloadAndSetTuerblattAsset(string assetId)
    //{
    //    Debug.Log("DownloadAndSetTuerblattAsset 1 vom Download. Init!");
    //    //string urlManifest = baseUrl + "assets/downloadManifest/" + assetId;

    //    //UnityWebRequest manifestRequest = UnityWebRequestAssetBundle.GetAssetBundle(urlManifest, 0);
    //    //yield return manifestRequest.SendWebRequest();
    //    //AssetBundle manifestBundle = DownloadHandlerAssetBundle.GetContent(manifestRequest);
    //    //AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    //    //Debug.Log(manifest);

    //    //if (manifest == null)
    //    //{
    //    //    Debug.Log("Failed to load AssetManifest!");
    //    //    yield return null;
    //    //}

    //    ////////////////////////////////////////////////////


    //    Debug.Log("DownloadAndSetTuerblattAsset 1 vom Download. Init!");
    //    string url = baseUrl + "assets/download/" + assetId;

    //    Debug.Log("Not downloaded yet, so continuing!");

    //    UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
    //    Debug.Log("DownloadAndSetTuerblattAsset 2 vom Download. Sende Request!");
    //    yield return request.SendWebRequest();

    //    Debug.Log("DownloadAndSetTuerblattAsset 3 vom Download. Get Content!");

    //    AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);



    //    if (myLoadedAssetBundle == null)
    //    {
    //        Debug.Log("Failed to load AssetBundle!");
    //        yield return null;
    //    }


    //    //AssetBundleManifest manifest = myLoadedAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

    //    //Debug.Log(manifest);
    //    Debug.Log("DownloadAndSetTuerblattAsset 4 vom Download. Finalising!");
    //    Debug.Log("Folgende Assets im Bundle gefunden:");

    //    foreach (var name in myLoadedAssetBundle.GetAllAssetNames())
    //    {
    //        Debug.Log(name);
    //    }

    //    var asset = myLoadedAssetBundle.LoadAsset<GameObject>(myLoadedAssetBundle.GetAllAssetNames()[0]);
    //    Debug.Log("DownloadAndSetTuerblattAsset 5: Loaded following Asset:");
    //    Debug.Log(asset);


    //    Debug.Log("Trying to set new Material now!");

    //    konfigurator.UpdateTuerblatt("ASSET", asset);

    //    //TuerGenerator tuerGenerator = GameObject.Find("TuerManager").GetComponent<TuerGenerator>();
    //    //tuerGenerator.toggleMaterial(material);

    //    Debug.Log("was it succesful?");
    //    //myLoadedAssetBundle.Unload(false);

    //    Debug.Log("Finished Coroutine to download Asset!");

    //    //toggleMaterial(material);    }
    //}


    //private IEnumerator DownloadAndSetZargenMaterial(string materialId)
    //{
    //    Debug.Log("ImportFromApi 1 vom Download. Init!");
    //    string url = baseUrl + "materials/download/" + materialId;

    //    Debug.Log("Not downloaded yet, so continuing!");

    //    UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
    //    Debug.Log("ImportFromApi 2 vom Download. Sende Request!");
    //    yield return request.SendWebRequest();

    //    Debug.Log("ImportFromApi 3 vom Download. Get Content!");

    //    AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);

    //    if (myLoadedAssetBundle == null)
    //    {
    //        Debug.Log("Failed to load AssetBundle!");
    //        yield return null;
    //    }

    //    Debug.Log("ImportFromApi 4 vom Download. Finalising!");
    //    Debug.Log("Folgende Assets im Bundle gefunden:");

    //    foreach (var name in myLoadedAssetBundle.GetAllAssetNames())
    //    {
    //        Debug.Log(name);
    //    }

    //    var material = myLoadedAssetBundle.LoadAsset<Material>(myLoadedAssetBundle.GetAllAssetNames()[0]);
    //    Debug.Log("ImportFromApi 5: Loaded following Asset:");
    //    Debug.Log(material);


    //    Debug.Log("Trying to set new Material now!");

    //    konfigurator.UpdateZarge("MATERIAL", material);

    //    //TuerGenerator tuerGenerator = GameObject.Find("TuerManager").GetComponent<TuerGenerator>();
    //    //tuerGenerator.toggleMaterial(material);

    //    Debug.Log("was it succesful?");
    //    myLoadedAssetBundle.Unload(false);

    //    Debug.Log("Finished Coroutine to download Material!");

    //    //toggleMaterial(material);    }
    //}

    //private IEnumerator DownloadAndSetTuerblattMaterial(string materialId)
    //{
    //    Debug.Log("ImportFromApi 1 vom Download. Init!");
    //    string url = baseUrl + "materials/download/" + materialId;

    //    Debug.Log("Not downloaded yet, so continuing!");

    //    UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
    //    Debug.Log("ImportFromApi 2 vom Download. Sende Request!");
    //    yield return request.SendWebRequest();

    //    Debug.Log("ImportFromApi 3 vom Download. Get Content!");

    //    AssetBundle myLoadedAssetBundle = DownloadHandlerAssetBundle.GetContent(request);

    //    if (myLoadedAssetBundle == null)
    //    {
    //        Debug.Log("Failed to load AssetBundle!");
    //        yield return null;
    //    }

    //    Debug.Log("ImportFromApi 4 vom Download. Finalising!");
    //    Debug.Log("Folgende Assets im Bundle gefunden:");

    //    foreach (var name in myLoadedAssetBundle.GetAllAssetNames())
    //    {
    //        Debug.Log(name);
    //    }

    //    var material = myLoadedAssetBundle.LoadAsset<Material>(myLoadedAssetBundle.GetAllAssetNames()[0]);
    //    Debug.Log("ImportFromApi 5: Loaded following Asset:");
    //    Debug.Log(material);


    //    Debug.Log("Trying to set new Material now!");

    //    konfigurator.UpdateTuerblatt("MATERIAL", material);

    //    //TuerGenerator tuerGenerator = GameObject.Find("TuerManager").GetComponent<TuerGenerator>();
    //    //tuerGenerator.toggleMaterial(material);

    //    Debug.Log("was it succesful?");
    //    myLoadedAssetBundle.Unload(false);

    //    Debug.Log("Finished Coroutine to download Material!");

    //    //toggleMaterial(material);    }
    //}
}
