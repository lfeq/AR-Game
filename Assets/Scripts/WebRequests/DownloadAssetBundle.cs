using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadAssetBundle : MonoBehaviour {
    public string bundleName;

    // Start is called before the first frame update
    private void Start() {
        StartCoroutine(DownloadModel());
    }

    private IEnumerator DownloadModel() {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("http://192.168.68.82:3000/descargar-archivo/" + bundleName);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError) {
            Debug.LogError("Error al descargar el Asset Bundle: " + www.error);
        } else {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (bundle != null) {
                var prefab = bundle.LoadAllAssets<GameObject>(); //Aqui estan todos los prefabs de los carritos
                for (int i = 0; i < prefab.Length; i++) {
                    Instantiate(prefab[i]);
                }
            }
        }
    }
}