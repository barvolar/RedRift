using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class IconLoader : MonoBehaviour
{
    [SerializeField] private Image _icon;

     private string _url = "https://picsum.photos/1000";

    private void Start()
    {
        StartCoroutine(LoadImage());
    }

    private IEnumerator LoadImage()
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(_url);

        yield return webRequest.SendWebRequest();

        if (webRequest.isDone == false)
            Debug.Log(webRequest.error);
        else
        {
            Texture texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            _icon.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        StopCoroutine(LoadImage());
    }
}
