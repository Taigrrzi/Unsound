using UnityEngine;
using System.Collections;

public class InventoryItem {

    public string type;
    public string subType;
    public Sprite image;
    public int size;

    public InventoryItem(string ty, string subty,Sprite img,int si)
    {
        subType = subty;
        type = ty;
        img = image;
        size = si;
    }

}
