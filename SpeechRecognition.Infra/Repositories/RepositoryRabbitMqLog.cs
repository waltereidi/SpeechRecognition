using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Interfaces.Base;
using SpeechRecognition.Infra.Repositories.Base;

namespace Unimar.ProjetoAcademico.Domain.Interfaces.Repositories;

public class RepositoryRabbitMqLog(AppDbContext context) 
    : RepositoryBase<AppDbContext, RabbitMqLog , Guid>(context), IRepositoryRabbitMqLog;
