using UnityEngine;

public static class SceneRouteManager
{
    public enum WorldArea
    {
        Farm,
        House,
        Forest,
        Market,
        Cave
    }

    public enum EntryPoint
    {
        Default,
        FromFarm,
        FromHouse,
        FromForest,
        FromMarket,
        FromCave
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

            case WorldArea.Market:
                return GetMarketRoute(day, period, entryPoint);
            case WorldArea.Cave:
                return GetCaveRoute(entryPoint);

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
                case 2: sceneName = "Farm_Day_2"; break;
                case 3: sceneName = "Farm_Day_3"; break;
                case 4: sceneName = "Farm_Day_4"; break;
                case 5: sceneName = "Farm_Day_5"; break;
                case 6: sceneName = "Farm_Day_6"; break;
                default: sceneName = "Farm_Day_6"; break;
            }
        }
        else
        {
            switch (day)
            {
                case 1: sceneName = "Farm_Night_1"; break;
                case 2: sceneName = "Farm_Night_2"; break;
                case 3: sceneName = "Farm_Night_3"; break;
                case 4: sceneName = "Farm_Night_4"; break;
                case 5: sceneName = "Farm_Night_5"; break;
                default: sceneName = "Farm_Night_1"; break;
            }
        }

        string spawnPointID = entryPoint switch
        {
            EntryPoint.FromHouse => "FromHouse",
            EntryPoint.FromForest => "FromForest",
            EntryPoint.FromCave => "FromCave",
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
                case 2: sceneName = "House_Day_2"; break;
                case 3: sceneName = "House_Day_3"; break;
                case 4: sceneName = "House_Day_4"; break;
                case 5: sceneName = "House_Day_5"; break;
                default: sceneName = "House_Day_1"; break;
            }
        }
        else
        {
            switch (day)
            {
                case 1: sceneName = "House_Night_1"; break;
                case 2: sceneName = "House_Night_2"; break;
                case 3: sceneName = "House_Night_3"; break;
                case 4: sceneName = "House_Night_4"; break;
                case 5: sceneName = "House_Night_5"; break;
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
                case 2: sceneName = "Forest_Day_2"; break;
                case 3: sceneName = "Forest_Day_3"; break;
                case 4: sceneName = "Forest_Day_4"; break;
                case 5: sceneName = "Forest_Day_5"; break;
                default: sceneName = "Forest_Day_1"; break;
            }
        }
        else
        {
            switch (day)
            {
                case 1: sceneName = "Forest_Night_1"; break;
                case 2: sceneName = "Forest_Night_2"; break;
                case 3: sceneName = "Forest_Night_3"; break;
                case 4: sceneName = "Forest_Night_4"; break;
                case 5: sceneName = "Forest_Night_5"; break;
                default: sceneName = "Forest_Night_1"; break;
            }
        }

        string spawnPointID = entryPoint switch
        {
            EntryPoint.FromFarm => "FromFarm",
            EntryPoint.FromMarket => "FromMarket",
            _ => "Default"
        };

        return new RouteData(sceneName, spawnPointID);
    }

    private static RouteData GetMarketRoute(int day, ProgressionManager.DayPeriod period, EntryPoint entryPoint)
    {
        string sceneName;

        if (period == ProgressionManager.DayPeriod.Day)
        {
            switch (day)
            {
                case 1: sceneName = "Market_Day_1"; break;
                case 2: sceneName = "Market_Day_2"; break;
                case 3: sceneName = "Market_Day_3"; break;
                case 4: sceneName = "Market_Day_4"; break;
                case 5: sceneName = "Market_Day_5"; break;
                default: sceneName = "Market_Day_1"; break;
            }
        }
        else
        {
            switch (day)
            {
                case 1: sceneName = "Market_Night_1"; break;
                case 2: sceneName = "Market_Night_2"; break;
                case 3: sceneName = "Market_Night_3"; break;
                case 4: sceneName = "Market_Night_4"; break;
                case 5: sceneName = "Market_Night_5"; break;
                default: sceneName = "Market_Night_1"; break;
            }
        }

        string spawnPointID = entryPoint switch
        {
            EntryPoint.FromForest => "FromForest",
            _ => "Default"
        };

        return new RouteData(sceneName, spawnPointID);
    }

    private static RouteData GetCaveRoute(EntryPoint entryPoint)
    {
        if (ProgressionManager.Instance.currentDay == 5 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night)
        {
            string spawnPointID = entryPoint switch
            {
                EntryPoint.FromFarm => "FromFarm",
                _ => "Default"
            };

            return new RouteData("Cave_Night", spawnPointID);
        }

        return new RouteData("Farm_Night_5", "Default");
    }
    
}