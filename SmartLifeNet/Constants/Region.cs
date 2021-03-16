using System;

namespace SmartLifeNet.Constants
{
   internal static class Region
   {
      internal static string GetRegion(Regions region)
      {
         return region switch
         {
            Regions.EU => "eu",
            Regions.US => "us",
            Regions.CN => "cn",
            _ => throw new ArgumentException("Region is not valid", region.ToString()),
         };
      }
   }

   public enum Regions
   {
      None = 0,
      EU = 1,
      US = 2,
      CN = 3,
   }
}