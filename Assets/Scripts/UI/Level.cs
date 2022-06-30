using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level: MonoBehaviour
{
    public int id;
    public string title;
    private int unlocked = 0;
    public Text textTitle;
    public GameObject unlockedImage, lockedImage;

    void Start()
    {
        //0 -> bloqueado
        //1 -> desbloqueado
        //PlayerPrefs.SetInt("UnlockLevel2", 0);
        if (id == 1)
            unlocked = 1;
        else
            unlocked = PlayerPrefs.GetInt("UnlockLevel"+id.ToString());
        
        /*
        if (unlocked == 0){
            unlockedImage.SetActive(false);
            lockedImage.SetActive(true);
        }
        else
            textTitle.text = title;
        */
        textTitle.text = title;
    }
}
