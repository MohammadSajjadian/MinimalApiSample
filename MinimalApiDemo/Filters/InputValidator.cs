
using FluentValidation;

namespace MinimalApiDemo.Filters
{
    public class InputValidator<T>(IValidator<T> validator) : IEndpointFilter
    {
        IValidator<T> _validator = validator;

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            T? inputData = (T)context.Arguments?.FirstOrDefault(a => a.GetType() == typeof(T));
            if (inputData is not null)
            {
                var validationResult = await _validator.ValidateAsync(inputData);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
            }

            return await next.Invoke(context);
        }
    }
}
