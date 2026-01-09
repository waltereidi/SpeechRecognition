using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechRecognition.Infra.Context
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
