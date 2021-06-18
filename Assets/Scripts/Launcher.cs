using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [Tooltip("Maximum player in room")]
    [SerializeField]
    private byte maxPlayerPerRoom = 4;
   #region Private Serializable Fields

   #endregion
   
   #region Private Fields
   string gameVersion = "1";
   #endregion
    #region Public fields
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField] private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField] private GameObject progressLabel;
    #endregion
   #region MonoBehavior CallBacks

   private void Awake()
   {
       PhotonNetwork.AutomaticallySyncScene = true;
   }

    private void Start() 
    {
       progressLabel.SetActive(false);
       controlPanel.SetActive(true);
    }

   #endregion

   #region Public Methods
       public void Connect()
       {
           progressLabel.SetActive(true);
           controlPanel.SetActive(false);
           if(PhotonNetwork.IsConnected)
           {
               PhotonNetwork.JoinRandomRoom();
           }
           else 
           {
               PhotonNetwork.ConnectUsingSettings();
               PhotonNetwork.GameVersion = gameVersion;
           }
       }

   #endregion

   #region MonoBehaviorPunCollbacks

       public override void OnConnectedToMaster()
       {
           Debug.Log("PUN Basic Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
           PhotonNetwork.JoinRandomRoom();
       }

       public override void OnDisconnected(DisconnectCause cause) 
       {
           progressLabel.SetActive(false);
           controlPanel.SetActive(true);
           Debug.LogWarningFormat("PUN Basic Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
       }

       public override void OnJoinRandomFailed(short returnCode, string message) 
       {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = maxPlayerPerRoom});
       }

       public override void OnJoinedRoom()
       {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
       }
   #endregion
}
