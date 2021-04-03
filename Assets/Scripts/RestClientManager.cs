using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using UnityEngine;
using RestSharp;
using RestSharp.Authenticators;
using UnityTemplateProjects;

public class RestClientManager 
{

    private string baseUrl = "http://62.171.164.125:3000/api/v1/";

    public List<T_Band> GetExecuteResultsBand(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Band> queryResult = client.Execute<List<T_Band>>(request).Data;
        return queryResult;
    }

    public List<T_Bandaufnahme> GetExecuteResultsBandaufnahme(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Bandaufnahme> queryResult = client.Execute<List<T_Bandaufnahme>>(request).Data;
        return queryResult;
    }

    public List<T_Druecker> GetExecuteResultsDruecker(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Druecker> queryResult = client.Execute<List<T_Druecker>>(request).Data;
        return queryResult;
    }

    public List<T_Schliessblech> GetExecuteResultsSchliessblech(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Schliessblech> queryResult = client.Execute<List<T_Schliessblech>>(request).Data;
        return queryResult;
    }

    public List<T_Schlosskasten> GetExecuteResultsSchlosskasten(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Schlosskasten> queryResult = client.Execute<List<T_Schlosskasten>>(request).Data;
        return queryResult;
    }

    public List<T_Schwelle> GetExecuteResultsSchwelle(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Schwelle> queryResult = client.Execute<List<T_Schwelle>>(request).Data;
        return queryResult;
    }

    public List<T_Tuerblatt> GetExecuteResultsTuerblatt(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Tuerblatt> queryResult = client.Execute<List<T_Tuerblatt>>(request).Data;
        return queryResult;
    }

    public List<T_Zarge> GetExecuteResultsZarge(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Zarge> queryResult = client.Execute<List<T_Zarge>>(request).Data;
        return queryResult;
    }

    public List<T_Innentuer> GetExecuteResultsInnentuer(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Innentuer> queryResult = client.Execute<List<T_Innentuer>>(request).Data;
        return queryResult;
    }

    public List<T_Objektteil> GetExecuteResultsObjektteil(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Objektteil> queryResult = client.Execute<List<T_Objektteil>>(request).Data;
        return queryResult;
    }

    public List<T_Hersteller> GetExecuteResultsHersteller(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_Hersteller> queryResult = client.Execute<List<T_Hersteller>>(request).Data;
        return queryResult;
    }

    public List<T_MAT> GetExecuteResultsMAT(string query)
    {
        var client = new RestClient(baseUrl + "materials/execute/" + query);
        var request = new RestRequest();
        var response = client.Get(request);
        List<T_MAT> queryResult = client.Execute<List<T_MAT>>(request).Data;
        return queryResult;
    }



    public List<AssetDTO> GetAllAssetInformation()
    {
        var client = new RestClient(baseUrl + "assets/getAllAssets");
        var request = new RestRequest();
        var response = client.Get(request);

        List<AssetDTO> queryResult = client.Execute<List<AssetDTO>>(request).Data;

        return queryResult;
    }

    public List<MaterialDTO> GetAllMaterialInformation()
    {
        var client = new RestClient(baseUrl + "materials/getAllMaterials");
        var request = new RestRequest();
        var response = client.Get(request);

        List<MaterialDTO> queryResult = client.Execute<List<MaterialDTO>>(request).Data;


        return queryResult;
    }


}

public class T_Band
{
    public string Id { get; set; }
    public int Rollendurchmesser { get; set; }
}

public class T_Bandaufnahme
{
    public string Id { get; set; }
    public int Rollendurchmesser { get; set; }
}

public class T_Druecker
{
    public string Id { get; set; }
    public int Laenge { get; set; }
    public int Rosettenbreite { get; set; }
    public int Lochabstand { get; set; }
}

public class T_Hersteller
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class T_Innentuer
{
    public string Id { get; set; }
    public string Detail { get; set; }
    public string Zarge { get; set; }
    public string Tuerblatt { get; set; }
    public string Band1 { get; set; }
    public string Band2 { get; set; }
    public string Bandaufnahme1 { get; set; }
    public string Bandaufnahme2 { get; set; }
    public string DrueckerZier { get; set; }
    public string DrueckerFalz { get; set; }
    public string Schlosskasten { get; set; }
    public string Schliessblech { get; set; }
    public string Schwelle { get; set; }
}

public class T_MAT
{
    public string Id { get; set; }
    public string Bezeichnung { get; set; }
    public string Kategorie { get; set; }
    public int Aufloesung { get; set; }
    public string Pfad { get; set; }
    public string Quelle { get; set; }
    public string QuelleLink { get; set; }
}

public class T_Objektteil
{
    public string Id { get; set; }
    public string Kategorie { get; set; }
    public string Pfad { get; set; }
    public string Detail { get; set; }
    public decimal Gewicht { get; set; }
    public decimal PreisBrutto { get; set; }
    public string Hersteller { get; set; }
    public string MAT { get; set; }
}

public class T_Schliessblech
{
    public string Id { get; set; }
    public int Hoehe { get; set; }
    public int Breite { get; set; }
    public int Tiefe { get; set; }
    public int Staerke { get; set; }
    public int Lochabstand { get; set; }
}

public class T_Schlosskasten
{
    public string Id { get; set; }
    public int Dornmass { get; set; }
    public int Lochabstand { get; set; }
    public int Fallenlaenge { get; set; }
}

public class T_Schwelle
{
    public string Id { get; set; }
    public int Bautiefe { get; set; }
    public int Hoehe { get; set; }
    public int Profil { get; set; }
}

public class T_Tuerblatt
{
    public string Id { get; set; }
    public int HoeheDIN { get; set; }
    public int BreiteDIN { get; set; }
    public int Kantenrundung { get; set; }
    public int Dornmass { get; set; }
    public int Drueckerhoehe { get; set; }
    public int Lochabstand { get; set; }
    public int Bandabstand { get; set; }
}

public class T_Zarge
{
    public string Id { get; set; }
    public int HoeheDIN { get; set; }
    public int BreiteDIN { get; set; }
    public int Wandstaerke { get; set; }
    public int Bekleidungsbreite { get; set; }
}