using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Score : MonoBehaviour
{
    [SerializeField]
    int MyScore = 100;
    public int Score
    {
        get
        {
            return MyScore;
        }
    }
}
