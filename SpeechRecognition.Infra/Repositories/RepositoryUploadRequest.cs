using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Interfaces.Base;
using SpeechRecognition.Infra.Repositories.Base;

namespace Unimar.ProjetoAcademico.Domain.Interfaces.Repositories;

public class RepositoryUploadRequest(AppDbContext context) : RepositoryBase<AppDbContext, UploadRequest, UploadRequestId>(context), IRepositoryUploadRequest;