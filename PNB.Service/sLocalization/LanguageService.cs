using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sLocalization
{
   public partial class LanguageService : ILanguageService
    {
        private readonly IRepository<Language> _languagerepository;
        public LanguageService(IRepository<Language> languagerepository)
        {
            _languagerepository = languagerepository;
        }
        public Language  GetLanguageById(int LanguageId)
        {
            return _languagerepository.GetById(LanguageId);
        }
    }
}
