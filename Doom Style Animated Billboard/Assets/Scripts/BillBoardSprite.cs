using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BillBoardSprite : MonoBehaviour {

    public bool FaceY = false;
    public bool MirrorLeft = true;
    public Camera MainCamera;
    public TextMesh debugAngle;
    Animator m_Anim;
    SpriteRenderer m_SpriteRenderer;
	// Use this for initialization
	void Start () {
        m_Anim = this.GetComponent<Animator>();
        m_SpriteRenderer = this.GetComponent<SpriteRenderer>();
	}

    private void Awake()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update () {
        //var target = Camera.main.transform.position;
        //target.y = transform.position.y;
        //transform.LookAt(target, Camera.main.transform.up);



        float y = (FaceY) ? MainCamera.transform.forward.y : 0;
        Vector3 viewDirection = -new Vector3(MainCamera.transform.forward.x, y, MainCamera.transform.forward.z);
        transform.LookAt(transform.position + viewDirection);
        m_Anim.SetFloat("ViewAngle", transform.localEulerAngles.y);
        if(debugAngle != null)
        {
            debugAngle.text = transform.localEulerAngles.y.ToString();
        }
        if (MirrorLeft)
        {
            m_SpriteRenderer.flipX = !(transform.localEulerAngles.y >= 25 && transform.localEulerAngles.y <= 157) ;
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Rotate billboards to face editor camera while game not running.
    /// </summary>
    public void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            UnityEditor.SceneView sceneView = GetActiveSceneView();
            if (sceneView)
            {
                // Editor camera stands in for player camera in edit mode
                float y = (FaceY) ? MainCamera.transform.forward.y : 0;
                Vector3 viewDirection = -new Vector3(sceneView.camera.transform.forward.x, y, sceneView.camera.transform.forward.z);
                transform.LookAt(transform.position + viewDirection);
            }
        }
    }

    private SceneView GetActiveSceneView()
    {
        // Return the focused window if it is a SceneView
        if (EditorWindow.focusedWindow != null && EditorWindow.focusedWindow.GetType() == typeof(SceneView))
            return (SceneView)EditorWindow.focusedWindow;

        return null;
    }
#endif

}
