using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Action { None, Chest, Power, Coins}
public class ActionImpl
{
    public Action action;
    public int power;

    public ActionImpl (Action action, int power)
    {
        this.action = action;
        this.power = power;
    }
}

public abstract class Card : MonoBehaviour {

    public TextMeshProUGUI powerText;

    private int power = 0;
    public int getPower() { return power; }
    public void initializePower(int modifier)
    {
        power = modifier;
        updateText();
    }
    public void setPower(int modifier)
    {
        power += modifier;
        updateText();
        if (power <= 0)
            Debug.Log("Die");
    }

    private void updateText()
    {
         powerText.text = power.ToString();
    }

    protected abstract void finalAction();

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

    public abstract ActionImpl activateAction();
}
