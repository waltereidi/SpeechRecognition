using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Interfaces.Base;
using SpeechRecognition.Infra.Repositories.Base;

namespace Unimar.ProjetoAcademico.Domain.Interfaces.Repositories;

public class RepositoryFileStorageConversion(AppDbContext context)
    : RepositoryBase<AppDbContext, FileStorageConversion, FileStorageConversionId>(context), IRepositoryFileStorageConversion;
