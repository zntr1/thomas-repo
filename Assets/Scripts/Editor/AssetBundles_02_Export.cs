using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundles_02_Export : MonoBehaviour
{

    // MenuItem Constructor
    [MenuItem("Assets/ZTools/Asset Bundles/02 Export", false, 10002)]

    static void BuildAllAssetBundles()
    {


        var assetNamen = new List<string>();
        var assetPfade = new List<string>();


        // für Prefabs (3D-Modelle) dieser Block

        string outputPath = @"F:\AssetBundles\";
        //string workingPath = @"Assets\Resources\Prefabs\";

       // var directories = Directory.GetDirectories(workingPath, "*", SearchOption.AllDirectories);

        if (!Directory.Exists(outputPath))
        {
            var folder = Directory.CreateDirectory(outputPath);
        }

        //foreach (var d in directories)
        //{

        //    //Debug.Log("Directory: " + d);
        //    string dReverse = ReverseString(d);
        //    //Debug.Log("Directory reverse: " + dReverse);
        //    int index = dReverse.IndexOf("\\");
        //    //Debug.Log(index.ToString());
        //    dReverse = dReverse.Substring(0, index);
        //    string subfolder = ReverseString(dReverse);
        //    Debug.Log(subfolder);

        //    go = Resources.LoadAll("Prefabs\\" + subfolder, typeof(GameObject));

        //    foreach (var g in go)
        //    {
        //        Debug.Log(g.name);
        //        string zielPfad = (outputPath + subfolder + "\\" + g.name);
        //        Debug.Log(zielPfad);
        //        assetNamen.Add(g.name);
        //        assetPfade.Add(zielPfad);

        //        if (!Directory.Exists(zielPfad))
        //        {
        //            var folder = Directory.CreateDirectory(zielPfad);
        //        }
        //    }
        //}

        //// für Materialien dieser Block
        //workingPath = @"Assets\Resources\Material\";

        //if (!Directory.Exists(outputPath + "Material\\"))
        //{
        //    var folder = Directory.CreateDirectory(outputPath + "Material\\");
        //}

        //directories = Directory.GetDirectories(workingPath, "*", SearchOption.AllDirectories);

        //foreach (var d in directories)
        //{

        //    //Debug.Log("Directory: " + d);
        //    string dReverse = ReverseString(d);
        //    //Debug.Log("Directory reverse: " + dReverse);
        //    int index = dReverse.IndexOf("\\");
        //    //Debug.Log(index.ToString());
        //    dReverse = dReverse.Substring(0, index);
        //    string subfolder = ReverseString(dReverse);
        //    Debug.Log(subfolder);

        //    go = Resources.LoadAll("Material\\", typeof(Material));

        //    foreach (var g in go)
        //    {
        //        Debug.Log(g.name);
        //        string zielPfad = (outputPath + "Material\\" + g.name);
        //        Debug.Log(zielPfad);
        //        assetNamen.Add(g.name);
        //        assetPfade.Add(zielPfad);

        //        if (!Directory.Exists(zielPfad))
        //        {
        //            var folder = Directory.CreateDirectory(zielPfad);
        //        }

        //    }
        //}

        // für Prefabs und Materialien 

        BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        //for (var index = 0; index < assetNamen.Count; index++)
        //{
        //    Debug.Log("Move: " + outputPath + assetNamen[index] + ", " + assetPfade[index] + "\\" + assetNamen[index]);
        //    File.Move(outputPath + assetNamen[index], assetPfade[index] + "\\" + assetNamen[index]);
        //    File.Move(outputPath + assetNamen[index] + ".manifest", assetPfade[index] + "\\" + assetNamen[index] + ".manifest");
        //}
    }


    public static string ReverseString(string s)
    {
        char[] arr = s.ToCharArray();
        System.Array.Reverse(arr);
        return new string(arr);
    }
}
