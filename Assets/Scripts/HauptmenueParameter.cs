using System.Collections.Generic;

public partial class GUI_Innentuer
{
    public class HauptmenueParameter
    {
        public int MaximaleAnzahlMenuepunkte = 8;
        public int MaximaleAnzahlToogglePunkte = 10;
        public List<string> Menuepunkt { get; set; }
        public string[][] Toggle { get; set; }
        public string[][] ToggleAktion { get; set; }
        public int[] TogglePunktIndex { get; set; }
    }
}