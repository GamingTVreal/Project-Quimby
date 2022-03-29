using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectQuimbly.UI;
public class BellySlap : MonoBehaviour
{
public AudioClip[] BellySlaps; 
  public AudioSource source;
  public GameObject ScriptController;

[SerializeField] LayerMask grabbableLayers;
public void Update(){
    //CheckBellySlap();
}
void Start(){
    ScriptController = GameObject.FindWithTag("GameController");
    source = ScriptController.GetComponent<AudioSource>();
}
 private void CheckBellySlap()
        { 
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, grabbableLayers);
            Debug.Log(hit.collider);
           if (hit.collider != null && hit.collider.name == "GirlSprite"){
               Debug.Log("HitDaBelly");
               source.PlayOneShot(BellySlaps[Random.Range(0,5)]);
           }
        }



}