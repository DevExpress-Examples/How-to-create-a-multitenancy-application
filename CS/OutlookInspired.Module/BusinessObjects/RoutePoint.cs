using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace OutlookInspired.Module.BusinessObjects{
    [DomainComponent]
    public class RoutePoint {
        [DevExpress.ExpressApp.Data.Key]
        public int ID{ get; set; }
        [HideInUI(HideInUI.ListView)]
        public BingManeuverType Maneuver{ get; set; }
        [MaxLength(255)]
        public string ManeuverInstruction{ get; set; }
        [MaxLength(255)]
        public string Distance{ get; set; }
    }

    public enum BingManeuverType{
        None,
        Unknown,
        DepartStart,
        DepartIntermediateStop,
        DepartIntermediateStopReturning,
        ArriveFinish,
        ArriveIntermediateStop,
        TurnLeft,
        TurnRight,
        TurnBack,
        UTurn,
        TurnToStayLeft,
        TurnToStayRight,
        BearLeft,
        BearRight,
        KeepToStayLeft,
        KeepToStayRight,
        KeepToStayStraight,
        KeepLeft,
        KeepRight,
        KeepStraight,
        Take,
        TakeRampLeft,
        TakeRampRight,
        TakeRampStraight,
        KeepOnrampLeft,
        KeepOnrampRight,
        KeepOnrampStraight,
        Merge,
        Continue,
        RoadNameChange,
        EnterRoundabout,
        ExitRoundabout,
        TurnRightThenTurnRight,
        TurnRightThenTurnLeft,
        TurnRightThenBearRight,
        TurnRightThenBearLeft,
        TurnLeftThenTurnLeft,
        TurnLeftThenTurnRight,
        TurnLeftThenBearLeft,
        TurnLeftThenBearRight,
        BearRightThenTurnRight,
        BearRightThenTurnLeft,
        BearRightThenBearRight,
        BearRightThenBearLeft,
        BearLeftThenTurnLeft,
        BearLeftThenTurnRight,
        BearLeftThenBearRight,
        BearLeftThenBearLeft,
        RampThenHighwayRight,
        RampThenHighwayLeft,
        RampToHighwayStraight,
        EnterThenExitRoundabout,
        BearThenMerge,
        TurnThenMerge,
        BearThenKeep,
        Transfer,
        Wait,
        TakeTransit,
        Walk,
        TurnLeftSharp,
        TurnRightSharp
    }

    public class MapsMarker(string title, double latitude, double longitude) : IMapsMarker{
        public string Title{ get; } = title;

        public double Latitude{ get; } = latitude;

        public double Longitude{ get; } = longitude;
    }

}