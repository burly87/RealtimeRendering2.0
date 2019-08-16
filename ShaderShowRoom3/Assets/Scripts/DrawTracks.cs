using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTracks : MonoBehaviour
{
    private RenderTexture _splatmap;                // map that is going to be drawn

    public Shader drawShader;
    private Material drawMaterial;
    private Material myMaterial;
    public GameObject terrain;

    public Transform[] wheel;                       // Array of all Wheels

    //for Ground
    private RaycastHit groundHit;
    private int layerMask;

    // Draw
    [Range(0, 2)] public float brushSize;
    [Range(0, 1)] public float brushStrength;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");

        drawMaterial = new Material(drawShader);
        // with setVector we can call the Value in the Shader of string
        myMaterial = terrain.GetComponent<MeshRenderer>().material;
        myMaterial.SetTexture("_Splat", _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < wheel.Length; i++)
        {
            if (Physics.Raycast(wheel[i].position, Vector3.down, out groundHit, 1.0f, layerMask))
            {
                drawMaterial.SetVector("_Coord", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Size", brushSize);
                drawMaterial.SetFloat("_Strength", brushStrength);

                // temp text to draw
                RenderTexture tmp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, tmp); // write the _splatmap to tmp
                Graphics.Blit(tmp, _splatmap, drawMaterial);
                RenderTexture.ReleaseTemporary(tmp); // release it eache frame
            }
        }
    }
}
