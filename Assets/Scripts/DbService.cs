using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class DbService
    {

        public List<T_Band> GetBänder()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Band";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsBand(query);
            return queryResult;
        }

        public List<T_Bandaufnahme> GetBandaufnahmen()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Bandaufnahme";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsBandaufnahme(query);
            return queryResult;
        }

        public List<T_Druecker> GetDrücker()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Druecker";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsDruecker(query);
            return queryResult;
        }

        public List<T_Schliessblech> GetSchliessbleche()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Schliessblech";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsSchliessblech(query);
            return queryResult;
        }

        public List<T_Schlosskasten> GetSchlosskästen()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Schlosskasten";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsSchlosskasten(query);
            return queryResult;
        }

        public List<T_Schwelle> GetSchwellen()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Schwelle";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsSchwelle(query);
            return queryResult;
        }

        public List<T_Tuerblatt> GetTürblätter()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Tuerblatt";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsTuerblatt(query);
            return queryResult;
        }

        public List<T_Zarge> GetZargen()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Zarge";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsZarge(query);
            return queryResult;
        }

        public List<T_Innentuer> GetInnentüren()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Innentuer";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsInnentuer(query);
            return queryResult;
        }

        public List<T_Objektteil> GetObjektteile()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Objektteil";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsObjektteil(query);
            return queryResult;
        }

        public List<T_Hersteller> GetHersteller()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.Hersteller";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsHersteller(query);
            return queryResult;
        }

        public List<T_MAT> GetMaterials()
        {
            var restClient = new RestClientManager();
            var query = "SELECT * FROM Thomas2.MAT";
            //Debug.Log(query);
            var queryResult = restClient.GetExecuteResultsMAT(query);
            return queryResult;
        }
    }
}
