using UnityEngine;
using UnityEditor;

public class AssetBundles_01_GiveName : MonoBehaviour
{

    // MenuItem Constructor
    [MenuItem("Assets/ZTools/Asset Bundles/01 Give Name", false, 10001)]

    static void GiveName()
    {
        Object[] go;

        go = Resources.LoadAll("Prefabs", typeof(GameObject));

        foreach (var g in go)
        {
            string assetPath = AssetDatabase.GetAssetPath(g);
            Debug.Log(assetPath + " : " + g.name);
            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(g.name, "");
        }

        go = Resources.LoadAll("Material", typeof(Material));

        foreach (var g in go)
        {
            string assetPath = AssetDatabase.GetAssetPath(g);
            Debug.Log(assetPath + " : " + g.name);
            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(g.name, "");
        }
    }

}
