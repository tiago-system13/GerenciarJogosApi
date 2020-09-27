using FluentValidation.Results;
using GerenciadorDeJogos.Application.Exceptions;
using GerenciadorDeJogos.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeJogos.Application.Validations
{
    public class FluentResultAdapter
    {
        public void VerificaErros(ValidationResult result)
        {
            var listaErro = new List<string>();

            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    listaErro.Add(failure.ErrorMessage);
                }

            }

            if (listaErro != null && listaErro.Count > 0)
            {
                throw new NegocioException(new Problema() { Erros = listaErro });
            }

        }
    }
}
