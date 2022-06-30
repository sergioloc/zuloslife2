using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    public Sprite mute, unmute;
    private Image image;
    private Animator animator;

    void Start()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
        SetIcons();
    }

    private void SetIcons(){
        if (PlayerPrefs.GetInt("Mute") == 1){
            image.sprite = mute;
            MenuValues.muteMusic = true;
        }
        else {
            image.sprite = unmute;
            MenuValues.muteMusic = false;
        }
    }

    public void PressButton(){
        if (PlayerPrefs.GetInt("Mute") == 1){
            PlayerPrefs.SetInt("Mute", 0);
        }
        else {
            PlayerPrefs.SetInt("Mute", 1);
        }
        animator.SetTrigger("Pressed");
        SetIcons();
    }
}
