using UnityEngine;

namespace Model
{
    public interface IPlayerData
    {
        Quaternion Rotation { get; }
        Vector3 Direction { get; }
    }
}