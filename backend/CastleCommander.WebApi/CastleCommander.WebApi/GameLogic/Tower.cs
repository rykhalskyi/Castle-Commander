﻿namespace CastleCommander.WebApi.GameLogic
{
    public class Tower
    {
        public Guid PlayerId { get; set; }
        public string PrimaryColor { get; set; } = "#cccccc";
        public string SecondaryColor { get; set; } = "#666666";
    }
}
