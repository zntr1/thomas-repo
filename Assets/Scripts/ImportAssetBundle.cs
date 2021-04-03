using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportAssetBundle : MonoBehaviour
{
    AssetBundle assetBundleToImport;
    public string pfadAssetBundle;
    public string nameAssetBundle;
    public string [] OBTid;
    public int indexOBTid = 0;
    List<string[]> oMatrix = new List<string[]>();
    List<string[]> pfadMatrix = new List<string[]>();


    void Start()
    {
        generiereOidArray();
        generierePfadArray();
        LoadOBTidsZuOid(indexOBTid);
        
        //LoadAssetBundle(pfadAssetBundle);
        //InstantiateObjectFromAssetBundle(pfadAssetBundle);
        //InstantiateObjectFromAssetBundle(nameAssetBundle);
    }

    void generiereOidArray()
    {
        oMatrix.Add(new string[] { "Innnentuer_0001_Var01", "Zargenschnitt_001_1875x625_WS115_BB50", "Tuerblatt_1875x625", "Band_001", "Band_001", "Bandaufnahme_001", "Bandaufnahme_001", "Haefele_900_81_004_1", "Haefele_900_81_004_1", "Schlosskasten_001", "Schliessblech_001", "" });
        oMatrix.Add(new string[] { "Innnentuer_0001_Var02", "Zargenschnitt_001_1875x750_WS115_BB50", "Tuerblatt_1875x750", "Band_001", "Band_001", "Bandaufnahme_001", "Bandaufnahme_001", "Haefele_900_81_004_1", "Haefele_900_81_004_1", "Schlosskasten_001", "Schliessblech_001", "" });
    }

    void generierePfadArray()
    {
        pfadMatrix.Add(new string[] { "Zargenschnitt_001_1875x625_WS115_BB50", @"F:\AssetBundles\Zarge\Zargenschnitt_001_1875x625_WS115_BB50" });
        pfadMatrix.Add(new string[] { "Zargenschnitt_001_1875x750_WS115_BB50", @"F:\AssetBundles\Zarge\Zargenschnitt_001_1875x750_WS115_BB50" });
        pfadMatrix.Add(new string[] { "Tuerblatt_1875x625", @"F:\AssetBundles\Tuerblatt\Tuerblatt_1875x625" });
        pfadMatrix.Add(new string[] { "Tuerblatt_1875x750", @"F:\AssetBundles\Tuerblatt\Tuerblatt_1875x750" });
        pfadMatrix.Add(new string[] { "Band_001", @"F:\AssetBundles\Band\Band_001" });
        pfadMatrix.Add(new string[] { "Bandaufnahme_001", @"F:\AssetBundles\Bandaufnahme\Bandaufnahme_001" });
        pfadMatrix.Add(new string[] { "Haefele_900_81_004_1", @"F:\AssetBundles\Druecker\Haefele_900_81_004_1" });
        pfadMatrix.Add(new string[] { "Schlosskasten_001", @"F:\AssetBundles\Schlosskasten\Schlosskasten_001" });
        pfadMatrix.Add(new string[] { "Schliessblech_001", @"F:\AssetBundles\Schliessblech\Schliessblech_001" });
    }

    void LoadOBTidsZuOid(int indexOBTid)
    {
        for (int i = 1; i < 12; i++)
        {
            //Debug.Log(oMatrix[indexOBTid][i]);
            for (int j = 0; j < pfadMatrix.Count; j++)
            {
                if (oMatrix[indexOBTid][i] == pfadMatrix[j][0])
                {
                    Debug.Log(oMatrix[indexOBTid][i] + " - " + pfadMatrix[j][1]);
                    LoadAssetBundle(pfadMatrix[j][1] + "\\" + oMatrix[indexOBTid][i], oMatrix[indexOBTid][i]);
                }
            }

        }




        }

    void LoadAssetBundle(string pfadAssetBundle, string nameAssetBundle)
    {
        assetBundleToImport = AssetBundle.LoadFromFile(pfadAssetBundle);
        Debug.Log(assetBundleToImport == null ? "Import fehlgeschlagen" : "Import erfolgreich");
        InstantiateObjectFromAssetBundle(nameAssetBundle);
        assetBundleToImport.Unload(false);
    }

    void InstantiateObjectFromAssetBundle(string nameAssetBundle)
    {
        var prefab = assetBundleToImport.LoadAsset(nameAssetBundle);
        Instantiate(prefab);

    }
}
