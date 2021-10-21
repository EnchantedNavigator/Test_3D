using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Screen_Animation : MonoBehaviour
{
    [SerializeField] bool goToBottom=true;
    [SerializeField] GameObject _myPannel;
    Tween fromBottom;

    private void OnEnable()
    {
        if (goToBottom == true)
        {
            transform.Translate(0, -1000, 0);
            FromBottomToCentrer();
        }
    }
    // Start is called before the first frame update
    public void PlaceToBottom()
    {

    }
     public void FromBottomToCentrer() 
    {
        fromBottom = transform.DOLocalMoveY(0, (float)0.8).SetEase(Ease.InSine);
        fromBottom = _myPannel.transform.DOPunchPosition(new Vector3(0, -100, 0), (float)0.5, 2, 1).SetDelay((float)0.8);
        
        
    }

}
