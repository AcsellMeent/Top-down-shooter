using UnityEngine;
using TMPro;

public class PlayerRecordDisplay : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;

    [SerializeField]
    private string _labelFormat;

    private void Awake()
    {
        PlayerRecord playerRecord = StorageSystem.LoadPlayerRecord();
        _label.text = string.Format(_labelFormat, playerRecord == null ? "0" : playerRecord.Points.ToString());
    }

    public void EraseRecord()
    {
        StorageSystem.SavePlayerRecord(new PlayerRecord(0));
        _label.text = string.Format(_labelFormat, "0");
    }
}
