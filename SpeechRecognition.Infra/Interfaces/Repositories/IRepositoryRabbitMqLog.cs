using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Interfaces.Base;

namespace Unimar.ProjetoAcademico.Domain.Interfaces.Repositories;

public interface IRepositoryRabbitMqLog : IRepositoryBase<RabbitMqLog , Guid>;