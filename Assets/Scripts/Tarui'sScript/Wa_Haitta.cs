using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wa_Haitta : MonoBehaviour
{
    [SerializeField]
    int RingInCount_S = 0, RingInCount_M = 0, RingInCount_L = 0;

    [SerializeField]
    int MyScore = 0;
    public int Score
    {
        get
        {
            return MyScore;
        }
    }

    public void RingCount(bool Flg, RingScale ring, int score)
    {
        if (Flg)
        {
            switch (ring)
            {
                case RingScale.大:
                    RingInCount_L += 1;
                    break;
                case RingScale.中:
                    RingInCount_M += 1;
                    break;
                case RingScale.小:
                    RingInCount_S += 1;
                    break;
            }

            MyScore += score;
        }
        else
        {
            switch (ring)
            {
                case RingScale.大:
                    RingInCount_L -= 1;
                    break;
                case RingScale.中:
                    RingInCount_M -= 1;
                    break;
                case RingScale.小:
                    RingInCount_S -= 1;
                    break;
            }

            MyScore -= score;
        }
    }
}
