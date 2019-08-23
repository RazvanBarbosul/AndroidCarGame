using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIElementDragger : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "UIDraggable";
    public const string DROP_POSTION = "UIDropPosition";
    private bool Dragging = false;
    private Vector2 OriginalPosition;
    private Transform ObjectToDrag;
    private Image ObjectToDragImage;
    List<RaycastResult> HitObjects = new List<RaycastResult>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ObjectToDrag = GetDraggableTransform();

            if (ObjectToDrag)
            {
                Dragging = true;

                ObjectToDrag.SetAsLastSibling();

                OriginalPosition = ObjectToDrag.GetComponent<CarButton>().OriginalPosition; ;
                ObjectToDragImage = ObjectToDrag.GetComponent<Image>();
                ObjectToDragImage.raycastTarget = false;


            }
        }

        if (Dragging)
        {
            ObjectToDrag.position = Input.GetTouch(0).position;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (ObjectToDrag != null)
            {
                TryToPlaceCar();
                //Transform ObjToReplace = GetDraggableTransform();

                //if(ObjToReplace != null)
                //{
                //    ObjectToDrag.position = ObjToReplace.position;
                //    ObjToReplace.position = OriginalPosition;
                //}
                //else
                //{
                //    ObjectToDrag.position = OriginalPosition;
                //}
                //ObjectToDragImage.raycastTarget = true;
                //ObjectToDrag = null;
            }
        }

    }


    private GameObject GetObjectToDrag()
    {
        var Pointer = new PointerEventData(EventSystem.current);

        Pointer.position = Input.GetTouch(0).position;

        EventSystem.current.RaycastAll(Pointer, HitObjects);

        if(HitObjects.Count <= 0)
        {
            return null;
        }
        else
        {
            for(int i = 0; i < HitObjects.Count; i++)
            {
                if(HitObjects[i].gameObject.tag == DRAGGABLE_TAG || HitObjects[i].gameObject.tag == DROP_POSTION)
                {
                    return HitObjects[i].gameObject;
                }
            }
            return null;
            //return HitObjects[0].gameObject;
        }

       }

    private Transform GetDraggableTransform()
    {
        GameObject ClickedObject = GetObjectToDrag();

        if(ClickedObject != null && ClickedObject.tag == DRAGGABLE_TAG)
        {
            return ClickedObject.transform;
        }
        else
        {
            return null;
        }
    }

    private void TryToPlaceCar()
    {
        GameObject DropPosition = GetObjectToDrag();

        if (DropPosition)
        {
            if (DropPosition.tag == DROP_POSTION)
            {
                ObjectToDrag.position = DropPosition.gameObject.transform.position;
            }
            else if (DropPosition.tag == DRAGGABLE_TAG)
            {
                ObjectToDrag.position = DropPosition.gameObject.transform.position;
                DropPosition.gameObject.transform.position = DropPosition.GetComponent<CarButton>().OriginalPosition;
            }
            else
            {
                ObjectToDrag.position = OriginalPosition;
            }
            ObjectToDragImage.raycastTarget = true;
            ObjectToDrag = null;
            Dragging = false;
        }
        else
        {
            ObjectToDrag.position = OriginalPosition;
            ObjectToDragImage.raycastTarget = true;
            ObjectToDrag = null;
            Dragging = false;
        }
        
    }

}
