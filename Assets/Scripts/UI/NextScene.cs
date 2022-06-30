using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string scene;
    public GameObject fade;

    public void LoadScene(){
        SceneManager.LoadScene(scene);
    }

    public void ClickToLoadScene(){ 
        StartCoroutine(DelayLoadScene());
    }

    private IEnumerator DelayLoadScene()
    {
        GetComponent<Animator>().SetTrigger("Pressed");
        var createImage = Instantiate(fade) as GameObject;
        createImage.transform.SetParent(GameObject.Find("Canvas").transform, false);
        createImage.GetComponent<Animator>().SetTrigger("Out");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(scene);
    }
}
