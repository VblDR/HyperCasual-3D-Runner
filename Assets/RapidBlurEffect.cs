using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Learning Unity Shader/Lecture 15/RapidBlurEffect")]
public class RapidBlurEffect : MonoBehaviour
{
    #region Variables

    private string ShaderName = "Learning Unity Shader/Lecture 15/RapidBlurEffect";

    public Shader CurShader;
    private Material CurMaterial;

    public static int ChangeValue;
    public static float ChangeValue2;
    public static int ChangeValue3;

    [Range(0, 6)]
    public int DownSampleNum = 2;
    [Range(0.0f, 20.0f)]
    public float BlurSpreadSize = 3.0f;
    [Range(0, 8)]
    public int BlurIterations = 3;

    #endregion
    #region MaterialGetAndSet
    Material material
    {
        get
        {
            if (CurMaterial == null)
            {
                CurMaterial = new Material(CurShader);
                CurMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return CurMaterial;
        }
    }
    #endregion

    #region Functions
    void Start()
    {
        ChangeValue = DownSampleNum;
        ChangeValue2 = BlurSpreadSize;
        ChangeValue3 = BlurIterations;

        CurShader = Shader.Find(ShaderName);

        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (CurShader != null)
        {
            float widthMod = 1.0f / (1.0f * (1 << DownSampleNum));
            material.SetFloat("_DownSampleValue", BlurSpreadSize * widthMod);
            sourceTexture.filterMode = FilterMode.Bilinear;
            int renderWidth = sourceTexture.width >> DownSampleNum;
            int renderHeight = sourceTexture.height >> DownSampleNum;

            RenderTexture renderBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, sourceTexture.format);
            renderBuffer.filterMode = FilterMode.Bilinear;
            Graphics.Blit(sourceTexture, renderBuffer, material, 0);

            for (int i = 0; i < BlurIterations; i++)
            {

                float iterationOffs = (i * 1.0f);
                material.SetFloat("_DownSampleValue", BlurSpreadSize * widthMod + iterationOffs);

                RenderTexture tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, sourceTexture.format);
                Graphics.Blit(renderBuffer, tempBuffer, material, 1);
                RenderTexture.ReleaseTemporary(renderBuffer);
                renderBuffer = tempBuffer;

                tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, sourceTexture.format);
                Graphics.Blit(renderBuffer, tempBuffer, CurMaterial, 2);

                RenderTexture.ReleaseTemporary(renderBuffer);
                renderBuffer = tempBuffer;
            }

            Graphics.Blit(renderBuffer, destTexture);
            RenderTexture.ReleaseTemporary(renderBuffer);

        }

        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void OnValidate()
    {

        ChangeValue = DownSampleNum;
        ChangeValue2 = BlurSpreadSize;
        ChangeValue3 = BlurIterations;
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            DownSampleNum = ChangeValue;
            BlurSpreadSize = ChangeValue2;
            BlurIterations = ChangeValue3;
        }
#if UNITY_EDITOR
        if (Application.isPlaying != true)
        {
            CurShader = Shader.Find(ShaderName);
        }
#endif

    }

    void OnDisable()
    {
        if (CurMaterial)
        {
            DestroyImmediate(CurMaterial);
        }

    }

    #endregion

}