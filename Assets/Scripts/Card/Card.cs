using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _icon;

    private CardParameters _parameters;
    private Animation _animation;
    public event UnityAction EndChangignParameter;

    private void Start()
    {
        _parameters = GetComponent<CardParameters>();
        _animation = GetComponent<Animation>();
        initialization();
    }

    private void initialization()
    {
        _nameText.text = _parameters.Name;
        _descriptionText.text = _parameters.Description;
        _parameters.ChangingDamage += OnChangingDamage;
        _parameters.ChangingHealth += OnChangingHealth;
        _parameters.EndChangigng += OnEndChanging;
    }

    private void OnChangingDamage(int value)
    {
        _damageText.text = value.ToString();
    }

    private void OnChangingHealth(int value)
    {
        _healthText.text = value.ToString();

        if (value <= 0)
            Died();
    }

    private void OnEndChanging()
    {
        EndChangignParameter?.Invoke();
    }

    private void Died()
    {
        _animation.Play();       
    }

    private void Disabled()
    {
        gameObject.SetActive(false);
    }

    public void ChangeRandomParameter(int minValue, int maxValue)
    {
        _parameters.ChangeRandomParameter(minValue, maxValue);
    }

    public void SetRandomParameterIndex(int index)
    {
        _parameters.SetRandomIndex(index);
    }
}
