using UnityEngine;

public class FragileWall : MonoBehaviour
{
    public int index { get; set; }

    public void Disappear()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        string key = "FragileWall " + index.ToString();     // String is required for keys.
        WallManager.SetHashtable(key, false);
    }
}