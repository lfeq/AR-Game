using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class UploadAssetBundle : MonoBehaviour {
    public string modelPath = "Assets/AssetBundles/"; // Ruta donde se guardará el archivo.
    public string modelName;

    private void Start() {
        StartCoroutine(Upload());
    }

    private IEnumerator Upload() {
        // Ruta del archivo que deseas subir
        byte[] fileData = File.ReadAllBytes(modelPath);
        // Crear un formulario con los datos
        WWWForm form = new WWWForm();
        // Agregar el nombre del modelo como un campo normal
        form.AddField("nombreModelo", modelName);
        // Agregar el archivo como un campo binario
        form.AddBinaryData("archivo", fileData, Path.GetFileName(modelPath));

        UnityWebRequest www = UnityWebRequest.Post("http://192.168.68.82:3000/subir-archivo", form);
        // No es necesario especificar el tipo de contenido, se asigna automáticamente
        // www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.LogError(www.error);
            Debug.Log(www.downloadHandler.text);
        } else {
            Debug.Log("Archivo cargado con éxito");
        }
    }
}