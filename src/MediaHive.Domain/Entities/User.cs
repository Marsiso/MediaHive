using MediaHive.Domain.Entities.Common;

namespace MediaHive.Domain.Entities;

public class User : ChangeTrackingEntity
{
	public int UserID { get; set; }
	public string Email { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string? ProfilePhotoUrl { get; set; }
	public string GoogleCloudIdentityIdentifier { set; get; } = string.Empty;
}