using Microsoft.AspNetCore.Builder;

namespace MediaHive.Application.Security;

public static class SecurityHeaderHelpers
{
	public static HeaderPolicyCollection GetHeaderPolicyCollection(bool development)
	{
		var policies = new HeaderPolicyCollection();

		policies.AddFrameOptionsDeny();
		policies.AddXssProtectionBlock();
		policies.AddContentTypeOptionsNoSniff();
		policies.AddStrictTransportSecurityMaxAgeIncludeSubDomains();
		policies.AddReferrerPolicyStrictOriginWhenCrossOrigin();
		policies.RemoveServerHeader();

		policies.AddContentSecurityPolicy(builder =>
		{
			builder.AddObjectSrc().Self();
			builder.AddFormAction().Self();
			builder.AddFrameAncestors().Self();
			builder.AddStyleSrc().Self();
			builder.AddScriptSrc().Self();
			builder.AddFontSrc().Self();
			builder.AddMediaSrc().Self();
			builder.AddConnectSrc().Self();
		});

		
        policies.AddCrossOriginOpenerPolicy(builder => { builder.SameOrigin(); });

		policies.AddCrossOriginEmbedderPolicy(builder => { builder.RequireCorp(); });

		policies.AddCrossOriginResourcePolicy(builder => { builder.SameOrigin(); });

		policies.AddPermissionsPolicy(builder =>
		{
			builder.AddAmbientLightSensor().None();
			builder.AddAccelerometer().None();
			builder.AddAutoplay().Self();
			builder.AddCamera().None();
			builder.AddEncryptedMedia().Self();
			builder.AddFullscreen().All();
			builder.AddGeolocation().None();
			builder.AddGyroscope().None();
			builder.AddMagnetometer().None();
			builder.AddMicrophone().None();
			builder.AddMidi().None();
			builder.AddPayment().None();
			builder.AddPictureInPicture().None();
			builder.AddSpeaker().None();
			builder.AddSyncXHR().None();
			builder.AddUsb().None();
			builder.AddVR().None();
		});

		if (development is false)
		{
			policies.AddStrictTransportSecurityMaxAgeIncludeSubDomains();
		}

		return policies;
	}
}