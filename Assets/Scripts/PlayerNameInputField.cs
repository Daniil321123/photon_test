using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Private const
    const string PLAYERNAMEPREFKEY = "PlayerName";
    #endregion

    #region MonoBehavior
    private void Start()
    {
        string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();
        if(_inputField!=null)
        {
            if(PlayerPrefs.HasKey(PLAYERNAMEPREFKEY))
            {
                defaultName = PlayerPrefs.GetString(PLAYERNAMEPREFKEY);
                _inputField.text = defaultName;
            }
            PhotonNetwork.NickName = defaultName;
        }
    }
    #endregion

    #region Public Method
    public void SetPlayerName(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(PLAYERNAMEPREFKEY, value);
    }
        
    #endregion
}
