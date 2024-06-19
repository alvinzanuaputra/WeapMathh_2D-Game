using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerChanger : NetworkBehaviour
{
    GameManager gm;

    void Start()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));
        if (IsServer)
        {
            gm.changePlayer(1);
            gm.startMatch();
        }
        else if (IsClient)
        {
            gm.changePlayer(2);
        }
        gm.waitingPlayer();
    }
}
