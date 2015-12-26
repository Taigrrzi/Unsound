using UnityEngine;
using System.Collections;
[System.Serializable]
public class Person {

    public string fullName;
    public int age;
    public int aggression;
    public int ambition;
    public int confidence;
    public int pride;
    public int intelligence;
    public float lImpression;
    public float rImpression;
    public int manner;
    public Sprite image;
    public Response[] conversation;

    public Race race;

    public Person (float rand,string name)
    {
        race = archive.requestRandomRace(rand);
        fullName = name;
        age = Mathf.FloorToInt(Random.value *race.maxAge);
        aggression = Mathf.FloorToInt(Random.value * 100) + race.agroMod;
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
        aggression = Mathf.FloorToInt(Random.value * 100) + race.agroMod;
        ambition = Mathf.FloorToInt(Random.value * 100) + race.ambMod;
        confidence = Mathf.FloorToInt(Random.value * 100) + race.conMod;
        pride = Mathf.FloorToInt(Random.value * 100) + race.priMod;
        intelligence = Mathf.FloorToInt(Random.value * 100) + race.intMod;
    }

    public void StartConversation(GameObject panel)
    {
        panel.SetActive(true);
        //panel.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = image;
        panel.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = fullName;
        //panel.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "TEST!!!";

        panel.transform.GetChild(2).GetComponent<speechController>().p = this;
        panel.transform.GetChild(2).GetComponent<speechController>().currentResponse = 0;
    }
}
