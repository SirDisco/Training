namespace BusBoard
{
    public enum States
    {
        Lost,
        ChooseYourStop,
        ProvideLocation,
        AtStop,
        AtArrivals,
        AtDirections,
    }

    public enum LocationType
    {
        Postcode,
        LatLong,
        StopID,
        Invalid
    }
}