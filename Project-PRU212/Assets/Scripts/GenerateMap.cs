using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System.Linq;
public class GenerateMap : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile waterTile;
    public Tile treeTile;
    public Tile wallSteelTile;
    public Tile emptyTile;
    void Start()
    {
        LoadMap("Assets/Files/Map_1.txt");
    }
    void LoadMap(string filePath)
    {
        string[] mapLines = File.ReadAllLines(filePath); 
        for (int i = 0; i < mapLines.Length; i++)
        {
            string line = mapLines[i];
            string[] tiles = line.Split(' ');
            int count = tiles.Select(x => x.Trim()).Where(x => string.IsNullOrEmpty(x)).Count();
            if (count % 2 == 0) count /= 2;
            else count = count / 2 + 1;
            for (int j = 0; j < tiles.Length - count; j++)
            {
                if (string.IsNullOrEmpty(tiles[j].Trim())) continue;
                int tileType = int.Parse(tiles[j]);
                PlaceTile(j, -i, tileType);
            }
        }
    }
    void PlaceTile(int x, int y, int tileType)
    {
        Vector3Int tilePosition = new(x, y, 0); // Vị trí của tile trên Tilemap
        switch (tileType)
        {
            case 1:
                tilemap.SetTile(tilePosition, wallTile); // Đặt tile là tường
                break;
            case 2:
                tilemap.SetTile(tilePosition, waterTile); // Đặt tile là nước
                break;
            case 3:
                tilemap.SetTile(tilePosition, treeTile); // Đặt tile là cây
                break;
            case 0:
                tilemap.SetTile(tilePosition, wallSteelTile); // Đặt tile là đường (hoặc để trống nếu không có tile)
                break;
            default:
                tilemap.SetTile(tilePosition, null); // Không có tile nếu không khớp
                break;
        }
    }
    void Update()
    {

    }
}
