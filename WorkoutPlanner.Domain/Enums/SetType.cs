using System.ComponentModel;

namespace WorkoutPlanner.Domain.Enums;

public enum SetType
{
    [Description("AMRAP - As Many Rounds As Possible")]
    AMRAP = 1,
    [Description("EMON - Every Minute On a Minute")]
    EMON,
    [Description("RTF - Rounds For Time")]
    RFT,
    [Description("RM - Round Max or Rep Max")]
    RM,
    [Description("Ladder - Increasing or Decreasing Workload over time")]
    Ladder,
    [Description("Tabata - 8 rounds of High-Intensity intervals. 20 seconds effort + 10 seconds rest")]
    Tabata,
    [Description("Chipper - List of excercises to do in the order that is listed")]
    Chipper
}