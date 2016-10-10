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
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace smashgg_api
{
    public partial class Form1 : Form
    {
        static int PLAYER_BYE = -1;

        enum PhaseType { Bracket, Pool }

        Dictionary<int, Player> entrantList = new Dictionary<int, Player>();
        Dictionary<int, Team> teamList = new Dictionary<int, Team>();
        List<Set> setList = new List<Set>();
        Dictionary<int, List<Set>> roundList = new Dictionary<int, List<Set>>();
        List<Phase> phaseList = new List<Phase>();

        JObject tournamentStructure;
        string tournament = string.Empty;

        public Form1()
        {
            InitializeComponent();

            SetCueText(textBoxURLSingles, FormStrings.CuetextURL);
            SetCueText(textBoxURLDoubles, FormStrings.CuetextURL);

            richTextBoxExLpWinnersBracket.Cue = FormStrings.CuetextLpWinners;
            richTextBoxExLpLosersBracket.Cue = FormStrings.CuetextLpLosers;
        }

        private int parseURL(string url)
        {
            string[] splitURL = url.Split(new string[1] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            // Take the last number in the url
            int num;
            if (int.TryParse(splitURL[splitURL.Length - 1], out num))
            {
                return num;
            }

            return -1;
        }

        // Retrieve webpage via api
        private bool parseURL(PhaseType type, string url, out string json)
        {
            json = string.Empty;
            string urlPrefix = string.Empty;
            string urlSuffix = string.Empty;

            // The offset for URL parsing depends on whether this is a pool or a bracket
            switch (type)
            {
                case PhaseType.Bracket:
                    urlPrefix = SmashggStrings.UrlPrefixPhaseGroup;
                    urlSuffix = SmashggStrings.UrlSuffixPhaseGroup;
                    break;
                case PhaseType.Pool:
                    urlPrefix = SmashggStrings.UrlPrefixPhase;
                    urlSuffix = SmashggStrings.UrlSuffixPhase;
                    break;
                default:
                    return false;
            }

            // Example of a valid URL: https://smash.gg/tournament/genesis-3/events/melee-singles/brackets/3861
            try
            {
                string[] splitURL = url.Split(new string[1] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                // Take the last number in the url
                int num;
                if (int.TryParse(splitURL[splitURL.Length - 1], out num))
                {
                    // Get the phase group id using the phase id
                    foreach (Phase phase in phaseList)
                    {
                        if (phase.phaseId == num && phase.PhaseType == Type.Bracket)
                        {
                            // fix this hack later ********************************
                            retrievePhaseGroup(num, out json);
                        }
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                richTextBoxLog.Text += ex + "\r\n";
                return false;
            }
        }

        private bool retrievePhaseGroup(int phaseGroup, out string json)
        {
            json = string.Empty;
            try
            {
                WebRequest r = WebRequest.Create(SmashggStrings.UrlPrefixPhaseGroup + phaseGroup + SmashggStrings.UrlSuffixPhaseGroup);
                WebResponse resp = r.GetResponse();
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }

                return true;
            }
            catch (Exception ex)
            {
                richTextBoxLog.Text += ex + "\r\n";
                return false;
            }
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
            checkBoxWinnersSingles.Checked = false;
            checkBoxLosersSingles.Checked = false;
            roundList.Clear();

            string json = string.Empty;

            UpdateTournamentStructure();
            if (!retrievePhaseGroup(parseURL(textBoxURLSingles.Text), out json))
            {
                richTextBoxLog.Text += "Error retrieving bracket.\r\n";
                return;
            }

            JObject bracketJson = JsonConvert.DeserializeObject<JObject>(json);

            smashgg parser = new smashgg();
            parser.GetEntrants(bracketJson.SelectToken(SmashggStrings.Entities + "." + SmashggStrings.Entrants), ref entrantList);
            parser.GetSets(bracketJson.SelectToken(SmashggStrings.Entities + "." + SmashggStrings.Sets), ref setList);

            foreach (Set currentSet in setList)
            {
                // Check if the round already exists in roundList
                if (roundList.ContainsKey(currentSet.displayRound))
                {
                    // Rounds with a larger originalRound take priority
                    if (Math.Abs(roundList[currentSet.displayRound][0].originalRound) < Math.Abs(currentSet.originalRound))
                    {
                        roundList[currentSet.displayRound].Clear();
                        roundList[currentSet.displayRound].Add(currentSet);
                    }
                    else
                    {
                        roundList[currentSet.displayRound].Add(currentSet);
                    }
                }
                else    // Make a new entry for the round if it doesn't exist
                {
                    // Add the current set to the newly created list
                    List<Set> newSetList = new List<Set>();
                    newSetList.Add(currentSet);

                    roundList.Add(currentSet.displayRound, newSetList);
                }
            }

            // Clear winners side numericUpDown controls unless it's locked
            if (!checkBoxLockWinners.Checked)
            {
                numericUpDownWinnersStart.Value = 0;
                numericUpDownWinnersEnd.Value = 0;
                numericUpDownWinnersOffset.Value = 0;
            }

            // Clear losers side numericUpDown controls unless it's locked
            if (!checkBoxLockWinners.Checked)
            {
                numericUpDownLosersStart.Value = 0;
                numericUpDownLosersEnd.Value = 0;
                numericUpDownLosersOffset.Value = 0;
            }

            // Set values in form controls
            foreach (int displayRound in roundList.Keys.ToList<int>())
            {
                int round = Math.Abs(displayRound);

                if (displayRound > 0)
                {
                    checkBoxWinnersSingles.Checked = true;

                    if (checkBoxLockWinners.Checked) continue;

                    // Remember lowest round number
                    if (numericUpDownWinnersStart.Value == 0)
                    {
                        numericUpDownWinnersStart.Value = round;
                    }
                    else if (round < numericUpDownWinnersStart.Value)
                    {
                        numericUpDownWinnersStart.Value = round;
                    }

                    // Remember highest round number
                    if (numericUpDownWinnersEnd.Value == 0)
                    {
                        numericUpDownWinnersEnd.Value = round;
                    }
                    else if (round > numericUpDownWinnersEnd.Value)
                    {
                        numericUpDownWinnersEnd.Value = round;
                    }
                }
                else if (displayRound < 0)
                {
                    checkBoxLosersSingles.Checked = true;

                    if (checkBoxLockLosers.Checked) continue;

                    // Remember lowest round number
                    if (numericUpDownLosersStart.Value == 0)
                    {
                        numericUpDownLosersStart.Value = round;
                    }
                    else if (round < numericUpDownLosersStart.Value)
                    {
                        numericUpDownLosersStart.Value = round;
                    }

                    // Remember highest round number
                    if (numericUpDownLosersEnd.Value == 0)
                    {
                        numericUpDownLosersEnd.Value = round;
                    }
                    else if (round > numericUpDownLosersEnd.Value)
                    {
                        numericUpDownLosersEnd.Value = round;
                    }
                }
            }

            
            int p1padding = 0;
            int wpadding = 0;
            int lpadding = 0;

            // Set padding for players for textbox output
            foreach (Player player in entrantList.Values)
            {
                if (player.name.Length > p1padding)
                {
                    p1padding = player.name.Length;
                }
            }

            // Set padding for sets for textbox output
            for (int i = 0; i < roundList.Count; i++)
            {
                foreach (Set set in roundList.ElementAt(i).Value)
                {
                    if (set.originalRound > 0)
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
            }

            // Output entrants to textbox
            foreach (KeyValuePair<int, Player> entrant in entrantList)
            {
                if (entrant.Key == -1) continue;

                richTextBoxEntrants.Text += entrant.Key.ToString().PadRight(8) + entrant.Value.name.PadRight(p1padding + 2) + "  " + entrant.Value.country + "\r\n";
            }

            // Output sets to textbox
            for (int i = 0; i < roundList.Count; i++)
            {
                foreach (Set set in roundList.ElementAt(i).Value)
                {
                    if (checkBoxFillUnfinishedSingles.Checked == false && set.state == 1)
                    {
                        continue;
                    }

                    if (set.displayRound > 0)
                    {
                        OutputSetToTextBox(ref richTextBoxWinners, set, wpadding);
                    }
                    else
                    {
                        OutputSetToTextBox(ref richTextBoxLosers, set, lpadding);
                    }
                }
            }
        }

        private void fillBracket(int startRound, int endRound, int offset, ref string bracketText)
        {
            int increment;
            string bracketSide;
            if (startRound > 0)
            {
                increment = 1;
                bracketSide = LpStrings.WRound;
            }
            else
            {
                increment = -1;
                bracketSide = LpStrings.LRound;
            }

            for (int i = startRound; Math.Abs(i) <= Math.Abs(endRound); i += increment)
            {
                int outputRound;

                // Skip rounds that don't exist
                if (!roundList.ContainsKey(i)) continue;

                // Iterate through all sets in the round
                for (int j = 0; j < roundList[i].Count; j++)
                {
                    Set currentSet = roundList[i][j];

                    outputRound = Math.Abs(i) + offset;

                    // Skip unfinished sets unless otherwise specified
                    if (checkBoxFillUnfinishedSingles.Checked == false && currentSet.state == 1)
                    {
                        continue;
                    }

                    // Check for player byes
                    if (currentSet.entrantID1 == PLAYER_BYE && currentSet.entrantID2 == PLAYER_BYE)
                    {
                        // If both players are byes, skip this entry
                        continue;
                    }
                    else if (currentSet.entrantID1 == PLAYER_BYE)
                    {
                        // Fill in player 1 as a bye
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1, "Bye");

                        // Give player 2 a checkmark
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2, entrantList[currentSet.entrantID2].name);
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2 + LpStrings.Flag, entrantList[currentSet.entrantID2].country);
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2 + LpStrings.Score, LpStrings.Checkmark);

                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.Win, "2");
                    }
                    else if (currentSet.entrantID2 == PLAYER_BYE)
                    {
                        // Fill in player 2 as a bye
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2, "Bye");

                        // Give player 1 a checkmark
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1, entrantList[currentSet.entrantID1].name);
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1 + LpStrings.Flag, entrantList[currentSet.entrantID1].country);
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1 + LpStrings.Score, LpStrings.Checkmark);

                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.Win, "1");
                    }
                    else
                    {
                        // Fill in the set normally
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1, entrantList[currentSet.entrantID1].name);
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1 + LpStrings.Flag, entrantList[currentSet.entrantID1].country);
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2, entrantList[currentSet.entrantID2].name);
                        FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2 + LpStrings.Flag, entrantList[currentSet.entrantID2].country);

                        // Check for DQs
                        if (currentSet.entrant1wins == -1)
                        {
                            FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1 + LpStrings.Score, "DQ");
                            FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2 + LpStrings.Score, LpStrings.Checkmark);
                        }
                        else if (currentSet.entrant2wins == -1)
                        {
                            FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2 + LpStrings.Score, "DQ");
                            FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1 + LpStrings.Score, LpStrings.Checkmark);
                        }
                        else
                        {
                            if (currentSet.entrant1wins != -99 && currentSet.entrant2wins != -99)
                            {
                                FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1 + LpStrings.Score, currentSet.entrant1wins.ToString());
                                FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2 + LpStrings.Score, currentSet.entrant2wins.ToString());
                            }
                            else
                            {
                                if (currentSet.winner == currentSet.entrantID1)
                                {
                                    FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P1 + LpStrings.Score, "{{win}}");
                                }
                                else if (currentSet.winner == currentSet.entrantID2)
                                {
                                    FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.P2 + LpStrings.Score, "{{win}}");
                                }
                            }
                        }

                        // Set the winner
                        if (currentSet.winner == currentSet.entrantID1)
                        {
                            FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.Win, "1");
                        }
                        else if (currentSet.winner == currentSet.entrantID2)
                        {
                            FillLPParameter(ref bracketText, bracketSide + outputRound + LpStrings.Match + currentSet.match + LpStrings.Win, "2");
                        }
                    }
                }
            }
        }

        private void buttonFillSingles_Click(object sender, EventArgs e)
        {
            string output = string.Empty;

            if (richTextBoxExLpWinnersBracket.Text != string.Empty) 
            {
                output += "==Winners Bracket==\r\n" + richTextBoxExLpWinnersBracket.Text + "\r\n";
            }

            if (richTextBoxExLpLosersBracket.Text != string.Empty)
            {
                output += "==Losers Bracket==\r\n" + richTextBoxExLpLosersBracket.Text + "\r\n";
            }

            // If the corresponding checkbox is not checked, skip that side of the bracket
            if (checkBoxWinnersSingles.Checked == true)
            {
                fillBracket((int)numericUpDownWinnersStart.Value, (int)numericUpDownWinnersEnd.Value, (int)numericUpDownWinnersOffset.Value, ref output);
            }
            if (checkBoxLosersSingles.Checked == true)
            {
                fillBracket(-(int)numericUpDownLosersStart.Value, -(int)numericUpDownLosersEnd.Value, (int)numericUpDownLosersOffset.Value, ref output);
            }

            richTextBoxLpOutput.Text = output;
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

            UpdateTournamentStructure();
            int phaseNumber = parseURL(textBoxURLSingles.Text);

            // Find the matching phase in the tournament structure
            string json = string.Empty;
            smashgg parser = new smashgg();
            foreach (Phase phase in phaseList)
            {
                if (phase.phaseId == phaseNumber && phase.id.Count > 1)
                {
                    // Sort the list by wave and number
                    // Assume that if the first element has a wave and number, the rest do too
                    if (phase.id[0].waveNumberDetected)
                    {
                        phase.id = phase.id.OrderBy(c => c.Wave).ThenBy(c => c.Number).ToList();
                    }
                    else
                    {
                        phase.id = phase.id.OrderBy(c => c.DisplayIdentifier).ToList();
                    }

                    // Retrieve pages for each group
                    string lastWave = string.Empty;
                    for (int j = 0; j < phase.id.Count; j++) 
                    {
                        // Clear data
                        entrantList.Clear();
                        setList.Clear();
                        richTextBoxEntrants.Clear();
                        richTextBoxWinners.Clear();
                        richTextBoxLosers.Clear();

                        // Get json for group
                        if (!retrievePhaseGroup(phase.id[j].id, out json))
                        {
                            richTextBoxLog.Text += "Error retrieving bracket " + phase.id[j].id + ".\r\n";
                            continue;
                        }

                        JObject bracketJson = JsonConvert.DeserializeObject<JObject>(json);

                        // Parse entrant and set data
                        parser.GetEntrants(bracketJson.SelectToken("entities.entrants"), ref entrantList);
                        parser.GetSets(bracketJson.SelectToken("entities.sets"), ref setList);

                        // Create a record for each player
                        Dictionary<int, PoolRecord> poolData = new Dictionary<int, PoolRecord>();
                        foreach (KeyValuePair<int, Player> entrant in entrantList)
                        {
                            poolData.Add(entrant.Key, new PoolRecord());
                        }

                        // Add data to each record based on set information
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
                        for (int i = 0; i < poolData.Count; i++)
                        {
                            if (poolData.ElementAt(i).Value.isinGroup == false)
                            {
                                poolData.Remove(poolData.ElementAt(i).Key);
                                i--;
                            }
                        }

                        // Sort the entrants by their rank and W-L records
                        poolData = poolData.OrderBy(x => x.Value.rank).ThenBy(x => x.Value.matchesLoss).ThenByDescending(x => x.Value.matchesWin).ThenByDescending(x => x.Value.GameWinrate).ToDictionary(x => x.Key, x => x.Value);

                        // Output to textbox
                        // Wave headers
                        if (phase.id[0].waveNumberDetected)
                        {
                            if (lastWave != phase.id[j].Wave)
                            {
                                richTextBoxLpOutput.Text += "==Wave " + phase.id[j].Wave + "==\r\n";
                                richTextBoxLpOutput.Text += LpStrings.BoxStart + "\r\n";

                                lastWave = phase.id[j].Wave;
                            }
                        }
                        else if (j == 0)    // Start a box at the first element
                        {
                            richTextBoxLpOutput.Text += LpStrings.BoxStart + "\r\n";
                        }

                        // Pool headers
                        if (phase.id[0].waveNumberDetected)
                        {
                            richTextBoxLpOutput.Text += LpStrings.SortStart + "===" + phase.id[j].Wave + phase.id[j].Number.ToString() + "===" + LpStrings.SortEnd + "\r\n";
                            richTextBoxLpOutput.Text += LpStrings.GroupStart + "Bracket " + phase.id[j].Wave + phase.id[j].Number.ToString() + LpStrings.GroupStartWidth + "\r\n";
                        }
                        else
                        {
                            richTextBoxLpOutput.Text += LpStrings.SortStart + "===" + phase.id[j].Wave + phase.id[j].DisplayIdentifier.ToString() + "===" + LpStrings.SortEnd + "\r\n";
                            richTextBoxLpOutput.Text += LpStrings.GroupStart + "Bracket " + phase.id[j].DisplayIdentifier.ToString() + LpStrings.GroupStartWidth + "\r\n";
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
                            richTextBoxLpOutput.Text += LpStrings.SlotStart + currentPlayer.name +
                                                          LpStrings.SlotFlag + currentPlayer.country +
                                                          LpStrings.SlotMWin + poolData[poolData.ElementAt(i).Key].matchesWin +
                                                          LpStrings.SlotMLoss + poolData[poolData.ElementAt(i).Key].matchesLoss;
                            if (radioButtonRoundRobin.Checked == true)
                            {
                                if (poolData[poolData.ElementAt(i).Key].matchesWin == lastWin && poolData[poolData.ElementAt(i).Key].matchesLoss == lastLoss)
                                {
                                    richTextBoxLpOutput.Text += LpStrings.SlotPlace + lastRank;
                                }
                                else
                                {
                                    richTextBoxLpOutput.Text += LpStrings.SlotPlace + (i + 1);
                                    lastRank = i + 1;
                                    lastWin = poolData[poolData.ElementAt(i).Key].matchesWin;
                                    lastLoss = poolData[poolData.ElementAt(i).Key].matchesLoss;
                                }
                            }
                            else if (poolData[poolData.ElementAt(i).Key].rank != -99)
                            {
                                richTextBoxLpOutput.Text += LpStrings.SlotPlace + poolData[poolData.ElementAt(i).Key].rank;
                            }
                            else
                            {
                                richTextBoxLpOutput.Text += LpStrings.SlotPlace;
                            }

                            if (advance > 0)
                            {
                                richTextBoxLpOutput.Text += LpStrings.SlotBg + "up";
                                advance--;
                            }
                            else
                            {
                                richTextBoxLpOutput.Text += LpStrings.SlotBg + "down";
                            }

                            richTextBoxLpOutput.Text += LpStrings.SlotEnd + "\r\n";
                        }

                        // Pool footers
                        richTextBoxLpOutput.Text += LpStrings.GroupEnd + "\r\n";
                        if (phase.id[0].waveNumberDetected)     // Waves exist
                        {
                            if (j + 1 >= phase.id.Count)
                            {
                                richTextBoxLpOutput.Text += LpStrings.BoxEnd + "\r\n\r\n";
                            }
                            else if (phase.id[j + 1].Wave != lastWave)
                            {
                                richTextBoxLpOutput.Text += LpStrings.BoxEnd + "\r\n\r\n";
                            }
                            else
                            {
                                richTextBoxLpOutput.Text += LpStrings.BoxBreak + "\r\n\r\n";
                            }
                        }
                        else        // Waves don't exist
                        {
                            if (j == phase.id.Count - 1) // End box at the group end
                            {
                                richTextBoxLpOutput.Text += LpStrings.BoxEnd + "\r\n\r\n";
                            }
                            else
                            {
                                richTextBoxLpOutput.Text += LpStrings.BoxBreak + "\r\n\r\n";
                            }
                        }
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

            // Get the data from smash.gg
            //if (!parseURL(PhaseType.Bracket, textBoxURLDoubles.Text, out webText))
            //{
            //    richTextBoxLog.Text += "Could not retrieve webpage.\r\n";
            //    return;
            //}

            smashgg parser = new smashgg_api.smashgg();

            //string rawEntrants = parser.ExpandOnly(webText, GG_ENTRANTS, 0, out endPos);
            //parser.GetDoublesEntrants(rawEntrants, ref teamList);

            //string rawSets = parser.ExpandOnly(webText, GG_SETS, 0, out endPos);
            //parser.GetSets(rawSets, ref setList);

            // Set values in form controls
            foreach (Set set in setList)
            {
                int round = Math.Abs(set.originalRound);

                if (set.originalRound > 0)
                {
                    checkBoxWinnersDoubles.Checked = true;
                }
                else if (set.originalRound < 0)
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
                if (set.originalRound > 0)
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

                richTextBoxEntrants.Text += entrant.Key.ToString().PadRight(8) + 
                                            (entrant.Value.player1.name + " / " + entrant.Value.player2.name).PadRight(t1padding + 3) + "  " +
                                            entrant.Value.player1.country + " / " + entrant.Value.player1.country + "\r\n";
            }

            // Output sets to textbox
            foreach (Set set in setList)
            {
                if (checkBoxFillUnfinishedSingles.Checked == false && set.state == 1)
                {
                    continue;
                }

                if (set.originalRound > 0)
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
            string output = richTextBoxLpOutput.Text;
            string bracketSide = string.Empty;

            foreach (Set set in setList)
            {
                // Detect what side of the bracket this set is in. If the corresponding checkbox is not checked, skip that side of the bracket
                if (set.originalRound > 0 && checkBoxWinnersDoubles.Checked == true)
                {
                    bracketSide = LpStrings.WRound;
                }
                else if (set.originalRound < 0 && checkBoxLosersDoubles.Checked == true)
                {
                    bracketSide = LpStrings.LRound;
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
                if (Math.Abs(set.originalRound) >= numericUpDownStartDoubles.Value && Math.Abs(set.originalRound) <= numericUpDownEndDoubles.Value)
                {
                    // Offset the output round by the number specified in the numericUpDown control
                    int outputRound = Math.Abs(set.originalRound) + (int)numericUpDownOffsetDoubles.Value;

                    // Check for player byes
                    if (set.entrantID1 == PLAYER_BYE && set.entrantID2 == PLAYER_BYE)
                    {
                        // If both players are byes, skip this entry
                        continue;
                    }
                    else if (set.entrantID1 == PLAYER_BYE)
                    {
                        // Fill in team 1 as a bye
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P1, "Bye");
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P2, "Bye");

                        // Give team 2 a checkmark
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P1, teamList[set.entrantID2].player1.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P1 + LpStrings.Flag, teamList[set.entrantID2].player1.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P2, teamList[set.entrantID2].player2.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P2 + LpStrings.Flag, teamList[set.entrantID2].player2.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.Score, LpStrings.Checkmark);

                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.Win, "2");
                    }
                    else if (set.entrantID2 == PLAYER_BYE)
                    {
                        // Fill in team 2 as a bye
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P1, "Bye");
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P2, "Bye");

                        // Give team 1 a checkmark
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P1, teamList[set.entrantID1].player1.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P1 + LpStrings.Flag, teamList[set.entrantID1].player1.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P2, teamList[set.entrantID1].player2.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P2 + LpStrings.Flag, teamList[set.entrantID1].player2.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.Score, LpStrings.Checkmark);

                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.Win, "1");
                    }
                    else
                    {
                        // Fill in the set normally
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P1, teamList[set.entrantID1].player1.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P1 + LpStrings.Flag, teamList[set.entrantID1].player1.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P2, teamList[set.entrantID1].player2.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.P2 + LpStrings.Flag, teamList[set.entrantID1].player2.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P1, teamList[set.entrantID2].player1.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P1 + LpStrings.Flag, teamList[set.entrantID2].player1.country);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P2, teamList[set.entrantID2].player2.name);
                        FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.P2 + LpStrings.Flag, teamList[set.entrantID2].player2.country);

                        // Check for DQs
                        if (set.entrant1wins == -1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.Score, "DQ");
                            FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.Score, LpStrings.Checkmark);
                        }
                        else if (set.entrant2wins == -1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.Score, "DQ");
                            FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.Score, LpStrings.Checkmark);
                        }
                        else
                        {
                            if (set.entrant1wins != -99 && set.entrant2wins != -99)
                            {
                                FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.Score, set.entrant1wins.ToString());
                                FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.Score, set.entrant2wins.ToString());
                            }
                            else
                            {
                                if (set.winner == set.entrantID1)
                                {
                                    FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T1 + LpStrings.Score, "{{win}}");
                                }
                                else if (set.winner == set.entrantID2)
                                {
                                    FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.T2 + LpStrings.Score, "{{win}}");
                                }
                            }
                        }

                        // Set the winner
                        if (set.winner == set.entrantID1)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.Win, "1");
                        }
                        else if (set.winner == set.entrantID2)
                        {
                            FillLPParameter(ref output, bracketSide + outputRound + LpStrings.Match + set.match + LpStrings.Win, "2");
                        }
                    }
                }
            }

            richTextBoxLpOutput.Text = output;
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
            textbox.AppendText(set.displayRound.ToString().PadRight(4));

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
            textbox.AppendText(set.originalRound.ToString().PadRight(4));

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

        #region Cue Banner
        // https://jasonkemp.ca/blog/the-missing-net-1-cue-banners-in-windows-forms-em_setcuebanner-text-prompt/
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg,
        int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static void SetCueText(Control control, string text)
        {
            SendMessage(control.Handle, EM_SETCUEBANNER, 0, text);
        }
        #endregion

        private void UpdateTournamentStructure()
        {
            TextBox box;
            if (tabControl1.SelectedTab.Text == "Singles")
            {
                box = textBoxURLSingles;
            }
            else
            {
                box = textBoxURLDoubles;
            }

            try
            {
                string[] splitURL = box.Text.Split(new string[1] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < splitURL.Length; i++)
                {
                    // A valid url will have smash.gg followed by tournament
                    if (splitURL[i].ToLower() == "smash.gg" && splitURL[i + 1].ToLower() == "tournament")
                    {
                        if (tournament != splitURL[i + 2])
                        {
                            tournament = splitURL[i + 2];

                            // Retrieve tournament page and get the json into a JObject
                            WebRequest r = WebRequest.Create(SmashggStrings.UrlPrefixTourney + tournament + SmashggStrings.UrlSuffixPhase);
                            WebResponse resp = r.GetResponse();
                            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                            {
                                tournamentStructure = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                            }

                            break;
                        }
                    }
                }

                // Get phase data
                foreach (JToken token in tournamentStructure.SelectToken(SmashggStrings.Entities + "." + SmashggStrings.Groups))
                {
                    // If the phase already exists, append the phase group id into the id list
                    bool phaseExists = false;
                    foreach (Phase phase in phaseList)
                    {
                        if (phase.phaseId == token[SmashggStrings.PhaseId].Value<int>())
                        {
                            PhaseGroup newPhaseGroup = new PhaseGroup();
                            newPhaseGroup.id = token[SmashggStrings.ID].Value<int>();
                            newPhaseGroup.DisplayIdentifier = token[SmashggStrings.DisplayIdent].Value<string>();

                            phase.id.Add(newPhaseGroup);
                            phaseExists = true;
                            break;
                        }
                    }

                    // If the phase does not exist, create it
                    if (!phaseExists)
                    {
                        PhaseGroup newPhaseGroup = new PhaseGroup();
                        newPhaseGroup.id = token[SmashggStrings.ID].Value<int>();
                        newPhaseGroup.DisplayIdentifier = token[SmashggStrings.DisplayIdent].Value<string>();

                        Phase newPhase = new Phase();
                        newPhase.id.Add(newPhaseGroup);
                        newPhase.phaseId = token[SmashggStrings.PhaseId].Value<int>();

                        if (!token[SmashggStrings.WaveId].IsNullOrEmpty())
                        {
                            newPhase.WaveId = token[SmashggStrings.WaveId].Value<int>();
                        }

                        phaseList.Add(newPhase);
                    }
                }
            }
            catch
            {
                richTextBoxLog.Text += "Couldn't update tournament structure.\r\n";
            }
        }

        private void checkBoxLock_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox changedCheckBox = (CheckBox)sender;
            if (changedCheckBox == checkBoxLockWinners)
            {
                if (changedCheckBox.Checked == true)
                {
                    numericUpDownWinnersStart.Enabled = false;
                    numericUpDownWinnersEnd.Enabled = false;
                    numericUpDownWinnersOffset.Enabled = false;
                }
                else
                {
                    numericUpDownWinnersStart.Enabled = true;
                    numericUpDownWinnersEnd.Enabled = true;
                    numericUpDownWinnersOffset.Enabled = true;
                }
            }
            else if (changedCheckBox == checkBoxLockLosers)
            {
                if (changedCheckBox.Checked == true)
                {
                    numericUpDownLosersStart.Enabled = false;
                    numericUpDownLosersEnd.Enabled = false;
                    numericUpDownLosersOffset.Enabled = false;
                }
                else
                {
                    numericUpDownLosersStart.Enabled = true;
                    numericUpDownLosersEnd.Enabled = true;
                    numericUpDownLosersOffset.Enabled = true;
                }
            }
        }
    }
}
