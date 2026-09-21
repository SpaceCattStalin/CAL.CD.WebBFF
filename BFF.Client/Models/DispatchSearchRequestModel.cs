namespace BFF.Client.Dispatches;

public class DispatchSearchRequestModel
{
    public double? PriceTotalMin { get; set; }
    public double? PriceTotalMax { get; set; }
    public DateTime? PickupDateFrom { get; set; }
    public DateTime? PickupDateTo { get; set; }
    public DateTime? DropoffDateFrom { get; set; }
    public DateTime? DropoffDateTo { get; set; }
    public string[]? DispatchStatus { get; set; }
    public string? VehicleVin { get; set; }
    public int? Size { get; set; } = 50;
    public int? CurrentPage { get; set; } = 0;
    public ICollection<SortFieldRequest> SortFields { get; set; } = [];
}

public class SortFieldRequest
{
    public string Name { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
}
