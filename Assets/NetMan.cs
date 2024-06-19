using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetMan : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.SetSingleton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
