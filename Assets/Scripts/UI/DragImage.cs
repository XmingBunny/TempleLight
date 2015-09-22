using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/DragImage", 40)]
    class DragImage : Image, IDragHandler, IDropHandler
    {
        Transform Canvas;
        Transform oldParent;
        void Start()
        {
            Canvas = GameObject.Find("Canvas").transform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
            if (oldParent == null)
                oldParent = transform.parent;
            transform.parent = Canvas;
        }

        public void OnDrop(PointerEventData eventData)
        {
            print(eventData.ToString());

            RaycastHit raycasthit = new RaycastHit();
            if (Physics.Raycast(transform.position, Vector3.forward, out raycasthit))
            {
                GameObject colliderGameObject = raycasthit.collider.gameObject;
                if (colliderGameObject.name == "Cell")
                {
                    transform.parent = colliderGameObject.transform;
                }
            }
            else
            {
                transform.parent = oldParent;
            }

            transform.localPosition = new Vector3(0f, -0.1f, 0f);
            oldParent = null;
        }

        void Update()
        {
            RaycastHit raycasthit = new RaycastHit();
            if (Physics.Raycast(transform.position, Vector3.forward, out raycasthit))
            {
                GameObject colliderGameObject = raycasthit.collider.gameObject;
                if (colliderGameObject.name == "Cell")
                {
                    transform.parent = colliderGameObject.transform;
                }
            }
        }
    }
}
