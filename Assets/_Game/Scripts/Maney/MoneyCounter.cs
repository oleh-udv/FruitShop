using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
    public class MoneyCounter : MonoBehaviour
    {
        [SerializeField] private GameSettings _gameSettings;

        [Space]
        [SerializeField] private RectTransform _moneyPanel;
        [SerializeField] private Text _moneyText;

        private Tween _moneyTween;

        private int _moneyCount;

        public static MoneyCounter Instance { get; private set; }

        #region UnityMethods
        private void Awake()
        {
            if (Instance)
                Destroy(Instance);

            Instance = this;

            UpdateMoney(0);
        }
        #endregion

        #region PublicMethods
        public void UpdateMoney(int addValue)
        {
            _moneyTween.Kill(true);

            _moneyTween = DOVirtual.Float(_moneyCount, _moneyCount + addValue, _gameSettings.CounterValueChangeTime, (f) =>
            { 
                _moneyText.text = ((int)f).ToString();
                _moneyTween = _moneyPanel.DOScale(Vector3.one * _gameSettings.CounterImpulse, _gameSettings.PunchCounterTime / 2f).OnComplete(() =>
                {
                    _moneyTween = _moneyPanel.DOScale(Vector3.one, _gameSettings.PunchCounterTime / 2f);
                });
            }).SetEase(Ease.Linear);

            _moneyCount += addValue;
        }
        #endregion
    }
}
