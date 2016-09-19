﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace smashgg_api
{
    public partial class Form1 : Form
    {
        // URLs
        static string GG_URL_PHASEGROUP = "https://api.smash.gg/phase_group/";
        static string GG_URL_PHASE = "https://api.smash.gg/phase/";
        static string GG_URL_BRACKET = "?expand[]=sets&expand[]=entrants";
        static string GG_URL_GROUPS = "?expand[]=groups";

        // General parameters
        static string GG_ID = "\"id\":";

        // Expands
        static string GG_ENTRANTS = "\"entrants\":";
        static string GG_SETS = "\"sets\":";
        static string GG_GROUPS = "\"groups\":";

        // Group parameters
        static string GG_DISPLAYIDENT = "\"displayIdentifier\":";

        // Liquipedia parameters
        static string LP_P1 = "p1";
        static string LP_P2 = "p2";
        static string LP_WROUND = "r";
        static string LP_LROUND = "l";
        static string LP_MATCH = "m";
        static string LP_FLAG = "flag";
        static string LP_SCORE = "score";
        static string LP_WIN = "win";
        static string LP_T1 = "t1";
        static string LP_T2 = "t2";

        // Liquipedia pool tables
        static string LP_BOX_START = "{{Box|start|padding=4em}}";
        static string LP_BOX_BREAK = "{{Box|break|padding=4em}}";
        static string LP_BOX_END = "{{Box|end}}";
        static string LP_SORT_START = "{{HiddenSort|";
        static string LP_SORT_END = "}}";
        static string LP_GROUP_START = "{{GroupTableStart | ";
        static string LP_GROUP_STARTWIDTH = " |width=300px}}";
        static string LP_GROUP_END = "{{GroupTableEnd}}";
        static string LP_SLOT_START = "{{GroupTableSlot|";
        static string LP_SLOT_FLAG = " |flag=";
        static string LP_SLOT_PLACE = " |place=";
        static string LP_SLOT_MWIN = " |win_m=";
        static string LP_SLOT_MLOSS = " |lose_m=";
        static string LP_SLOT_BG = " |bg=";
        static string LP_SLOT_END = "}}";
        static string LP_CHECKMARK = "{{win}}";

        // Misc
        static int PLAYER_BYE = -1;

        Dictionary<int, Player> entrantList = new Dictionary<int, Player>();
        Dictionary<int, Team> teamList = new Dictionary<int, Team>();
        List<Set> setList = new List<Set>();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonBracket_Click(object sender, EventArgs e)
        {
            // Clear data
            richTextBoxLog.Clear();
            richTextBoxEntrants.Clear();
            richTextBoxWinners.Clear();
            richTextBoxLosers.Clear();
            entrantList.Clear();
            setList.Clear();
            numericUpDownStartSingles.Value = 0;
            numericUpDownEndSingles.Value = 0;
            numericUpDownOffsetSingles.Value = 0;
            checkBoxWinnersSingles.Checked = false;
            checkBoxLosersSingles.Checked = false;

            string webText = string.Empty;
            int endPos = 0;

            // Retrieve webpage via api
            try
            {
                WebRequest r = WebRequest.Create(GG_URL_PHASEGROUP + textBoxURLSingles.Text + GG_URL_BRACKET);
                WebResponse resp = r.GetResponse();
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    webText = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                richTextBoxLog.Text += ex + "\r\n";
            }

            smashgg parser = new smashgg();

            string rawEntrants = parser.ExpandOnly(webText, GG_ENTRANTS, 0, out endPos);
            parser.GetEntrants(rawEntrants, ref entrantList);

            string rawSets = parser.ExpandOnly(webText, GG_SETS, 0, out endPos);
            parser.GetSets(rawSets, ref setList);

            // Set values in form controls
            foreach (Set set in setList)
            {
                int round = Math.Abs(set.round);

                if (set.round > 0)
                {
                    checkBoxWinnersSingles.Checked = true;
                }
                else if (set.round < 0)
                {
                    checkBoxLosersSingles.Checked = true;
                }

                // Remember lowest round number
                if (numericUpDownStartSingles.Value == 0)
                {
                    numericUpDownStartSingles.Value = round;
                }
                else if (round < numericUpDownStartSingles.Value)
                {
                    numericUpDownStartSingles.Value = round;
                }

                // Remember highest round number
                if (numericUpDownEndSingles.Value == 0)
                {
                    numericUpDownEndSingles.Value = round;
                }
                else if (round > numericUpDownEndSingles.Value)
                {
                    numericUpDownEndSingles.Value = round;
                }
            }

            // Set padding for textbox output
            int p1padding = 0;
            int wpadding = 0;
            int lpadding = 0;

            foreach (Player player in entrantList.Values)
            {
                if (player.name.Length > p1padding)
                {
                    p1padding = player.name.Length;
                }
            }

            foreach (Set set in setList)
            {
                if (set.round > 0)
                {
                    if (entrantList[set.entrantID1].name.Length > wpadding)
                    {
                        wpadding = entrantList[set.entrantID1].name.Length;
                    }
                }
                else
                {
                    if (entrantList[set.entrantID1].name.Length > lpadding)
                    {
                        lpadding = entrantList[set.entrantID1].name.Length;
                    }
                }
            }

            // Output entrants to textbox
            foreach (KeyValuePair<int, Player> entrant in entrantList)
            {
                if (entrant.Key == -1) continue;

                richTextBoxEntrants.Text += entrant.Key.ToString().PadRight(8) + entrant.Value.name.PadRight(p1padding + 2) + "  " + entrant.Value.country + "\r\n";
            }

            // Output sets to textbox
            foreach (Set set in setList)
            {
                if (checkBoxFillUnfinishedSingles.Checked == false && set.state == 1)
                {
                    continue;
                }

                if (set.round > 0)
                {
                    OutputSetToTextBox(ref richTextBoxWinners, set, wpadding);
                }
                else
                {
                    OutputSetToTextBox(ref richTextBoxLosers, set, lpadding);
                }
            }
        }

        private void buttonFillSingles_Click(object sender, EventArgs e)
        {
            string output = richTextBoxLiquipedia.Text;
            string bracketSide = string.Empty;

            foreach (Set set in setList)
            {
                // Detect what side of the bracket this set is in. If the corresponding checkbox is not checked, skip that side of the bracket
                if (set.round > 0 && checkBoxWinnersSingles.Checked == true)
                {
                    bracketSide = LP_WROUND;
                }
                else if (set.round < 0 && checkBoxLosersSingles.Checked == true)
                {
                    bracketSide = LP_LROUND;
                }
                else
                {
                    continue;
                }

                // Skip unfinished sets unless otherwise specified
                if(checkBoxFillUnfinishedSingles.Checked == false && set.state == 1)
                {
                    continue;
                }

                // The set's round number must be within the bounds of the numericUpDown controls
                if (Math.Abs(set.round) >= numericUpDownStartSingles.Value && Math.Abs(set.round) <= numericUpDownEndSingles.Value)
                {
                    // Offset the output round by the number specified in the numericUpDown control
                    int outputRound = Math.Abs(set.round) + (int)numericUpDownOffsetSingles.Value;

                    // Check for player byes
                    if (set.entrantID1 == PLAYER_BYE && set.entrantID2 == PLAYER_BYE)
                    {
                        // If both players are byes, skip this entry
                        continue;
                    }
                    else if (set.entrantID1 == PLAYER_BYE)
                    {
                        // Fill in player 1 as a bye
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1, "Bye");

                        // Give player 2 a checkmark
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2, entrantList[set.entrantID2].name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2 + LP_FLAG, entrantList[set.entrantID2].country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2 + LP_SCORE, LP_CHECKMARK);

                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_WIN, "2");
                    }
                    else if (set.entrantID2 == PLAYER_BYE)
                    {
                        // Fill in player 2 as a bye
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2, "Bye");

                        // Give player 1 a checkmark
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1, entrantList[set.entrantID1].name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1 + LP_FLAG, entrantList[set.entrantID1].country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1 + LP_SCORE, LP_CHECKMARK);

                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_WIN, "1");
                    }
                    else
                    {
                        // Fill in the set normally
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1, entrantList[set.entrantID1].name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1 + LP_FLAG, entrantList[set.entrantID1].country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2, entrantList[set.entrantID2].name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2 + LP_FLAG, entrantList[set.entrantID2].country);

                        // Check for DQs
                        if (set.entrant1wins == -1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1 + LP_SCORE, "DQ");
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2 + LP_SCORE, LP_CHECKMARK);
                        }
                        else if (set.entrant2wins == -1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2 + LP_SCORE, "DQ");
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1 + LP_SCORE, LP_CHECKMARK);
                        }
                        else
                        {
                            if (set.entrant1wins != -99 && set.entrant2wins != -99)
                            {
                                FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1 + LP_SCORE, set.entrant1wins.ToString());
                                FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2 + LP_SCORE, set.entrant2wins.ToString());
                            }
                            else
                            {
                                if (set.winner == set.entrantID1)
                                {
                                    FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P1 + LP_SCORE, "{{win}}");
                                }
                                else if (set.winner == set.entrantID2)
                                {
                                    FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_P2 + LP_SCORE, "{{win}}");
                                }
                            }
                        }

                        // Set the winner
                        if (set.winner == set.entrantID1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_WIN, "1");
                        }
                        else if (set.winner == set.entrantID2)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_WIN, "2");
                        }
                    }
                }
            }

            richTextBoxLiquipedia.Text = output;
        }

        private void buttonPhase_Click(object sender, EventArgs e)
        {
            // Clear data
            richTextBoxLog.Clear();
            richTextBoxEntrants.Clear();
            richTextBoxWinners.Clear();
            richTextBoxLosers.Clear();
            entrantList.Clear();
            setList.Clear();

            string webText = string.Empty;
            int endPos = 0;

            // Retrieve webpage via api
            try
            {
                WebRequest r = WebRequest.Create(GG_URL_PHASE + textBoxURLSingles.Text + GG_URL_GROUPS);
                WebResponse resp = r.GetResponse();
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    webText = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                richTextBoxLog.Text += ex + "\r\n";
            }

            smashgg parser = new smashgg();

            // Get a list of groups
            List<string> rawGroupList = new List<string>();
            string temp;
            string rawGroups = parser.ExpandOnly(webText, GG_GROUPS, 0, out endPos);
            endPos = 0;
            do
            {
                temp = parser.SplitExpand(rawGroups, endPos, out endPos);
                if (temp != string.Empty)
                {
                    rawGroupList.Add(temp);
                }
            } while (temp != string.Empty);

            List<Group> groupList = new List<Group>();
            foreach (string groupString in rawGroupList)
            {
                Group newGroup = new Group();
                newGroup.DisplayIdentifier = parser.GetStringParameter(groupString, GG_DISPLAYIDENT);
                newGroup.id = parser.GetIntParameter(groupString, GG_ID);

                groupList.Add(newGroup);
            }

            rawGroupList.Clear();

            // Sort the list by wave and number
            if (groupList[0].waveNumberDetected)
            {
                groupList = groupList.OrderBy(c => c.Wave).ThenBy(c => c.Number).ToList();
            }
            else
            {
                groupList = groupList.OrderBy(c => c.DisplayIdentifier).ToList();
            }

            // Retrieve pages for each group and process them
            string lastWave = string.Empty;
            for (int j = 0; j < groupList.Count; j++) 
            {
                try
                {
                    WebRequest r = WebRequest.Create(GG_URL_PHASEGROUP + groupList[j].id + GG_URL_BRACKET);
                    WebResponse resp = r.GetResponse();
                    using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                    {
                        webText = sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    richTextBoxLog.Text += ex + "\r\n";
                }

                // Clear data
                entrantList.Clear();
                setList.Clear();
                richTextBoxEntrants.Clear();
                richTextBoxWinners.Clear();
                richTextBoxLosers.Clear();

                string rawEntrants = parser.ExpandOnly(webText, GG_ENTRANTS, 0, out endPos);
                parser.GetEntrants(rawEntrants, ref entrantList);
                
                string rawSets = parser.ExpandOnly(webText, GG_SETS, 0, out endPos);
                parser.GetSets(rawSets, ref setList);

                Dictionary<int, PoolRecord> poolData = new Dictionary<int, PoolRecord>();
                foreach (KeyValuePair<int,Player> entrant in entrantList)
                {
                    poolData.Add(entrant.Key, new PoolRecord());
                }

                foreach (Set set in setList)
                {
                    poolData[set.entrantID1].isinGroup = true;
                    poolData[set.entrantID2].isinGroup = true;

                    // Record match data for each player's record
                    if (set.winner == set.entrantID1)
                    {
                        if (set.entrantID2 == PLAYER_BYE) continue;

                        poolData[set.entrantID1].matchesWin++;
                        poolData[set.entrantID2].matchesLoss++;

                        if (set.entrant2wins != -1) // Ignore W-L for DQs for now
                        {
                            poolData[set.entrantID1].AddWins(set.entrant1wins);
                            poolData[set.entrantID2].AddWins(set.entrant2wins);
                            poolData[set.entrantID1].AddLosses(set.entrant2wins);
                            poolData[set.entrantID2].AddLosses(set.entrant1wins);
                        }

                        if (poolData[set.entrantID1].rank == 0 || set.wPlacement < poolData[set.entrantID1].rank)
                        {
                            if (set.wPlacement != -99)
                            {
                                poolData[set.entrantID1].rank = set.wPlacement;
                            }
                        }

                        if (poolData[set.entrantID2].rank == 0 || set.lPlacement < poolData[set.entrantID2].rank)
                        {
                            if (set.lPlacement != -99)
                            {
                                poolData[set.entrantID2].rank = set.lPlacement;
                            }
                        }
                    }
                    else if (set.winner == set.entrantID2)
                    {
                        if (set.entrantID1 == PLAYER_BYE) continue;

                        poolData[set.entrantID2].matchesWin++;
                        poolData[set.entrantID1].matchesLoss++;

                        if (set.entrant1wins != -1)
                        {
                            poolData[set.entrantID1].AddWins(set.entrant1wins);
                            poolData[set.entrantID2].AddWins(set.entrant2wins);
                            poolData[set.entrantID1].AddLosses(set.entrant2wins);
                            poolData[set.entrantID2].AddLosses(set.entrant1wins);
                        }

                        if (poolData[set.entrantID1].rank == 0 || set.lPlacement < poolData[set.entrantID1].rank)
                        {
                            if (set.lPlacement != -99)
                            {
                                poolData[set.entrantID1].rank = set.lPlacement;
                            }
                        }

                        if (poolData[set.entrantID2].rank == 0 || set.wPlacement < poolData[set.entrantID2].rank)
                        {
                            if (set.wPlacement != -99)
                            {
                                poolData[set.entrantID2].rank = set.wPlacement;
                            }
                        }
                    }
                }

                // Remove entrants without listed sets (smash.gg seems to list extraneous entrants sometimes)
                for(int i=0; i<poolData.Count;i++)
                {
                    if(poolData.ElementAt(i).Value.isinGroup == false)
                    {
                        poolData.Remove(poolData.ElementAt(i).Key);
                        i--;
                    }
                }

                // Sort the entrants by their rank and W-L records
                poolData = poolData.OrderBy(x => x.Value.rank).ThenBy(x => x.Value.matchesLoss).ThenByDescending(x => x.Value.matchesWin).ThenByDescending(x => x.Value.GameWinrate).ToDictionary(x => x.Key, x => x.Value);

                // Output to textbox
                // Wave headers
                if (groupList[0].waveNumberDetected)
                {
                    if (lastWave != groupList[j].Wave)
                    {
                        richTextBoxLiquipedia.Text += "==Wave " + groupList[j].Wave + "==\r\n";
                        richTextBoxLiquipedia.Text += LP_BOX_START + "\r\n";

                        lastWave = groupList[j].Wave;
                    }
                }
                else if (j == 0)    // First element of groupList
                {
                    richTextBoxLiquipedia.Text += LP_BOX_START + "\r\n";
                }

                // Pool headers
                if (groupList[0].waveNumberDetected)
                {
                    richTextBoxLiquipedia.Text += LP_SORT_START + "===" + groupList[j].Wave + groupList[j].Number.ToString() + "===" + LP_SORT_END + "\r\n";
                    richTextBoxLiquipedia.Text += LP_GROUP_START + "Bracket " + groupList[j].Wave + groupList[j].Number.ToString() + LP_GROUP_STARTWIDTH + "\r\n";
                }
                else
                {
                    richTextBoxLiquipedia.Text += LP_SORT_START + "===" + groupList[j].Wave + groupList[j].DisplayIdentifier.ToString() + "===" + LP_SORT_END + "\r\n";
                    richTextBoxLiquipedia.Text += LP_GROUP_START + "Bracket " + groupList[j].DisplayIdentifier.ToString() + LP_GROUP_STARTWIDTH + "\r\n";
                }

                // Pool slots
                int lastRank = 0;
                int lastWin = 0;
                int lastLoss = 0;
                int advance = (int)numericUpDownAdvance.Value;
                for (int i = 0; i < poolData.Count; i++)
                {
                    // Skip bye
                    if (poolData.ElementAt(i).Key == PLAYER_BYE)
                    {
                        continue;
                    }

                    Player currentPlayer = entrantList[poolData.ElementAt(i).Key];
                    richTextBoxLiquipedia.Text += LP_SLOT_START + currentPlayer.name +
                                                  LP_SLOT_FLAG + currentPlayer.country +
                                                  LP_SLOT_MWIN + poolData[poolData.ElementAt(i).Key].matchesWin +
                                                  LP_SLOT_MLOSS + poolData[poolData.ElementAt(i).Key].matchesLoss;
                    if (radioButtonRoundRobin.Checked == true)
                    {
                        if (poolData[poolData.ElementAt(i).Key].matchesWin == lastWin && poolData[poolData.ElementAt(i).Key].matchesLoss == lastLoss)
                        {
                            richTextBoxLiquipedia.Text += LP_SLOT_PLACE + lastRank; 
                        }
                        else
                        {
                            richTextBoxLiquipedia.Text += LP_SLOT_PLACE + (i + 1);
                            lastRank = i + 1;
                            lastWin = poolData[poolData.ElementAt(i).Key].matchesWin;
                            lastLoss = poolData[poolData.ElementAt(i).Key].matchesLoss;
                        }
                    }
                    else if (poolData[poolData.ElementAt(i).Key].rank != -99)
                    {
                        richTextBoxLiquipedia.Text += LP_SLOT_PLACE + poolData[poolData.ElementAt(i).Key].rank;
                    }
                    else
                    {
                        richTextBoxLiquipedia.Text += LP_SLOT_PLACE;
                    }

                    if (advance > 0)
                    {
                        richTextBoxLiquipedia.Text += LP_SLOT_BG + "up";
                        advance--;
                    }
                    else
                    {
                        richTextBoxLiquipedia.Text += LP_SLOT_BG + "down";
                    }

                    richTextBoxLiquipedia.Text += LP_SLOT_END + "\r\n";
                }

                // Pool footers
                richTextBoxLiquipedia.Text += LP_GROUP_END + "\r\n";
                if (groupList[0].waveNumberDetected)
                {
                    if (j + 1 >= groupList.Count)
                    {
                        richTextBoxLiquipedia.Text += LP_BOX_END + "\r\n\r\n";
                    }
                    else if (groupList[j + 1].Wave != lastWave)
                    {
                        richTextBoxLiquipedia.Text += LP_BOX_END + "\r\n\r\n";
                    }
                    else
                    {
                        richTextBoxLiquipedia.Text += LP_BOX_BREAK + "\r\n\r\n";
                    }
                }
                else
                {
                    if (j == groupList.Count - 1)
                    {
                        richTextBoxLiquipedia.Text += LP_BOX_END + "\r\n\r\n";
                    }
                    else
                    {
                        richTextBoxLiquipedia.Text += LP_BOX_BREAK + "\r\n\r\n";
                    }
                }
            }
        }

        private void buttonDoubles_Click(object sender, EventArgs e)
        {
            // Clear data
            richTextBoxLog.Clear();
            richTextBoxEntrants.Clear();
            richTextBoxWinners.Clear();
            richTextBoxLosers.Clear();
            entrantList.Clear();
            teamList.Clear();
            setList.Clear();

            string webText = string.Empty;
            int endPos = 0;

            // Retrieve webpage via api
            try
            {
                WebRequest r = WebRequest.Create(GG_URL_PHASEGROUP + textBoxURLDoubles.Text + GG_URL_BRACKET);
                WebResponse resp = r.GetResponse();
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    webText = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                richTextBoxLog.Text += ex + "\r\n";
            }

            smashgg parser = new smashgg_api.smashgg();

            string rawEntrants = parser.ExpandOnly(webText, GG_ENTRANTS, 0, out endPos);
            parser.GetDoublesEntrants(rawEntrants, ref teamList);

            string rawSets = parser.ExpandOnly(webText, GG_SETS, 0, out endPos);
            parser.GetSets(rawSets, ref setList);

            // Set values in form controls
            foreach (Set set in setList)
            {
                int round = Math.Abs(set.round);

                if (set.round > 0)
                {
                    checkBoxWinnersDoubles.Checked = true;
                }
                else if (set.round < 0)
                {
                    checkBoxLosersDoubles.Checked = true;
                }

                // Remember lowest round number
                if (numericUpDownStartDoubles.Value == 0)
                {
                    numericUpDownStartDoubles.Value = round;
                }
                else if (round < numericUpDownStartDoubles.Value)
                {
                    numericUpDownStartDoubles.Value = round;
                }

                // Remember highest round number
                if (numericUpDownEndDoubles.Value == 0)
                {
                    numericUpDownEndDoubles.Value = round;
                }
                else if (round > numericUpDownEndDoubles.Value)
                {
                    numericUpDownEndDoubles.Value = round;
                }
            }

            // Set padding for textbox output
            int t1padding = 0;
            int wpadding = 0;
            int lpadding = 0;

            foreach(Team team in teamList.Values)
            {
                if (team.player1.name.Length + team.player2.name.Length > t1padding)
                {
                    t1padding = team.player1.name.Length + team.player2.name.Length;
                }
            }

            foreach (Set set in setList)
            {
                if (set.round > 0)
                {
                    if (teamList[set.entrantID1].player1.name.Length > wpadding)
                    {
                        wpadding = teamList[set.entrantID1].player1.name.Length;
                    }
                }
                else
                {
                    if (teamList[set.entrantID1].player1.name.Length > lpadding)
                    {
                        lpadding = teamList[set.entrantID1].player1.name.Length;
                    }
                }
            }

            // Output entrants to textbox
            foreach (KeyValuePair<int, Team> entrant in teamList)
            {
                if (entrant.Key == -1) continue;

                richTextBoxEntrants.Text += entrant.Key.ToString().PadRight(8) + entrant.Value.player1.name + " / " +
                                            entrant.Value.player2.name.PadRight(t1padding + 2) + "  " +
                                            entrant.Value.player1.country + " / " + entrant.Value.player1.country + "\r\n";
            }

            // Output sets to textbox
            foreach (Set set in setList)
            {
                if (checkBoxFillUnfinishedSingles.Checked == false && set.state == 1)
                {
                    continue;
                }

                if (set.round > 0)
                {
                    OutputDoublesSetToTextBox(ref richTextBoxWinners, set, wpadding);
                }
                else
                {
                    OutputDoublesSetToTextBox(ref richTextBoxLosers, set, lpadding);
                }
            }
        }

        private void buttonFillDoubles_Click(object sender, EventArgs e)
        {
            string output = richTextBoxLiquipedia.Text;
            string bracketSide = string.Empty;

            foreach (Set set in setList)
            {
                // Detect what side of the bracket this set is in. If the corresponding checkbox is not checked, skip that side of the bracket
                if (set.round > 0 && checkBoxWinnersDoubles.Checked == true)
                {
                    bracketSide = LP_WROUND;
                }
                else if (set.round < 0 && checkBoxLosersDoubles.Checked == true)
                {
                    bracketSide = LP_LROUND;
                }
                else
                {
                    continue;
                }

                // Skip unfinished sets unless otherwise specified
                if (checkBoxFillUnfinishedDoubles.Checked == false && set.state == 1)
                {
                    continue;
                }

                // The set's round number must be within the bounds of the numericUpDown controls
                if (Math.Abs(set.round) >= numericUpDownStartDoubles.Value && Math.Abs(set.round) <= numericUpDownEndDoubles.Value)
                {
                    // Offset the output round by the number specified in the numericUpDown control
                    int outputRound = Math.Abs(set.round) + (int)numericUpDownOffsetDoubles.Value;

                    // Check for player byes
                    if (set.entrantID1 == PLAYER_BYE && set.entrantID2 == PLAYER_BYE)
                    {
                        // If both players are byes, skip this entry
                        continue;
                    }
                    else if (set.entrantID1 == PLAYER_BYE)
                    {
                        // Fill in team 1 as a bye
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P1, "Bye");
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P2, "Bye");

                        // Give team 2 a checkmark
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P1, teamList[set.entrantID2].player1.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P1 + LP_FLAG, teamList[set.entrantID2].player1.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P2, teamList[set.entrantID2].player2.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P2 + LP_FLAG, teamList[set.entrantID2].player2.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_SCORE, LP_CHECKMARK);

                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_WIN, "2");
                    }
                    else if (set.entrantID2 == PLAYER_BYE)
                    {
                        // Fill in team 2 as a bye
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P1, "Bye");
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P2, "Bye");

                        // Give team 1 a checkmark
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P1, teamList[set.entrantID1].player1.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P1 + LP_FLAG, teamList[set.entrantID1].player1.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P2, teamList[set.entrantID1].player2.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P2 + LP_FLAG, teamList[set.entrantID1].player2.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_SCORE, LP_CHECKMARK);

                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_WIN, "1");
                    }
                    else
                    {
                        // Fill in the set normally
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P1, teamList[set.entrantID1].player1.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P1 + LP_FLAG, teamList[set.entrantID1].player1.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P2, teamList[set.entrantID1].player2.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_P2 + LP_FLAG, teamList[set.entrantID1].player2.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P1, teamList[set.entrantID2].player1.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P1 + LP_FLAG, teamList[set.entrantID2].player1.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P2, teamList[set.entrantID2].player2.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_P2 + LP_FLAG, teamList[set.entrantID2].player2.country);

                        // Check for DQs
                        if (set.entrant1wins == -1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_SCORE, "DQ");
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_SCORE, LP_CHECKMARK);
                        }
                        else if (set.entrant2wins == -1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_SCORE, "DQ");
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_SCORE, LP_CHECKMARK);
                        }
                        else
                        {
                            if (set.entrant1wins != -99 && set.entrant2wins != -99)
                            {
                                FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_SCORE, set.entrant1wins.ToString());
                                FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_SCORE, set.entrant2wins.ToString());
                            }
                            else
                            {
                                if (set.winner == set.entrantID1)
                                {
                                    FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T1 + LP_SCORE, "{{win}}");
                                }
                                else if (set.winner == set.entrantID2)
                                {
                                    FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_T2 + LP_SCORE, "{{win}}");
                                }
                            }
                        }

                        // Set the winner
                        if (set.winner == set.entrantID1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_WIN, "1");
                        }
                        else if (set.winner == set.entrantID2)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LP_MATCH + set.match + LP_WIN, "2");
                        }
                    }
                }
            }

            richTextBoxLiquipedia.Text = output;
        }

        private void FillLPParameter(ref string input, string param, string value)
        {
            int start = input.IndexOf("|" + param + "=");

            if (start != -1)
            {
                start += param.Length + 2;
                input = input.Insert(start, value);
            }
        }

        private void OutputSetToTextBox(ref RichTextBox textbox, Set set, int p1padding)
        {
            // Output the round
            textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            textbox.AppendText(set.round.ToString().PadRight(4));

            // Highlight font if player 1 is the winner
            if (set.winner == set.entrantID1)
            {
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Italic);
            }
            else
            {
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            }

            // Output player 1
            textbox.AppendText(entrantList[set.entrantID1].name.PadRight(p1padding) + "  ");

            // Output player 1's score
            textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            if (set.entrant1wins == -1)
            {
                textbox.AppendText("DQ");
            }
            else if (set.entrant1wins != -99)
            {
                textbox.AppendText(set.entrant1wins.ToString().PadLeft(2));
            }
            else
            {
                textbox.AppendText(" ?");
            }

            textbox.AppendText(" - ");

            // Output player 2's score
            if (set.entrant2wins == -1)
            {
                textbox.AppendText("DQ");
            }
            else if (set.entrant2wins != -99)
            {
                textbox.AppendText(set.entrant2wins.ToString().PadRight(2));
            }
            else
            {
                textbox.AppendText("? ");
            }

            // Highlight font if player 2 is the winner
            if (set.winner == set.entrantID2)
            {
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Italic);
            }
            else
            {
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            }
            // Output player 2
            textbox.AppendText("  " + entrantList[set.entrantID2].name + "\r\n");
        }

        private void OutputDoublesSetToTextBox(ref RichTextBox textbox, Set set, int p1padding)
        {
            // Output the round
            textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            textbox.AppendText(set.round.ToString().PadRight(4));

            // Highlight font if player 1 is the winner
            if (set.winner == set.entrantID1)
            {
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Italic);
            }
            else
            {
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            }

            // Output player 1
            textbox.AppendText(teamList[set.entrantID1].player1.name.PadRight(p1padding) + "  ");

            // Output player 1's score
            textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            if (set.entrant1wins == -1)
            {
                textbox.AppendText("DQ");
            }
            else if (set.entrant1wins != -99)
            {
                textbox.AppendText(set.entrant1wins.ToString().PadLeft(2));
            }
            else
            {
                textbox.AppendText(" ?");
            }

            textbox.AppendText(" - ");

            // Output player 2's score
            if (set.entrant2wins == -1)
            {
                textbox.AppendText("DQ");
            }
            else if (set.entrant2wins != -99)
            {
                textbox.AppendText(set.entrant2wins.ToString().PadRight(2));
            }
            else
            {
                textbox.AppendText("? ");
            }

            // Highlight font if player 2 is the winner
            if (set.winner == set.entrantID2)
            {
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Italic);
            }
            else
            {
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            }
            // Output player 2
            textbox.AppendText("  " + teamList[set.entrantID2].player1.name + "\r\n");
        }
    }
}
