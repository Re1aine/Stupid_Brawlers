using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class LoadingScreenEffect : MonoBehaviour
{
    public float duration = 1f; // Длительность схлопывания
    public AnimationCurve easingCurve; // Кривая Easing для плавности анимации

    private Vector3 initialScale; // Начальный масштаб круга
    private float elapsedTime = 0f; // Время, прошедшее с начала анимации
    private bool isCollapsing = false;

    private void Start()
    {
        // Сохраняем изначальный масштаб
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (isCollapsing)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // Нормализованное время (0 - 1)

            // Применяем easing-кривую для вычисления текущего масштаба
            float easedT = easingCurve.Evaluate(t);
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, easedT);

            // Остановка анимации, если она завершена
            if (t >= 1f)
            {
                isCollapsing = false;
                gameObject.SetActive(false);
                
            }
        }
    }
    
    public void StartCollapse()
    {
        // Запуск схлопывания
        elapsedTime = 0f;
        isCollapsing = true;
    }
}