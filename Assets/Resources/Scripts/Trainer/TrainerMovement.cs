using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace NRKernal.NRExamples
{
    public class TrainerMovement : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        Animator anim;
        public TMP_Text _title;
        string handChoice;
        HandState handState;
        HandGesture handGesture;

        void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            anim.enabled = false;
            handChoice = PlayerPrefs.GetString("handChoice");
        }

        void Update()
        {
            if (handChoice == "rightHand")
                handState = NRInput.Hands.GetHandState(HandEnum.RightHand);
            else
                handState = NRInput.Hands.GetHandState(HandEnum.LeftHand);

            handGesture = handState.currentGesture;
        }

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
        }

        public void OnPointerEnter(PointerEventData eventData) { Debug.Log("enter inside cube"); }

        public void OnPointerExit(PointerEventData eventData) {}
    }
}
