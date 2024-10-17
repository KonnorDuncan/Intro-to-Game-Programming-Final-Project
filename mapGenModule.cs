using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGenModule : MonoBehaviour
{
    public string[] tags;
    public bool startingRoom = false;

    public ModuleConnector[] getConnectors(){
        return GetComponentsInChildren<ModuleConnector>();
    }
}
