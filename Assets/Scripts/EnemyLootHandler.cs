using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class EnemyLootHandler : MonoBehaviour
{
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private ItemCollection _itemCollection;
    private Item _item;
    private EnemyState _enemyState;
        
    private void Start()
    {
        _enemyState = gameObject.GetComponent<EnemyState>();
        _enemyState.DropLoot += DropLoot;
        _item = _itemCollection.itemCollection[Random.Range(0, _itemCollection.itemCollection.Count)];
    }

    private void DropLoot()
    {
        var itemObject = Instantiate(_lootPrefab, transform.position, Quaternion.identity);
        itemObject.GetComponent<SpriteRenderer>().sprite = _item.Icon;
        itemObject.GetComponent<ItemDataComponent>().Item = _item;

        var itemTransform = itemObject.transform;
        itemTransform.position = transform.position;

        Vector3 topPosition = transform.position + Vector3.up * 1f;
        Vector3 endPosition = transform.position + Vector3.down * 1f;

        float duration = 1.0f;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(itemTransform.DOMove(topPosition, duration / 2).SetEase(Ease.OutQuad));
        sequence.Append(itemTransform.DOMove(endPosition, duration).SetEase(Ease.OutQuad));
    }
}