using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    private string dragTag = "draggable";
    public LayerMask dragLayer;
    private bool dragging = false;
    private bool touched = false;
    private Transform dragTransform;
    private Rigidbody dragRigidbody;
    private Vector3 prevPos, prevPosScr;
    private float posX, posY;
    void Update()
    {
        if(Input.touchCount != 1)
        {
            dragging = false;
            touched = false;
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector3 touchPos = touch.position;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touchPos);
                if(Physics.Raycast(ray, out hit, dragLayer) && hit.collider.tag == dragTag)
                {
                    dragTransform = hit.transform;
                    dragRigidbody = dragTransform.GetComponent<Rigidbody>();
                    prevPos = dragTransform.position;
                    prevPosScr = Camera.main.WorldToScreenPoint(prevPos);
                    posX = touchPos.x - prevPosScr.x;
                    posY = touchPos.y - prevPosScr.y;

                    SetDraggingProperties(dragRigidbody);

                    touched = true;
                }
                break;
            case TouchPhase.Moved:
                if (touched)
                {
                    if (dragRigidbody == null) return;
                    dragging = true;
                    float newPosX = touchPos.x - posX;
                    float newPosY = touchPos.y - posY;
                    Vector3 currPos = new Vector3(newPosX, newPosY, prevPosScr.z);
                    Vector3 wrldPos = Camera.main.ScreenToWorldPoint(currPos) - prevPos;
                    wrldPos = new Vector3(wrldPos.x, 0f, wrldPos.z);
                    dragRigidbody.velocity = wrldPos / (Time.deltaTime);
                    prevPos = dragTransform.position;
                }
                break;
            case TouchPhase.Ended:
                if (dragging)
                {
                    dragging = false;
                    touched = false;
                    prevPos = new Vector3(0f, 0f, 0f);
                    SetFreeProperties(dragRigidbody);
                }
                break;
            case TouchPhase.Canceled:
                if (dragging)
                {
                    dragging = false;
                    touched = false;
                    prevPos = new Vector3(0f, 0f, 0f);
                    SetFreeProperties(dragRigidbody);
                }
                break;
        }

        if (touched)
        {
            dragTransform.position = Vector3.Lerp(dragTransform.position, new Vector3(dragTransform.position.x, 1f, dragTransform.position.z), 0.1f);
        }
        else
        {
            dragTransform.position = Vector3.Lerp(dragTransform.position, new Vector3(dragTransform.position.x, 0f, dragTransform.position.z), 0.1f);
        }
        
    }

    private void SetDraggingProperties(Rigidbody rb)
    {
        if (rb == null) return;
        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.drag = 20;
    }

    private void SetFreeProperties(Rigidbody rb)
    {
        if (rb == null) return;
        rb.useGravity = true;
        rb.freezeRotation = false;
        rb.drag = 5;
    }
}
