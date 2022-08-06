using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

public class RegionDropdownHandler : MonoBehaviour
{
    public TextAsset jsonRegion;
    RegionDataCollection regionDataCollection;
    [SerializeField]
    TMP_Dropdown regionDropdown;

    int regionCode;
    void Start()
    {
        regionDropdown = GetComponent<TMP_Dropdown>();
        FileReader();

    }
    private void Update()
    {
        string regionName = regionDropdown.options[regionDropdown.value].text;
        regionDropdown.captionText.text = regionName;
    }
    void FileReader()
    {
        regionDataCollection = JsonConvert.DeserializeObject<RegionDataCollection>(jsonRegion.text);
        PopulateCode();
    }
    void PopulateCode()
    {
        regionDropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> regionsOption = new List<TMP_Dropdown.OptionData>();
        foreach (var region in regionDataCollection.data)
        {
            string regionNamewithRegionCode = $"{region.RegionName} ({region.RegionCode})";
            var option = new TMP_Dropdown.OptionData(regionNamewithRegionCode);
            regionsOption.Add(option);
            Debug.Log(option);
        }

        regionDropdown.AddOptions(regionsOption);

    }


}
public class RegionDataCollection
{
    public List<RegionData> data;
}
public class RegionData
{
    public string RegionCode { get; set; }
    public string RegionName { get; set; }
}
