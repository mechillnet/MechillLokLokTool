using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sLocalization
{
    public interface ILanguageService
    {
        Language GetLanguageById(int languageId);
    }
}
