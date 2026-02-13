using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Interfaces.Base;
using SpeechRecognition.Infra.Repositories.Base;

namespace Unimar.ProjetoAcademico.Domain.Interfaces.Repositories;

public class RepositoryFileStorage(AppDbContext context)
    : RepositoryBase<AppDbContext, FileStorage, Guid>(context), IRepositoryFileStorage;