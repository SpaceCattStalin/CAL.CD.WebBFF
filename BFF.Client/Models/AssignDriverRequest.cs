namespace BFF.Client.Dispatches;

public class AssignDriverRequest(Guid? DriverId)
{
    public Guid? DriverId { get; init; } = DriverId;
}
