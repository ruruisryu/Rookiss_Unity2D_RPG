using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    public Grid CurrentGrid { get; private set; }

    public void LoadMap(int mapID)
    {
        DestroyMap();

        string mapName = "Map_" + mapID.ToString("000");
        GameObject go = Managers.Resource.Instantiate($"Map/{mapName}");
        go.name = "Map";

        GameObject collision = Util.FindChild(go, "TileMap_Collision", true);
        if (collision != null)
            collision.SetActive(false);

        CurrentGrid = go.GetComponent<Grid>();

    }

    public void DestroyMap()
    {
        GameObject map = GameObject.Find("Map");
        if (map != null)
        {
            GameObject.Destroy(map);
            CurrentGrid = null;
        }
    }
}
