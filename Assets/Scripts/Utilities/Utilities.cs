using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

namespace CindaAlterLife
{

    public class Utilities
    {
        public static Sprite SpriteFromTexture2D(Texture2D _texture)
        {
            var texture = GetTextureCopy(_texture);
            return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        /// <summary>
        /// convert texture to bytes array
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] ConvertTextureToBytes(Texture2D source)
        {

            return GetTextureCopy(source).EncodeToPNG();
        }

        public static string ConvertTextureToBase64(Texture2D source)
        {
            return Convert.ToBase64String(ConvertTextureToBytes(source));
        }

        public static Sprite LoadSpriteUsingString64(string source)
        {
            Texture2D tex = new Texture2D(256, 256);
            tex.LoadImage(Convert.FromBase64String(source));
            return SpriteFromTexture2D(tex);
        }

        /// <summary>
        /// convert byte to sprite
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Sprite LoadSpriteUsingBytes(byte[] pngByte)
        {
            Texture2D tex = new Texture2D(256, 256);
            tex.LoadImage(pngByte);
            return SpriteFromTexture2D(tex);
        }

        /// <summary>
        /// convert byte to texture2d
        /// </summary>
        /// <param name="pngByte"></param>
        /// <returns></returns>
        public static Texture2D LoadTextureUsingBytes(byte[] pngByte)
        {
            Texture2D tex = new Texture2D(256, 256);
            tex.LoadImage(pngByte);
            return tex;
        }
        public static Texture2D LoadTextureUsingString64(string source)
        {
            Texture2D tex = new Texture2D(256, 256);
            tex.LoadImage(Convert.FromBase64String(source));
            return tex;
        }

        public static Texture2D GetTextureCopy(Texture2D texture2d)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(
                         texture2d.width,
                         texture2d.height,
                         0,
                         RenderTextureFormat.Default,
                         RenderTextureReadWrite.sRGB);

            Graphics.Blit(texture2d, renderTexture);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTexture;
            Texture2D readableTextur2D = new Texture2D(texture2d.width, texture2d.height);
            readableTextur2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            readableTextur2D.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTexture);
            return readableTextur2D;
        }
    }

}
