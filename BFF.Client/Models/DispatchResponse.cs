using BFF.Client.Companies;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BFF.Client.Dispatches;

public class DispatchResponse(
    Guid DispatchId,
    CarrierResponse? Shipper,
    CarrierResponse? Carrier,
    string DispatchStatus,
    decimal Price,
    DateTime PickupDate,
    DateTime DropoffDate,
    string? Description,
    bool IsSigned,
    StopResponse? PickupStop,
    StopResponse? DropoffStop,
    IEnumerable<VehicleResponse> Vehicles,
    IEnumerable<DriverResponse> Drivers,
    DateTime CreatedAt)
{
    public Guid DispatchId { get; init; } = DispatchId;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CarrierResponse? Shipper { get; init; } = Shipper;
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CarrierResponse? Carrier { get; init; } = Carrier;
    public string DispatchStatus { get; init; } = DispatchStatus;
    public decimal Price { get; init; } = Price;
    public DateTime PickupDate { get; init; } = PickupDate;
    public DateTime DropoffDate { get; init; } = DropoffDate;
    public string? Description { get; init; } = Description;
    public bool IsSigned { get; init; } = IsSigned;
    public StopResponse? PickupStop { get; init; } = PickupStop;
    public StopResponse? DropoffStop { get; init; } = DropoffStop;
    public IEnumerable<VehicleResponse> Vehicles { get; init; } = Vehicles;
    public IEnumerable<DriverResponse> Drivers { get; init; } = Drivers;
    public DateTime CreatedAt { get; init; } = CreatedAt;
}

