
using UnityEngine;
using R3;
/// <summary>
/// グリッド情報の受け渡し(外部からデータをもらうことができればmonobeなくなる)
/// </summary>
public class GridGeneratePresenter : MonoBehaviour
{
    [SerializeField]
    private int _gridSizeX = default;

    [SerializeField]
    private int _gridSizeZ = default;

    [SerializeField]
    private float _gridCellSize = default;

    public float GridCellSize { get => _gridCellSize; }

    [SerializeField]
    private Material _material = default;

    private GridGenerateModel _model = default;
    private GridGenerateView _view;

    private void Awake()
    {
        _model = new GridGenerateModel(new GridGenerateData(_gridSizeX, _gridSizeZ, _gridCellSize));
        _view = new GridGenerateView(_material, this.transform);
        Bind();
        _model.InitializeGrid();
    }

    private void Start()
    {
        
    }

    public Node GetNodeWorldPosition(Vector3 worldPosition)
    {
      return _model.GetNodeFromWorldPosition(worldPosition);
    }

    private void Bind()
    {
        _model.Grid.Subscribe(node => _view.UpdateGrid(node)).AddTo(this);
    }
}