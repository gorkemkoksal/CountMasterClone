using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _default, _pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = _pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = _default;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
