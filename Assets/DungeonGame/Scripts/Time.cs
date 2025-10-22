using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace TimeSystem
{
    [AddComponentMenu("Time/Game Timer")]
    public class GameTimer : MonoBehaviour
    {
        [Header("Configurações")]
        [Tooltip("Começa automaticamente ao iniciar a cena")]
        public bool startOnAwake = true;

        [Tooltip("Usar tempo real (ignora Time.timeScale)")]
        public bool runUnscaledTime = false;

        [Header("UI (opcional)")]
        public TextMeshProUGUI displayText;

        [Header("Eventos")]
        public UnityEvent onTimerStarted;
        public UnityEvent onTimerStopped;

        private float elapsedTime = 0f;
        private bool isRunning = false;

        void Start()
        {
            if (startOnAwake)
                StartTimer();
            UpdateDisplay();
        }

        void Update()
        {
            if (!isRunning) return;

            float delta = runUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            elapsedTime += delta;
            UpdateDisplay();
        }

        // === Métodos públicos ===
        public void StartTimer()
        {
            elapsedTime = 0f;
            isRunning = true;
            onTimerStarted?.Invoke();
            Debug.Log("Começou");
        }

        public void StopTimer()
        {
            isRunning = false;
            onTimerStopped?.Invoke();
        }

        public void PauseTimer()
        {
            isRunning = false;
        }

        public void ResumeTimer()
        {
            isRunning = true;
        }

        public float GetElapsedSeconds() => elapsedTime;

        private void UpdateDisplay()
        {
            if (displayText != null)
                displayText.text = FormatTime(elapsedTime);
        }

        private string FormatTime(float t)
        {
            int totalSeconds = Mathf.FloorToInt(t);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes:00}:{seconds:00}";
        }
    }
}
