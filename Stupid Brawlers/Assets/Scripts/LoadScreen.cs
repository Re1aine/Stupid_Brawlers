using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    private static readonly int Out = Animator.StringToHash("FadeOut");
    
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void Show() => gameObject.SetActive(true);

    public void Hide()
    {
        FadeOut();
        gameObject.SetActive(false);
    }

    private void FadeOut() => _animator.SetTrigger(Out);
}