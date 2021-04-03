using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Material_Generator : MonoBehaviour
{
    [Tooltip("Das genutzte Logging-Script muss in dem nutzenden Script instanziert werden.")]
    public Logging log; // immer erst initialisieren via log.Initialisiere();
    
    [Tooltip("true = Erhoehung der Loggingtiefe")]
    public bool debug = false;
    
    [Tooltip("true = Bereits vorhandene Materialien werden nochmals generiert und ueberschrieben.")]
    public bool vorhandeneUeberschreiben = false;

    //------------------------------------------------------------------    
    //                                                          URP
    //------------------------------------------------------------------
    private Texture2D texture_ALB;   // ALB  RGB  = COL = DIF     x  
    private Texture2D texture_COL;   // COL  RGB  = ALB = DIF       
    private Texture2D texture_DIF;   // DIF  RGB  = ALB = COL       
    //------------------------------------------------------------------
    private Texture2D texture_MET;   // MET  GS                  (x)
    //------------------------------------------------------------------
    private Texture2D texture_SPE;   // SPE  RGB  = REF          (x)
    private Texture2D texture_REF;   // REF  RGB  = SPE          
    //------------------------------------------------------------------
    private Texture2D texture_ROU;   // ROU  GS   = invert GLO    -
    private Texture2D texture_GLO;   // GLO  GS   = invert ROU    -
    //------------------------------------------------------------------
    private Texture2D texture_NRM;   // NRM  RGB  --> NRM         x  
    //------------------------------------------------------------------    
    private Texture2D texture_OCC;   // OCC  RGB                  x
    //------------------------------------------------------------------
    private Texture2D texture_HEI;   // HEI  GS   = DIS           -
    private Texture2D texture_DIS;   // DIS  GS   = HEI           -
    //------------------------------------------------------------------
    private Texture2D texture_EMI;   // EMI                       -   
    //------------------------------------------------------------------

    private Object[] textures;
    private string pathInFolderResourcesToMaterial;
    private bool colTextureInFolder;

    // Start is called before the first frame update
    void Start()
    {
        log.Initialisiere();
        getAllMaterialSubdirectories();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getAllMaterialSubdirectories()
    {
        string aktuellerPfad;
        string lastFolderName;
        string aktuellerPfadOhnePraefix;
        string aktuellerPfadOhnePraefix2;
        string aktuellerPfadOhnePraefixUndOhneLastFolderName;

        var directories = Directory.GetDirectories("Assets\\Resources\\MaterialTextures","*",SearchOption.AllDirectories);

        foreach (var dir in directories)
        {
            log.wl("\nAlle Unterordner in:  'Assets\\Resources\\MaterialTextures'");
            log.wl(dir.ToString());
            
            lastFolderName = "";
            aktuellerPfadOhnePraefix = "";
            aktuellerPfadOhnePraefixUndOhneLastFolderName = "";
            aktuellerPfad = dir.ToString();
            lastFolderName = getLastFolderName(aktuellerPfad);
                       

            resetAllTextures();
            if (lastFolderName.Substring(0, 2) == "M_")
            {

                // der aktueller Pfad wird um 17 Stellen "Assets/Resources\" gekürzt
                aktuellerPfadOhnePraefix = aktuellerPfad.Substring(17);

                // der aktueller Pfad wird um 34 Stellen "Assets/Resources\MaterialTextures\" gekürzt
                aktuellerPfadOhnePraefix2 = aktuellerPfad.Substring(34);

                aktuellerPfadOhnePraefixUndOhneLastFolderName = aktuellerPfadOhnePraefix2.Substring(0, aktuellerPfadOhnePraefix2.Length - lastFolderName.Length - 1);

                if (debug)
                {
                    // Beispiel:    Assets\Resources\MaterialTextures\01_Architektur\01-1_Beton\M_2K_CC0T_Concrete026_2K-JPG 
                    log.wl("aktuellerPfad :                                  " + aktuellerPfad);
                    // Beispiel:    M_2K_CC0T_Concrete026_2K-JPG
                    log.wl("lastFolderName :                                 " + lastFolderName);
                    // Beispiel:    28
                    log.wl("lastFolderName.Length :                          " + lastFolderName.Length);
                    // Beispiel:    MaterialTextures\01_Architektur\01-1_Beton\M_2K_CC0T_Concrete026_2K-JPG
                    log.wl("aktuellerPfadOhnePraefix :                       " + aktuellerPfadOhnePraefix);
                    // Beispiel:    71
                    log.wl("aktuellerPfadOhnePraefix.Length :                " + aktuellerPfadOhnePraefix.Length);
                    // Beispiel:    01_Architektur\01-1_Beton\M_2K_CC0T_Concrete026_2K-JPG
                    log.wl("aktuellerPfadOhnePraefix2 :                      " + aktuellerPfadOhnePraefix);
                    // Beispiel:    MaterialTextures\01_Architektur\01-1_Beton
                    log.wl("aktuellerPfadOhnePraefixUndOhneLastFolderName :  " + aktuellerPfadOhnePraefixUndOhneLastFolderName);
                }

                checkTextures(aktuellerPfadOhnePraefix);
                if (texture_ALB || texture_COL || texture_DIF)
                {
                    log.wl("");
                    log.wl("createMaterial('" + aktuellerPfadOhnePraefixUndOhneLastFolderName + "', '" + lastFolderName +"')");
                    createMaterial(aktuellerPfadOhnePraefixUndOhneLastFolderName, lastFolderName);
                }
            }


        }
    }

    string getLastFolderName(string aktuellerPfad)
    {
        int anzahlZeichenAktuellerPfad;
        anzahlZeichenAktuellerPfad = aktuellerPfad.Length;
        while (aktuellerPfad.Substring(anzahlZeichenAktuellerPfad - 1, 1) != "\\")
        {
            anzahlZeichenAktuellerPfad--;
        }
        return aktuellerPfad.Substring(anzahlZeichenAktuellerPfad);
    }

    void resetAllTextures()
    {
        //colTextureInFolder = false;
        texture_ALB = null;
        texture_COL = null;
        texture_DIF = null;
        texture_MET = null;
        texture_SPE = null;
        texture_REF = null;
        texture_ROU = null;
        texture_GLO = null;
        texture_NRM = null;
        texture_OCC = null;
        texture_HEI = null;
        texture_DIS = null;
        texture_EMI = null;
    }

    void checkTextures(string aktuellerPfadOhnePraefix)
    {

        //"MaterialTextures/01_Architektur/01-1_Beton/M_2K_CC0T_Concrete020_2K-JPG";

        log.wl("\naktuellerPfadOhnePraefix :      " + aktuellerPfadOhnePraefix);


        textures = Resources.LoadAll(aktuellerPfadOhnePraefix, typeof(Texture2D));
        
        foreach (Texture2D t in textures)
        {
            log.wl("Texture2D :      " + t.ToString());

            switch (t.name.Substring(0, 3))
            {
                case "ALB":
                    log.wl("ALB: " + t.name);
                    texture_ALB = t;
                    //colTextureInFolder = true;
                    break;
                case "COL":
                    log.wl("COL: " + t.name);
                    texture_COL = t;
                    //colTextureInFolder = true;
                    break;
                case "DIF":
                    log.wl("DIF: " + t.name);
                    texture_DIF = t;
                    //colTextureInFolder = true;
                    break;
                case "MET":
                    log.wl("MET: " + t.name);
                    texture_MET = t;
                    break;
                case "SPE":
                    log.wl("SPE: " + t.name);
                    texture_SPE = t;
                    break;
                case "REF":
                    log.wl("REF: " + t.name);
                    texture_REF = t;
                    break;
                case "ROU":
                    log.wl("ROU: " + t.name);
                    texture_ROU = t;
                    break;
                case "GLO":
                    log.wl("GLO: " + t.name);
                    texture_GLO = t;
                    break;
                case "NRM":
                    log.wl("NRM: " + t.name);
                    texture_NRM = t;
                    break;
                case "OCC":
                    log.wl("OCC: " + t.name);
                    texture_OCC = t;
                    break;
                case "HEI":
                    log.wl("HEI: " + t.name);
                    texture_HEI = t;
                    break;
                case "DIS":
                    log.wl("DIS: " + t.name);
                    texture_DIS = t;
                    break;
                case "EMI":
                    log.wl("EMI: " + t.name);
                    texture_EMI = t;
                    break;
                default:
                    log.wl("--- Was ist den das für ein Material? ---" + t.name);
                    break;
            }
        }
    }

    void createMaterial(string aktuellerPfadOhnePraefixUndOhneLastFolderName, string materialName)
    {

        // Set Shader
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));


        // Set WorkflowMode
        material.EnableKeyword("_WorkflowMode");
        if (texture_MET)
        {
            // WorkflowMode: Metallic
            material.SetFloat("_WorkflowMode", 1);
        }
        else
        {
            // WorkflowMode: Specular
            material.SetFloat("_WorkflowMode", 0);
        }

        //material.EnableKeyword("_MainTex"); // TextureBaseMap ???

        // Set Base Map

        if (texture_ALB)
        {
            material.EnableKeyword("_BaseMap");
            material.SetTexture("_BaseMap", texture_ALB);
        }

        if (texture_COL)
        {
            material.EnableKeyword("_BaseMap");
            material.SetTexture("_BaseMap", texture_COL);
        }

        if (texture_DIF)
        {
            material.EnableKeyword("_BaseMap");
            material.SetTexture("_BaseMap", texture_DIF);
        }

       // Set Metallic Map
        if (texture_MET)
        {
            material.EnableKeyword("_MetallicGlossMap");
            material.SetTexture("_MetallicGlossMap", texture_MET);
        }

        // Set Specular Map
        if (texture_SPE)
        {
            material.EnableKeyword("_SpecGlossMap");
            material.SetTexture("_SpecGlossMap", texture_SPE);
        }

        if (texture_REF)
        {
            material.EnableKeyword("_SPECGLOSSMAP");
            material.SetTexture("_SpecGlossMap", texture_REF);
        }

        // Set Normal Map
        if (texture_NRM)
        {
            material.EnableKeyword("_BumpMap");
            material.SetTexture("_BumpMap", texture_NRM);
        }

        // Set Occlusion Map
        if (texture_OCC)
        {
            material.EnableKeyword("_OcclusionMap");
            material.SetTexture("_OcclusionMap", texture_OCC);
        }

        // Set Emission Map
        if (texture_EMI)
        {
            material.EnableKeyword("_EmissionMap");
            material.SetTexture("_EmissionMap", texture_EMI);
        }

        //// weitere Parameter; wahrscheinlich nicht in URP
        //material.SetTexture("_ParallaxMap", texture_EMI);
        //material.SetTexture("_DetailMask", texture_EMI);
        //material.SetTexture("_DetailAlbedoMap", texture_EMI);
        //material.SetTexture("_DetailNormalMap", texture_EMI);

        //string temp = "Assets/Resources/Material/" + aktuellerPfadOhnePraefixUndOhneLastFolderName + "/";
        string temp = "Assets\\Resources\\Material\\" + aktuellerPfadOhnePraefixUndOhneLastFolderName + "\\";
        log.wl("temp :                       " + temp);


        if (!Directory.Exists("Assets\\Resources\\Material\\" + aktuellerPfadOhnePraefixUndOhneLastFolderName + "\\"))
        {
            Directory.CreateDirectory("Assets\\Resources\\Material\\" + aktuellerPfadOhnePraefixUndOhneLastFolderName + "\\");
        }

        if (!File.Exists("Assets\\Resources\\Material\\" + aktuellerPfadOhnePraefixUndOhneLastFolderName + "/" + materialName + ".mat") || vorhandeneUeberschreiben)
        {
           //AssetDatabase.CreateAsset(material, "Assets\\Resources\\Material\\" + aktuellerPfadOhnePraefixUndOhneLastFolderName + "\\" + materialName + ".mat");
        }

        //AssetDatabase.SaveAssets();

    }
}
