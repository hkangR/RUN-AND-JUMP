using UnityEngine;
using UnityEngine.UI;
public class Heart : MonoBehaviour {
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private Image heartImage;

    void Awake() {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartState(int state) {
        switch (state) {
            case 2:
                heartImage.sprite = fullHeart;
                break;
            case 1:
                heartImage.sprite = halfHeart;
                break;
            case 0:
                heartImage.sprite = emptyHeart;
                break;
        }
    }
}
