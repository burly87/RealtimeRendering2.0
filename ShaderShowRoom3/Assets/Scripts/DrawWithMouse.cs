using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public Camera _camera;
    public Shader _drawShader;

    private RenderTexture _splatmap;                // map that is going to be drawn
    private Material snowMaterial, drawMaterial;  // materials

    // to Draw
    private RaycastHit hit;
    [Range(1, 100)] public float brushSize;
    [Range(0, 1)] public float brushStrength;


    // Start is called before the first frame update
    void Start()
    {
        drawMaterial = new Material(_drawShader);
        // with setVector we can call the Value in the Shader of string
        drawMaterial.SetVector("_Color", Color.red);

        snowMaterial = GetComponent<MeshRenderer>().material;
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", _splatmap);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                drawMaterial.SetVector("_Coord", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
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

    // for debugging
    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 256, 256), _splatmap, ScaleMode.ScaleToFit, false, 1);
    }
}
