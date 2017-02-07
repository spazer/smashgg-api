using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace smashgg_to_liquipedia
{
    public partial class Form1 : Form
    {
        static int PLAYER_BYE = -1;
        #region Bracket Template Contants
        static string deFinalBracketTemplateReset = "{{DEFinalBracket\r\n" +
                                                    "<!-- FROM WINNERS -->\r\n" +
                                                    "|r1m1p1= |r1m1p1flag= |r1m1p1score=\r\n" +
                                                    "|r1m1p2= |r1m1p2flag= |r1m1p2score=\r\n" +
                                                    "|r1m1win=\r\n\r\n" +
                                                    "<!-- FROM LOSERS -->\r\n" +
                                                    "|l1m1p1= |l1m1p1flag= |l1m1p1score=\r\n" +
                                                    "|l1m1p2= |l1m1p2flag= |l1m1p2score=\r\n" +
                                                    "|l1m1win=\r\n\r\n" +
                                                    "<!-- LOSERS FINALS -->\r\n" +
                                                    "|l2m1p1= |l2m1p1flag= |l2m1p1score=\r\n" +
                                                    "|l2m1p2= |l2m1p2flag= |l2m1p2score=\r\n" +
                                                    "|l2m1win=\r\n\r\n" +
                                                    "<!-- GRAND FINALS -->\r\n" +
                                                    "|r3m1p1= |r3m1p1flag= |r3m1p1score= |r3m2p1score=\r\n" +
                                                    "|r3m1p2= |r3m1p2flag= |r3m1p2score= |r3m2p2score=\r\n" +
                                                    "|r3m1win=\r\n" +
                                                    "}}";

        static string deFinalBracketTemplate =  "{{DEFinalBracket\r\n" +
                                                "<!-- FROM WINNERS -->\r\n" +
                                                "|r1m1p1= |r1m1p1flag= |r1m1p1score=\r\n" +
                                                "|r1m1p2= |r1m1p2flag= |r1m1p2score=\r\n" +
                                                "|r1m1win=\r\n\r\n" +
                                                "<!-- FROM LOSERS -->\r\n" +
                                                "|l1m1p1= |l1m1p1flag= |l1m1p1score=\r\n" +
                                                "|l1m1p2= |l1m1p2flag= |l1m1p2score=\r\n" +
                                                "|l1m1win=\r\n\r\n" +
                                                "<!-- LOSERS FINALS -->\r\n" +
                                                "|l2m1p1= |l2m1p1flag= |l2m1p1score=\r\n" +
                                                "|l2m1p2= |l2m1p2flag= |l2m1p2score=\r\n" +
                                                "|l2m1win=\r\n\r\n" +
                                                "<!-- GRAND FINALS -->\r\n" +
                                                "|r3m1p1= |r3m1p1flag= |r3m1p1score=\r\n" +
                                                "|r3m1p2= |r3m1p2flag= |r3m1p2score=\r\n" +
                                                "|r3m1win=\r\n" +
                                                "}}";

        static string deFinalDoublesBracketTemplateReset = "{{DEFinalDoublesBracket\r\n" +
                                                        "<!-- FROM WINNERS -->\r\n" +
                                                        "|r1m1t1p1= |r1m1t1p1flag=\r\n" +
                                                        "|r1m1t1p2= |r1m1t1p2flag= |r1m1t1score=\r\n" +
                                                        "|r1m1t2p1= |r1m1t2p1flag=\r\n" +
                                                        "|r1m1t2p2= |r1m1t2p2flag= |r1m1t2score=\r\n" +
                                                        "|r1m1win=\r\n" +
                                                        "\r\n" +
                                                        "<!-- FROM LOSERS -->\r\n" +
                                                        "|l1m1t1p1= |l1m1t1p1flag=\r\n" +
                                                        "|l1m1t1p2= |l1m1t1p2flag= |l1m1t1score=\r\n" +
                                                        "|l1m1t2p1= |l1m1t2p1flag=\r\n" +
                                                        "|l1m1t2p2= |l1m1t2p2flag= |l1m1t2score=\r\n" +
                                                        "|l1m1win=\r\n" +
                                                        "\r\n" +
                                                        "<!-- LOSERS FINALS -->\r\n" +
                                                        "|l2m1t1p1= |l2m1t1p1flag=\r\n" +
                                                        "|l2m1t1p2= |l2m1t1p2flag= |l2m1t1score=\r\n" +
                                                        "|l2m1t2p1= |l2m1t2p1flag=\r\n" +
                                                        "|l2m1t2p2= |l2m1t2p2flag= |l2m1t2score=\r\n" +
                                                        "|l2m1win=\r\n" +
                                                        "\r\n" +
                                                        "<!-- GRAND FINALS -->\r\n" +
                                                        "|r3m1t1p1= |r3m1t1p1flag=\r\n" +
                                                        "|r3m1t1p2= |r3m1t1p2flag= |r3m1t1score= |r3m2t1score=\r\n" +
                                                        "|r3m1t2p1= |r3m1t2p1flag=\r\n" +
                                                        "|r3m1t2p2= |r3m1t2p2flag= |r3m1t2score= |r3m2t2score=\r\n" +
                                                        "|r3m1win=\r\n" +
                                                        "}}";

        static string deFinalDoublesBracketTemplate = "{{DEFinalDoublesBracket\r\n" +
                                                            "<!-- FROM WINNERS -->\r\n" +
                                                            "|r1m1t1p1= |r1m1t1p1flag=\r\n" +
                                                            "|r1m1t1p2= |r1m1t1p2flag= |r1m1t1score=\r\n" +
                                                            "|r1m1t2p1= |r1m1t2p1flag=\r\n" +
                                                            "|r1m1t2p2= |r1m1t2p2flag= |r1m1t2score=\r\n" +
                                                            "|r1m1win=\r\n" +
                                                            "\r\n" +
                                                            "<!-- FROM LOSERS -->\r\n" +
                                                            "|l1m1t1p1= |l1m1t1p1flag=\r\n" +
                                                            "|l1m1t1p2= |l1m1t1p2flag= |l1m1t1score=\r\n" +
                                                            "|l1m1t2p1= |l1m1t2p1flag=\r\n" +
                                                            "|l1m1t2p2= |l1m1t2p2flag= |l1m1t2score=\r\n" +
                                                            "|l1m1win=\r\n" +
                                                            "\r\n" +
                                                            "<!-- LOSERS FINALS -->\r\n" +
                                                            "|l2m1t1p1= |l2m1t1p1flag=\r\n" +
                                                            "|l2m1t1p2= |l2m1t1p2flag= |l2m1t1score=\r\n" +
                                                            "|l2m1t2p1= |l2m1t2p1flag=\r\n" +
                                                            "|l2m1t2p2= |l2m1t2p2flag= |l2m1t2score=\r\n" +
                                                            "|l2m1win=\r\n" +
                                                            "\r\n" +
                                                            "<!-- GRAND FINALS -->\r\n" +
                                                            "|r3m1t1p1= |r3m1t1p1flag=\r\n" +
                                                            "|r3m1t1p2= |r3m1t1p2flag= |r3m1t1score=\r\n" +
                                                            "|r3m1t2p1= |r3m1t2p1flag=\r\n" +
                                                            "|r3m1t2p2= |r3m1t2p2flag= |r3m1t2score=\r\n" +
                                                            "|r3m1win=\r\n" +
                                                            "}}";
        #endregion

        enum UrlNumberType { Phase, Phase_Group, None }
        enum EventType { Singles, Doubles, None }
        enum PoolType { Bracket, RoundRobin }
        enum BracketSide { Winners, Losers }

        Dictionary<int, string> entrantList = new Dictionary<int, string>();
        List<Set> setList = new List<Set>();
        Dictionary<int, List<Set>> roundList = new Dictionary<int, List<Set>>();
        List<Phase> phaseList = new List<Phase>();

        JObject tournamentStructure;
        string tournament = string.Empty;
        EventType retreivedDataType = EventType.None;

        public Form1()
        {
            InitializeComponent();

        }

        #region Buttons
        /// <summary>
        /// Indicates that user wishes to retrieve a bracket
        /// </summary>
        /// <param name="sender">N/A</param>
        /// <param name="e">N/A</param>
        private void buttonGetBracket_Click(object sender, EventArgs e)
        {
            JObject bracketJson = JsonConvert.DeserializeObject<JObject>(richTextBoxEntrants.Text);

            smashgg parser = new smashgg();

            parser.GetEntrants(bracketJson.SelectToken("entities.player"), ref entrantList);

            richTextBoxLpOutput.Text = string.Empty;

            foreach (KeyValuePair<int,string> player in entrantList)
            {
                richTextBoxLpOutput.Text += player.Key + "," + player.Value + "\r\n";
            }

        }






        #endregion
    }
}
