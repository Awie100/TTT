using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager inst;

    public int clicks;

    // Start is called before the first frame update
    void Awake()
    {
        if (inst == null) inst = this;
        DontDestroyOnLoad(gameObject);
    }
}
