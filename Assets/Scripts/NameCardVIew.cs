using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameCardVIew : MonoBehaviour
{
    public Canvas canvas;

    public Image displayImage;

    public Image profileImage;

    public TextMeshProUGUI nameText;

    private string[] links = new string[3];

    List<Sprite> sprites = new List<Sprite>();

    public float changeInterval = 2f;   // 2ÃÊ

    int currentIndex = 0;
    float timer = 0f;

    public void OnEnable()
    {
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }
    }

    void Update()
    {
        if (sprites == null || sprites.Count == 0) return;

        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % sprites.Count;
            displayImage.sprite = sprites[currentIndex];
        }
    }

    public void SetUserData(UserDataSO userData)
    {
        profileImage.sprite = userData.profile;
        nameText.text = userData.userName;

        links = new string[3];
        links[0] = userData.eMail;
        links[1] = userData.webUrl_1;
        links[2] = userData.webUrl_2;

        sprites = userData.sprites;
    }

    public void OnClickButton(int index)
    {
        if(links.Length > index && index >= 0)
        {
            Application.OpenURL(links[index]);
        }
    }
}
