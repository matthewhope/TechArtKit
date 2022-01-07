using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausticsMaskObject : MonoBehaviour
{
    [SerializeField]
    Transform causticsMaskTransform = null;
    void Awake()
    {
        if( causticsMaskTransform == null )
        {
            causticsMaskTransform = this.transform;
        }
    }

    void OnValidate()
    {
        UpdateMaskCenter();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMaskCenter();
    }

    void UpdateMaskCenter()
    {
        if( causticsMaskTransform == null )
        {
            return;
        }

        Shader.SetGlobalVector("_MaskCenter", causticsMaskTransform.position);

    }
}
