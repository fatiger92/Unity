using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    static readonly int ANIM_PARAM_ENDED = Animator.StringToHash("ended");
    
    public Animator anim;
    public string nextLevel;
    public float waitToEndLevel = 2f;
    public GameObject blocker;
    bool isEnding;
    public float fadeTime = 1f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnding == false)
        {
            if (other.CompareTag("Player"))
            {
                isEnding = true;
                anim.SetTrigger(ANIM_PARAM_ENDED);

                AudioManager.instance.PlayLevelCompleteMusic();
                blocker.SetActive(true);
                
                StartCoroutine(EndLevelCo());
            }
        }
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToEndLevel - fadeTime); // 2.5
        
        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds(fadeTime); // 1
        
        InfoTracker.instance.GetInfo();
        InfoTracker.instance.SaveInfo();
        
        SceneManager.LoadScene(nextLevel);
    }
}
