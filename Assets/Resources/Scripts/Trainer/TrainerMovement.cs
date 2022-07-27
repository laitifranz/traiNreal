using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace NRKernal.NRExamples
{
    /// <summary> A cube interactive test. </summary>
    public class TrainerMovement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary> The mesh render. </summary>
        //private MeshRenderer m_MeshRender;
        Animator anim;
        public TMP_Text _title;
        string handChoice;
        HandState handState;
        HandGesture handGesture;
        /// <summary> Awakes this object. </summary>
        void Awake()
        {
            //m_MeshRender = transform.GetComponent<MeshRenderer>();
            //anim = GameObject.trans GetComponent<Animator>();
            //anim = gameObject.GetComponentInParent<Animator>();
        }

        void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            anim.enabled = false;
            handChoice = PlayerPrefs.GetString("handChoice");

            //Debug.Log(handChoice);
        }
        /// <summary> Updates this object. </summary>
        void Update()
        {
            if (handChoice == "rightHand")
                handState = NRInput.Hands.GetHandState(HandEnum.RightHand);
            else
                handState = NRInput.Hands.GetHandState(HandEnum.LeftHand);

            handGesture = handState.currentGesture;

            //bool isThumbsUp = anim.GetBool("isThumbsUp");

            //if ((handGesture == HandGesture.Victory) && (!isThumbsUp))
            //{
            //    _title.text = "Good job!";
            //    anim.SetBool("isThumbsUp", true);
            //}
            //else
            //{
            //    anim.SetBool("isThumbsUp", false);
            //}

            //get controller rotation, and set the value to the cube transform
            //transform.rotation = NRInput.GetRotation();
        }

        /// <summary> when pointer click, set the cube color to random color. </summary>
        /// <param name="eventData"> Current event data.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (anim.enabled)
            {
                _title.text = "Stopped";
                anim.enabled = false;
            }
            else
            {
                _title.text = "Showing";
                anim.enabled = true;
            }
            //m_MeshRender.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        /// <summary> when pointer hover, set the cube color to green. </summary>
        /// <param name="eventData"> Current event data.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            //m_MeshRender.material.color = Color.green;
            //anim = GetComponent<Animator>();
            //if (anim.enabled)
            //{
            //    _title.text = "Stop trainer";
            //    anim.enabled = false;
            //}

            Debug.Log("enter inside cube");
        }

        /// <summary> when pointer exit hover, set the cube color to white. </summary>
        /// <param name="eventData"> Current event data.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
        //    if (!anim.enabled)
        //    {
        //        _title.text = "Start trainer";
        //        anim.enabled = true;
        //    }
            
            //anim = GetComponent<Animator>();
            //m_MeshRender.material.color = Color.white;
        }
    }
}
