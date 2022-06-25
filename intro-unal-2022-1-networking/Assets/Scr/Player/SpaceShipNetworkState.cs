using Unity.Netcode;
using UnityEngine;

struct SpaceShipNetworkState : INetworkSerializable {
    private float _posX, _posY;
    private short _rotZ;

    internal Vector2 Position {
        get => new Vector2(_posX, _posY);
        set {
            _posX = value.x;
            _posY = value.y;
        }
    }

    internal Vector3 Rotation {
        get => new Vector3(0, 0, _rotZ);
        set => _rotZ = (short)value.z;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
        serializer.SerializeValue(ref _posX);
        serializer.SerializeValue(ref _posY);

        serializer.SerializeValue(ref _rotZ);
    }
}