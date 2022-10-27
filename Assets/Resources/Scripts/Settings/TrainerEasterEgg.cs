using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NRKernal.NRExamples
{
    public class TrainerEasterEgg : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        public Animator anim;

        public void OnPointerClick(PointerEventData eventData)
        {
            anim.Play("Breakdance Freeze Var 2");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            anim.Play("Standing Idle To Fight Idle");
        }
    }
}
