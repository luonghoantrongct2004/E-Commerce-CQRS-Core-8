using E.API.Contracts;

namespace E.API.Controllers.V1;

[ApiVersion("1.0")]
[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator _mediator;
    protected IMapper _mapper;
    private readonly IErrorResponseHandler _errorResponseHandler;
    private readonly ILogger<BaseController> _logger;

    public BaseController(IMediator mediator, IMapper mapper, IErrorResponseHandler errorResponseHandler, ILogger<BaseController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _errorResponseHandler = errorResponseHandler ?? throw new ArgumentNullException(nameof(errorResponseHandler));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected IActionResult HandleErrorResponse(List<Error> errors)
    {
        _logger.LogError("Handling errors: {@Errors}", errors);
        var apiError = _errorResponseHandler.HandleErrors(errors);
        return StatusCode(apiError.StatusCode, apiError);
    }
}