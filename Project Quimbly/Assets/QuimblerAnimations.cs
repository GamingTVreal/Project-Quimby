using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuimblerAnimations : MonoBehaviour
{
    [SerializeField] Animator AccountAnimator, HomeAnimator;
    // Start is called before the first frame update
    void Start()
    {
        AccountAnimator.SetBool("CloseMenu", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenAccount()
    {
        AccountAnimator.SetBool("CloseMenu", false);
    }
    public void CloseAccount()
    {
        AccountAnimator.SetBool("CloseMenu", true);
    }
    public void GetButton(int AnimID)
    {
        HomeAnimator.ResetTrigger("Which Animation");
        HomeAnimator.SetInteger("Which Animation", AnimID);
        
    }
}
