using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// Main scene CanvasManager
/// </summary>
public class RecordsTrackerCanvasManager : MonoBehaviour
{
    //In-game TextMeshPro text panels
    [Header("TextMeshPro Text panels", order = 0)]
    [Header("In-Game", order = 1)]
    public TMP_Text timeText;

    //End-game TextMeshPro text panels
    [Header("Death screen")]
    public TMP_Text runTimeText;
    public TMP_Text highScoreText;
    public TMP_Text recentTimesText;

    private float elapsedTime;
    [HideInInspector]
    public bool updateRunTime = false;


    public delegate void OnStartRecordsTrackerDelegate();
    public static OnStartRecordsTrackerDelegate onStartRecordsTracker;

    public void Start()
    {
        Player.onPlayersDeath += EndGame;
        onStartRecordsTracker += StartGame;
        onStartRecordsTracker();
    }

    private void StartGame()
    {
        updateRunTime = true;//Start updating elapsed time
        timeText.transform.parent.gameObject.SetActive(true);//Enable run time text
        RunTimeHistoryRegistry runTimeHistory = FileHandler.LoadRunTimeHistoryRegistry();//Load recent run times file

        runTimeHistory = SetTextForRunTimesPanel(runTimeHistory);
        UpdateBestTimeText(runTimeHistory);
        UpdateRunTimeText();
    }

    private void FixedUpdate()
    {
        if (updateRunTime)
        {
            elapsedTime += Time.fixedDeltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds((double)elapsedTime);
            timeText.SetText(string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds));
        }
    }

    /// <summary>
    /// Show End-game (game over) panel
    /// </summary>
    public void EndGame()
    {
        RunTimeHistoryRegistry runTimeHistory = FileHandler.LoadRunTimeHistoryRegistry();//Load recent run times file
        runTimeHistory.AddRunTime(TimeSpan.FromSeconds(elapsedTime));
        UpdateTimeTexts(runTimeHistory); // Update recent runs file and text panels
        FileHandler.SaveRunTimeHistoryRegistry(runTimeHistory); //Save recent runs file
    }

    /// <summary>
    /// Update text labels for BestRun and RunTime TMP objects
    /// </summary>
    /// <param name="runTimeHistory">registry, that is being serialized and deserialized every game ending to save data</param>
    private void UpdateTimeTexts(RunTimeHistoryRegistry runTimeHistory)
    {
        runTimeHistory = SetTextForRunTimesPanel(runTimeHistory);
        TimeSpan timeSpan = UpdateRunTimeText();
        UpdateBestTimeText(runTimeHistory);
    }

    /// <summary>
    /// On button click Start. Clear history of RecentRuns panel
    /// </summary>
    public void ClearHistory()
    {
        RunTimeHistoryRegistry runTimeHistoryRegistry = new RunTimeHistoryRegistry();
        UpdateTimeTexts(runTimeHistoryRegistry);
        FileHandler.SaveRunTimeHistoryRegistry(runTimeHistoryRegistry); //Save recent runs file
    }


    #region UI updates
    /// <summary>
    /// Update best time text panel
    /// </summary>
    /// <param name="runTimeHistory"></param>
    private void UpdateBestTimeText(RunTimeHistoryRegistry runTimeHistory)
    {
        TimeSpan timeSpan = runTimeHistory.bestTime;
        highScoreText.SetText(string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds));
    }

    /// <summary>
    /// Update current time time text panel
    /// </summary>
    /// <returns></returns>
    private TimeSpan UpdateRunTimeText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds((double)elapsedTime);

        if (timeSpan <= new TimeSpan(0))
        {
            runTimeText.SetText(string.Format("{0:D2}:{1:D2}", "--", "--"));
            return timeSpan;
        }
        runTimeText.SetText(string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds));
        return timeSpan;
    }

    /// <summary>
    /// Build string for new
    /// </summary>
    /// <param name="runTimeHistory"></param>
    /// <returns></returns>
    private RunTimeHistoryRegistry SetTextForRunTimesPanel(RunTimeHistoryRegistry runTimeHistory)
    {
        TimeSpan[] recentTimesSpansArr = runTimeHistory.GetRunTimes();
        DateTime[] recentTimesDatesArr = runTimeHistory.GetDateTimes();

        var sb = new System.Text.StringBuilder();
        for (int id = recentTimesDatesArr.Length - 1; id >= 0 && id >= 0; id--)
        {
            TimeSpan time = recentTimesSpansArr[id];
            DateTime date = recentTimesDatesArr[id];

            string line = string.Format("{0:D2}:{1:D2} | {2:dd/MM}", time.Minutes, time.Seconds, date);
            sb.AppendLine(line);
        }
        recentTimesText.SetText(sb.ToString());

        return runTimeHistory;
    }
    #endregion
}
