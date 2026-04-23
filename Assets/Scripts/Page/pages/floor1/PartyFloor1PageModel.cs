using System.Collections.Generic;
using UnityEngine;

public class PartyFloor1PageModel {
  public const string PAGE_KEY = "floor1/party";
  private const string CHOICE_CANCEL = AarrowFloor1PageModel.PAGE_KEY;
  private const string PARTY_TITLE_KEY = "floor1_party_title";
  private const string PARTY_USAGI_KEY = "floor1_party_usagi";
  private const string PARTY_SHIORI_KEY = "floor1_party_shiori";
  private const string PARTY_CANCEL_KEY = "floor1_party_cancel";

  private sealed class AllyChoice {
    public AllyChoice(string joinedKey, string label, string pageKey) {
      JoinedKey = joinedKey;
      Label = label;
      PageKey = pageKey;
    }

    public string JoinedKey { get; }
    public string Label { get; }
    public string PageKey { get; }
  }

  private static readonly AllyChoice[] ALLY_CHOICES = new AllyChoice[] {
    new AllyChoice("ally_usagi_joined", PARTY_USAGI_KEY, UsagiFloor1PageModel.PAGE_KEY),
    new AllyChoice("ally_shiori_joined", PARTY_SHIORI_KEY, ShioriFloor1PageModel.PAGE_KEY),
  };

  static public PageModel getPageData() {
    PageModel model = new PageModel();
    model.setPageTypeChoice();
    model.main_bg = "240_135/dungeon_up";
    model.speaker = "カッパ";

    List<AllyChoice> availableChoices = GetAvailableChoices();
    if (availableChoices.Count == 0) {
      model.page_type = PageModel.PAGE_TYPE_NORMAL;
      model.main_text = LocalizationUtil.GetOrDefault(PARTY_TITLE_KEY, "誰に頼る？");
      model.next_page = AarrowFloor1PageModel.PAGE_KEY;
      return model;
    }

    Shuffle(availableChoices);

    ChoiceModel.instance.setTitle(LocalizationUtil.GetOrDefault(PARTY_TITLE_KEY, "誰に頼る？"));
    ChoiceModel.instance.AddButton(availableChoices[0].PageKey, GetChoiceLabel(availableChoices[0]));
    if (availableChoices.Count >= 2) {
      ChoiceModel.instance.AddButton(availableChoices[1].PageKey, GetChoiceLabel(availableChoices[1]));
      ChoiceModel.instance.AddButton(CHOICE_CANCEL, LocalizationUtil.GetOrDefault(PARTY_CANCEL_KEY, "やっぱりやめる"));
    } else {
      ChoiceModel.instance.AddButton(CHOICE_CANCEL, LocalizationUtil.GetOrDefault(PARTY_CANCEL_KEY, "やっぱりやめる"));
    }

    return model;
  }

  static public void pushedChoiceButton(string key) {
    if (string.IsNullOrEmpty(key)) return;
    DataMgr.SetStr("page", key);
    GameSceneMgr.instance.updateScene(key);
  }

  private static List<AllyChoice> GetAvailableChoices() {
    List<AllyChoice> result = new List<AllyChoice>();
    foreach (AllyChoice allyChoice in ALLY_CHOICES) {
      if (DataMgr.GetBool(allyChoice.JoinedKey)) {
        result.Add(allyChoice);
      }
    }
    return result;
  }

  private static string GetChoiceLabel(AllyChoice allyChoice) {
    if (allyChoice == null) return "";
    if (allyChoice.Label == PARTY_USAGI_KEY) {
      return LocalizationUtil.GetOrDefault(PARTY_USAGI_KEY, "ウサギに頼る");
    }
    if (allyChoice.Label == PARTY_SHIORI_KEY) {
      return LocalizationUtil.GetOrDefault(PARTY_SHIORI_KEY, "シオリーナに頼る");
    }
    return allyChoice.Label;
  }

  private static void Shuffle(List<AllyChoice> choices) {
    for (int i = choices.Count - 1; i > 0; i--) {
      int swapIndex = Random.Range(0, i + 1);
      AllyChoice temp = choices[i];
      choices[i] = choices[swapIndex];
      choices[swapIndex] = temp;
    }
  }
}
