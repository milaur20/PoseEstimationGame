using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poses : MonoBehaviour
{
    public GameObject[] poses; // Array of pose GameObjects
    private GameObject posesObj;

    void Start()
    {
        posesObj = GameObject.Find("Poses");

        //put each child of posesobj into poses array
        poses = new GameObject[posesObj.transform.childCount];
        for (int i = 0; i < posesObj.transform.childCount; i++)
        {
            poses[i] = posesObj.transform.GetChild(i).gameObject;
        }
    }
}
