namespace MediaHive.Domain.Validators;

public class FluentEntityValidationException<TEntity> : Exception where TEntity : class
{
	public FluentEntityValidationException(TEntity entity, Dictionary<string, string[]> entityErrors)
	{
		Entity = entity;
		EntityErrors = entityErrors;
	}

	public FluentEntityValidationException(string? message, TEntity entity, Dictionary<string, string[]> entityErrors) : base(message)
	{
		Entity = entity;
		EntityErrors = entityErrors;
	}

	public TEntity Entity { get; }
	public Dictionary<string, string[]> EntityErrors { get; }
}