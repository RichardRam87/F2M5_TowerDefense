using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
    [SerializeField] 
    private float _dissapearDelay = 1f;
    
    [SerializeField]
    private Text _gamestateText;

    private Animation _animation;

    private readonly int _fade = Animator.StringToHash("Fade");

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }

    public void ShowGameStateUI(string text)
    {
        gameObject.SetActive(true);
        StartCoroutine(GameStateUIRoutine(text));
    }

    private IEnumerator GameStateUIRoutine(string text)
    {
        _gamestateText.text = text;
        gameObject.SetActive(true);
        _animation.Play();
        //_animation.speed = 1f / _animationSpeed;
        //_animation.SetTrigger(_fade);
        yield return new WaitForSeconds(_dissapearDelay + _animation.clip.length);

        gameObject.SetActive(false);
    }
    
}
