using System.Collections.Generic;

namespace Assets.Scripts
{
    public class AktuelleGetoggelteInnentuer
    {

        public AktuelleGetoggelteInnentuer()
        {
            Zarge = new TürObjekt();
            Tuerblatt = new TürObjekt();
            Band1 = new TürObjekt();
            Band2 = new TürObjekt();
            Bandaufnahme1 = new TürObjekt();
            Bandaufnahme2 = new TürObjekt();
            DrueckerFalz = new TürObjekt();
            DrueckerZier = new TürObjekt();
            Schlosskasten = new TürObjekt();
            Schliessblech = new TürObjekt();
            Schwelle = new TürObjekt();
        }

        public List<string> Liste = new List<string>();
        public int Index { get; set; }
        public string Id { get; set; }
        public string Detail { get; set; }
        public TürObjekt Zarge { get; set; }
        public TürObjekt Tuerblatt { get; set; }
        public TürObjekt Band1 { get; set; }
        public TürObjekt Band2 { get; set; }
        public TürObjekt Bandaufnahme1 { get; set; }
        public TürObjekt Bandaufnahme2 { get; set; }
        public TürObjekt DrueckerFalz { get; set; }
        public TürObjekt DrueckerZier { get; set; }
        public TürObjekt Schlosskasten { get; set; }
        public TürObjekt Schliessblech { get; set; }
        public TürObjekt Schwelle { get; set; }
    }

    public class TürObjekt
    {
        public TürObjekt()
        {
            MaterialKombination = new MaterialKombination();
        }
        public string Bezeichnung { get; set; }
        public MaterialKombination MaterialKombination { get; set; }
    }
}
