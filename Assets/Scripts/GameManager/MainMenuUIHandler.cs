using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerNickName"))
        {
            inputField.text = PlayerPrefs.GetString("PlayerNickName");
        }
    }

    public void OnJoinGameClicked()
    {
        PlayerPrefs.SetString("PlayerNickName", inputField.text);
        PlayerPrefs.Save();

        SceneManager.LoadScene("ScenaElias");
    }
}
