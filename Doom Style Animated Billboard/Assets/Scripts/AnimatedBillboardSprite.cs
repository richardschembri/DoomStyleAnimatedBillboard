using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedBillboardSprite : MonoBehaviour {

    public int directions = 8;
    public bool MirrorLeft = true;
    public Camera MainCamera;
    Animator m_Anim;
    SpriteRenderer m_SpriteRenderer;
    float minMirrorAngle = 0;
    float maxMirrorAngle = 0;

	void Start () {
        m_Anim = this.GetComponent<Animator>();
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
        if (directions <= 0)
        {
            directions = 1;
        } 
        minMirrorAngle = (360 / directions) / 2;
        maxMirrorAngle = 180 - minMirrorAngle; 
	}

    private void Awake()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    void Update () {
        Vector3 viewDirection = -new Vector3(MainCamera.transform.forward.x, 0, MainCamera.transform.forward.z);
        transform.LookAt(transform.position + viewDirection);
        m_Anim.SetFloat("ViewAngle", transform.localEulerAngles.y);
        if (MirrorLeft)
        {
            m_SpriteRenderer.flipX = !(transform.localEulerAngles.y >= minMirrorAngle && transform.localEulerAngles.y <= maxMirrorAngle) ;
        }
    }

    /// <summary>
    /// Rotate billboards to face editor camera while game not running.
    /// </summary>
    public void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            SceneView sceneView = GetActiveSceneView();
            if (sceneView)
            {
                // Editor camera stands in for player camera in edit mode
                Vector3 viewDirection = -new Vector3(sceneView.camera.transform.forward.x, 0, sceneView.camera.transform.forward.z);
                transform.LookAt(transform.position + viewDirection);
            }
        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay( transform.parent.position, transform.parent.forward * 2);

    }

    private SceneView GetActiveSceneView()
    {
        // Return the focused window if it is a SceneView
        if (EditorWindow.focusedWindow != null && EditorWindow.focusedWindow.GetType() == typeof(SceneView))
            return (SceneView)EditorWindow.focusedWindow;

        return null;
    }
}
