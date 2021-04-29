using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _healthComponent = null;
    [SerializeField] private RectTransform _foreground = null;
    [SerializeField] private Canvas _rootCanvas = null;

    // maybe hook this up to a unity event
    private void Update()
    {
        if (_healthComponent.GetNormalizedHealth() <= 0)
        {
            _rootCanvas.enabled = false;
            return;
        }
        
        _foreground.localScale = new Vector3(_healthComponent.GetNormalizedHealth(), 1, 1);
    }
}
