using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using VisualDualCamera;
using UnityEngine.UIElements;
using System;
public class DualCamera : MonoBehaviour
{
    [SerializeField]
    public Camera LeftCamera;
    [SerializeField]
    public Camera RightCamera;
    private RenderTexture _renderTexture;

    [Inject]
    GameObjectTools _gameObjectTools { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        LeftCamera = _gameObjectTools.FindChildrenByName<Camera>(this, "LeftCamera");
        RightCamera = _gameObjectTools.FindChildrenByName<Camera>(this, "RightCamera");
        _renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
    }

    public DualCameraPayload GetDualCameraParameter()
    {
        var l = GetCameraParameter(LeftCamera);
        var r = GetCameraParameter(RightCamera);
        Debug.Log(LeftCamera == null);
        if(l == null || r == null) { return null; }
        return new DualCameraPayload() { Left = l, Right = r };
    }

    private CameraPayload GetCameraParameter(Camera cam)
    {
        if(cam == null) { return null; }
        var rawImage = CaptureImageFromRenderTexture();
        string imgBase64String = Convert.ToBase64String(rawImage);  // 編碼為 Base64 字符串
        
        return new CameraPayload {IntrinsicParameter = GetCameraIntrinsicParameter(cam) ,
                                  ImgRaw = imgBase64String };
    }

    byte[] CaptureImageFromRenderTexture()
    {
        RenderTexture.active = _renderTexture;  
        Texture2D texture2D = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.RGB24, false);
        texture2D.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);  
        texture2D.Apply(); 
        RenderTexture.active = null;  
        
        byte[] rawData = texture2D.GetRawTextureData();  // 取得原始的字節數據
        return rawData;

    }

    private IntrinsicParameter GetCameraIntrinsicParameter(Camera cam)
    {
        float fov = cam.fieldOfView;
        float fx = (cam.pixelHeight / 2) / Mathf.Tan(Mathf.Deg2Rad * fov / 2);
        float fy = fx / cam.aspect;
        float cx = cam.pixelWidth / 2;
        float cy = cam.pixelHeight / 2;
        return new IntrinsicParameter() { CenterX = cx, CenterY = cy, FocalX = fx, FocalY = fy };

    }

    private void Update()
    {
        if (LeftCamera != null && RightCamera != null)
        {
            Debug.Log($"L: {GetDualCameraParameter().Left.IntrinsicParameter}");
            Debug.Log($"R: {GetDualCameraParameter().Right.IntrinsicParameter}");
        }
    }
}
 