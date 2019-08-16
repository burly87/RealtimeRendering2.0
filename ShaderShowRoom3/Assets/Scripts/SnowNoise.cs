using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour
{
    public Shader snowFallShader;
    private Material snowFallMat;
    private MeshRenderer meshRenderer;

    [Range(0.001f, 0.1f)] public float snowAmount;
    [Range(0.0f, 1.0f)] public float snowOpacity;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        snowFallMat = new Material(snowFallShader);
    }

    void Update()
    {
        snowFallMat.SetFloat("_SnowAmount", snowAmount);
        snowFallMat.SetFloat("_SnowOpacity", snowOpacity);
        //get the SplatTexture
        RenderTexture snow = (RenderTexture)meshRenderer.material.GetTexture("_Splat");
        RenderTexture tmp = RenderTexture.GetTemporary(snow.width, snow.height, 0, RenderTextureFormat.ARGBFloat);
        //Drawcall
        Graphics.Blit(snow, tmp, snowFallMat);
        Graphics.Blit(tmp, snow);
        meshRenderer.material.SetTexture("_Splat", snow);
        //set free
        RenderTexture.ReleaseTemporary(tmp);

    }
}
