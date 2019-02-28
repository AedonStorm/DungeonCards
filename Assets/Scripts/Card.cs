using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {

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

    private void OnEnable()
    {
        StartCoroutine(inMoveCoroutine());
    }
    private IEnumerator inMoveCoroutine()
    {
        float finalScale = 0.7f;
        for (float t = 0.1f; t < finalScale; t += Time.deltaTime / 0.2f)
        {
            transform.localScale = new Vector3(t, t, t);
            yield return null;
        }
        transform.localScale = new Vector3(finalScale, finalScale, finalScale);
    }

    public abstract void activateAction();
}
