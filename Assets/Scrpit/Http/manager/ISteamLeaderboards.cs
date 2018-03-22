using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface ISteamLeaderboards
{
    void findOrCreateLeaderboard(FindOrCreateLeaderboardParams baseParams, HttpResponseHandler<FindOrCreateLeaderboardResult> responseHandler);
}

