using System.Collections.Generic;

namespace BusBoard
{
    public class JourneyPlanner
    {
        public List<Journey> Journeys { get; set; }
    }
    public class Journey
    {
        public List<JourneyPlannerLeg> Legs { get; set; }
    }
    public class JourneyPlannerLeg
    {
        public Instruction Instruction { get; set; }
    }
    public class Instruction
    {
        public List<Direction> Steps { get; set; }
    }
    public class Direction
    {
        public string Description { get; set; }
        public string DescriptionHeading { get; set; }
    }
}