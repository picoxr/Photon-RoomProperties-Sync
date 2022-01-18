using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class WallManager : MonoBehaviourPunCallbacks
{
    public static Dictionary<int, FragileWall> wallDic;

    public void Awake()
    {
        wallDic = new Dictionary<int, FragileWall>(); // A dictionary is convenient for local search.
        for (int dicIndex = 0; dicIndex < transform.childCount; dicIndex++)
        {
            FragileWall wall = transform.GetChild(dicIndex).GetComponentInChildren<FragileWall>();    
            wallDic.Add(dicIndex, wall);
            wall.index = dicIndex; // Set index of each wall.
        }
    }

    // Apply data(from room properties) to local walls.
    public static void ApplyVisibility(string s, bool v)
    {   
        int num = int.Parse(s.Substring(s.LastIndexOf(" ") + 1)); // Get index X from a key "FragileWall X".
        FragileWall wall = wallDic[num];
        wall.GetComponent<MeshRenderer>().enabled = v;
        wall.GetComponent<Collider>().enabled = v;
    }

    // Set local data to room properties.
    public static void SetHashtable(string key, bool value)
    {
        Hashtable properties = new Hashtable { { key, value } };
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
    }

    // Callback of "SetCustomProperties".
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        foreach (string propertiesKey in propertiesThatChanged.Keys)
        {
            object propertiesValue = propertiesThatChanged[propertiesKey];      
            ApplyVisibility(propertiesKey, (bool)propertiesValue);
        }
    }
}