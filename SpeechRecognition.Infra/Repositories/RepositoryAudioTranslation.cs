using Microsoft.EntityFrameworkCore.Query;
using SpeechRecognition.Dominio.Entidades;
using SpeechRecognition.Infra.Context;
using SpeechRecognition.Infra.Interfaces.Base;
using SpeechRecognition.Infra.Repositories.Base;

namespace Unimar.ProjetoAcademico.Domain.Interfaces.Repositories;

public class RepositoryAudioTranslation(AppDbContext context) : RepositoryBase<AppDbContext, AudioTranslation, Guid>(context), IRepositoryAudioTranslation;