using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{

    public GameObject water;
    public Grid grid;
    public GameObject[] map2d;

    public const int MAP_WIDTH = 80;
    public const int MAP_HEIGHT = 60;

    // Start is called before the first frame update
    void Start()
    {
        map2d = new GameObject[MAP_WIDTH * MAP_HEIGHT];
        for (int i = 0; i < map2d.Length; i++)
        {
            int row = i / MAP_WIDTH;
            int column = i % MAP_WIDTH;

            if (row == 0 || column == 0 || row == MAP_HEIGHT - 1 || column == MAP_WIDTH - 1)
            {
                Instantiate(water, new Vector3(column, row, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
