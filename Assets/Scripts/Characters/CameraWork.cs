using UnityEngine;

public class CameraWork : MonoBehaviour
{
    [Tooltip("The distance in the local x-z plane to the target")]
    [SerializeField] private float distance = 7.0f;

    [Tooltip("The height we want the camera to be above the target")]
    [SerializeField] private float heigh = 3.0f;

    [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
    [SerializeField] Vector3 centerOffset = Vector3.zero;

    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
    [SerializeField] private bool followOnStart = false;

    [Tooltip("The Smoothing for the camera to follow the target")]
    [SerializeField] private float smoothSpeed = 0.125f;

    Transform cameraTransform;
    bool isFollowing;
    Vector3 cameraOffset = Vector3.zero;

    private void Start()
    {
        if (followOnStart)
        {
            OnStartFollowing();
        }
        
    }

    private void LateUpdate()
    {
        if (cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }

        if (isFollowing)
        {
            Follow();
        }
    }

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;

        Cut();
    }

    private void Follow()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = heigh;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset), smoothSpeed * Time.deltaTime);
        cameraTransform.LookAt(this.transform.position + centerOffset);
    }

    private void Cut()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = heigh;

        cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);
        cameraTransform.LookAt(this.transform.position + centerOffset);
    }
}
