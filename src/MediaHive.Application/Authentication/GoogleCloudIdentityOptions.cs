namespace MediaHive.Application.Authentication;

public sealed class GoogleCloudIdentityOptions
{
	public const string SegmentName = "GoogleCloudIdentity";
	
	public required string ClientID { get; set; }
	public required string ClientSecret { get; set; }
	public required string CallbackPath { get; set; }
}