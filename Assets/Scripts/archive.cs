using UnityEngine;
using System.Collections;

public class archive {

    public int math = 0;

    public static Race requestRandomRace(float rand)
    {

        Race[] commonRaces = new Race[5];
        commonRaces[0] = new Race(0,"Human", "Human", "English", 100, 5, 5, 0, -10, 0);
        commonRaces[1] = new Race(1,"Klingon", "Klingon", "Klingon", 200, 20, 0, 20, 40, -5);
        commonRaces[2] = new Race(2,"Android", "Robotic", "Binary", 500, -20, -40, 0, -40, 30);

        return commonRaces[0];//Mathf.FloorToInt(rand*commonRaces.Length)];
    }

    public static string requestRandomName(int raceID,float rand)
    {
        switch (raceID)
        {
            case 0:
                return "RANDOMHUMANNAME";
            case 1:
                return "RANDOMKLINGONNAME";
            case 2:
                return "RANDOMANDROIDNAME";
            default:
                return "INVALIDRACEID";
        }
    }

}
