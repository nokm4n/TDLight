using DG.Tweening;
using UnityEngine;

public class MovingShard : MonoBehaviour
{
    private float time = .8f;

    private void Start()
    {
        time = Random.Range(.2f, .7f);
        MoveUp();
    }

    void MoveUp()
    {
        transform.DOMoveY(transform.position.y + .2f, time).OnComplete(MoveDown);
    }

    void MoveDown()
    {
        transform.DOMoveY(transform.position.y - .2f, time).OnComplete(MoveUp);

    }
}
