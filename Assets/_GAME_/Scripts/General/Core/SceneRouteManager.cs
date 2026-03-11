using UnityEngine;

public static class SceneRouteManager
{
    public enum WorldArea
    {
        Farm,
        House,
        Forest
    }

    public enum EntryPoint
    {
        Default,
        FromFarm,
        FromHouse,
        FromForest
    }

    public struct RouteData
    {
        public string SceneName;
        public string SpawnPointID;

        public RouteData(string sceneName, string spawnPointID)
        {
            SceneName = sceneName;
            SpawnPointID = spawnPointID;
        }
    }

    public static string GetScene(WorldArea area)
    {
        return GetRoute(area, EntryPoint.Default).SceneName;
    }

    public static RouteData GetRoute(WorldArea area, EntryPoint entryPoint)
    {
        int day = ProgressionManager.Instance.currentDay;
        ProgressionManager.DayPeriod period = ProgressionManager.Instance.currentPeriod;

        switch (area)
        {
            case WorldArea.Farm:
                return GetFarmRoute(day, period, entryPoint);

            case WorldArea.House:
                return GetHouseRoute(day, period, entryPoint);

            case WorldArea.Forest:
                return GetForestRoute(day, period, entryPoint);

            default:
                return GetFarmRoute(day, period, entryPoint);
        }
    }

    private static RouteData GetFarmRoute(int day, ProgressionManager.DayPeriod period, EntryPoint entryPoint)
    {
        string sceneName;

        if (period == ProgressionManager.DayPeriod.Day)
        {
            switch (day)
            {
                case 1: sceneName = "Farm_Day_1"; break;
                default: sceneName = "Farm_Day_1"; break;
            }
        }
        else
        {
            switch (day)
            {
                case 1: sceneName = "Farm_Night_1"; break;
                default: sceneName = "Farm_Night_1"; break;
            }
        }

        string spawnPointID = entryPoint switch
        {
            EntryPoint.FromHouse => "FromHouse",
            EntryPoint.FromForest => "FromForest",
            _ => "Default"
        };

        return new RouteData(sceneName, spawnPointID);
    }

    private static RouteData GetHouseRoute(int day, ProgressionManager.DayPeriod period, EntryPoint entryPoint)
    {
        string sceneName;

        if (period == ProgressionManager.DayPeriod.Day)
        {
            switch (day)
            {
                case 1: sceneName = "House_Day_1"; break;
                default: sceneName = "House_Day_1"; break;
            }
        }
        else
        {
            switch (day)
            {
                case 1: sceneName = "House_Night_1"; break;
                default: sceneName = "House_Night_1"; break;
            }
        }

        string spawnPointID = entryPoint switch
        {
            EntryPoint.FromFarm => "FromFarm",
            _ => "Default"
        };

        return new RouteData(sceneName, spawnPointID);
    }

    private static RouteData GetForestRoute(int day, ProgressionManager.DayPeriod period, EntryPoint entryPoint)
    {
        string sceneName;

        if (period == ProgressionManager.DayPeriod.Day)
        {
            switch (day)
            {
                case 1: sceneName = "Forest_Day_1"; break;
                default: sceneName = "Forest_Day_1"; break;
            }
        }
        else
        {
            switch (day)
            {
                case 1: sceneName = "Forest_Night_1"; break;
                default: sceneName = "Forest_Night_1"; break;
            }
        }

        string spawnPointID = entryPoint switch
        {
            EntryPoint.FromFarm => "FromFarm",
            _ => "Default"
        };

        return new RouteData(sceneName, spawnPointID);
    }
}