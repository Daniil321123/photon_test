using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections;


public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [Tooltip("The Beams GameObject to control")]
    [SerializeField] private GameObject beams;
    bool isFiring;

    [Tooltip("The current Health of our player")]
    public float Health = 1f;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isFiring);
            stream.SendNext(Health);
        }
        else
        {
            this.isFiring = (bool)stream.ReceiveNext();
            this.Health = (float)stream.ReceiveNext();
        }
    }


    private void Awake()
    {
        if (beams == null)
        {
            Debug.Log("Beam nor exist");
        }
        else
        {
            beams.SetActive(false);
        }
    }

    private void Start()
    {
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (photonView.IsMine)
            {
                ProccessInputs();
            }
            if (Health <= 0)
            {
                GameManager.Instance.LeaveRoom();
            }
        }
       

        if(beams != null && isFiring != beams.activeInHierarchy)
        {
            beams.SetActive(isFiring);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (!other.name.Contains("Beam"))
        {
            return;
        }
        Health -= 0.1f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!other.name.Contains("Beam"))
        {
            return;
        }
        Health -= 0.1f * Time.deltaTime;
    }

    private void ProccessInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            if (!isFiring)
            {
                isFiring = true;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (isFiring)
            {
                isFiring = false;
            }
        }
    }
}
