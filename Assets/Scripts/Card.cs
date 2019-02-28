using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    public void slowMove(Vector3 nextPosition)
    {
        StartCoroutine(slowMoveCoroutine(nextPosition));
    }
    private IEnumerator slowMoveCoroutine(Vector3 nextPosition)
    {
        for (float t = 0f; t < 1f; t += Time.deltaTime / 1f)
        {
            transform.position = Vector3.Lerp(transform.position, nextPosition, t);
            yield return null;
        }
    }

    public void outMove() {
        StartCoroutine(outMoveCoroutine());
    }
    private IEnumerator outMoveCoroutine()
    {
        for (float t = transform.localScale.x; t > 0; t -= Time.deltaTime / 0.5f)
        {
            transform.localScale = new Vector3(t, t, t);
            yield return null;
        }
    }
}
