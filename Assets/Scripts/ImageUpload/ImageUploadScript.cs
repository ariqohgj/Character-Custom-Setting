using Opsive.UltimateCharacterController.SurfaceSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinda.AlterLife.Singleton;

namespace Cinda.AlterLife
{
    public class ImageUploadScript : Singleton<ImageUploadScript>
    {
        [Serializable]
        public class ResultPage
        {
            [Header("TEXTURE TO SAVE")]
            public Texture2D texSave;
            public GameObject panel;
            public Image imageResult;

            public void SetDataImage(Texture2D _text)
            {
                texSave = Utilities.GetTextureCopy(_text);
                imageResult.sprite = Utilities.SpriteFromTexture2D(texSave);
                imageResult.preserveAspect = true;
                panel.SetActive(true);
            }
        }

        [Serializable]
        public class CameraPage
        {
            public RawImage imageRaw;
            public List<GameObject> objHide;
            public AspectRatioFitter fit;
            WebCamTexture webCamTexture = null;
            public GameObject panel;
            public void SetCamera()
            {
                panel.SetActive(true);

                webCamTexture = null;
                imageRaw.texture = null;

                WebCamDevice[] devices = WebCamTexture.devices;

                foreach (var device in devices)
                {
                    if (device.isFrontFacing)
                    {
                        webCamTexture = new WebCamTexture(device.name, Screen.height, Screen.width, 60);
                    }
                }

                if (webCamTexture != null)
                {
                    webCamTexture.Play();
                    imageRaw.texture = webCamTexture;


                    float ratio = (float)webCamTexture.width / (float)webCamTexture.height;
                    fit.aspectRatio = ratio;
                    float scaleY = webCamTexture.videoVerticallyMirrored ? 1f : -1;
                    imageRaw.rectTransform.localScale = new Vector3(1f / ratio, scaleY / ratio, 1f / ratio);
                    int orient = -webCamTexture.videoRotationAngle;
                    imageRaw.rectTransform.localEulerAngles = new Vector3(0f, 0f, orient);
                }
                else
                {
                    return;
                }
            }

            public void StopCamera()
            {
                if (webCamTexture != null)
                    webCamTexture.Stop();
            }


        }

        [Header("CAMERA")]
        public CameraPage cameraPage;
        [Header("RESULT")]
        public ResultPage resultPage;

        public void OpenCamera()
        {
            cameraPage.SetCamera();
        }

        public void TakePict()
        {
            StartCoroutine(RecordFrame());
        }

        IEnumerator RecordFrame()
        {
            foreach (GameObject obj in cameraPage.objHide) obj.SetActive(false);
            yield return new WaitForEndOfFrame();
            var texture = ScreenCapture.CaptureScreenshotAsTexture();
            foreach (GameObject obj in cameraPage.objHide) obj.SetActive(true);
            StopCamera();
            instance.resultPage.SetDataImage(texture);
            cameraPage.panel.SetActive(false);

        }

        public void ClickGallery()
        {
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
            {
                Debug.Log("Image path: " + path);
                if (path != null)
                {
                    SetImage(path, 512);
                }
            });

        }

        public void ClickSend()
        {

        }

        public void StopCamera() => cameraPage.StopCamera();


        void SetImage(string path, int maxSize)
        {
            // Create Texture from selected image
            Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
            if (texture == null)
            {
                Debug.Log("Couldn't load texture from " + path);
                return;
            }
            resultPage.SetDataImage(texture);
        }
    }

}
