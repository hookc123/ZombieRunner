using KeyCards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace keyCards
{
    public class lockedDoorController : MonoBehaviour
    {
        private Animator doorAnim;
        private bool doorOpen = false;

        [Header("Animations")]
        [SerializeField] public AnimationClip openAnim ;
        [SerializeField] public AnimationClip closeAnim ;


        [SerializeField] private KeycardInventory _keyCards = null;
        [SerializeField] private int waitTime = 1;
        [SerializeField] private bool pauseInteraction = false;
        [SerializeField] private GameObject keycard;

        void Awake()
        {
            doorAnim = GetComponent<Animator>();
            keycard = GameObject.FindGameObjectWithTag("keycardInv");
            _keyCards = keycard.GetComponent<KeycardInventory>();
        }
        IEnumerator PauseInteract()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTime);
            pauseInteraction = false;
        }
        public void PlayAnim()
        {
                if(!doorOpen && !pauseInteraction)
                {
                    AudioManager.instance.openDoor();
                    doorAnim.Play(openAnim.name, 0, 0.0f);
                    doorOpen = true;
                    StartCoroutine(PauseInteract());
                }
                else if(doorOpen && !pauseInteraction)
                {
                    AudioManager.instance.closeDoor();
                    doorAnim.Play(closeAnim.name, 0, 0.0f);
                    doorOpen = false;
                    StartCoroutine(PauseInteract());
                }
        }

    }
}

