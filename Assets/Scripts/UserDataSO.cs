using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "User Data", menuName = "User Data")]
public class UserDataSO : ScriptableObject
{
    public string imageId;
    public Sprite profile;
    public string userName;
    public string eMail;
    public string webUrl_1;
    public string webUrl_2;

    public List<Sprite> sprites;
}
