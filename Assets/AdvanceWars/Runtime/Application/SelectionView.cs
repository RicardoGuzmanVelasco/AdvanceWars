using System.Threading.Tasks;
using UnityEngine;

namespace AdvanceWars.Runtime.Application
{
    public interface SelectionView
    {
        Task Show(Vector2Int position);
        Task Hide();
    }
}