using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;

public class CoinCountAnimator : MonoBehaviour
{
    public static CoinCountAnimator instance;

    [SerializeField] private RectTransform coinIcon;
    [SerializeField] private RectTransform diamondIcon;

    [SerializeField] private RectTransform skullStart;
    [SerializeField] private RectTransform skullEnd;

    [SerializeField] private RectTransform animatedCoinIcon;
    [SerializeField] private RectTransform animatedDiamondIcon;
    [SerializeField] private RectTransform animatedSkullIcon;


    [SerializeField] private RectTransform transformHolder;
    [SerializeField] private float duration;


    private void Awake()
    {
        int i = 500;
        if (instance != null)
        {
            Debug.LogWarning("There is more than one CoinCount on scene!!!");
            Destroy(this);
        }
        instance = this;

        while(gameObject.transform.childCount > 0)
        {
            i--;
            Destroy(gameObject.transform.GetChild(0));
            if (i < 0) break;
        }
    }

    public async void AnimateCoinCollect(Vector3 basePos)
    {
        //await Task.Delay(150);
        await Task.Yield();
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(basePos);
        RectTransform newIcon = Instantiate(animatedCoinIcon, screenPoint, Quaternion.identity, transformHolder);
        newIcon.DOMove(coinIcon.position, duration - 0.01f, true).SetEase(Ease.InSine);
        Destroy(newIcon.gameObject, duration);
    }

    public async void AnimateDiamondCollect(Vector3 basePos)
    {
        float duration2 = 1f;
       // await Task.Delay(300);
        await Task.Yield();
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(basePos);
        RectTransform newIcon = Instantiate(animatedDiamondIcon, screenPoint, Quaternion.identity, transformHolder);
        newIcon.DOMove(diamondIcon.position, duration2 - 0.01f, true).SetEase(Ease.InSine);
        Destroy(newIcon.gameObject, duration2);
    }

    public async void AnimateSkull()
    {
      //  await Task.Delay(150);
        await Task.Yield();
        RectTransform newIcon = Instantiate(animatedSkullIcon, skullStart.position, Quaternion.identity, transformHolder);
        newIcon.DOMove(skullEnd.position, 1f - 0.01f, true).SetEase(Ease.InCubic);        

        FadeColor(newIcon);
        Destroy(newIcon.gameObject, 1f);
    }

    private async void FadeColor(RectTransform rextImage)
    {
        if (rextImage == null) return;

        Image image;

        image = rextImage.GetComponent<Image>();

        float alpa = 0.01f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - alpa);
        //alpa+=.01f;
        // await Task.Delay(10);
        await Task.Yield();
        if (image.color.a > 0)
        FadeColor(rextImage);

    }

}

