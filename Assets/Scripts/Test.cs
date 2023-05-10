using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Get());
    }

    private IEnumerator Get()
    {
        var req = UnityWebRequest.Get("http://a0808874.xsph.ru/main.php");
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(req.error);
            yield break;
        }
        Debug.Log(req.downloadHandler.text);
    }

    private IEnumerator Post()
    {
        var form = new WWWForm();
        form.AddField("number", 3);
        var req = UnityWebRequest.Post("http://a0806436.xsph.ru/test.php", form);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(req.error);
            yield break;
        }
        Debug.Log(req.downloadHandler.text);

        var xxx = JsonUtility.FromJson<TestPost>(req.downloadHandler.text);
        Debug.Log(xxx.Age);
    }

    
}
