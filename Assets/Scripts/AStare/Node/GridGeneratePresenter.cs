
using UnityEngine;
using R3;
/// <summary>
/// �O���b�h���̎󂯓n��(�O������f�[�^�����炤���Ƃ��ł����monobe�Ȃ��Ȃ�)
/// </summary>
public class GridGeneratePresenter : MonoBehaviour
{
    [SerializeField]
    private int _gridSizeX = default;

    [SerializeField]
    private int _gridSizeZ = default;

    [SerializeField]
    private float _gridCellSize = default;

    [SerializeField]
    private Material _material = default;

    private GridGenerateModel _model = default;
    private GridGenerateView _view;

    private void Awake()
    {
        _model = new GridGenerateModel(new GridGenerateData(_gridSizeX, _gridSizeZ, _gridCellSize));
        _view = new GridGenerateView(_material);
        Bind();
    }

    private void Start()
    {
        _model.InitializeGrid();
    }

    private void Bind()
    {
        _model.Grid.Subscribe(node => _view.UpdateGrid(node)).AddTo(this);
    }
}