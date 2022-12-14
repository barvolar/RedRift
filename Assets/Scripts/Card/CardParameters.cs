using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CardParameters : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private int _manaCost;
    [SerializeField] private string _name;
    [SerializeField] private string _description;

    private WaitForSeconds _waitForSeconds;
    private int _index;

    public int Damage => _damage;
    public int Health => _health;
    public int ManaCost => _manaCost;
    public string Name => _name;
    public string Description => _description;

    public event UnityAction<int> ChangingDamage;
    public event UnityAction<int> ChangingHealth;
    public event UnityAction EndChangigng;
    public event UnityAction Died;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(0.7f);
        _index = Random.Range(0, 2);
        ChangingDamage?.Invoke(_damage);
        ChangingHealth?.Invoke(_health);
    }

    private IEnumerator ChangeDamage(float targetValue)
    {
        float tempDamage = (float)_damage;
        float step = 1f;
        int duration = Mathf.Abs((int)(targetValue - _damage));

        for (int i = 0; i < duration; i++)
        {
            tempDamage = Mathf.MoveTowards(tempDamage, targetValue, step);
            _damage = (int)tempDamage;
            ChangingDamage?.Invoke(_damage);
            yield return _waitForSeconds;
        }

        EndChangigng?.Invoke();

        StopAllCoroutines();
    }

    private IEnumerator ChangeHealth(float targetValue)
    {
        float tempHealth = (float)_health;
        float step = 1f;
        int duration = Mathf.Abs((int)targetValue - _health);

        for (int i = 0; i < duration; i++)
        {
            tempHealth = Mathf.MoveTowards(tempHealth, targetValue, step);
            _health = (int)tempHealth;
            ChangingHealth?.Invoke(_health);

            CheckLife();

            yield return _waitForSeconds;
        }

        EndChangigng?.Invoke();

        StopAllCoroutines();
    }

    private void CheckLife()
    {
        if (_health <= 0)
        {
            EndChangigng?.Invoke();
            Died?.Invoke();
        }
    }

    public void SetRandomIndex(int index)
    {
        _index = index;
    }

    public void ChangeRandomParameter(int minValue, int MaxValue)
    {
        switch (_index)
        {
            case 0:
                int targetDamage = Random.Range(minValue, MaxValue + 1);
                StartCoroutine(ChangeDamage(targetDamage));
                break;

            case 1:
                int targetHealth = Random.Range(minValue, MaxValue + 1);
                StartCoroutine(ChangeHealth(targetHealth));
                break;

            default:
                break;
        }
    }
}
