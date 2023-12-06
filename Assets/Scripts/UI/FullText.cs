using System.Collections;
using UnityEngine;

public class FullText : MonoBehaviour
{
    
    private void Start()
    {
        StartCoroutine(AutoDisable());
    }


    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
