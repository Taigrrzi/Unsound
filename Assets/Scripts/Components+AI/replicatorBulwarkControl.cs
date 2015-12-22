using UnityEngine;
using System.Collections;

public class replicatorBulwarkControl : MonoBehaviour
{

    public enum HullPart
    {
        NE, NW, SE, SW
    }

    public int currentState;
    public int desiredState;
    public float foldRotSpeed;
    public float foldMoveSpeed;
    public HullPart hullpart;
    public Vector3 basePos;

    public ReplicatorMotherShipControl shipControl;

    // Use this for initialization
    void Start()
    {
        basePos = transform.localPosition;
        for (int i = 0; i<transform.parent.childCount;i++)
        {
            if (transform.parent.GetChild(i).GetComponent<Collider2D>()!=null)
            {
                Physics2D.IgnoreCollision(transform.parent.GetChild(i).GetComponent<Collider2D>(),GetComponent<Collider2D>(),true); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (desiredState!=currentState)
        {
            if ((transform.localPosition - StatePosition(desiredState)).magnitude > foldMoveSpeed * Time.deltaTime)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, StatePosition(desiredState), foldMoveSpeed * Time.deltaTime);
            }
            else
            {
                transform.localPosition = StatePosition(desiredState);
                if (transform.localRotation == Quaternion.Euler(new Vector3(0, 0, StateRotation(desiredState))))
                {
                    currentState = desiredState;
                }
            }

            if (Mathf.Abs(transform.localRotation.eulerAngles.z - StateRotation(desiredState)) > foldRotSpeed*Time.deltaTime)
            {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation,Quaternion.Euler(0,0,StateRotation(desiredState)),foldRotSpeed);
            } else
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, StateRotation(desiredState)));
                if (transform.localPosition == StatePosition(desiredState))
                {
                    currentState = desiredState;
                }
            }
        }     
    }

    public float toNinety(float foldRot)
    {
        if (Mathf.Abs(foldRot)==270)
        {
            return Mathf.Sign(foldRot)*90;
        } else
        {
            return foldRot;
        }
    }

    public float StateRotation(int dState)
    {
        if (dState == 1)
        {
            if (hullpart == HullPart.NE || hullpart == HullPart.SW)
            {
                return 90;
            } else
            {
                return 270;
            }
        } else if (dState == -1)
        {
            if (hullpart == HullPart.NE || hullpart == HullPart.SW)
            {
                return 270;
            } else
            {
                return 90;
            }
        } else
        {
            return 0;
        }
    }

    public Vector3 StatePosition(int dState)
    {
        return basePos;
    }
}
