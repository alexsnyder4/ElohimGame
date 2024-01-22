using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerLayerData 
{
    [field: SerializeField] public LayerMask GroundLayer {  get; set; }

    public bool ContainsLayer(LayerMask layerMask, int layer)
    {
        return(1<< layer & layerMask) !=0;
    }

    public bool IsGroundLayer(int layer)
    {
        return ContainsLayer(GroundLayer, layer);
    }
}
