using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //[SerializeField, NotNull] private GameObject hand;

    private RectTransform _image;

    [SerializeField] private RectTransform startPos;
    [SerializeField] private RectTransform endPos;

    private Transform start;
    private Transform end;

    private bool _isPlaying = false;
    private bool _isPlaying2 = false;

	private void Awake()
	{
        _image = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
	public void MoveUpDown()
    {
        if (_isPlaying) return;
        gameObject.SetActive(true);
        _isPlaying = true;
        
        TweenStart();
    }

    private void TweenStart()
    {
        if( !_isPlaying) return;
        _image.DOAnchorPos(startPos.anchoredPosition, 1f).OnComplete(TweenReset);
    }
    private void TweenReset()
    {
        if (!_isPlaying) return;
        _image.DOAnchorPos(endPos.anchoredPosition, 1f).OnComplete(TweenStart);
    }

    public void StopMove()
    {
        _isPlaying = false;
        gameObject.SetActive(false);
    }

    public void MoveTowers(Transform tower1, Transform tower2)
    {
        if (_isPlaying2) return;

        gameObject.SetActive(true);
        start = tower1;
        end = tower2;


        _isPlaying2 = true;
        _image = GetComponent<RectTransform>();
        TweenStart2();
    }

    private void TweenStart2()
    {
        if (!_isPlaying2) return;

        _image.DOMove(Camera.main.WorldToScreenPoint(start.position), 1f).OnComplete(TweenReset2);
        //_image.DOAnchorPos(Camera.main.WorldToViewportPoint(start.position), 1f).OnComplete(TweenReset2);
    }
    private void TweenReset2()
    {
        if (!_isPlaying2) return;

        _image.DOMove(Camera.main.WorldToScreenPoint(end.position), 1f).OnComplete(TweenStart2);
       // _image.DOAnchorPos(Camera.main.WorldToViewportPoint(end.position), 1f).OnComplete(TweenStart2);
    }
}
