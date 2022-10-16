using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramSpawner : MonoBehaviour
{

    public GameObject hologram;
    public GameObject hologramPrefab;

    public GameObject panel;
    private PhotonView PV;

    int count;
    
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }


    public void MakeCopy()
    {
        if( PhotonNetwork.OfflineMode == true)
        {
            MakeCopyHologram();
        }
        else
        {
            //SplineDraw() RPC Online Mode
            PV.RPC( "MakeCopyHologram"
                          , RpcTarget.All
                          , new object[] {}
                          );
        }

    }

    //RPC call to make a hologram copy
    [PunRPC]
    public void MakeCopyHologram()
    {
        if (count == 0)
        {
            GameObject hologramCopy = Instantiate(hologramPrefab, hologram.transform);
                    // currentSpline = Instantiate(splinePrefab, position, rotation, whiteboardObjectParent.transform);

            //Increment photon view ID by 20
            int currentID = PV.ViewID;
            currentID = currentID + 20;
            PV.ViewID = currentID;
            hologram.transform.parent = null;
            print("Spawning CHILL ICE Hologram");
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
