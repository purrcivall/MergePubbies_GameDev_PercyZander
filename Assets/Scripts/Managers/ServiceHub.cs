using UnityEngine;

//<summary> This script is used to allow Scripts to talk to eachother. - Zander :3 </summary>
public class ServiceHub : MonoBehaviour
{
    public static ServiceHub Instance { get; private set; }

    [Header("System References")]
    [SerializeField] private Spawner spawner;
    [SerializeField] private MergeLogic mergelogic;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SaveManager saveManager;

    public Spawner Spawner => spawner;
    public MergeLogic MergeLogic => mergelogic;
    public GameManager GameManager => gameManager;
    public UIManager UIManager => uiManager;
    public AudioManager AudioManager => audioManager;
    public SaveManager SaveManager => saveManager;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
}