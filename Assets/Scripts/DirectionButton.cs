using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionButton : MonoBehaviour {
    [SerializeField] public int movePosition;

    private void OnEnable()
    {
        StartCoroutine(scaleUp());
    }
    private IEnumerator scaleUp()
    {
        for (float t = 0.1f; t < 1f; t += Time.deltaTime / 0.05f)
        {
            transform.localScale = new Vector3(t, t, t);
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void disable()
    {
        if (isActiveAndEnabled)
            StartCoroutine(scaleDown());
    }
    private IEnumerator scaleDown()
    {
        for (float t = 1; t > 0; t -= Time.deltaTime / 0.05f)
        {
            transform.localScale = new Vector3(t, t, t);
            yield return null;
        }
        transform.localScale = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
    }


}
