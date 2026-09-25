using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Chapter.Singleton 
{
    public class GameManager : Singleton<GameManager>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        private bool _has30sTrophy;
        private bool _has1mTrophy;
        private bool _has6mTrophy;

        private void Start()
        {
            // Add other functionality here

            _sessionStartTime = DateTime.Now;
            Debug.Log("Game session started @: " + DateTime.Now);

            LoadTrophies();
        }

        private void Update()
        {
            CheckTrophies(); 
        }

        private void CheckTrophies()
        {
            double _timeElapsed = (DateTime.Now - _sessionStartTime).TotalSeconds;

            if (!_has30sTrophy && _timeElapsed >= 30f)
            {
                UnlockTrophy(ref _has30sTrophy, "Trophy_30s", "Played For 30 Seconds");
            }
            if (!_has1mTrophy && _timeElapsed >= 60f)
            {
                UnlockTrophy(ref _has1mTrophy, "Trophy_60s", "Played for 1 Minute");
            }
            if (!_has6mTrophy && _timeElapsed >= 360f)
            {
                UnlockTrophy(ref _has6mTrophy, "Trophy_360s", "Suffered 6 Minutes of this...");
            }
        }

        private void UnlockTrophy(ref bool trophy, string prefsKey, string description)
        {
            trophy = true;
            PlayerPrefs.SetInt(prefsKey, 1);
            PlayerPrefs.Save();

            Debug.Log("Trophy Unlocked: " + description);

        }

        public void LoadTrophies()
        {
            _has30sTrophy = PlayerPrefs.GetInt("Trophy_30s", 0) == 1;
            _has1mTrophy = PlayerPrefs.GetInt("Trophy_60s", 0) == 1;
            _has6mTrophy = PlayerPrefs.GetInt("Trophy_360s", 0) == 1;

            LogTrophyCount();
        }

        public void LogTrophyCount()
        {
            int total = (_has30sTrophy ? 1 : 0) + (_has1mTrophy ? 1 : 0) + (_has6mTrophy ? 1 : 0);
                Debug.Log("Total Unlocked Trophies: " + total  + "/ 3"); 
        }


        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;

            TimeSpan _timeDifference = _sessionEndTime.Subtract(_sessionStartTime);

            Debug.Log("Session Ended @: " + _sessionEndTime);
            Debug.Log("Total Session Time: " + _timeDifference);
        }


        [ContextMenu("Reset Trophies")]
        public void ResetTrophies()
        {
            PlayerPrefs.DeleteKey("Trophy_30s");
            PlayerPrefs.DeleteKey("Trophy_60s");
            PlayerPrefs.DeleteKey("Trophy_360s");
            PlayerPrefs.Save();

            _has30sTrophy = false;
            _has1mTrophy = false;
            _has6mTrophy = false;

            LoadTrophies();
        }

    }
}


