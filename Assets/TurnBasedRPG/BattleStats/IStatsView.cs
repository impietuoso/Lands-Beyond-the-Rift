using System.Linq;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class IStatsView : DataView<IStats>
{
    [SerializeField] private ListView listView;

    protected override void Subscribe()
    {
        listView.SetData(Stats.All.Select(a => Data[a]).ToArray());
    }

    protected override void Unsubscribe()
    {
        listView.SetData(Enumerable.Empty<int>());
    }
}