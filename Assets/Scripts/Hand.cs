using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] private List<Card> _cards = new List<Card>();
    [SerializeField] private Card _template;
    [SerializeField] private Button _button;

    private int _cardIndex;
    private int _capacity => Random.Range(4, 7);

    private void Start()
    {     
        for (int i = 0; i < _capacity; i++)
        {
            Card spawned = Instantiate(_template, transform); ;
            _cards.Add(spawned);
            spawned.EndChangignParameter += OnEndChangignParameter;
        }

        _button.onClick.AddListener(ChangeParametersCard);
    }

    private void ChangeParametersCard()
    {
        int minValue = -2;
        int maxValue = 8;
        StopCoroutine(ChangeParametersCar(minValue, maxValue));
        StartCoroutine(ChangeParametersCar(minValue, maxValue));
    }

    private IEnumerator ChangeParametersCar(int minValue, int maxValue)
    {
        while (true)
        {
            if (_cards[_cardIndex].gameObject.activeSelf == true)
                _cards[_cardIndex].ChangeRandomParameter(minValue, maxValue);
            else
                ChangeIndex();

            yield return new WaitForSeconds(0.09f);
        }
    }

    private void OnEndChangignParameter()
    {
        ChangeIndex();
    }

    private void ChangeIndex()
    {
        if (_cardIndex == _cards.Count - 1)
        {
            _cardIndex = 0;
            int parameterIndex = Random.Range(0, 2);

            foreach (var card in _cards)
                card.SetRandomParameterIndex(parameterIndex);
        }

        else
            _cardIndex++;
    }
}
