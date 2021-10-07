namespace BusBoard
{
    public enum States
    {
        Lost,
        ChooseYourStop,
        AtStop,
        AtArrivals,
        AtDirections,
    }

    public enum LocationType
    {
        Postcode,
        LonLat,
        StopID,
        Invalid
    }
}