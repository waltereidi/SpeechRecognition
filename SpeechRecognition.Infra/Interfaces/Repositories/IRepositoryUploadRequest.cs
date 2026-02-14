using SpeechRecognition.FileStorageDomain.Entidades;
using SpeechRecognition.Infra.Interfaces.Base;

namespace Unimar.ProjetoAcademico.Domain.Interfaces.Repositories;

public interface IRepositoryUploadRequest : IRepositoryBase<UploadRequest , UploadRequestId>;