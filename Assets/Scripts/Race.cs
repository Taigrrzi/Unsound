using UnityEngine;
using System.Collections;
[System.Serializable]
public class Race {
    public string raceNoun;// Human, Cyborg
    public string raceAdjective;// Human, French
    public string language;// English, Spanish
    //public string languageNoun;
    public int maxAge;
    public int agroMod;
    public int ambMod;
    public int conMod;
    public int priMod;
    public int intMod;
    public int id;

    public Race(int myid,string noun, string adj, string lang, int max,int agMod, int amMod, int coMod, int prMod, int inMod)
    {
        id = myid;
        raceNoun = noun;
        raceAdjective = adj;
        language = lang;
        maxAge = max;
        agroMod = agMod;
        ambMod = amMod;
        conMod = coMod;
        priMod = prMod;
        intMod = inMod;
    }

}
