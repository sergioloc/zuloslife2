using UnityEngine;

public class MenuValues {

    public static float position = 7f;
    public static bool moveRight = false;
    public static bool muteMusic = GetMuteValue();

    private static bool GetMuteValue() {
        if (PlayerPrefs.GetInt("Mute") == 0)
            return false;
        else
            return true;
    }

}
