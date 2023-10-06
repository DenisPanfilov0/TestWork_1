using UnityEngine;
using UnityEngine.UI;

public class ReplaceImageSprite : MonoBehaviour
{
    [SerializeField] private Image _parentImage;
    [SerializeField] private Sprite _newSprite;

    private void Start()
    {
        if (_parentImage != null)
        {
            Image childImage = GetComponentInChildren<Image>();

            if (childImage != null)
            {
                childImage.sprite = _newSprite;
                Vector2 parentSize = _parentImage.rectTransform.sizeDelta;
                Vector2 spriteSize = _newSprite.rect.size;
                float scaleX = parentSize.x / spriteSize.x;
                float scaleY = parentSize.y / spriteSize.y;
                childImage.rectTransform.localScale = new Vector3(scaleX, scaleY, 1f);
            }
        }
    }
}