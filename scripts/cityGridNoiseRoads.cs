using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cityGridNoiseRoads : MonoBehaviour
{
    public GameObject[] cityAssets;
    public GameObject xDirRoad;
    public GameObject zDirRoad;
    public GameObject intersection;

    public int cityWidth = 20;
    public int cityHeight = 20;
    public int citySpacing = 2;

    int wRand;
    int hRand;
    int[,] cityGrid;


    // Use this for initialization
    void Start()
    {
        cityGrid = new int[cityWidth, cityHeight];

        // Generate Initial Layout
        for (int h = 0; h < cityHeight; h++)
        {
            for (int w = 0; w < cityWidth; w++)
            {
                // Add some random variation to the Perlin Noise generation
                wRand = w + Random.Range(0, 3);
                hRand = h + Random.Range(0, 3);
                
                // Generate the Perlin Noise value for a point on the gid
                cityGrid[w, h] = (int)(Mathf.PerlinNoise(wRand / 10.0f, hRand / 10.0f) * 10);
            }
        }

        // Modify Grid Layout to Add Roads in Rows (Unity Z-direction)
        int row = 0;
        // Note that the Height and Width are reversed to call rows in order
        for (int w = 0; w < cityWidth; w++)
        {
            for (int h = 0; h < cityHeight; h++)
            {
                // Assign a z-direction road
                cityGrid[row, h] = -1;
            }
            // Increment to another row at random intervals within the range
            row += Random.Range(3, 4);
            if (row >= cityWidth) break;
        }

        // Modify Grid Layout to Add Roads in Columns (Unity X-Direction)
        int col = 0;
        for (int h = 0; h < cityHeight; h++)
        {
            for (int w = 0; w < cityWidth; w++)
            {
                //Check if previously assigned z-direction road exists
                if (cityGrid[w, col] == -1)
                {
                    // If ture, assign an intersection
                    cityGrid[w, col] = 0;
                }
                else
                {
                    // Otherwise, assign a x-directon road
                    cityGrid[w, col] = -2;
                }
            }
            // Increment to another column at random intervals within the range
            col += Random.Range(4, 8);
            if (col >= cityHeight) break;
        }

        // Generate City
        for (int h = 0; h < cityHeight; h++)
        {
            for (int w = 0; w < cityWidth; w++)
            {
                int assetAssign = cityGrid[w, h];
                Vector3 pos = new Vector3(w * citySpacing, 0, h * citySpacing);

                if (assetAssign == 0)
                {
                    // Display an intersection
                    Instantiate(intersection, pos, intersection.transform.rotation);
                }
                else if (assetAssign == -1)
                {
                    // Display a Z-direction road
                    Instantiate(zDirRoad, pos, zDirRoad.transform.rotation);
                }
                else if (assetAssign == -2)
                {
                    // Display a X-direction road
                    Instantiate(xDirRoad, pos, xDirRoad.transform.rotation);
                } 
                // All other cityAssets below
                else if (assetAssign <= 2)
                {
                    Instantiate(cityAssets[0], pos, Quaternion.identity);
                }
                else if (assetAssign <= 4)
                {
                    Instantiate(cityAssets[1], pos, Quaternion.identity);
                }
                else if (assetAssign <= 6)
                {
                    Instantiate(cityAssets[2], pos, Quaternion.identity);
                }
                else if (assetAssign <= 8)
                {
                    Instantiate(cityAssets[3], pos, Quaternion.identity);
                }
            }
        }

        // Unity debug console output to test visual asset correlation with generated grid
        //string arrStr = "";
        //for (int i = 0; i < cityHeight; i++)
        //{
        //    for (int j = 0; j < cityWidth; j++)
        //    {
        //        arrStr += string.Format(cityGrid[i, j] + "\t");
        //    }
        //    arrStr += System.Environment.NewLine + System.Environment.NewLine;
        //}
        //Debug.Log(arrStr);
        
    }
}
