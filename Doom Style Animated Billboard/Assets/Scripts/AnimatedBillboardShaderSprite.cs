using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedBillboardShaderSprite : MonoBehaviour {

    public int directions = 8;
    public Camera MainCamera;
    public bool MirrorLeft = true;
    SpriteRenderer m_SpriteRenderer;
    Animator m_Anim;
    float minMirrorAngle = 0;
    float maxMirrorAngle = 0;
	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {
		
        Vector3 viewDirection = -new Vector3(MainCamera.transform.forward.x, 0, MainCamera.transform.forward.z);
        var viewAngle = Vector3.Angle(transform.forward, viewDirection);
        var cross = Vector3.Cross(transform.forward, viewDirection);
        if(cross.y < 0)
        {
            viewAngle = 360 - viewAngle; 
        }


        m_Anim.SetFloat("ViewAngle", viewAngle);
        if (MirrorLeft)
        {
            m_SpriteRenderer.flipX = (viewAngle >= minMirrorAngle  && viewAngle <= maxMirrorAngle  ) ;
        }

	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
}
