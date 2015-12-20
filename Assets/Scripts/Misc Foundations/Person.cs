using UnityEngine;
using System.Collections;
[System.Serializable]
public class Person {

    public string fullName;
    public int age;
    public int agression;
    public int ambition;
    public int confidence;
    public int pride;
    public int intelligence;
    public Race race;

    public Person (float rand,string name)
    {
        race = archive.requestRandomRace(rand);
        //fullName = archive.requestRandomName(race.id);
        age = Mathf.FloorToInt(Random.value *race.maxAge);
        agression = Mathf.FloorToInt(Random.value * 100) + race.agroMod;
        ambition = Mathf.FloorToInt(Random.value * 100) + race.ambMod;
        confidence = Mathf.FloorToInt(Random.value * 100) + race.conMod;
        pride = Mathf.FloorToInt(Random.value * 100) + race.priMod;
        intelligence = Mathf.FloorToInt(Random.value * 100) + race.intMod;

    }

    public Person(float rand)
    {
        race = archive.requestRandomRace(rand);
        fullName = archive.requestRandomName(race.id,rand);
        age = Mathf.FloorToInt(Random.value * race.maxAge);
        agression = Mathf.FloorToInt(Random.value * 100) + race.agroMod;
        ambition = Mathf.FloorToInt(Random.value * 100) + race.ambMod;
        confidence = Mathf.FloorToInt(Random.value * 100) + race.conMod;
        pride = Mathf.FloorToInt(Random.value * 100) + race.priMod;
        intelligence = Mathf.FloorToInt(Random.value * 100) + race.intMod;

    }
}
