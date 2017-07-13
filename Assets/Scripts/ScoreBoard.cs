using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public Text[] rankText;
    public Button btn;
    public GameObject ScoreBoardPanel;
    public Sprite hide, discover;

    private int hideDiscover = 0;

	public void setScorBoard(List<Cartesian> list)
    {
        foreach (Cartesian c in list)
        {
            switch (c.athlete.rank)
            {
                case 1:
                    rankText[0].text = c.athlete.gname;
                    break;

                case 2:
                    rankText[1].text = c.athlete.gname;
                    break;

                case 3:
                    rankText[2].text = c.athlete.gname;
                    break;

                case 4:
                    rankText[3].text = c.athlete.gname;
                    break;

                case 5:
                    rankText[4].text = c.athlete.gname;
                    break;
            }
        }        
    }

    public void hideOrDiscoverScoreBoard()
    {
        if (hideDiscover == 0)
        {
            btn.GetComponent<Image>().sprite = discover;
            ScoreBoardPanel.SetActive(false);
            hideDiscover += 1;
        }
        else
        {
            btn.GetComponent<Image>().sprite = hide;
            ScoreBoardPanel.SetActive(true);
            hideDiscover -= 1;
        }
            
    }
}
