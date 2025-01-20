using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomGrass : MonoBehaviour
{
    public List<GameObject> brushes;
    float seed;
    public float maxSlicePieces;//最大细分次数
    void Start()
    {
        foreach(GameObject brush in brushes)
        {
            //选取随机种子
            seed = Random.Range(0, 100f);
            for (int i = 0; i < maxSlicePieces+1; i++)
            {
                GenerateBrush(i,brush);
            }
        }
    }
    void GenerateBrush(int count,GameObject brush)
    {
        float a = Mathf.PerlinNoise1D(seed+count);
        Vector3 pos = new Vector3(transform.position.x - 1+(2/maxSlicePieces*count), transform.position.y + 1.2f, 0);
        if(a >= 0.6f)
        {
            Instantiate(brush, pos, transform.rotation,transform);
        }
    }
}
