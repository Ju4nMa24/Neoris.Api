using MediatR;
using Microsoft.Extensions.Logging;
using Neoris.Abstractions.Repositories.Persons;
using Neoris.Business.Commands.Person;
using Newtonsoft.Json;

namespace Neoris.Business.Processors.Person
{
    public class PersonProcessor : IRequestHandler<PersonCommand, PersonResponse>
    {
        private readonly PersonResponse _response;
        private readonly IPersonRepository _repository;
        private readonly ILogger<PersonProcessor> _logger;

        public PersonProcessor(IPersonRepository repository, ILogger<PersonProcessor> logger)
        {
            _response = new();
            _repository = repository;
            _logger = logger;
        }
        /// <summary>
        /// This method is used to person create.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PersonResponse> Handle(PersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Person: {request.Identification}", DateTimeOffset.UtcNow);
                _response.InnerContext.Result.Success = false;
                _response.InnerContext.Result.ResponseCode = "400";
                _response.InnerContext.Result.Response = "No se pudo procesar la solicitud";
                if (request is null)
                    return _response;
                Neoris.Commons.Types.Tables.Person person = new()
                {
                    Address = request.Address,
                    Identification = request.Identification,
                    Gender = request.Gender,
                    PersonName = request.PersonName,
                    Phone = request.Phone,
                    Years = request.Years
                };
                if (_repository.Create(person))
                {
                    _response.StatusCode = "200";
                    _response.InnerContext.Result.Success = true;
                    _response.Response = $"Persona: {request.Identification} registrada con exito...";
                }
                await Task.Run(() => _logger.LogInformation($"Response: {_response.Response?.ToString()}", DateTimeOffset.UtcNow));
                return _response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Exception: {ex.Message}", DateTimeOffset.UtcNow, ex);
                return _response;

            }
        }
    }
}
