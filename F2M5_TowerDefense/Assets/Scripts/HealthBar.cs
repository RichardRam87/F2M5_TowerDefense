using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _healtComponent = null;
    [SerializeField] private RectTransform _foreground = null;
    [SerializeField] private Canvas _rootCanvas = null;

    private void Update()
    {
        if (_healtComponent.GetNormalizedHealth() <= 0)
        {
            _rootCanvas.enabled = false;
            return;
        }
        
        _foreground.localScale = new Vector3(_healtComponent.GetNormalizedHealth(), 1, 1);
    }
}
